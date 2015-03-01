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
// <summary>
//   Defines the AllorsStrategySql type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Allors.Populations;

    using Meta;

    internal class Strategy : IStrategy
    {
        private readonly Reference reference;
        private readonly ObjectId objectId;

        private IObject allorsObject;
        private Roles roles;

        internal Strategy(Reference reference)
        {
            this.reference = reference;
            this.objectId = reference.ObjectId;
        }

        public ISession Session
        {
            get { return this.reference.Session; }
        }

        public IDatabaseSession DatabaseSession
        {
            get
            {
                return this.reference.Session;
            }
        }

        public IClass ObjectType
        {
            get
            {
                if (!this.reference.Exists)
                {
                    throw new Exception("Object that had  " + this.reference.ObjectType.Name + " with id " + this.ObjectId + " does not exist");
                }

                return this.reference.ObjectType;
            }
        }

        public ObjectId ObjectId
        {
            get { return this.objectId; }
        }

        public bool IsDeleted
        {
            get
            {
                return !this.reference.Exists;
            }
        }

        public bool IsNewInSession
        {
            get
            {
                return this.reference.IsNew;
            }
        }

        public bool IsNewInWorkspace
        {
            get
            {
                return false;
            }
        }

        internal DatabaseSession SqlSession
        {
            get { return this.reference.Session; }
        }

        internal Roles Roles
        {
            get
            {
                return this.roles ?? (this.roles = this.reference.Session.GetOrCreateRoles(this.reference));
            }
        }

        internal Reference Reference
        {
            get
            {
                return this.reference;
            }
        }

        public IObject GetObject()
        {
            return this.allorsObject ?? (this.allorsObject = this.reference.Session.Database.ObjectFactory.Create(this));
        }

        public virtual void Delete()
        {
            this.AssertExist();

            foreach (var roleType in this.ObjectType.RoleTypes)
            {
                if (roleType.ObjectType.IsComposite)
                {
                    this.RemoveRole(roleType);
                }
            }

            foreach (var associationType in this.ObjectType.AssociationTypes)
            {
                var roleType = associationType.RoleType;

                if (associationType.IsMany)
                {
                    foreach (var association in this.SqlSession.GetAssociations(this, associationType))
                    {
                        var associationStrategy = this.SqlSession.GetOrCreateAssociationForExistingObject(association).Strategy;
                        if (roleType.IsMany)
                        {
                            associationStrategy.RemoveCompositeRole(roleType, this.GetObject()); 
                        }
                        else
                        {
                            associationStrategy.RemoveCompositeRole(roleType);
                        }
                    }
                }
                else
                {
                    var association = this.GetCompositeAssociation(associationType);
                    if (association != null)
                    {
                        if (roleType.IsMany)
                        {
                            association.Strategy.RemoveCompositeRole(roleType, this.GetObject());
                        }
                        else
                        {
                            association.Strategy.RemoveCompositeRole(roleType);
                        }
                    }
                }
            }

            this.SqlSession.SessionCommands.DeleteObjectCommand.Execute(this);
            this.reference.Exists = false;

            this.SqlSession.SqlChangeSet.OnDeleted(this.ObjectId);
        }

        public virtual bool ExistRole(IRoleType roleType)
        {
            if (roleType.ObjectType.IsUnit)
            {
                return this.ExistUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.ExistCompositeRoles(roleType);
            }

            return this.ExistCompositeRole(roleType);
        }

        public virtual object GetRole(IRoleType roleType)
        {
            if (roleType.ObjectType.IsUnit)
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
            if (roleType.ObjectType.IsUnit)
            {
                this.SetUnitRole(roleType, value);
            }
            else
            {
                if (roleType.IsMany)
                {
                    var roleExtent = value as Allors.Extent;
                    if (roleExtent == null)
                    {
                        var roleList = new ArrayList((ICollection)value);
                        roleExtent = (IObject[])roleList.ToArray(typeof(IObject));
                    }

                    this.SetCompositeRoles(roleType, roleExtent);
                }
                else
                {
                    this.SetCompositeRole(roleType, (IObject)value);
                }
            }
        }

        public virtual void RemoveRole(IRoleType roleType)
        {
            if (roleType.ObjectType.IsUnit)
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

        public virtual bool ExistUnitRole(IRoleType roleType)
        {
            return this.GetUnitRole(roleType) != null;
        }

        public virtual object GetUnitRole(IRoleType roleType)
        {
            this.AssertExist();

            return this.Roles.GetUnitRole(roleType);
        }

        public virtual void SetUnitRole(IRoleType roleType, object role)
        {
            this.AssertExist();

            RoleAssertions.UnitRoleChecks(this, roleType);

            if (role != null)
            {
                role = roleType.Normalize(role);
            }

            var oldUnit = this.GetUnitRole(roleType);
            if (!Equals(oldUnit, role))
            {
                this.Roles.SetUnitRole(roleType, role);
            }
        }

        public virtual void RemoveUnitRole(IRoleType roleType)
        {
            this.SetUnitRole(roleType, null);
        }

        public virtual bool ExistCompositeRole(IRoleType roleType)
        {
            return this.GetCompositeRole(roleType) != null;
        }

        public virtual IObject GetCompositeRole(IRoleType roleType)
        {
            this.AssertExist();
            var role = this.Roles.GetCompositeRole(roleType);
            return (role == null) ? null : this.SqlSession.GetOrCreateAssociationForExistingObject(role).Strategy.GetObject();
        }

        public virtual void SetCompositeRole(IRoleType roleType, IObject newRoleObject)
        {
            if (newRoleObject == null)
            {
                this.RemoveCompositeRole(roleType);
                return;
            }

            this.AssertExist();

            RoleAssertions.CompositeRoleChecks(this, roleType, newRoleObject);

            var newRoleObjectId = (Strategy)newRoleObject.Strategy;

            this.Roles.SetCompositeRole(roleType, newRoleObjectId);
        }

        public virtual void RemoveCompositeRole(IRoleType roleType)
        {
            this.AssertExist();

            RoleAssertions.CompositeRoleChecks(this, roleType);

            this.Roles.RemoveCompositeRole(roleType);
        }

        public virtual bool ExistCompositeRoles(IRoleType roleType)
        {
            return this.GetCompositeRoles(roleType).Count != 0;
        }

        public virtual Allors.Extent GetCompositeRoles(IRoleType roleType)
        {
            this.AssertExist();

            return new ExtentRoles(this, roleType);
        }

        public virtual void AddCompositeRole(IRoleType roleType, IObject roleObject)
        {
            this.AssertExist();

            if (roleObject != null)
            {
                RoleAssertions.CompositeRolesChecks(this, roleType, roleObject);

                var role = (Strategy)roleObject.Strategy;
                
                this.Roles.AddCompositeRole(roleType, role);
            }
        }

        public virtual void RemoveCompositeRole(IRoleType roleType, IObject roleObject)
        {
            this.AssertExist();
            
            if (roleObject != null)
            {
                RoleAssertions.CompositeRolesChecks(this, roleType, roleObject);
                
                var role = (Strategy)roleObject.Strategy;

                this.Roles.RemoveCompositeRole(roleType, role);
            }
        }

        public virtual void SetCompositeRoles(IRoleType roleType, Allors.Extent roleObjects)
        {
            if (roleObjects == null || roleObjects.Count == 0)
            {
                this.RemoveCompositeRoles(roleType);
            }
            else
            {
                this.AssertExist();
                
                // TODO: use CompositeRoles
                var previousRoles = new List<ObjectId>(this.Roles.GetCompositeRoles(roleType));
                var newRoles = new HashSet<ObjectId>();

                foreach (IObject roleObject in roleObjects)
                {
                    if (roleObject != null)
                    {
                        RoleAssertions.CompositeRolesChecks(this, roleType, roleObject);
                        var role = (Strategy)roleObject.Strategy;

                        if (!previousRoles.Contains(role.ObjectId))
                        {
                            this.Roles.AddCompositeRole(roleType, role);
                        }

                        newRoles.Add(role.ObjectId);
                    }
                }

                foreach (var previousRole in previousRoles)
                {
                    if (!newRoles.Contains(previousRole))
                    {
                        this.Roles.RemoveCompositeRole(roleType, this.SqlSession.GetOrCreateAssociationForExistingObject(previousRole).Strategy);
                    }
                }
            }
        }

        public virtual void RemoveCompositeRoles(IRoleType roleType)
        {
            this.AssertExist();

            RoleAssertions.CompositeRoleChecks(this, roleType);

            var previousRoles = this.Roles.GetCompositeRoles(roleType);

            foreach (var previousRole in previousRoles)
            {
                this.Roles.RemoveCompositeRole(roleType, this.SqlSession.GetOrCreateAssociationForExistingObject(previousRole).Strategy);
            }
        }

        public virtual bool ExistAssociation(IAssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.ExistCompositeAssociations(associationType);
            }

            return this.ExistCompositeAssociation(associationType);
        }

        public virtual object GetAssociation(IAssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.GetCompositeAssociations(associationType);
            }

            return this.GetCompositeAssociation(associationType);
        }

        public virtual bool ExistCompositeAssociation(IAssociationType associationType)
        {
            return this.GetCompositeAssociation(associationType) != null;
        }

        public virtual IObject GetCompositeAssociation(IAssociationType associationType)
        {
            this.AssertExist();

            var association = this.SqlSession.GetAssociation(this, associationType);

            return association == null ? null : association.Strategy.GetObject();
        }

        public virtual bool ExistCompositeAssociations(IAssociationType associationType)
        {
            return this.GetCompositeAssociations(associationType).Count != 0;
        }

        public virtual Allors.Extent GetCompositeAssociations(IAssociationType associationType)
        {
            this.AssertExist();
            return new ExtentAssociations(this, associationType);
        }

        public override string ToString()
        {
            return "[" + this.ObjectType + ":" + this.ObjectId + "]";
        }

        internal virtual void Release()
        {
            this.roles = null;
        }

        internal int ExtentRolesGetCount(IRoleType roleType)
        {
            this.AssertExist();

            return this.Roles.ExtentCount(roleType);
        }

        internal IObject ExtentRolesFirst(IRoleType roleType)
        {
            this.AssertExist();

            return this.Roles.ExtentFirst(this.SqlSession, roleType);
        }

        internal void ExtentRolesCopyTo(IRoleType roleType, Array array, int index)
        {
            this.Roles.ExtentCopyTo(this.SqlSession, roleType, array, index);
        }

        internal int ExtentIndexOf(IRoleType roleType, IObject value)
        {
            var i = 0;
            foreach (var oid in this.Roles.GetCompositeRoles(roleType))
            {
                if (oid.Equals(value.Id))
                {
                    return i;
                }
                ++i;
            }

            return -1;
        }

        internal IObject ExtentGetItem(IRoleType roleType, int index)
        {
            var i = 0;
            foreach (var oid in this.Roles.GetCompositeRoles(roleType))
            {
                if (i == index)
                {
                    return this.SqlSession.GetOrCreateAssociationForExistingObject(oid).Strategy.GetObject();
                }
                ++i;
            }

            return null;
        }

        internal bool ExtentRolesContains(IRoleType roleType, IObject value)
        {
            return this.Roles.ExtentContains(roleType, value.Id);
        }

        internal virtual ObjectId[] ExtentGetCompositeAssociations(IAssociationType associationType)
        {
            this.AssertExist();

            return this.SqlSession.GetAssociations(this, associationType);
        }

        protected virtual void AssertExist()
        {
            if (!this.reference.Exists)
            {
                throw new Exception("Object of class " + this.ObjectType.Name + " with id " + this.ObjectId + " does not exist");
            }
        }
    }
}