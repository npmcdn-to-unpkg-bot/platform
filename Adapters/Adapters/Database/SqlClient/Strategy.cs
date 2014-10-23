// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Strategy.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections;

    using Allors.Meta;

    public class Strategy : IStrategy
    {
        private readonly DatabaseSession session;
        private readonly ObjectId objectId;
        private WeakReference<IObject> weakReferenceToObject;

        private IClass objectType;
        private bool isNew;
        private bool? isDeleted;
        
        public Strategy(DatabaseSession session, IClass objectType, ObjectId objectId, bool isNew, bool? isDeleted)
        {
            this.session = session;
            this.objectType = objectType;
            this.objectId = objectId;
            this.isNew = isNew;
            this.isDeleted = isDeleted;
        }

        ISession IStrategy.Session 
        {
            get
            {
                return this.session;
            }
        }

        public DatabaseSession Session
        {
            get
            {
                return this.session;
            }
        }

        IDatabaseSession IStrategy.DatabaseSession 
        {
            get
            {
                return this.session;
            }
        }

        public DatabaseSession DatabaseSession
        {
            get
            {
                return this.session;
            }
        }

        public IClass ObjectType 
        {
            get
            {
                return this.objectType;
            }
        }

        public ObjectId ObjectId 
        {
            get
            {
                return this.objectId;
            }
        }

        public bool IsDeleted 
        {
            get
            {
                if (this.isDeleted.HasValue)
                {
                    return this.isDeleted.Value;
                }

                throw new NotImplementedException();
            }
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
            get
            {
                return false;
            }
        }

        public IObject GetObject()
        {
            IObject obj = null;
            if (this.weakReferenceToObject != null)
            {
                this.weakReferenceToObject.TryGetTarget(out obj);
            }

            if (obj == null)
            {
                obj = this.session.Database.ObjectFactory.Create(this);
                this.weakReferenceToObject = new WeakReference<IObject>(obj);
            }

            return obj;
        }

        public void Delete()
        {
            throw new NotImplementedException();
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
                    var roleExtent = value as Extent;
                    if (roleExtent == null)
                    {
                        // TODO: Use Linq
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
        
        public bool ExistUnitRole(IRoleType roleType)
        {
            return this.session.ExistUnitRole(this.objectId, roleType);
        }

        public object GetUnitRole(IRoleType roleType)
        {
            return this.session.GetUnitRole(this.ObjectId, roleType);
        }

        public void SetUnitRole(IRoleType roleType, object unit)
        {
            if (unit == null)
            {
                this.RemoveUnitRole(roleType);
                return;
            }

            this.Session.Database.RoleChecks.UnitRoleChecks(this, roleType);
            this.session.SetUnitRole(this.objectId, roleType, unit);
        }

        public void RemoveUnitRole(IRoleType roleType)
        {
            this.session.RemoveUnitRole(this.objectId, roleType);
        }

        public bool ExistCompositeRole(IRoleType roleType)
        {
            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    return this.session.ExistCompositeRoleOneToOne(this.objectId, roleType);
                case Multiplicity.ManyToOne:
                    return this.session.ExistCompositeRoleManyToOne(this.objectId, roleType);
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public IObject GetCompositeRole(IRoleType roleType)
        {
            ObjectId role;
            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    role = this.session.GetCompositeRoleOneToOne(this.objectId, roleType);
                    break;
                case Multiplicity.ManyToOne:
                    role = this.session.GetCompositeRoleManyToOne(this.objectId, roleType);
                    break;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }

            return role != null ? this.session.InstantiateStrategy(role).GetObject() : null;
        }

        public void SetCompositeRole(IRoleType roleType, IObject role)
        {
            if (role == null)
            {
                this.RemoveCompositeRole(roleType);
                return;
            }

            this.Session.Database.RoleChecks.CompositeRoleChecks(this, roleType, role);

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    this.session.SetCompositeRoleOneToOne(this.objectId, roleType, role.Id);
                    return;
                case Multiplicity.ManyToOne:
                    this.session.SetCompositeRoleManyToOne(this.objectId, roleType, role.Id);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public void RemoveCompositeRole(IRoleType roleType)
        {
            this.Session.Database.RoleChecks.CompositeRoleChecks(this, roleType);

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    this.session.RemoveCompositeRoleOneToOne(this.objectId, roleType);
                    return;
                case Multiplicity.ManyToOne:
                    this.session.RemoveCompositeRoleManyToOne(this.objectId, roleType);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public bool ExistCompositeRoles(IRoleType roleType)
        {
            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    return this.session.ExistCompositeRoleOneToMany(this.objectId, roleType);
                case Multiplicity.ManyToMany:
                    return this.session.ExistCompositeRoleManyToMany(this.objectId, roleType);
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public Extent GetCompositeRoles(IRoleType roleType)
        {
            return new AllorsExtentFilteredSql(this.session, this, roleType);
        }

        public void AddCompositeRole(IRoleType roleType, IObject role)
        {
            if (role == null)
            {
                return;
            }

            this.Session.Database.RoleChecks.CompositeRolesChecks(this, roleType, role);

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    this.session.AddCompositeRoleOneToMany(this.objectId, roleType, role.Id);
                    return;
                case Multiplicity.ManyToMany:
                    this.session.AddCompositeRoleManyToMany(this.objectId, roleType, role.Id);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public void RemoveCompositeRole(IRoleType roleType, IObject role)
        {
            if (role == null)
            {
                return;
            }

            this.Session.Database.RoleChecks.CompositeRolesChecks(this, roleType, role);

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    this.session.RemoveCompositeRoleOneToMany(this.objectId, roleType, role.Id);
                    return;
                case Multiplicity.ManyToMany:
                    this.session.RemoveCompositeRoleManyToMany(this.objectId, roleType, role.Id);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public void SetCompositeRoles(IRoleType roleType, Extent roles)
        {
            if (roles == null || roles.Count == 0)
            {
                this.RemoveCompositeRoles(roleType);
                return;
            }

            var roleObjectIds = new ObjectId[roles.Count];
            for (var i = 0; i < roles.Count; i++)
            {
                var role = roles[i];
                
                this.Session.Database.RoleChecks.CompositeRolesChecks(this, roleType, role);

                roleObjectIds[i] = role.Id;
            }

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    this.session.SetCompositeRoleOneToMany(this.objectId, roleType, roleObjectIds);
                    return;
                case Multiplicity.ManyToMany:
                    this.session.SetCompositeRoleManyToMany(this.objectId, roleType, roleObjectIds);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }
        }

        public void RemoveCompositeRoles(IRoleType roleType)
        {
            this.Session.Database.RoleChecks.CompositeRolesChecks(this, roleType);

            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    this.session.RemoveCompositeRolesOneToMany(this.objectId, roleType);
                    return;
                case Multiplicity.ManyToMany:
                    this.session.RemoveCompositeRolesManyToMany(this.objectId, roleType);
                    return;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
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

        public bool ExistCompositeAssociation(IAssociationType associationType)
        {
            switch (associationType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    return this.session.ExistCompositeAssociationOneToOne(this.objectId, associationType);
                case Multiplicity.OneToMany:
                    return this.session.ExistCompositeAssociationOneToMany(this.objectId, associationType);
                default:
                    throw new Exception("Unsupported multiplicity " + associationType.RelationType.Multiplicity);
            }
        }

        public IObject GetCompositeAssociation(IAssociationType associationType)
        {
            ObjectId association;
            switch (associationType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToOne:
                    association = this.session.GetCompositeAssociationOneToOne(this.objectId, associationType);
                    break;
                case Multiplicity.OneToMany:
                    association = this.session.GetCompositeAssociationOneToMany(this.objectId, associationType);
                    break;
                default:
                    throw new Exception("Unsupported multiplicity " + associationType.RelationType.Multiplicity);
            }

            return association != null ? this.session.InstantiateStrategy(association).GetObject() : null;
        }

        public bool ExistCompositeAssociations(IAssociationType associationType)
        {
            switch (associationType.RelationType.Multiplicity)
            {
                case Multiplicity.ManyToOne:
                    return this.session.ExistCompositeAssociationsManyToOne(this.objectId, associationType);
                case Multiplicity.ManyToMany:
                    return this.session.ExistCompositeAssociationsManyToMany(this.objectId, associationType);
                default:
                    throw new Exception("Unsupported multiplicity " + associationType.RelationType.Multiplicity);
            }
        }

        public Extent GetCompositeAssociations(IAssociationType associationType)
        {
            return new AllorsExtentFilteredSql(this.session, this, associationType);
        }

        public ObjectId[] ExtentGetCompositeRoles(IRoleType roleType)
        {
            ObjectId[] roles;
            switch (roleType.RelationType.Multiplicity)
            {
                case Multiplicity.OneToMany:
                    roles = this.session.GetCompositeRolesOneToMany(this.objectId, roleType);
                    break;
                case Multiplicity.ManyToMany:
                    roles = this.session.GetCompositeRolesManyToMany(this.objectId, roleType);
                    break;
                default:
                    throw new Exception("Unsupported multiplicity " + roleType.RelationType.Multiplicity);
            }

            return roles;
        }

        public ObjectId[] ExtentGetCompositeAssociations(IAssociationType associationType)
        {
            ObjectId[] associations;
            switch (associationType.RelationType.Multiplicity)
            {
                case Multiplicity.ManyToOne:
                    associations = this.session.GetCompositeAssociationsManyToOne(this.objectId, associationType);
                    break;
                case Multiplicity.ManyToMany:
                    associations = this.session.GetCompositeAssociationsManyToMany(this.objectId, associationType);
                    break;
                default:
                    throw new Exception("Unsupported multiplicity " + associationType.RelationType.Multiplicity);
            }

            return associations;
        }
    }
}