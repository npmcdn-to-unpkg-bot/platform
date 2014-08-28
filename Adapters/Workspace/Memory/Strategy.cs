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

namespace Allors.Adapters.Workspace.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    using Allors.Adapters;
    using Allors.Meta;

    public class Strategy : IStrategy
    {
        private static readonly byte[] EmptyByteArray = new byte[0];
        
        private readonly WorkspaceSession session;
        private readonly Class objectType;

        // TODO: move to a BitFlag
        private bool isDeleted;
        private bool rollbackIsDeleted;
        private bool isNew;
        
        private WeakReference allorizedObjectWeakReference;

        // New
        internal Strategy(WorkspaceSession session, Class objectType, ObjectId objectId)
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
            : this(session, strategy.ObjectType, strategy.ObjectId)
        {
            this.rollbackIsDeleted = false;
            this.isNew = false;
        }

        // Load
        internal Strategy(WorkspaceSession worspaceSession, Class objectType, ObjectId objectId, bool deleted)
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

        public Class ObjectType
        {
            get { return this.objectType; }
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

        public override string ToString()
        {
            return this.objectType.Name + " " + this.ObjectId;
        }

        public virtual object GetRole(RoleType roleType)
        {
            if (roleType.ObjectType is UnitType)
            {
                return this.GetUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.GetCompositeRoles(roleType);
            }

            return this.GetCompositeRole(roleType);
        }

        public virtual void SetRole(RoleType roleType, object value)
        {
            if (roleType.ObjectType is UnitType)
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

        public virtual void RemoveRole(RoleType roleType)
        {
            if (roleType.ObjectType is UnitType)
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

        public virtual bool ExistRole(RoleType roleType)
        {
            if (roleType.ObjectType is UnitType)
            {
                return this.ExistUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.ExistCompositeRoles(roleType);
            }

            return this.ExistCompositeRole(roleType);
        }

        public virtual object GetUnitRole(RoleType roleType)
        {
            this.CheckRemoved();
            this.RoleUnitChecks(roleType);

            var unitRole = this.GetInternalizedUnitRole(roleType);
            return unitRole;
        }

        public virtual void SetUnitRole(RoleType roleType, object role)
        {
            this.CheckRemoved();
            this.RoleUnitChecks(roleType);

            this.ChangeSet.OnChangingUnitRole(this, roleType);

            if (role == null)
            {
                this.session.SetUnitRole(this, roleType, null);
            }
            else
            {
                role = this.session.MemoryWorkspace.Internalize(role, roleType);

                this.session.SetUnitRole(this, roleType, role);
            }
        }

        public virtual void RemoveUnitRole(RoleType roleType)
        {
            this.SetUnitRole(roleType, null);
        }

        public virtual bool ExistUnitRole(RoleType roleType)
        {
            return this.GetUnitRole(roleType) != null;
        }

        public virtual IObject GetCompositeRole(RoleType roleType)
        {
            this.CheckRemoved();
            this.RoleCompositeChecks(roleType);

            var roleStrategy = this.session.GetCompositeRole(this, roleType);
            return roleStrategy != null ? roleStrategy.GetObject() : null;
        }

        public virtual void SetCompositeRole(RoleType roleType, IObject role)
        {
            if (role == null)
            {
                this.RemoveCompositeRole(roleType);
            }
            else
            {
                this.CheckRemoved();
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

        public virtual void RemoveCompositeRole(RoleType roleType)
        {
            this.CheckRemoved();
            this.RoleCompositeChecks(roleType);

            var previousRoleStrategy = this.session.GetCompositeRole(this, roleType);

            this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRoleStrategy, null);

            if (previousRoleStrategy != null)
            {
                this.session.RemoveCompositeRole(this, roleType, previousRoleStrategy);
            }
        }

        public virtual bool ExistCompositeRole(RoleType roleType)
        {
            return this.GetCompositeRole(roleType) != null;
        }

        public virtual Allors.Extent GetCompositeRoles(RoleType roleType)
        {
            this.CheckRemoved();

            return new ExtentRole(this, roleType);
        }

        public virtual void SetCompositeRoles(RoleType roleType, Allors.Extent roles)
        {
            if (roles == null || roles.Count == 0)
            {
                this.RemoveCompositeRoles(roleType);
            }
            else
            {
                this.CheckRemoved();

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

        public virtual void AddCompositeRole(RoleType roleType, IObject objectToAdd)
        {
            this.CheckRemoved();
            if (objectToAdd != null)
            {
                this.RoleCompositesChecks(roleType, objectToAdd);

                var previousRoles = this.session.GetCompositeRoles(this, roleType);
                var roleStrategy = this.session.GetStrategy(objectToAdd.Strategy.ObjectId);

                if (previousRoles == null || !previousRoles.Contains(roleStrategy))
                {
                    this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);
                    this.session.AddCompositeRole(this, roleType, roleStrategy);
                }
            }
        }

        public virtual void RemoveCompositeRole(RoleType roleType, IObject objectToRemove)
        {
            this.CheckRemoved();

            if (objectToRemove != null)
            {
                this.RoleCompositesChecks(roleType, objectToRemove);

                var previousRoles = this.session.GetCompositeRoles(this, roleType);
                var roleStrategy = this.session.GetStrategy(objectToRemove.Strategy.ObjectId);

                this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);

                if (previousRoles != null && previousRoles.Contains(roleStrategy))
                {
                    this.session.RemoveCompositeRole(this, roleType, roleStrategy, previousRoles);
                }
            }
        }

        public virtual void RemoveCompositeRoles(RoleType roleType)
        {
            this.CheckRemoved();
            this.RoleCompositesChecks(roleType);

            var previousRoleStrategies = this.session.GetCompositeRoles(this, roleType);

            this.ChangeSet.OnChangingCompositesRole(this, roleType, previousRoleStrategies);

            if (previousRoleStrategies != null)
            {
                this.session.RemoveCompositeRoles(this, roleType, previousRoleStrategies);
            }
        }

        public virtual bool ExistCompositeRoles(RoleType roleType)
        {
            // TODO: Optimize
            return this.GetCompositeRoles(roleType).Count > 0;
        }

        public virtual object GetAssociation(AssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.GetCompositeAssociations(associationType);
            }

            return this.GetCompositeAssociation(associationType);
        }

        public virtual bool ExistAssociation(AssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.ExistCompositeAssociations(associationType);
            }

            return this.ExistCompositeAssociation(associationType);
        }

        public virtual IObject GetCompositeAssociation(AssociationType associationType)
        {
            this.CheckRemoved();

            // TODO: check associationType (see roleType checks)
            var association = this.session.GetAssociation(this, associationType);
            return association != null ? association.GetObject() : null;
        }

        public virtual bool ExistCompositeAssociation(AssociationType associationType)
        {
            return this.GetCompositeAssociation(associationType) != null;
        }

        public virtual Allors.Extent GetCompositeAssociations(AssociationType associationType)
        {
            this.CheckRemoved();

            return new ExtentAssociation(this, associationType);
        }

        public virtual bool ExistCompositeAssociations(AssociationType associationType)
        {
            return this.GetCompositeAssociations(associationType).Count > 0;
        }

        public virtual void Delete()
        {
            this.CheckRemoved();

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

        internal static object LoadUnit(XmlReader reader, RoleType roleType, string value)
        {
            var unitType = (UnitType)roleType.ObjectType;
            var unitTypeTag = (UnitTags)unitType.UnitTag;
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

                throw new ArgumentException("Unit can not be empty for Unit ObjectType " + roleType.ObjectType);
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

        internal object GetInternalizedUnitRole(RoleType roleType)
        {
            return this.session.GetUnitRole(this, roleType);
        }

        internal virtual void RoleUnitChecks(RoleType relationType)
        {
            if (relationType.ObjectType is CompositeType)
            {
                throw new ArgumentException(relationType.ObjectType + " on relationType " + relationType + " is not a unit type.");
            }

            if (!relationType.AssociationType.ObjectType.RootClasses.Contains(this.objectType))
            {
                throw new ArgumentException(this.objectType + " has no relationType with role " + relationType + ".");
            }
        }

        internal virtual void RoleCompositeChecks(RoleType roleType, IObject role)
        {
            this.RoleCompositeChecks(roleType);

            if (role != null)
            {
                if (role.Strategy.IsDeleted)
                {
                    throw new ArgumentException(roleType + " on object " + this + " is removed.");
                }

                var compositeType = roleType.ObjectType as CompositeType;

                if (compositeType == null)
                {
                    throw new ArgumentException(roleType + " has no CompositeType");
                }

                if (!compositeType.RootClasses.Contains(role.Strategy.ObjectType))
                {
                    throw new ArgumentException(role.Strategy.ObjectType + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
                }
            }
        }

        internal virtual void RoleCompositesChecks(RoleType roleType, IObject allorsObject)
        {
            this.RoleCompositesChecks(roleType);

            this.RoleCompositesSharedChecks(roleType, allorsObject);
        }

        internal virtual object LoadUnitRole(XmlReader reader, RoleType roleType)
        {
            this.RoleUnitChecks(roleType);
            var value = string.Empty;
            if (!reader.IsEmptyElement)
            {
                value = reader.ReadString();
            }

            return LoadUnit(reader, roleType, value);
        }

        internal virtual Strategy LoadDatabaseCompositeRole(XmlReader reader, RoleType roleType)
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

        internal virtual HashSet<Strategy> LoadDatabaseCompositesRole(XmlReader reader, RoleType roleType)
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

        internal virtual object GetOriginalUnitRole(RoleType roleType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                return this.DatabaseStrategy.GetUnitRole(roleType);
            }

            return null;
        }

        internal virtual Strategy GetOriginalCompositeRole(RoleType roleType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                var compositeRoleObject = this.DatabaseStrategy.GetCompositeRole(roleType);
                return compositeRoleObject != null ? this.session.GetStrategy(compositeRoleObject) : null;
            }

            return null;
        }

        internal virtual HashSet<Strategy> GetOriginalCompositeRoles(RoleType roleType)
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

        internal virtual Strategy GetOriginalCompositeAssociation(AssociationType associationType)
        {
            if (this.DatabaseStrategy != null &&
                !this.DatabaseStrategy.IsDeleted)
            {
                var compositeAssociationObject = this.DatabaseStrategy.GetCompositeAssociation(associationType);
                return compositeAssociationObject != null ? this.session.GetStrategy(compositeAssociationObject) : null;
            }

            return null;
        }

        internal virtual HashSet<Strategy> GetOriginalCompositesAssociation(AssociationType associationType)
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

        private ChangeSet ChangeSet
        {
            get
            {
                return this.WorkspaceSession.WorkspaceChangeSet;
            }
        }

        private void CheckRemoved()
        {
            if (this.isDeleted)
            {
                throw new Exception("Object of class " + this.objectType.Name + " with id " + this.ObjectId +
                                    " has been deleted");
            }
        }

        private void RoleCompositeChecks(RoleType roleType)
        {
            if (!roleType.AssociationType.ObjectType.RootClasses.Contains(this.objectType))
            {
                throw new ArgumentException(this.objectType + " has no relationType with role " + roleType + ".");
            }

            if (!roleType.IsOne)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity many.");
            }
        }

        private void RoleCompositesChecks(RoleType roleType)
        {
            if (!roleType.AssociationType.ObjectType.RootClasses.Contains(this.objectType))
            {
                throw new ArgumentException(this.objectType + " has no relationType with role " + roleType + ".");
            }

            if (!roleType.IsMany)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity one.");
            }
        }

        private void RoleCompositesSharedChecks(RoleType roleType, IObject allorsObject)
        {
            if (allorsObject.Strategy.IsDeleted)
            {
                throw new ArgumentException(roleType + " on object " + this + " is removed.");
            }

            var compositeType = roleType.ObjectType as CompositeType;

            if (compositeType == null)
            {
                throw new ArgumentException(roleType + " has no CompositeType");
            }

            if (!compositeType.RootClasses.Contains(allorsObject.Strategy.ObjectType))
            {
                throw new ArgumentException(allorsObject.Strategy.ObjectType + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
            }
        }
    }
}