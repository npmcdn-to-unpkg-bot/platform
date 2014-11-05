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
        private Dictionary<IRoleType, Dictionary<ObjectId, Entry>> entryByAssociationByRoleType;

        public RoleCache()
        {
            this.entryByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, Entry>>();
        }

        public bool TryGet(ObjectId association, object cacheId, IRoleType roleType, out object role)
        {
            Dictionary<ObjectId, Entry> entryByAssociation;
            if (this.entryByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                Entry entry;
                if (entryByAssociation.TryGetValue(association, out entry))
                {
                    if (entry.CacheId.Equals(cacheId))
                    {
                        role = entry.Role;
                        return true;
                    }
                }
            }

            role = null;
            return false;
        }

        public void Set(ObjectId association, object cacheId, IRoleType roleType, object role)
        {
            Dictionary<ObjectId, Entry> entryByAssociation;
            if (!this.entryByAssociationByRoleType.TryGetValue(roleType, out entryByAssociation))
            {
                entryByAssociation = new Dictionary<ObjectId, Entry>();
                this.entryByAssociationByRoleType[roleType] = entryByAssociation;
            }

            entryByAssociation[association] = new Entry(cacheId, role);
        }

        public void Invalidate()
        {
            this.entryByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, Entry>>();
        }

        private class Entry
        {
            private readonly object cacheId;
            private readonly object role; 

            internal Entry(object cacheId, object role)
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
    }
}