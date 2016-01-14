// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CachedObject.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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

namespace Allors.Adapters.Object.SqlClient.Caching
{
    using System.Collections.Concurrent;

    using Allors.Meta;

    public abstract class CachedObject : ICachedObject
    {
        private readonly long localCacheVersion;

        private readonly ConcurrentDictionary<IRoleType, object> roleByRoleType;

        internal CachedObject(long localCacheVersion)
        {
            this.localCacheVersion = localCacheVersion;
            this.roleByRoleType = new ConcurrentDictionary<IRoleType, object>();
        }

        public long LocalCacheVersion
        {
            get
            {
                return this.localCacheVersion;
            }
        }

        public bool TryGetValue(IRoleType roleType, out object value)
        {
            return this.roleByRoleType.TryGetValue(roleType, out value);
        }

        public void SetValue(IRoleType roleType, object value)
        {
            this.roleByRoleType[roleType] = value;
        }
    }
}