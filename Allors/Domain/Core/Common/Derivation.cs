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

    using Allors;
    using Allors.Meta;

    using ObjectBase = Allors.ObjectBase;

    public class Derivation : IDerivation
    {
        private readonly ISession session;
        private readonly DerivationLog log;

        private readonly HashSet<Derivable> derivedObjects;
        private readonly HashSet<IObject> preparedObjects;
        
        private HashSet<ObjectId> forcedDerivations;
        private HashSet<IObject> addedDerivables;

        private DerivationGraph derivationGraph;
        private Dictionary<string, object> properties;

        private IChangeSet changeSet;

        private int generation;
        
        public Derivation(ISession session)
        {
            this.session = session;
            this.log = new DerivationLog(this);

            this.derivedObjects = new HashSet<Derivable>();
            this.preparedObjects = new HashSet<IObject>();
            this.changeSet = session.Checkpoint();

            this.generation = 0;

            var user = new Users(session).GetCurrentUser();
        }

        public Derivation(ISession session, IEnumerable<ObjectId> forcedDerivations)
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

        public bool InDatabase
        {
            get
            {
                return this.Session is IDatabaseSession;
            }
        }

        public bool InWorkspace
        {
            get
            {
                return this.Session is IWorkspaceSession;
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

        public ISet<Derivable> DerivedObjects
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

        private HashSet<ObjectId> ForcedDerivations
        {
            get
            {
                return this.forcedDerivations ?? (this.forcedDerivations = new HashSet<ObjectId>());
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

        public bool IsForced(ObjectId objectId)
        {
            return this.ForcedDerivations.Contains(objectId);
        }

        public ISet<RoleType> GetChangedRoleTypes(IObject association)
        {
            return this.changeSet.GetRoleTypes(association.Id);
        }

        public void AddDerivable(Derivable derivable)
        {
            if (this.DerivedObjects.Contains(derivable))
            {
                throw new InvalidEnumArgumentException("Object has alreadry been derived.");
            }

            this.derivationGraph.Add(derivable);

            this.addedDerivables.Add(derivable);
        }

        public void AddDerivables(IEnumerable<Derivable> derivables)
        {
            foreach (var derivable in derivables)
            {
                this.AddDerivable(derivable);
            }
        }

        // TODO: add additional methods in case dependent/dependee is already derived
        //       and should'nt be derived again.
        public void AddDependency(Derivable dependent, Derivable dependee)
        {
            this.derivationGraph.AddDependency(dependent, dependee);

            this.addedDerivables.Add(dependent);
            this.addedDerivables.Add(dependee);
        }

        public DerivationLog Derive()
        {
            var changedObjectIds = new HashSet<ObjectId>(this.changeSet.Associations);
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

                this.addedDerivables = new HashSet<IObject>();

                this.derivationGraph = new DerivationGraph(this);
                foreach (var changedObject in changedObjects)
                {
                    var objectBase = (ObjectBase)this.Session.Instantiate(changedObject);
                    objectBase.PrepareDerivation(this);
                    this.preparedObjects.Add(objectBase);
                }

                while (this.addedDerivables.Count > 0)
                {
                    var dependencyObjectsToPrepare = new HashSet<IObject>(this.addedDerivables);
                    dependencyObjectsToPrepare.ExceptWith(this.preparedObjects);

                    this.addedDerivables = new HashSet<IObject>();

                    foreach (ObjectBase dependencyObject in dependencyObjectsToPrepare)
                    {
                        dependencyObject.PrepareDerivation(this);
                        this.preparedObjects.Add(dependencyObject);
                    }
                }

                if (this.derivationGraph.Count == 0)
                {
                    break;
                }

                this.derivationGraph.Derive();

                this.changeSet = this.Session.Checkpoint();

                changedObjectIds = new HashSet<ObjectId>(this.changeSet.Associations);
                changedObjectIds.UnionWith(this.changeSet.Roles);
                changedObjectIds.UnionWith(this.changeSet.Created);

                changedObjects = new HashSet<IObject>(this.Session.Instantiate(changedObjectIds.ToArray()));
                changedObjects.ExceptWith(this.derivedObjects);

                this.derivationGraph = null;
            }

            return this.log;
        }
    }
}