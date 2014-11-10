// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleCache.cs" company="Allors bvba">
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

namespace Allors.Databases
{
    using System.Collections.Generic;
    using Allors.Meta;

    public class RoleCache : IRoleCache
    {
        private Dictionary<IRoleType, Dictionary<ObjectId, CachedUnitRole>> cachedUnitRoleByAssociationByRoleType;

        private Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositeRole>> cachedCompositeRoleByAssociationByRoleType;

        private Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositesRole>> cachedCompositesRoleByAssociationByRoleType;

        public RoleCache()
        {
            this.cachedUnitRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedUnitRole>>();
            this.cachedCompositeRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositeRole>>();
            this.cachedCompositesRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositesRole>>();
        }

        public bool TryGetUnit(ObjectId association, object cacheId, IRoleType roleType, out object role)
        {
            Dictionary<ObjectId, CachedUnitRole> entryByAssociation;
            if (this.cachedUnitRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                CachedUnitRole cachedUnitRole;
                if (entryByAssociation.TryGetValue(association, out cachedUnitRole))
                {
                    if (cachedUnitRole.CacheId.Equals(cacheId))
                    {
                        role = cachedUnitRole.Role;
                        return true;
                    }
                }
            }

            role = null;
            return false;
        }

        public void SetUnit(ObjectId association, object cacheId, IRoleType roleType, object role)
        {
            Dictionary<ObjectId, CachedUnitRole> entryByAssociation;
            if (!this.cachedUnitRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                entryByAssociation = new Dictionary<ObjectId, CachedUnitRole>();
                this.cachedUnitRoleByAssociationByRoleType[roleType] = entryByAssociation;
            }

            entryByAssociation[association] = new CachedUnitRole(cacheId, role);
        }

        public bool TryGetComposite(ObjectId association, object cacheId, IRoleType roleType, out ObjectId role)
        {
            Dictionary<ObjectId, CachedCompositeRole> entryByAssociation;
            if (this.cachedCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                CachedCompositeRole cachedCompositeRole;
                if (entryByAssociation.TryGetValue(association, out cachedCompositeRole))
                {
                    if (cachedCompositeRole.CacheId.Equals(cacheId))
                    {
                        role = cachedCompositeRole.Role;
                        return true;
                    }
                }
            }

            role = null;
            return false;
        }

        public void SetComposite(ObjectId association, object cacheId, IRoleType roleType, ObjectId role)
        {
            Dictionary<ObjectId, CachedCompositeRole> entryByAssociation;
            if (!this.cachedCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                entryByAssociation = new Dictionary<ObjectId, CachedCompositeRole>();
                this.cachedCompositeRoleByAssociationByRoleType[roleType] = entryByAssociation;
            }

            entryByAssociation[association] = new CachedCompositeRole(cacheId, role);
        }

        public bool TryGetComposites(ObjectId association, object cacheId, IRoleType roleType, out ObjectId[] role)
        {
            Dictionary<ObjectId, CachedCompositesRole> entryByAssociation;
            if (this.cachedCompositesRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                CachedCompositesRole cachedCompositesRole;
                if (entryByAssociation.TryGetValue(association, out cachedCompositesRole))
                {
                    if (cachedCompositesRole.CacheId.Equals(cacheId))
                    {
                        role = cachedCompositesRole.Role;
                        return true;
                    }
                }
            }

            role = null;
            return false;
        }

        public void SetComposites(ObjectId association, object cacheId, IRoleType roleType, ObjectId[] role)
        {
            Dictionary<ObjectId, CachedCompositesRole> entryByAssociation;
            if (!this.cachedCompositesRoleByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                entryByAssociation = new Dictionary<ObjectId, CachedCompositesRole>();
                this.cachedCompositesRoleByAssociationByRoleType[roleType] = entryByAssociation;
            }

            entryByAssociation[association] = new CachedCompositesRole(cacheId, role);
        }

        public void Invalidate()
        {
            this.cachedUnitRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedUnitRole>>();
            this.cachedCompositeRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositeRole>>();
            this.cachedCompositesRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, CachedCompositesRole>>();
        }

        public void Invalidate(ObjectId[] objectsToInvalidate)
        {
            foreach (var roleByAssociationEntry in this.cachedCompositeRoleByAssociationByRoleType)
            {
                var roleByAssociation = roleByAssociationEntry.Value;
                foreach (var objectToInvalidate in objectsToInvalidate)
                {
                    roleByAssociation.Remove(objectToInvalidate);
                }
            }

            foreach (var roleByAssociationEntry in this.cachedCompositeRoleByAssociationByRoleType)
            {
                var roleByAssociation = roleByAssociationEntry.Value;
                foreach (var objectToInvalidate in objectsToInvalidate)
                {
                    roleByAssociation.Remove(objectToInvalidate);
                }
            }

            foreach (var roleByAssociationEntry in this.cachedCompositesRoleByAssociationByRoleType)
            {
                var roleByAssociation = roleByAssociationEntry.Value;
                foreach (var objectToInvalidate in objectsToInvalidate)
                {
                    roleByAssociation.Remove(objectToInvalidate);
                }
            }
        }

        private class CachedUnitRole
        {
            private readonly object cacheId;
            private readonly object role; 

            internal CachedUnitRole(object cacheId, object role)
            {
                this.cacheId = cacheId;
                this.role = role;
            }

            public object CacheId
            {
                get
                {
                    return this.cacheId;
                }
            }

            public object Role
            {
                get
                {
                    return this.role;
                }
            }
        }

        private class CachedCompositeRole
        {
            private readonly object cacheId;
            private readonly ObjectId role;

            internal CachedCompositeRole(object cacheId, ObjectId role)
            {
                this.cacheId = cacheId;
                this.role = role;
            }

            public object CacheId
            {
                get
                {
                    return this.cacheId;
                }
            }

            public ObjectId Role
            {
                get
                {
                    return this.role;
                }
            }
        }

        private class CachedCompositesRole
        {
            private readonly object cacheId;
            private readonly ObjectId[] role;

            internal CachedCompositesRole(object cacheId, ObjectId[] role)
            {
                this.cacheId = cacheId;
                this.role = role;
            }

            public object CacheId
            {
                get
                {
                    return this.cacheId;
                }
            }

            public ObjectId[] Role
            {
                get
                {
                    return this.role;
                }
            }
        }
    }
}