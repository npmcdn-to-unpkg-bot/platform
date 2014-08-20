// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cache.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Caching
{
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// The Cache holds a CachedObject and/or ObjectType by ObjectId.
    /// </summary>
    public sealed class Cache : ICache
    {
        private readonly HashSet<ObjectType> transientConcreteClasses; 

        private readonly Dictionary<ObjectId, CachedObject> cachedObjectByObjectId;
        private readonly Dictionary<ObjectId, ObjectType> objectTypeByObjectId;

        public Cache(ObjectType[] transientObjectTypes)
        {
            this.cachedObjectByObjectId = new Dictionary<ObjectId, CachedObject>();
            this.objectTypeByObjectId = new Dictionary<ObjectId, ObjectType>();

            if (transientObjectTypes != null)
            {
                this.transientConcreteClasses = new HashSet<ObjectType>();
                foreach (var transientObjectType in transientObjectTypes)
                {
                    foreach (var transientConcreteClass in transientObjectType.ConcreteClasses)
                    {
                        this.transientConcreteClasses.Add(transientConcreteClass);
                    }
                }

                if (this.transientConcreteClasses.Count == 0)
                {
                    this.transientConcreteClasses = null;
                }
            }
        }

        /// <summary>
        /// Invalidates the Cache.
        /// </summary>
        public void Invalidate()
        {
            this.cachedObjectByObjectId.Clear();
            this.objectTypeByObjectId.Clear();
        }

        public ICachedObject GetOrCreateCachedObject(ObjectType concreteClass, ObjectId objectId, int localCacheId)
        {
            if (this.transientConcreteClasses != null && this.transientConcreteClasses.Contains(concreteClass))
            {
                return new CachedObject(localCacheId); 
            }

            CachedObject cachedObject;
            if (this.cachedObjectByObjectId.TryGetValue(objectId, out cachedObject))
            {
                if (!cachedObject.LocalCacheVersion.Equals(localCacheId))
                {
                    cachedObject = new CachedObject(localCacheId);
                    this.cachedObjectByObjectId[objectId] = cachedObject;
                }
            }
            else
            {
                cachedObject = new CachedObject(localCacheId);
                this.cachedObjectByObjectId[objectId] = cachedObject;
            }

            return cachedObject;
        }

        public ObjectType GetObjectType(ObjectId objectId)
        {
            ObjectType objectType;
            this.objectTypeByObjectId.TryGetValue(objectId, out objectType);
            return objectType;
        }

        public void SetObjectType(ObjectId objectId, ObjectType objectType)
        {
            this.objectTypeByObjectId[objectId] = objectType;
        }

        public void OnCommit(IList<ObjectId> accessedObjectIds, IList<ObjectId> changedObjectIds)
        {
            if (changedObjectIds.Count > 0)
            {
                foreach (var changedObjectId in changedObjectIds)
                {
                    this.cachedObjectByObjectId.Remove(changedObjectId);
                }
            }
        }

        public void OnRollback(IList<ObjectId> accessedObjectIds)
        {
        }
    }
}