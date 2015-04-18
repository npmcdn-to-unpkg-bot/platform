// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Strategy.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Workspaces.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    using Allors.Meta;
    using Allors.Populations;

    public class Strategy : IStrategy
    {
        private static readonly byte[] EmptyByteArray = new byte[0];
        
        private readonly WorkspaceSession session;
        private readonly IClass objectType;

        // TODO: move to a BitFlag
        private bool isDeleted;
        private bool rollbackIsDeleted;
        private bool isNew;
        
        private WeakReference allorizedObjectWeakReference;

        // New
        internal Strategy(WorkspaceSession session, IClass objectType, ObjectId objectId)
        {
            this.session = session;
            this.objectType = objectType;
            this.ObjectId = objectId;

            this.isDeleted = false;
            this.rollbackIsDeleted = true;
            this.isNew = true;
        }

        // Existing
        internal Strategy(WorkspaceSession session, IStrategy strategy)
            : this(session, strategy.Class, strategy.ObjectId)
        {
            this.rollbackIsDeleted = false;
            this.isNew = false;
        }

        // Load
        internal Strategy(WorkspaceSession worspaceSession, IClass objectType, ObjectId objectId, bool deleted)
            : this(worspaceSession, objectType, objectId)
        {
            if (deleted)
            {
                this.isDeleted = true;
            }
            else
            {
                this.rollbackIsDeleted = false;
            }

            this.isNew = false;
        }

        public bool IsDeleted
        {
            get { return this.isDeleted; }
        }

        public bool IsNewInSession
        {
            get
            {
                return this.isNew;
            }
        }

        public bool IsNewInWorkspace
        {
            get { return this.WorkspaceSession.MemoryWorkspace.IsWorkspaceNew(this.ObjectId); }
        }

        public ObjectId ObjectId { get; internal set; }

        public IClass Class
        {
            get
            {
                this.AssertNotDeleted();
                return this.objectType;
            }
        }

        public ISession Session
        {
            get { return this.session; }
        }

        public IDatabaseSession DatabaseSession
        {
            get
            {
                return this.session.DatabaseSession;
            }
        }

        internal IClass UncheckedObjectType
        {
            get
            {
                return this.objectType;
            }
        }

        internal WorkspaceSession WorkspaceSession
        {
            get { return this.session; }
        }

        private IStrategy DatabaseStrategy
        {
            get
            {
                if (!this.IsNewInWorkspace)
                {
                    var databaseObject = this.session.DatabaseSession.Instantiate(this.ObjectId);
                    return databaseObject != null ? databaseObject.Strategy : null;
                }

                return null;
            }
        }

        private ChangeSet ChangeSet
        {
            get
            {
                return this.WorkspaceSession.WorkspaceChangeSet;
            }
        }

        public override string ToString()
        {
            return this.objectType.Name + " " + this.ObjectId;
        }

        public virtual object GetRole(IRoleType roleType)
        {
            if (roleType.ObjectType is IUnit)
            {
                return this.GetUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.GetCompositeRoles(roleType);
            }

            return this.GetCompositeRole(roleType);
        }

        public virtual void SetRole(IRoleType roleType, object value)
        {
            if (roleType.ObjectType is IUnit)
            {
                this.SetUnitRole(roleType, value);
            }
            else
            {
                if (roleType.IsMany)
                {
                    var roles = value as Allors.Extent;
                    if (roles == null)
                    {
                        var roleList = new ArrayList((ICollection)value);
                        roles = (IObject[])roleList.ToArray(typeof(IObject));
                    }

                    this.SetCompositeRoles(roleType, roles);
                }
                else
                {
                    this.SetCompositeRole(roleType, (IObject)value);
                }
            }
        }

        public virtual void RemoveRole(IRoleType roleType)
        {
            if (roleType.ObjectType is IUnit)
            {
                this.RemoveUnitRole(roleType);
            }
            else
            {
                if (roleType.IsMany)
                {
                    this.RemoveCompositeRoles(roleType);
                }
                else
                {
                    this.RemoveCompositeRole(roleType);
                }
            }
        }

        public virtual bool ExistRole(IRoleType roleType)
        {
            if (roleType.ObjectType is IUnit)
            {
                return this.ExistUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.ExistCompositeRoles(roleType);
            }

            return this.ExistCompositeRole(roleType);
        }

        public virtual object GetUnitRole(IRoleType roleType)
        {
            this.AssertNotDeleted();
            RoleAssertions.UnitRoleChecks(this, roleType);

            var unitRole = this.GetInternalizedUnitRole(roleType);
            return unitRole;
        }

        public virtual void SetUnitRole(IRoleType roleType, object role)
        {
            this.AssertNotDeleted();
            RoleAssertions.UnitRoleChecks(this, roleType);

            this.ChangeSet.OnChangingUnitRole(this, roleType);

            if (role == null)
            {
                this.session.SetUnitRole(this, roleType, null);
            }
            else
            {
                role = RoleTypeExtensions.Normalize(roleType, role);

                this.session.SetUnitRole(this, roleType, role);
            }
        }

        public virtual void RemoveUnitRole(IRoleType roleType)
        {
            this.SetUnitRole(roleType, null);
        }

        public virtual bool ExistUnitRole(IRoleType roleType)
        {
            return this.GetUnitRole(roleType) != null;
        }

        public virtual IObject GetCompositeRole(IRoleType roleType)
        {
            this.AssertNotDeleted();
            this.RoleCompositeChecks(roleType);

            var roleStrategy = this.session.GetCompositeRole(this, roleType);
            return roleStrategy != null ? roleStrategy.GetObject() : null;
        }

        public virtual void SetCompositeRole(IRoleType roleType, IObject role)
        {
            if (role == null)
            {
                this.RemoveCompositeRole(roleType);
            }
            else
            {
                this.AssertNotDeleted();
                this.RoleCompositeChecks(roleType, role);

                var roleStrategy = this.session.GetStrategy(role);
                var previousRoleStrategy = this.session.GetCompositeRole(this, roleType);

                if (!roleStrategy.Equals(previousRoleStrategy))
                {
                    this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRoleStrategy, roleStrategy);
                    this.session.SetCompositeRole(this, roleType, roleStrategy, previousRoleStrategy);
                }
            }
        }

        public virtual void RemoveCompositeRole(IRoleType roleType)
        {
            this.AssertNotDeleted();
            this.RoleCompositeChecks(roleType);

            var previousRoleStrategy = this.session.GetCompositeRole(this, roleType);

            this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRoleStrategy, null);

            if (previousRoleStrategy != null)
            {
                this.session.RemoveCompositeRole(this, roleType, previousRoleStrategy);
            }
        }

        public virtual bool ExistCompositeRole(IRoleType roleType)
        {
            return this.GetCompositeRole(roleType) != null;
        }

        public virtual Allors.Extent GetCompositeRoles(IRoleType roleType)
        {
            this.AssertNotDeleted();

            return new ExtentRole(this, roleType);
        }

        public virtual void SetCompositeRoles(IRoleType roleType, Allors.Extent roles)
        {
            if (roles == null || roles.Count == 0)
            {
                this.RemoveCompositeRoles(roleType);
            }
            else
            {
                this.AssertNotDeleted();

                this.RoleCompositesChecks(roleType);
                foreach (IObject allorsObject in roles)
                {
                    if (allorsObject != null)
                    {
                        this.RoleCompositesSharedChecks(roleType, allorsObject);
                    }
                }

                // TODO: Optimize this
                this.RemoveCompositeRoles(roleType);
                foreach (IObject role in roles)
                {
                    if (role != null)
                    {
                        this.AddCompositeRole(roleType, role);
                    }
                }
            }
        }

        public virtual void AddCompositeRole(IRoleType roleType, IObject role)
        {
            this.AssertNotDeleted();
            if (role != null)
            {
                this.RoleCompositesChecks(roleType, role);

                var previousRoles = this.session.GetCompositeRoles(this, roleType);
                var roleStrategy = this.session.GetStrategy(role.Strategy.ObjectId);

                if (previousRoles == null || !previousRoles.Contains(roleStrategy))
                {
                    this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);
                    this.session.AddCompositeRole(this, roleType, roleStrategy);
                }
            }
        }

        public virtual void RemoveCompositeRole(IRoleType roleType, IObject role)
        {
            this.AssertNotDeleted();

            if (role != null)
            {
                this.RoleCompositesChecks(roleType, role);

                var previousRoles = this.session.GetCompositeRoles(this, roleType);
                var roleStrategy = this.session.GetStrategy(role.Strategy.ObjectId);

                this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);

                if (previousRoles != null && previousRoles.Contains(roleStrategy))
                {
                    this.session.RemoveCompositeRole(this, roleType, roleStrategy, previousRoles);
                }
            }
        }

        public virtual void RemoveCompositeRoles(IRoleType roleType)
        {
            this.AssertNotDeleted();
            this.RoleCompositesChecks(roleType);

            var previousRoleStrategies = this.session.GetCompositeRoles(this, roleType);

            this.ChangeSet.OnChangingCompositesRole(this, roleType, previousRoleStrategies);

            if (previousRoleStrategies != null)
            {
                this.session.RemoveCompositeRoles(this, roleType, previousRoleStrategies);
            }
        }

        public virtual bool ExistCompositeRoles(IRoleType roleType)
        {
            // TODO: Optimize
            return this.GetCompositeRoles(roleType).Count > 0;
        }

        public virtual object GetAssociation(IAssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.GetCompositeAssociations(associationType);
            }

            return this.GetCompositeAssociation(associationType);
        }

        public virtual bool ExistAssociation(IAssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.ExistCompositeAssociations(associationType);
            }

            return this.ExistCompositeAssociation(associationType);
        }

        public virtual IObject GetCompositeAssociation(IAssociationType associationType)
        {
            this.AssertNotDeleted();

            // TODO: check associationType (see roleType checks)
            var association = this.session.GetAssociation(this, associationType);
            return association != null ? association.GetObject() : null;
        }

        public virtual bool ExistCompositeAssociation(IAssociationType associationType)
        {
            return this.GetCompositeAssociation(associationType) != null;
        }

        public virtual Allors.Extent GetCompositeAssociations(IAssociationType associationType)
        {
            this.AssertNotDeleted();

            return new ExtentAssociation(this, associationType);
        }

        public virtual bool ExistCompositeAssociations(IAssociationType associationType)
        {
            return this.GetCompositeAssociations(associationType).Count > 0;
        }

        public virtual void Delete()
        {
            this.AssertNotDeleted();

            this.session.Delete(this);

            this.isDeleted = true;

            this.ChangeSet.OnDeleted(this);
        }

        public virtual IObject GetObject()
        {
            IObject allorsObject;
            if (this.allorizedObjectWeakReference == null)
            {
                allorsObject = this.session.Workspace.Database.ObjectFactory.Create(this);
                this.allorizedObjectWeakReference = new WeakReference(allorsObject);
            }
            else
            {
                allorsObject = (IObject)this.allorizedObjectWeakReference.Target;
                if (allorsObject == null)
                {
                    allorsObject = this.session.Workspace.Database.ObjectFactory.Create(this);
                    this.allorizedObjectWeakReference.Target = allorsObject;
                }
            }

            return allorsObject;
        }

        internal static object LoadUnit(XmlReader reader, IRoleType roleType, string value)
        {
            var unitType = (IUnit)roleType.ObjectType;
            var unitTypeTag = unitType.UnitTag;
            if (reader.IsEmptyElement)
            {
                if (unitTypeTag == UnitTags.AllorsString)
                {
                    return string.Empty;
                }

                if (unitTypeTag == UnitTags.AllorsBinary)
                {
                    return EmptyByteArray;
                }

                throw new ArgumentException("Unit can not be empty for Unit IObjectType " + roleType.ObjectType);
            }

            return Serialization.ReadString(value, unitTypeTag);
        }

        internal virtual void Commit()
        {
            this.rollbackIsDeleted = this.isDeleted;
            this.isNew = false;
        }

        internal virtual void Rollback()
        {
            this.isDeleted = this.rollbackIsDeleted;
            this.isNew = false;
        }

        internal object GetInternalizedUnitRole(IRoleType roleType)
        {
            return this.session.GetUnitRole(this, roleType);
        }

        internal virtual void RoleCompositeChecks(IRoleType roleType, IObject role)
        {
            this.RoleCompositeChecks(roleType);

            if (role != null)
            {
                if (role.Strategy.IsDeleted)
                {
                    throw new ArgumentException(roleType + " on object " + this + " is removed.");
                }

                var compositeType = roleType.ObjectType as IComposite;

                if (compositeType == null)
                {
                    throw new ArgumentException(roleType + " has no CompositeType");
                }

                if (!compositeType.IsAssignableFrom(role.Strategy.Class))
                {
                    throw new ArgumentException(role.Strategy.Class + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
                }
            }
        }

        internal virtual void RoleCompositesChecks(IRoleType roleType, IObject allorsObject)
        {
            this.RoleCompositesChecks(roleType);

            this.RoleCompositesSharedChecks(roleType, allorsObject);
        }

        internal virtual object LoadUnitRole(XmlReader reader, IRoleType roleType)
        {
            RoleAssertions.UnitRoleChecks(this, roleType);
            var value = string.Empty;
            if (!reader.IsEmptyElement)
            {
                value = reader.ReadString();
            }

            return LoadUnit(reader, roleType, value);
        }

        internal virtual Strategy LoadDatabaseCompositeRole(XmlReader reader, IRoleType roleType)
        {
            try
            {
                this.RoleCompositeChecks(roleType);
                var strategy = this.WorkspaceSession.LoadStrategy(reader);
                return strategy;
            }
            catch
            {
                throw new Exception("Could not load composite relation");
            }
        }

        internal virtual HashSet<Strategy> LoadDatabaseCompositesRole(XmlReader reader, IRoleType roleType)
        {
            try
            {
                this.RoleCompositesChecks(roleType);
                var strategies = this.WorkspaceSession.LoadRoleStrategies(reader);
                return strategies;
            }
            catch
            {
                throw new Exception("Could not load composites relation");
            }
        }

        internal virtual object GetOriginalUnitRole(IRoleType roleType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                return this.DatabaseStrategy.GetUnitRole(roleType);
            }

            return null;
        }

        internal virtual Strategy GetOriginalCompositeRole(IRoleType roleType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                var compositeRoleObject = this.DatabaseStrategy.GetCompositeRole(roleType);
                return compositeRoleObject != null ? this.session.GetStrategy(compositeRoleObject) : null;
            }

            return null;
        }

        internal virtual HashSet<Strategy> GetOriginalCompositeRoles(IRoleType roleType)
        {
            HashSet<Strategy> roles = null;

            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                var originalObjects = this.DatabaseStrategy.GetCompositeRoles(roleType);
                if (originalObjects.Count > 0)
                {
                    roles = new HashSet<Strategy>();
                    foreach (IObject roleObject in originalObjects)
                    {
                        var role = this.session.GetStrategy(roleObject);
                        roles.Add(role);
                    }
                }
            }

            return roles;
        }

        internal virtual Strategy GetOriginalCompositeAssociation(IAssociationType associationType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                var compositeAssociationObject = this.DatabaseStrategy.GetCompositeAssociation(associationType);
                return compositeAssociationObject != null ? this.session.GetStrategy(compositeAssociationObject) : null;
            }

            return null;
        }

        internal virtual HashSet<Strategy> GetOriginalCompositesAssociation(IAssociationType associationType)
        {
            var associations = new HashSet<Strategy>();

            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                foreach (IObject associationObject in this.DatabaseStrategy.GetCompositeAssociations(associationType))
                {
                    var association = this.session.GetStrategy(associationObject);
                    if (association != null)
                    {
                        associations.Add(association);
                    }
                }
            }

            return associations;
        }

        private void AssertNotDeleted()
        {
            if (this.isDeleted)
            {
                throw new Exception("Object of class " + this.objectType.Name + " with id " + this.ObjectId +
                                    " has been deleted");
            }
        }

        private void RoleCompositeChecks(IRoleType roleType)
        {
            if (!roleType.AssociationType.ObjectType.IsAssignableFrom(this.objectType))
            {
                throw new ArgumentException(this.objectType + " has no relationType with role " + roleType + ".");
            }

            if (!roleType.IsOne)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity many.");
            }
        }

        private void RoleCompositesChecks(IRoleType roleType)
        {
            if (!roleType.AssociationType.ObjectType.IsAssignableFrom(this.objectType))
            {
                throw new ArgumentException(this.objectType + " has no relationType with role " + roleType + ".");
            }

            if (!roleType.IsMany)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity one.");
            }
        }

        private void RoleCompositesSharedChecks(IRoleType roleType, IObject allorsObject)
        {
            if (allorsObject.Strategy.IsDeleted)
            {
                throw new ArgumentException(roleType + " on object " + this + " is removed.");
            }

            var compositeType = roleType.ObjectType as IComposite;

            if (compositeType == null)
            {
                throw new ArgumentException(roleType + " has no CompositeType");
            }

            if (!compositeType.IsAssignableFrom(allorsObject.Strategy.Class))
            {
                throw new ArgumentException(allorsObject.Strategy.Class + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
            }
        }
    }
}