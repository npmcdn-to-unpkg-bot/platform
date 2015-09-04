// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessControlCache.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//   // Dual Licensed under
//   //   a) the General Public Licence v3 (GPL)
//   //   b) the Allors License
//   // The GPL License is included in the file gpl.txt.
//   // The Allors License is an addendum to your contract.
//   // Allors Applications is distributed in the hope that it will be useful,
//   // but WITHOUT ANY WARRANTY; without even the implied warranty of
//   // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   // GNU General Public License for more details.
//   // For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   The access control cache.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Allors.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The access control cache.
    /// </summary>
    public class AccessControlCache
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        private static readonly string CacheKey = "Allors.Cache." + typeof(AccessControl);

        /// <summary>
        /// The entry by object id.
        /// </summary>
        private readonly Dictionary<ObjectId, Entry> entryByObjectId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlCache"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public AccessControlCache(ISession session)
        {
            var database = session.Database;
            this.entryByObjectId = (Dictionary<ObjectId, Entry>)database[CacheKey];
            if (this.entryByObjectId == null)
            {
                this.entryByObjectId = new Dictionary<ObjectId, Entry>();
                database[CacheKey] = this.entryByObjectId;
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="accessControl">
        /// The access control.
        /// </param>
        /// <returns>
        /// The <see cref="Entry"/>.
        /// </returns>
        public Entry this[AccessControl accessControl]
        {
            get
            {
                Entry entry;
                if(!this.entryByObjectId.TryGetValue(accessControl.Id, out entry) || entry.CacheId != accessControl.CacheId)
                {
                    entry = new Entry(accessControl);
                    this.entryByObjectId[accessControl.Id] = entry;
                }

                return entry;
            }
        }

        /// <summary>
        /// The entry.
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Entry"/> class.
            /// </summary>
            /// <param name="accessControl">
            /// The access control.
            /// </param>
            internal Entry(AccessControl accessControl)
            {
                this.CacheId = accessControl.CacheId;

                // TODO: Nested groups
                var users = accessControl.SubjectGroups.SelectMany(x => x.Members).ToList();
                users.AddRange(accessControl.Subjects);

                this.UserObjectIds = new HashSet<ObjectId>(users.Select(x => x.Id));
                this.RoleUniqueId = accessControl.Role.UniqueId;
            }

            /// <summary>
            /// Gets the cache id.
            /// </summary>
            public Guid CacheId
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the user object ids.
            /// </summary>
            public HashSet<ObjectId> UserObjectIds
            {
                get; private set;
            }

            /// <summary>
            /// Gets the role unique id.
            /// </summary>
            public Guid RoleUniqueId
            {
                get; private set;
            }
        }

    }
}