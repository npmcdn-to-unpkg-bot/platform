// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationBase.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Allors;
    using Allors.Meta;

    public abstract class DerivationBase : IDerivation
    {
        private HashSet<long> forcedDerivables;
        private HashSet<IObject> addedDerivables;

        private readonly HashSet<Object> derivedObjects;
        private readonly HashSet<IObject> preparedObjects;

        private IValidation validation;
       
        private DerivationGraphBase derivationGraph;
        private Dictionary<string, object> properties;

        private IChangeSet changeSet;

        private int generation;
        
        protected DerivationBase(ISession session)
        {
            this.Session = session;

            this.derivedObjects = new HashSet<Object>();
            this.preparedObjects = new HashSet<IObject>();
            this.changeSet = session.Checkpoint();

            this.generation = 0;
        }

        protected DerivationBase(ISession session, IEnumerable<long> forcedDerivables)
            : this(session)
        {
            this.ForcedDerivables.UnionWith(forcedDerivables);
        }

        protected DerivationBase(ISession session, IEnumerable<IObject> forcedDerivables)
            : this(session)
        {
            foreach (var obj in forcedDerivables)
            {
                this.ForcedDerivables.Add(obj.Id);
            }
        }

        public ISession Session { get; }

        public IValidation Validation
        {
            get
            {
                return this.validation;
            }

            protected set
            {
                this.validation = value;
            }
        }

        public IChangeSet ChangeSet => this.changeSet;

        public ISet<Object> DerivedObjects => this.derivedObjects;

        public int Generation => this.generation;

        private HashSet<long> ForcedDerivables => this.forcedDerivables ?? (this.forcedDerivables = new HashSet<long>());

        public object this[string name]
        {
            get
            {
                var lowerName = name.ToLowerInvariant();

                object value;
                if (this.properties != null && this.properties.TryGetValue(lowerName, out value))
                {
                    return value;
                }

                return null;
            }

            set
            {
                var lowerName = name.ToLowerInvariant();

                if (value == null)
                {
                    if (this.properties != null)
                    {
                        this.properties.Remove(lowerName);
                        if (this.properties.Count == 0)
                        {
                            this.properties = null;
                        }
                    }
                }
                else
                {
                    if (this.properties == null)
                    {
                        this.properties = new Dictionary<string, object>();
                    }

                    this.properties[lowerName] = value;
                }
            }
        }

        public bool IsForced(long objectId)
        {
            return this.ForcedDerivables.Contains(objectId);
        }

        public bool IsAdded(Object @object)
        {
            return this.addedDerivables?.Contains(@object) ?? false;
        }

        public bool IsChanged(Object @object)
        {
            return this.ChangeSet.Associations.Contains(@object.Id);
        }

        public ISet<IRoleType> GetChangedRoleTypes(IObject association)
        {
            return this.changeSet.GetRoleTypes(association.Id);
        }

        public void AddDerivable(Object derivable)
        {
            if (derivable != null)
            {
                if (this.DerivedObjects.Contains(derivable))
                {
                    throw new InvalidEnumArgumentException("Object has alreadry been derived.");
                }
                
                this.derivationGraph.Add(derivable);
                this.addedDerivables.Add(derivable);

                this.OnAddedDerivable(derivable);
            }
        }

        public void AddDerivables(IEnumerable<Object> derivables)
        {
            foreach (var derivable in derivables)
            {
                this.AddDerivable(derivable);
            }
        }

        /// <summary>
        /// The dependee is derived before the dependent object;
        /// </summary>
        /// <param name="dependent"></param>
        /// <param name="dependee"></param>
        public void AddDependency(Object dependent, Object dependee)
        {
            if (dependent != null && dependee != null)
            {
                // TODO: add additional methods in case dependent/dependee is already derived and should not be derived again.
                this.derivationGraph.AddDependency(dependent, dependee);

                this.addedDerivables.Add(dependent);
                this.addedDerivables.Add(dependee);

                this.OnAddedDependency(dependent, dependee);
            }
        }

        public IValidation Derive()
        {
            var changedObjectIds = new HashSet<long>(this.changeSet.Associations);
            changedObjectIds.UnionWith(this.changeSet.Roles);
            changedObjectIds.UnionWith(this.changeSet.Created);

            if (this.ForcedDerivables != null)
            {
                changedObjectIds.UnionWith(this.ForcedDerivables);
            }

            var changedObjects = new HashSet<IObject>(this.Session.Instantiate(changedObjectIds.ToArray()));

            while (changedObjects.Count > 0)
            {
                this.generation++;

                this.OnStartedGeneration(this.generation);

                this.addedDerivables = new HashSet<IObject>();

                var preparationRun = 1;
                
                this.OnStartedPreparation(preparationRun);

                this.derivationGraph = this.CreateDerivationGraph(this);
                foreach (var changedObject in changedObjects)
                {
                    var derivable = this.Session.Instantiate(changedObject) as Object;

                    if (derivable != null)
                    {
                        this.OnPreDeriving(derivable);

                        derivable.OnPreDerive(x => x.WithDerivation(this));

                        this.OnPreDerived(derivable);

                        this.preparedObjects.Add(derivable);
                    }
                }

                while (this.addedDerivables.Count > 0)
                {
                    preparationRun++;
                    this.OnStartedPreparation(preparationRun);
 
                    var dependencyObjectsToPrepare = new HashSet<IObject>(this.addedDerivables);
                    dependencyObjectsToPrepare.ExceptWith(this.preparedObjects);

                    this.addedDerivables = new HashSet<IObject>();

                    foreach (Object dependencyObject in dependencyObjectsToPrepare)
                    {
                        this.OnPreDeriving(dependencyObject);

                        dependencyObject.OnPreDerive(x => x.WithDerivation(this));

                        this.OnPreDerived(dependencyObject);

                        this.preparedObjects.Add(dependencyObject);
                    }
                }

                if (this.derivationGraph.Count == 0)
                {
                    break;
                }

                this.derivationGraph.Derive();

                this.changeSet = this.Session.Checkpoint();

                changedObjectIds = new HashSet<long>(this.changeSet.Associations);
                changedObjectIds.UnionWith(this.changeSet.Roles);
                changedObjectIds.UnionWith(this.changeSet.Created);

                changedObjects = new HashSet<IObject>(this.Session.Instantiate(changedObjectIds.ToArray()));
                changedObjects.ExceptWith(this.derivedObjects);

                this.derivationGraph = null;
            }

            return this.validation;
        }

        internal void AddDerivedObject(Object derivable)
        {
            this.derivedObjects.Add(derivable);
        }

        protected abstract DerivationGraphBase CreateDerivationGraph(DerivationBase derivation);

        protected abstract void OnAddedDerivable(Object derivable);

        protected abstract void OnAddedDependency(Object dependent, Object dependee);

        protected abstract void OnStartedGeneration(int generation);

        protected abstract void OnStartedPreparation(int preparationRun);

        protected abstract void OnPreDeriving(Object derivable);

        protected abstract void OnPreDerived(Object derivable);
    }
}