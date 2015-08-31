// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlExtent.cs" company="Allors bvba">
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
using Allors.Meta;

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Meta;

    internal abstract class SqlExtent : Extent
    {
        private IList<ObjectId> objectIds;

        private ExtentSort sorter;

        internal override SqlExtent ContainedInExtent
        {
            get
            {
                return this;
            }
        }

        public override int Count
        {
            get { return this.ObjectIds.Count; }
        }

        public override IObject First
        {
            get
            {
                if (this.ObjectIds.Count > 0)
                {
                    return this.Session.GetOrCreateReferenceForExistingObject(this.ObjectIds[0]).Strategy.GetObject();
                }

                return null;
            }
        }

        internal virtual ExtentOperation ParentOperationExtent { get; set; }

        internal abstract Session Session { get; }

        internal virtual ExtentSort Sorter
        {
            get { return this.sorter; }
        }

        private IList<ObjectId> ObjectIds
        {
            get
            {
                return this.objectIds ?? (this.objectIds = this.GetObjectIds());
            }
        }

        internal new IObject this[int index]
        {
            get { return this.GetItem(index); }
        }

        public override Allors.Extent AddSort(IRoleType roleType)
        {
            return this.AddSort(roleType, SortDirection.Ascending);
        }

        public override Allors.Extent AddSort(IRoleType roleType, SortDirection direction)
        {
            this.LazyLoadFilter();
            this.FlushCache();
            if (this.sorter == null)
            {
                this.sorter = new ExtentSort(this.Session, roleType, direction);
            }
            else
            {
                this.sorter.AddSort(roleType, direction);
            }

            return this;
        }

        public override bool Contains(object value)
        {
            if (value != null)
            {
                return this.ObjectIds.Contains(((IObject)value).Id);
            }

            return false;
        }

        public override void CopyTo(Array array, int index)
        {
            this.ToArray().CopyTo(array, index);
        }

        public override IEnumerator GetEnumerator()
        {
            var references = this.Session.GetOrCreateReferencesForExistingObjects(this.ObjectIds);
            return new ExtentEnumerator(references);
        }

        public override int IndexOf(object value)
        {
            if (value != null)
            {
                return this.ObjectIds.IndexOf(((IObject)value).Id);
            }

            return -1;
        }

        public override IObject[] ToArray()
        {
            var clrType = this.Session.Database.GetDomainType(this.ObjectType);
            return this.ToArray(clrType);
        }

        public override IObject[] ToArray(Type type)
        {
            var array = Array.CreateInstance(type, this.ObjectIds.Count);
            for (var i = 0; i < this.ObjectIds.Count; i++)
            {
                var allorsObject = this.Session.GetOrCreateReferenceForExistingObject(this.ObjectIds[i]).Strategy.GetObject();
                array.SetValue(allorsObject, i);
            }

            return (IObject[])array;
        }

        internal abstract string BuildSql(ExtentStatement statement);

        internal virtual void FlushCache()
        {
            this.objectIds = null;
            if (this.ParentOperationExtent != null)
            {
                this.ParentOperationExtent.FlushCache();
            }
        }

        internal IObject InternalGetItem(int index)
        {
            return this.GetItem(index);
        }

        protected override IObject GetItem(int index)
        {
            var objectId = this.ObjectIds[index];
            return this.Session.GetOrCreateReferenceForExistingObject(objectId).Strategy.GetObject();
        }

        protected abstract IList<ObjectId> GetObjectIds();

        protected abstract void LazyLoadFilter();
    }
}