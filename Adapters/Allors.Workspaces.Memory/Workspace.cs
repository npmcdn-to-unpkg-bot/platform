// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Workspace.cs" company="Allors bvba">
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

namespace Allors.Workspaces.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Allors.Meta;
    using Allors.Populations;

    public abstract class Workspace : IWorkspace
    {
        private readonly IDatabase database;
        private readonly IObjectFactory objectFactory;
        private readonly Dictionary<IObjectType, object> concreteClassesByObjectType;

        private Dictionary<string, object> properties;

        private WorkspaceSession workspaceSession;

        protected Workspace(Configuration configuration)
        {
            this.database = configuration.Database;
            this.objectFactory = configuration.ObjectFactory ?? this.database.ObjectFactory;

            this.concreteClassesByObjectType = new Dictionary<IObjectType, object>();
        }

        public IDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }
        
        public bool IsDatabase
        {
            get
            {
                return false;
            }
        }

        public bool IsWorkspace
        {
            get
            {
                return true;
            }
        }

        public Guid Id
        {
            get { return this.Database.Id; }
        }

        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.objectFactory.MetaPopulation;
            }
        }

        internal abstract ObjectIds ObjectIds { get; }

        private WorkspaceSession WorkspaceSession
        {
            get
            {
                return this.workspaceSession ?? (this.workspaceSession = this.CreateNewWorkspaceSession());
            }
        }

        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public ISession CreateSession()
        {
            return this.CreateWorkspaceSession();
        }

        IWorkspaceSession IWorkspace.CreateSession()
        {
            return this.CreateWorkspaceSession();
        }

        public void Load(XmlReader reader)
        {
            if (this.workspaceSession != null)
            {
                this.workspaceSession.Rollback();
                this.workspaceSession.Clear();
            }

            this.workspaceSession = null;

            while (reader.Read())
            {
                // only process elements, ignore others
                if (reader.NodeType.Equals(XmlNodeType.Element))
                {
                    if (reader.Name.Equals(Serialization.Population))
                    {
                        Serialization.CheckVersion(reader);

                        if (!reader.IsEmptyElement)
                        {
                            this.WorkspaceSession.LoadPopulation(reader);
                        }

                        break;
                    }
                }
            }

            this.WorkspaceSession.Commit();
        }

        public void Save(XmlWriter writer)
        {
            var writeDocument = false;
            if (writer.WriteState == WriteState.Start)
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(Serialization.Allors);
                writeDocument = true;
            }

            this.WorkspaceSession.Save(writer);

            if (writeDocument)
            {
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
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

        internal abstract bool IsWorkspaceNew(ObjectId objectId);

        protected abstract void ResetObjectIds();

        private bool ContainsConcreteClass(IComposite objectType, IObjectType concreteClass)
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

        private WorkspaceSession CreateNewWorkspaceSession()
        {
            return new WorkspaceSession(this);
        }

        private IWorkspaceSession CreateWorkspaceSession()
        {
            return this.WorkspaceSession;
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