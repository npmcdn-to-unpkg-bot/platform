// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reference.cs" company="Allors bvba">
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

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Allors.Meta;

    internal class Reference
    {
        internal const int UnknownCacheId = int.MaxValue;
        internal const int InitialCacheId = int.MaxValue - 1;

        private static readonly int MaskIsNew = BitVector32.CreateMask();
        private static readonly int MaskExists = BitVector32.CreateMask(MaskIsNew);
        private static readonly int MaskExistsKnown = BitVector32.CreateMask(MaskExists);

        private readonly DatabaseSession session;
        private readonly IClass @class;
        private readonly ObjectId objectId;
        private int cacheId;

        private BitVector32 flags;

        private WeakReference weakReference;

        internal Reference(DatabaseSession session, IClass @class, ObjectId objectId, bool isNew)
        {
            this.session = session;
            this.@class = @class;
            this.objectId = objectId;

            this.flags[MaskIsNew] = isNew;
            if (isNew)
            {
                this.flags[MaskExistsKnown] = true;
                this.flags[MaskExists] = true;
            }
        }

        internal Reference(DatabaseSession session, IClass @class, ObjectId objectId, int cacheId)
            : this(session, @class, objectId, false)
        {
            this.cacheId = cacheId;
            this.flags[MaskExistsKnown] = true;
            this.flags[MaskExists] = true;
        }

        internal virtual Strategy Strategy
        {
            get
            {
                var strategy = this.Target;

                if (strategy == null)
                {
                    strategy = this.CreateStrategy();
                    this.weakReference = new WeakReference(strategy);
                }

                return strategy;
            }
        }

        internal DatabaseSession Session
        {
            get
            {
                return this.session;
            }
        }

        internal IClass Class
        {
            get
            {
                return this.@class;
            }
        }

        internal ObjectId ObjectId
        {
            get
            {
                return this.objectId;
            }
        }

        internal int CacheId
        {
            get
            {
                if (!this.IsNew && this.cacheId == UnknownCacheId)
                {
                    this.Session.AddReferenceWithoutCacheIdOrExistsKnown(this);
                    this.Session.GetCacheIdsAndExists();
                }

                return this.cacheId;
            }

            set
            {
                this.cacheId = value;
            }
        }

        internal bool IsNew
        {
            get
            {
                return this.flags[MaskIsNew];
            }
        }

        internal bool IsUnknownCacheId
        {
            get
            {
                var isUnknown = this.cacheId == UnknownCacheId; 
                return isUnknown;
            }
        }

        internal bool Exists
        {
            get
            {
                var flagsExistsKnown = this.flags[MaskExistsKnown];
                if (!flagsExistsKnown)
                {
                    this.Session.AddReferenceWithoutCacheIdOrExistsKnown(this);
                    this.Session.GetCacheIdsAndExists();
                }

                return this.flags[MaskExists];
            }

            set
            {
                this.flags[MaskExistsKnown] = true;
                this.flags[MaskExists] = value;
            }
        }
        
        internal bool ExistsKnown
        {
            get
            {
                var existsKnown = this.flags[MaskExistsKnown];
                return existsKnown;
            }
        }

        internal Strategy Target
        {
            get
            {
                return (this.weakReference == null) ? null : (Strategy)this.weakReference.Target;
            }
        }

        public override int GetHashCode()
        {
            return this.objectId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var that = (Reference)obj;
            return that != null && that.objectId.Equals(this.objectId);
        }

        public override string ToString()
        {
            return "[" + this.@class + ":" + this.ObjectId + "]";
        }

        internal virtual void Commit(HashSet<Reference> referencesWithStrategy)
        {
            this.flags[MaskExistsKnown] = false;
            this.flags[MaskIsNew] = false;
            this.cacheId = UnknownCacheId;

            var strategy = this.Target;
            if (strategy != null)
            {
                referencesWithStrategy.Add(this);
                strategy.Release();
            }
        }

        internal virtual void Rollback(HashSet<Reference> referencesWithStrategy)
        {
            if (this.flags[MaskIsNew])
            {
                this.flags[MaskExistsKnown] = true;
                this.flags[MaskExists] = false;
            }
            else
            {
                this.flags[MaskExistsKnown] = false;
            }

            this.cacheId = UnknownCacheId;

            var strategy = this.Target;
            if (strategy != null)
            {
                referencesWithStrategy.Add(this);
                strategy.Release();
            }
        }
      
        protected virtual Strategy CreateStrategy()
        {
            return new Strategy(this);
        }
    }
}