// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultCache.cs" company="Allors bvba">
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

namespace Allors.Adapters.Object.SqlClient.Caching
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// The Cache holds a CachedObject and/or IObjectType by ObjectId.
    /// </summary>
    public abstract class Cache : ICache
    {
        protected readonly HashSet<IClass> ExcludedClasses;

        protected readonly ConcurrentDictionary<ObjectId, CachedObject> CachedObjectByObjectId;
        protected readonly ConcurrentDictionary<ObjectId, IClass> ObjectTypeByObjectId;

        protected Cache(IClass[] excludedClasses)
        {
            this.CachedObjectByObjectId = new ConcurrentDictionary<ObjectId, CachedObject>();
            this.ObjectTypeByObjectId = new ConcurrentDictionary<ObjectId, IClass>();

            if (excludedClasses != null)
            {
                this.ExcludedClasses = new HashSet<IClass>();
                foreach (var transientObjectType in excludedClasses)
                {
                    foreach (var transientConcreteClass in transientObjectType.Classes)
                    {
                        this.ExcludedClasses.Add(transientConcreteClass);
                    }
                }

                if (this.ExcludedClasses.Count == 0)
                {
                    this.ExcludedClasses = null;
                }
            }
        }

        /// <summary>
        /// Invalidates the Cache.
        /// </summary>
        public void Invalidate()
        {
            this.CachedObjectByObjectId.Clear();
            this.ObjectTypeByObjectId.Clear();
        }

        public ICachedObject GetOrCreateCachedObject(IClass concreteClass, ObjectId objectId, long localCacheId)
        {
            if (this.ExcludedClasses != null && this.ExcludedClasses.Contains(concreteClass))
            {
                return this.CreateCachedObject(localCacheId);
            }

            CachedObject cachedObject;
            if (this.CachedObjectByObjectId.TryGetValue(objectId, out cachedObject))
            {
                if (!cachedObject.LocalCacheVersion.Equals(localCacheId))
                {
                    cachedObject = this.CreateCachedObject(localCacheId);
                    this.CachedObjectByObjectId[objectId] = cachedObject;
                }
            }
            else
            {
                cachedObject = this.CreateCachedObject(localCacheId);
                this.CachedObjectByObjectId[objectId] = cachedObject;
            }

            return cachedObject;
        }

        public IClass GetObjectType(ObjectId objectId)
        {
            IClass objectType;
            this.ObjectTypeByObjectId.TryGetValue(objectId, out objectType);
            return objectType;
        }

        public void SetObjectType(ObjectId objectId, IClass objectType)
        {
            this.ObjectTypeByObjectId[objectId] = objectType;
        }

        public void OnCommit(IList<ObjectId> accessedObjectIds, IList<ObjectId> changedObjectIds)
        {
            if (changedObjectIds.Count > 0)
            {
                foreach (var changedObjectId in changedObjectIds)
                {
                    CachedObject removedObject;
                    this.CachedObjectByObjectId.TryRemove(changedObjectId, out removedObject);
                }
            }
        }

        public void OnRollback(IList<ObjectId> accessedObjectIds)
        {
        }

        protected abstract CachedObject CreateCachedObject(long localCacheId);
    }
}