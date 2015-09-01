// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="Allors bvba">
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

using Allors;

namespace Allors.Adapters.Object.SqlClient.Caching
{
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// The Cache holds a CachedObject and/or IObjectType by ObjectId.
    /// </summary>
    public interface ICache
    {
        ICachedObject GetOrCreateCachedObject(IClass concreteClass, ObjectId objectId, long localCacheId);

        IClass GetObjectType(ObjectId objectId);

        void SetObjectType(ObjectId objectId, IClass objectType);

        void OnCommit(IList<ObjectId> accessedObjectIds, IList<ObjectId> changedObjectIds);

        void OnRollback(IList<ObjectId> accessedObjectIds);

        /// <summary>
        /// Invalidates the Cache.
        /// </summary>
        void Invalidate();
    }
}