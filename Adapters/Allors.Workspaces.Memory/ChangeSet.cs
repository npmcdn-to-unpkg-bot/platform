// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeSet.cs" company="Allors bvba">
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
// <summary>
//   Defines the AllorsChangeSetMemory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Workspaces.Memory
{
    using System.Collections.Generic;

    using Allors.Meta;
    using Allors.Populations;

    public sealed class ChangeSet : IChangeSet
    {
        private readonly EmptySet<IRoleType> emptySet;

        private readonly HashSet<ObjectId> created;
        private readonly HashSet<ObjectId> deleted; 

        private readonly HashSet<ObjectId> associations;
        private readonly HashSet<ObjectId> roles;

        private readonly Dictionary<ObjectId, ISet<IRoleType>> roleTypesByAssociation;
        
        public ChangeSet()
        {
            this.emptySet = new EmptySet<IRoleType>(); 
            this.created = new HashSet<ObjectId>();
            this.deleted = new HashSet<ObjectId>();
            this.associations = new HashSet<ObjectId>();
            this.roles = new HashSet<ObjectId>();
            this.roleTypesByAssociation = new Dictionary<ObjectId, ISet<IRoleType>>();
        }

        public ISet<ObjectId> Created
        {
            get
            {
                return this.created;
            }
        }

        public ISet<ObjectId> Deleted
        {
            get
            {
                return this.deleted;
            }
        }

        public ISet<ObjectId> Associations
        {
            get
            {
                return this.associations;
            }
        }

        public ISet<ObjectId> Roles
        {
            get
            {
                return this.roles;
            }
        }

        public IDictionary<ObjectId, ISet<IRoleType>> RoleTypesByAssociation
        {
            get
            {
                return this.roleTypesByAssociation;
            }
        }

        public ISet<IRoleType> GetRoleTypes(ObjectId association)
        {
            ISet<IRoleType> roleTypes;
            if (this.RoleTypesByAssociation.TryGetValue(association, out roleTypes))
            {
                return roleTypes;
            }

            return this.emptySet;
        }

        public void OnCreated(Strategy strategy)
        {
            this.created.Add(strategy.ObjectId);
        }

        public void OnDeleted(Strategy strategy)
        {
            this.deleted.Add(strategy.ObjectId);
        }

        public void OnChangingUnitRole(Strategy association, IRoleType roleType)
        {
            this.associations.Add(association.ObjectId);

            this.RoleTypes(association.ObjectId).Add(roleType);
        }

        public void OnChangingCompositeRole(Strategy association, IRoleType roleType, Strategy previousRole, Strategy newRole)
        {
            this.associations.Add(association.ObjectId);

            if (previousRole != null)
            {
                this.roles.Add(previousRole.ObjectId);
            }

            if (newRole != null)
            {
                this.roles.Add(newRole.ObjectId);
            }

            this.RoleTypes(association.ObjectId).Add(roleType);
        }

        public void OnChangingCompositesRole(Strategy association, IRoleType roleType, Strategy changedRole)
        {
            this.associations.Add(association.ObjectId);

            if (changedRole != null)
            {
                this.roles.Add(changedRole.ObjectId);
            }

            this.RoleTypes(association.ObjectId).Add(roleType);
        }

        public void OnChangingCompositesRole(Strategy association, IRoleType roleType, HashSet<Strategy> previousRoles)
        {
            this.associations.Add(association.ObjectId);

            if (previousRoles != null)
            {
                foreach (var role in previousRoles)
                {
                    this.roles.Add(role.ObjectId);
                }
            }

            this.RoleTypes(association.ObjectId).Add(roleType);
        }

        private ISet<IRoleType> RoleTypes(ObjectId associationId)
        {
            ISet<IRoleType> roleTypes;
            if (!this.RoleTypesByAssociation.TryGetValue(associationId, out roleTypes))
            {
                roleTypes = new HashSet<IRoleType>();
                this.RoleTypesByAssociation[associationId] = roleTypes;
            }

            return roleTypes;
        }
    }
}