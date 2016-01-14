// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheFactory.cs" company="Allors bvba">
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
    using Allors.Meta;

    /// <summary>
    /// Factory for default cache.
    /// </summary>
    public abstract class CacheFactory : ICacheFactory
    {
        public IClass[] ExcludedClasses { get; set; } 

        public ICache CreateCache()
        {
            return this.CreateCache(this.ExcludedClasses);
        }

        protected abstract ICache CreateCache(IClass[] excludedClasses);
    }
}