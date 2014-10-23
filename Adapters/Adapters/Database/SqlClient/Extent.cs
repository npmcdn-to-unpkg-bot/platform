//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsExtentSql.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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
// <summary>Defines the AllorsExtentSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections;

    using Allors.Meta;

    public abstract class AllorsExtentSql : Extent
    {
        private ArrayList objects;
        private AllorsExtentOperationSql parentOperationExtent;
        private AllorsExtentSortSql sorter;

        public override int Count
        {
            get { return this.Objects.Count; }
        }

        public override IObject First
        {
            get
            {
                if (this.Objects.Count > 0)
                {
                    return (IObject)this.Objects[0];
                }

                return null;
            }
        }

        private ArrayList Objects
        {
            get
            {
                return this.objects ?? (this.objects = this.GetObjects());
            }
        }

        internal AllorsExtentOperationSql ParentOperationExtent
        {
            get { return this.parentOperationExtent; }
            set { this.parentOperationExtent = value; }
        }

        internal abstract DatabaseSession Session { get; }

        internal AllorsExtentSortSql Sorter
        {
            get { return this.sorter; }
        }

        public new IObject this[int index]
        {
            get { return this.GetItem(index); }
        }

        public override Extent AddSort(IRoleType roleType)
        {
            return this.AddSort(roleType, SortDirection.Ascending);
        }

        public override Extent AddSort(IRoleType roleType, SortDirection sortDirection)
        {
            this.LazyLoadFilter(); // This will upgrade a strategy extent to a full extent
            this.FlushCache();
            if (this.sorter == null)
            {
                this.sorter = new AllorsExtentSortSql(this.Session, roleType, sortDirection);
            }
            else
            {
                this.sorter.AddSort(roleType, sortDirection);
            }

            return this;
        }

        public override bool Contains(object value)
        {
            return this.Objects.Contains(value);
        }

        public override void CopyTo(Array array, int index)
        {
            this.Objects.CopyTo(array, index);
        }

        public override IEnumerator GetEnumerator()
        {
            return this.Objects.GetEnumerator();
        }

        public override int IndexOf(object value)
        {
            return this.Objects.IndexOf(value);
        }

        public override IObject[] ToArray()
        {
            Type clrType = this.Session.Database.ObjectFactory.GetTypeForObjectType(this.ObjectType);
            return (IObject[])this.Objects.ToArray(clrType);
        }

        public override IObject[] ToArray(Type type)
        {
            return (IObject[])this.Objects.ToArray(type);
        }

        protected override IObject GetItem(int index)
        {
            return (IObject)this.Objects[index];
        }

        protected abstract ArrayList GetObjects();

        protected abstract void LazyLoadFilter();

        internal abstract string BuildSql(AllorsExtentStatementSql statement);

        internal void FlushCache()
        {
            this.objects = null;
            if (this.ParentOperationExtent != null)
            {
                this.ParentOperationExtent.FlushCache();
            }
        }
    }
}