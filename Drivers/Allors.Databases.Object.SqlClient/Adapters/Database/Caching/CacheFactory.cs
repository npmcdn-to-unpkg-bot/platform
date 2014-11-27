// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheFactory.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.Caching
{
    using Allors.R1.Meta;

    /// <summary>
    /// Factory for default cache.
    /// </summary>
    public sealed class CacheFactory : ICacheFactory
    {
        public ObjectType[] TransientObjectTypes { get; set; } 

        public ICache CreateCache(IDatabase database)
        {
            return new Cache(this.TransientObjectTypes);
        }
    }
}