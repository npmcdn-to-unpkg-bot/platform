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

using Allors;

namespace Allors.Adapters.Object.SqlClient.Caching
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// The Cache holds a CachedObject and/or IObjectType by ObjectId.
    /// </summary>
    public sealed class Cache : ICache
    {
        private readonly HashSet<IClass> transientConcreteClasses; 

        private readonly ConcurrentDictionary<ObjectId, CachedObject> cachedObjectByObjectId;
        private readonly ConcurrentDictionary<ObjectId, IClass> objectTypeByObjectId;

        public Cache(IComposite[] transientObjectTypes)
        {
            this.cachedObjectByObjectId = new ConcurrentDictionary<ObjectId, CachedObject>();
            this.objectTypeByObjectId = new ConcurrentDictionary<ObjectId, IClass>();

            if (transientObjectTypes != null)
            {
                this.transientConcreteClasses = new HashSet<IClass>();
                foreach (var transientObjectType in transientObjectTypes)
                {
                    foreach (var transientConcreteClass in transientObjectType.Classes)
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

        public ICachedObject GetOrCreateCachedObject(IClass concreteClass, ObjectId objectId, int localCacheId)
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

        public IClass GetObjectType(ObjectId objectId)
        {
            IClass objectType;
            this.objectTypeByObjectId.TryGetValue(objectId, out objectType);
            return objectType;
        }

        public void SetObjectType(ObjectId objectId, IClass objectType)
        {
            this.objectTypeByObjectId[objectId] = objectType;
        }

        public void OnCommit(IList<ObjectId> accessedObjectIds, IList<ObjectId> changedObjectIds)
        {
            if (changedObjectIds.Count > 0)
            {
                foreach (var changedObjectId in changedObjectIds)
                {
                    CachedObject removedObject;
                    this.cachedObjectByObjectId.TryRemove(changedObjectId, out removedObject);
                }
            }
        }

        public void OnRollback(IList<ObjectId> accessedObjectIds)
        {
        }
    }
}