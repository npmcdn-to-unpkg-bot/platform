// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cache.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using Allors;
    using Allors.Meta;
    using System;
    using System.Collections.Generic;

    public class Cache<TKey, TObject>
        where TObject : class, IObject
    {
        private readonly string cacheKey = "Allors.Cache." + typeof(TObject);

        private readonly IWorkspaceSession workspaceSession;
        private readonly IDatabaseSession databaseSession;
        private readonly IDatabase database;
        private readonly RoleType roleType;

        private Dictionary<TKey, ObjectId> databaseSessionCache;
        private Dictionary<TKey, ObjectId> databaseCache;
        private Dictionary<TKey, ObjectId> workspaceSessionCache;

        public Cache(ISession session, RoleType roleType)
        {
            if (!roleType.ObjectType.IsUnit)
            {
                throw new ArgumentException("ObjectType of RoleType should be a Unit");
            }

            this.roleType = roleType;

            this.workspaceSession = session as IWorkspaceSession;
            if (this.workspaceSession == null)
            {
                this.databaseSession = (IDatabaseSession)session;
            }
            else
            {
                this.databaseSession = this.workspaceSession.DatabaseSession;
            }

            this.database = this.databaseSession.Database;
        }

        public TObject this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
        }

        public void Prefetch()
        {
            Extent<TObject> prefetchedObjects = this.databaseSession.Extent<TObject>();
            prefetchedObjects.Filter.AddExists(this.roleType);

            foreach (TObject prefetchedObject in prefetchedObjects)
            {
                this.Add(prefetchedObject);
            }
        }

        public TObject Get(TKey key)
        {
            this.LazyLoadDatabaseSessionCache();

            ObjectId cachedObjectId;
            if (!this.databaseSessionCache.TryGetValue(key, out cachedObjectId))
            {
                this.LazyLoadDatabaseCache();

                if (!this.databaseCache.TryGetValue(key, out cachedObjectId))
                {
                    if (this.workspaceSession != null)
                    {
                        this.LazyLoadWorkspaceSessionCache();

                        if (!this.workspaceSessionCache.TryGetValue(key, out cachedObjectId))
                        {
                            var objectType = (Composite)this.database.ObjectFactory.GetObjectTypeForType(typeof(TObject));
                            foreach (TObject workspaceObject in this.workspaceSession.LocalExtent(objectType))
                            {
                                var role = workspaceObject.Strategy.GetUnitRole(this.roleType);
                                if (Equals(key, role))
                                {
                                    cachedObjectId = workspaceObject.Id;

                                    if (!workspaceObject.Strategy.IsNewInWorkspace)
                                    {
                                        this.workspaceSessionCache[key] = cachedObjectId;
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    if (cachedObjectId == null)
                    {
                        Extent<TObject> extent = this.databaseSession.Extent<TObject>();
                        extent.Filter.AddEquals(this.roleType, key);

                        var databaseObject = extent.First;

                        if (databaseObject != null)
                        {
                            cachedObjectId = databaseObject.Id;

                            this.databaseSessionCache[key] = databaseObject.Id;
                            if (!databaseObject.Strategy.IsNewInSession)
                            {
                                this.databaseCache[key] = databaseObject.Id;
                            }
                        }
                    }
                }
            }

            if (cachedObjectId != null)
            {
                if (this.workspaceSession != null)
                {
                    return (TObject)this.workspaceSession.Instantiate(cachedObjectId);
                }

                return (TObject)this.databaseSession.Instantiate(cachedObjectId);
            }

            return null;
        }

        public void Add(TObject cachedObject)
        {
            if (this.workspaceSession == null && cachedObject != null)
            {
                if (cachedObject.Strategy.ExistUnitRole(this.roleType))
                {
                    var key = (TKey)cachedObject.Strategy.GetUnitRole(this.roleType);

                    this.LazyLoadDatabaseSessionCache();

                    this.databaseSessionCache[key] = cachedObject.Id;

                    if (!cachedObject.Strategy.IsNewInSession)
                    {
                        this.LazyLoadDatabaseCache();
                        this.databaseCache[key] = cachedObject.Id;
                    }
                }
            }
        }

        private void LazyLoadDatabaseSessionCache()
        {
            if (this.databaseSessionCache == null)
            {
                this.databaseSessionCache = (Dictionary<TKey, ObjectId>)this.databaseSession[this.cacheKey];
                if (this.databaseSessionCache == null)
                {
                    this.databaseSession[this.cacheKey] = new Dictionary<TKey, ObjectId>();
                    this.databaseSessionCache = (Dictionary<TKey, ObjectId>)this.databaseSession[this.cacheKey];
                }
            }
        }

        private void LazyLoadDatabaseCache()
        {
            if (this.databaseCache == null)
            {
                this.databaseCache = (Dictionary<TKey, ObjectId>)this.database[this.cacheKey];
                if (this.databaseCache == null)
                {
                    this.database[this.cacheKey] = new Dictionary<TKey, ObjectId>();
                    this.databaseCache = (Dictionary<TKey, ObjectId>)this.database[this.cacheKey];
                }
            }
        }

        private void LazyLoadWorkspaceSessionCache()
        {
            if (this.workspaceSessionCache == null)
            {
                this.workspaceSessionCache = (Dictionary<TKey, ObjectId>)this.workspaceSession[this.cacheKey];
                if (this.workspaceSessionCache == null)
                {
                    this.workspaceSession[this.cacheKey] = new Dictionary<TKey, ObjectId>();
                    this.workspaceSessionCache = (Dictionary<TKey, ObjectId>)this.workspaceSession[this.cacheKey];
                }
            }
        }
    }
}