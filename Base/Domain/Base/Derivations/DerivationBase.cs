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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Allors;
    using Allors.Meta;

    public abstract class DerivationBase : IDerivation
    {
        private readonly HashSet<Object> derivedObjects;
        private readonly HashSet<IObject> preparedObjects;

        private readonly ISession session;
        private IValidation log;
        
        private HashSet<long> forcedDerivations;
        private HashSet<IObject> addedDerivables;

        private DerivationGraph derivationGraph;
        private Dictionary<string, object> properties;

        private IChangeSet changeSet;

        private int generation;
        
        protected DerivationBase(ISession session)
        {
            this.session = session;

            this.derivedObjects = new HashSet<Object>();
            this.preparedObjects = new HashSet<IObject>();
            this.changeSet = session.Checkpoint();

            this.generation = 0;
        }

        protected DerivationBase(ISession session, IEnumerable<long> forcedDerivations)
            : this(session)
        {
            this.ForcedDerivations.UnionWith(forcedDerivations);
        }

        protected DerivationBase(ISession session, IEnumerable<IObject> forcedObjects)
            : this(session)
        {
            foreach (var obj in forcedObjects)
            {
                this.ForcedDerivations.Add(obj.Id);
            }
        }

        public ISession Session
        {
            get
            {
                return this.session;
            }
        }

        public IValidation Log
        {
            get
            {
                return this.log;
            }

            protected set
            {
                this.log = value;
            }
        }

        public IChangeSet ChangeSet
        {
            get
            {
                return this.changeSet;
            }
        }

        public ISet<Object> DerivedObjects
        {
            get
            {
                return this.derivedObjects;
            }
        }

        public int Generation
        {
            get
            {
                return this.generation;
            }
        }

        private HashSet<long> ForcedDerivations
        {
            get
            {
                return this.forcedDerivations ?? (this.forcedDerivations = new HashSet<long>());
            }
        }

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
            return this.ForcedDerivations.Contains(objectId);
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

            if (this.ForcedDerivations != null)
            {
                changedObjectIds.UnionWith(this.ForcedDerivations);
            }

            var changedObjects = new HashSet<IObject>(this.Session.Instantiate(changedObjectIds.ToArray()));

            while (changedObjects.Count > 0)
            {
                this.generation++;

                this.OnStartedGeneration(this.generation);

                this.addedDerivables = new HashSet<IObject>();

                var preparationRun = 1;
                
                this.OnStartedPreparation(preparationRun);

                this.derivationGraph = new DerivationGraph(this);
                foreach (var changedObject in changedObjects)
                {
                    var derivable = this.Session.Instantiate(changedObject) as Object;

                    if (derivable != null)
                    {
                        this.OnPreparing(derivable);

                        derivable.OnPreDerive(x => x.WithDerivation(this));

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
                        this.OnPreparing(dependencyObject);

                        dependencyObject.OnPreDerive(x => x.WithDerivation(this));
                        
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

            return this.log;
        }


        internal void AddDerivedObject(Object derivable)
        {
            this.derivedObjects.Add(derivable);
        }

        protected abstract void OnAddedDerivable(Object derivable);

        protected abstract void OnAddedDependency(Object dependent, Object dependee);

        protected abstract void OnStartedPreparation(int preparationRun);

        protected abstract void OnStartedGeneration(int generation);

        protected abstract void OnPreparing(Object derivable);
    }
}