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

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Allors.Meta;

    public class Reference
    {
        public const int UnknownCacheId = int.MaxValue;
        public const int InitialCacheId = int.MaxValue - 1;

        private static readonly int MaskIsNew = BitVector32.CreateMask();
        private static readonly int MaskExists = BitVector32.CreateMask(MaskIsNew);
        private static readonly int MaskExistsKnown = BitVector32.CreateMask(MaskExists);

        private readonly DatabaseSession session;
        private readonly IClass objectType;
        private readonly ObjectId objectId;
        private int cacheId;

        private BitVector32 flags;

        private WeakReference weakReference;

        public Reference(DatabaseSession session, IClass objectType, ObjectId objectId, bool isNew)
        {
            this.session = session;
            this.objectType = objectType;
            this.objectId = objectId;

            this.flags[MaskIsNew] = isNew;
            if (isNew)
            {
                this.flags[MaskExistsKnown] = true;
                this.flags[MaskExists] = true;
            }
        }

        public Reference(DatabaseSession session, IClass objectType, ObjectId objectId, int cacheId)
            : this(session, objectType, objectId, false)
        {
            this.cacheId = cacheId;
            this.flags[MaskExistsKnown] = true;
            this.flags[MaskExists] = true;
        }

        public virtual Strategy Strategy
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

        public DatabaseSession Session
        {
            get
            {
                return this.session;
            }
        }

        public IClass ObjectType
        {
            get
            {
                return this.objectType;
            }
        }

        public ObjectId ObjectId
        {
            get
            {
                return this.objectId;
            }
        }

        public int CacheId
        {
            get
            {
                if (!this.IsNew && this.cacheId == UnknownCacheId)
                {
                    this.Session.GetCacheIdsAndExists();
                }

                return this.cacheId;
            }

            set
            {
                this.cacheId = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.flags[MaskIsNew];
            }
        }

        public bool Exists
        {
            get
            {
                var flagsExistsKnown = this.flags[MaskExistsKnown];
                if (!flagsExistsKnown)
                {
                    this.Session.AddReferenceWithoutCacheId(this);
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

        public Strategy Target
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
            return "[" + this.objectType + ":" + this.ObjectId + "]";
        }

        public virtual void Commit(HashSet<Reference> referencesWithStrategy)
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

        public virtual void Rollback(HashSet<Reference> referencesWithStrategy)
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