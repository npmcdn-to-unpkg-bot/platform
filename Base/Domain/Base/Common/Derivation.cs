// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Derivation.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
    using System.Security.Cryptography.X509Certificates;

    using Allors;
    using Allors.Meta;

    public class Derivation : IDerivation
    {
        public delegate void AddedDerivableHandler(Object derivable);
        public delegate void AddedDependencyHandler(Object dependent, Object dependee);
        public delegate void StartedGenerationHandler(int generation);
        public delegate void StartedPreparationHandler(int preparation);
        public delegate void PreparingDerivationHandler(Object derivable);
        public delegate void DerivedHandler(Object derivable);

        public event AddedDerivableHandler AddedDerivable;
        public event AddedDependencyHandler AddedDependency;
        public event StartedGenerationHandler StartedGeneration;
        public event StartedPreparationHandler StartedPreparation;
        public event PreparingDerivationHandler Preparing;
        public event DerivedHandler Derived;

        private readonly ISession session;
        private readonly DerivationLog log;

        private readonly HashSet<Object> derivedObjects;
        private readonly HashSet<IObject> preparedObjects;
        
        private HashSet<long> forcedDerivations;
        private HashSet<IObject> addedDerivables;

        private DerivationGraph derivationGraph;
        private Dictionary<string, object> properties;

        private IChangeSet changeSet;

        private int generation;
        
        public Derivation(ISession session)
        {
            this.session = session;
            this.log = new DerivationLog(this);

            this.derivedObjects = new HashSet<Object>();
            this.preparedObjects = new HashSet<IObject>();
            this.changeSet = session.Checkpoint();

            this.generation = 0;

            var user = new Users(session).GetCurrentUser();
        }

        public Derivation(ISession session, IEnumerable<long> forcedDerivations)
            : this(session)
        {
            this.ForcedDerivations.UnionWith(forcedDerivations);
        }

        public Derivation(ISession session, IEnumerable<IObject> forcedObjects)
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

        public DerivationLog Log
        {
            get
            {
                return this.log;
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

                if (this.AddedDerivable != null)
                {
                    this.AddedDerivable(derivable);
                }
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

                if (this.AddedDependency != null)
                {
                    this.AddedDependency(dependent, dependee);
                }
            }
        }

        public DerivationLog Derive()
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

                if (this.StartedGeneration != null)
                {
                    this.StartedGeneration(this.generation);
                }

                this.addedDerivables = new HashSet<IObject>();

                var preparationRun = 1;
                
                if (this.StartedPreparation != null)
                {
                    this.StartedPreparation(preparationRun);
                }

                this.derivationGraph = new DerivationGraph(this);
                foreach (var changedObject in changedObjects)
                {
                    var derivable = this.Session.Instantiate(changedObject) as Object;

                    if (derivable != null)
                    {
                        if (this.Preparing != null)
                        {
                            this.Preparing(derivable);
                        }

                        derivable.OnPreDerive(x => x.WithDerivation(this));

                        this.preparedObjects.Add(derivable);
                    }
                }

                while (this.addedDerivables.Count > 0)
                {
                    preparationRun++;
                    if (this.StartedPreparation != null)
                    {
                        this.StartedPreparation(preparationRun);
                    }

                    var dependencyObjectsToPrepare = new HashSet<IObject>(this.addedDerivables);
                    dependencyObjectsToPrepare.ExceptWith(this.preparedObjects);

                    this.addedDerivables = new HashSet<IObject>();

                    foreach (Object dependencyObject in dependencyObjectsToPrepare)
                    {
                        if (this.Preparing != null)
                        {
                            this.Preparing(dependencyObject);
                        }

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

            if (this.Derived != null)
            {
                this.Derived(derivable);
            }
        }
    }
}