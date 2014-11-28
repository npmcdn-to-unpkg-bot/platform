// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeRoles.cs" company="Allors bvba">
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
    using System.Collections.Generic;

    using Allors.Meta;

    public class CompositeRoles
    {
        private readonly HashSet<ObjectId> baseline;
        private HashSet<ObjectId> original; 
        private HashSet<ObjectId> added;
        private HashSet<ObjectId> removed;

        public CompositeRoles(IEnumerable<ObjectId> compositeRoles)
        {
            this.baseline = new HashSet<ObjectId>(compositeRoles);
        }

        public HashSet<ObjectId> ObjectIds
        {
            get
            {
                if ((this.removed == null || this.removed.Count == 0) && (this.added == null || this.added.Count == 0))
                {
                    return this.baseline;
                }

                var merged = new HashSet<ObjectId>(this.baseline);
                if (this.removed != null && this.removed.Count > 0)
                {
                    merged.ExceptWith(this.removed);
                }

                if (this.added != null && this.added.Count > 0)
                {
                    merged.UnionWith(this.added);
                }

                return merged;
            }
        }

        public int Count
        {
            get
            {
                var addedCount = this.added == null ? 0 : this.added.Count;
                var removedCount = this.removed == null ? 0 : this.removed.Count;

                return this.baseline.Count + addedCount - removedCount;
            }
        }

        public ObjectId First
        {
            get
            {
                if (this.removed == null || this.removed.Count == 0)
                {
                    if (this.baseline.Count > 0)
                    {
                        return this.GetFirst(this.baseline);
                    }

                    if (this.added != null && this.added.Count > 0)
                    {
                        return this.GetFirst(this.added);
                    }

                    return null;
                }

                var roles = this.ObjectIds;
                if (roles.Count > 0)
                {
                    return this.GetFirst(roles);
                }

                return null;
            }
        }

        public bool Contains(ObjectId objectId)
        {
            if (this.removed != null && this.removed.Contains(objectId))
            {
                return false;
            }
                
            return this.baseline.Contains(objectId) || (this.added != null && this.added.Contains(objectId));
        }

        public void Add(ObjectId objectId)
        {
            if (this.original == null)
            {
                this.original = new HashSet<ObjectId>(this.baseline);
            }

            if (this.removed != null && this.removed.Contains(objectId))
            {
                this.removed.Remove(objectId);
                return;
            }

            if (!this.baseline.Contains(objectId))
            {
                if (this.added == null)
                {
                    this.added = new HashSet<ObjectId>();
                }

                this.added.Add(objectId);
            }
        }

        public void Remove(ObjectId objectId)
        {
            if (this.original == null)
            {
                this.original = new HashSet<ObjectId>(this.baseline);
            }

            if (this.added != null && this.added.Contains(objectId))
            {
                this.added.Remove(objectId);
                return;
            }

            if (this.baseline.Contains(objectId))
            {
                if (this.removed == null)
                {
                    this.removed = new HashSet<ObjectId>();
                }

                this.removed.Add(objectId);
            }
        }

        public void Flush(Flush flush, Roles roles, IRoleType roleType)
        {
            if (this.Count == 0)
            {
                flush.ClearCompositeAndCompositesRole(roles.Reference, roleType);
            }
            else
            {
                if (this.added != null && this.added.Count > 0)
                {
                    flush.AddCompositeRole(roles.Reference, roleType, this.added);
                }

                if (this.removed != null && this.removed.Count > 0)
                {
                    flush.RemoveCompositeRole(roles.Reference, roleType, this.removed);
                }
            }

            if (this.added != null)
            {
                this.baseline.UnionWith(this.added);
            }

            if (this.removed != null)
            {
                this.baseline.ExceptWith(this.removed);
            }

            this.added = null;
            this.removed = null;
        }

        private ObjectId GetFirst(HashSet<ObjectId> hashSet)
        {
            var enumerator = hashSet.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }
    }
}