namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections;

    using Allors.Meta;

    public class ObjectIdExtent : Extent
    {
        private readonly DatabaseSession sesison;
        private readonly ObjectId[] objectIds;

        public ObjectIdExtent(DatabaseSession sesison, ObjectId[] objectIds)
        {
            this.sesison = sesison;
            this.objectIds = objectIds;
        }

        public override void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public override IObject[] ToArray()
        {
            throw new NotImplementedException();
        }

        public override IObject[] ToArray(Type type)
        {
            throw new NotImplementedException();
        }

        protected override IObject GetItem(int index)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ICompositePredicate Filter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IObject First
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IComposite ObjectType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Extent AddSort(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public override Extent AddSort(IRoleType roleType, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(object value)
        {
            throw new NotImplementedException();
        }
    }
}