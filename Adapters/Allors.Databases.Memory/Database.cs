// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Allors.Meta;

    public abstract class Database : IDatabase
    {
                private readonly IObjectFactory objectFactory;

        private readonly Dictionary<IObjectType, object> concreteClassesByObjectType;

        protected Dictionary<string, object> Properties;

        private readonly Guid id;

        protected Database(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            if (this.objectFactory == null)
            {
                throw new Exception("Configuration.ObjectFactory is missing");
            }

            this.concreteClassesByObjectType = new Dictionary<IObjectType, object>();

            this.id = configuration.Id.Equals(Guid.Empty) ? Guid.NewGuid() : configuration.Id;
        }

        public event ObjectNotLoadedEventHandler ObjectNotLoaded;

        public event RelationNotLoadedEventHandler RelationNotLoaded;

        public Guid Id
        {
            get { return this.id; }
        }

        public bool IsDatabase
        {
            get
            {
                return true;
            }
        }

        public bool IsWorkspace
        {
            get
            {
                return false;
            }
        }

        public bool IsShared
        {
            get
            {
                return false;
            }
        }
        
        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.objectFactory.MetaPopulation;
            }
        }

        protected abstract Session Session { get; }

        public object this[string name]
        {
            get
            {
                if (this.Properties == null)
                {
                    return null;
                }

                object value;
                this.Properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.Properties == null)
                {
                    this.Properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.Properties.Remove(name);
                }
                else
                {
                    this.Properties[name] = value;
                }
            }
        }

        public ISession CreateSession()
        {
            return this.CreateDatabaseSession();
        }

        IDatabaseSession IDatabase.CreateSession()
        {
            return this.CreateDatabaseSession();
        }

        public IDatabaseSession CreateDatabaseSession()
        {
            return this.Session;
        }

        public void Init()
        {
            this.Session.Init();

            this.Properties = null;
        }

        public void Load(XmlReader reader)
        {
            this.Init();

            var load = new Load(this.Session, reader);
            load.Execute();

            this.Session.Commit();
        }

        public void Save(XmlWriter writer)
        {
            this.Session.Save(writer);
        }

        public bool ContainsConcreteClass(IComposite objectType, IObjectType concreteClass)
        {
            object concreteClassOrClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(objectType, out concreteClassOrClasses))
            {
                if (objectType.ExistExclusiveLeafClass)
                {
                    concreteClassOrClasses = objectType.ExclusiveLeafClass;
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
                else
                {
                    concreteClassOrClasses = new HashSet<IObjectType>(objectType.LeafClasses);
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
            }

            if (concreteClassOrClasses is IObjectType)
            {
                return concreteClass.Equals(concreteClassOrClasses);
            }

            var concreteClasses = (HashSet<IObjectType>)concreteClassOrClasses;
            return concreteClasses.Contains(concreteClass);
        }

        public void UnitRoleChecks(IStrategy strategy, IRoleType roleType)
        {
            if (!this.ContainsConcreteClass(roleType.AssociationType.ObjectType, strategy.ObjectType))
            {
                throw new ArgumentException(strategy.ObjectType + " is not a valid association object type for " + roleType + ".");
            }

            if (roleType.ObjectType is IComposite)
            {
                throw new ArgumentException(roleType.ObjectType + " on roleType " + roleType + " is not a unit type.");
            }
        }

        public void CompositeRoleChecks(IStrategy strategy, IRoleType roleType)
        {
            this.CompositeSharedChecks(strategy, roleType, null);
        }

        public void CompositeRoleChecks(IStrategy strategy, IRoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsOne)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity many.");
            }
        }

        public void CompositeRolesChecks(IStrategy strategy, IRoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsMany)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity one.");
            }
        }

        internal void OnObjectNotLoaded(Guid metaTypeId, string allorsObjectId)
        {
            var args = new ObjectNotLoadedEventArgs(metaTypeId, allorsObjectId);
            if (this.ObjectNotLoaded != null)
            {
                this.ObjectNotLoaded(this, args);
            }
            else
            {
                throw new Exception("Object not loaded: " + args);
            }
        }

        internal void OnRelationNotLoaded(Guid relationTypeId, string associationObjectId, string roleContents)
        {
            var args = new RelationNotLoadedEventArgs(relationTypeId, associationObjectId, roleContents);
            if (this.RelationNotLoaded != null)
            {
                this.RelationNotLoaded(this, args);
            }
            else
            {
                throw new Exception("RelationType not loaded: " + args);
            }
        }

        private void CompositeSharedChecks(IStrategy strategy, IRoleType roleType, IObject role)
        {
            if (!this.ContainsConcreteClass(roleType.AssociationType.ObjectType, strategy.ObjectType))
            {
                throw new ArgumentException(strategy.ObjectType + " has no roleType with role " + roleType + ".");
            }

            if (role != null)
            {
                if (!strategy.Session.Equals(role.Strategy.Session))
                {
                    throw new ArgumentException(role + " is from different session");
                }

                if (role.Strategy.IsDeleted)
                {
                    throw new ArgumentException(roleType + " on object " + strategy + " is removed.");
                }

                var compositeType = roleType.ObjectType as IComposite;

                if (compositeType == null)
                {
                    throw new ArgumentException(role + " has no CompositeType");
                }

                if (!compositeType.ContainsLeafClass(role.Strategy.ObjectType))
                {
                    throw new ArgumentException(role.Strategy.ObjectType + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
                }
            }
        }
    }
}