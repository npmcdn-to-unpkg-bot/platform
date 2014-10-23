//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsExtentOperationSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsExtentOperationSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections;
    using System.Data.Common;

    using Allors.Meta;

    public enum AllorsExtentOperationTypeSqlBundled
    {
        UNION,
        INTERSECT,
        EXCEPT
    }

    public class AllorsExtentOperationSql : AllorsExtentSql
    {
        protected readonly AllorsExtentSql first;
        protected readonly AllorsExtentOperationTypeSqlBundled operationType;
        protected readonly AllorsExtentSql second;

        internal AllorsExtentOperationSql(AllorsExtentSql first, AllorsExtentSql second, AllorsExtentOperationTypeSqlBundled operationType)
        {
            if (!first.ObjectType.Equals(second.ObjectType))
            {
                throw new ArgumentException("Both extents in a Union, Intersect or Except must be from the same type");
            }
            this.first = first;
            this.second = second;
            this.operationType = operationType;

            first.ParentOperationExtent = this;
            second.ParentOperationExtent = this;
        }

        public override ICompositePredicate Filter
        {
            get { return null; }
        }

        internal override DatabaseSession Session
        {
            get { return this.first.Session; }
        }

        public override IComposite ObjectType
        {
            get { return this.first.ObjectType; }
        }

        public override Extent AddSort(IRoleType roleType)
        {
            return this.AddSort(roleType, SortDirection.Ascending);
        }

        protected override ArrayList GetObjects()
        {
            this.first.Session.Flush();

            var statement = new AllorsExtentStatementRootSql(this);

            this.BuildSql(statement);

            if (statement.Sorter != null)
            {
                statement.Sorter.BuildOrder(this.Sorter, this.Session.Database.Schema, statement);
            }

            var objects = new ArrayList();
            using (var command = statement.CreateSqlCommand())
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectId = this.Session.Database.ObjectIds.Parse(reader.GetValue(0).ToString());
                        var typeId = command.GetUnique(reader, 1);
                        var instantiateObjectType = (IObjectType)this.Session.Database.ObjectFactory.MetaPopulation.Find(typeId);
                        objects.Add(this.Session.InstantiateExistingStrategy(instantiateObjectType, objectId).GetAllorsObject());
                    }

                    reader.Close();
                }
            }

            return objects;
        }

        //TODO: Refactor this
        protected override void LazyLoadFilter()
        {
        }

        internal override string BuildSql(AllorsExtentStatementSql statement)
        {
            first.BuildSql(statement);

            switch (operationType)
            {
                case AllorsExtentOperationTypeSqlBundled.UNION:
                    statement.Append("\nUNION\n");
                    break;
                case AllorsExtentOperationTypeSqlBundled.INTERSECT:
                    statement.Append("\nINTERSECT\n");
                    break;
                case AllorsExtentOperationTypeSqlBundled.EXCEPT:
                    statement.Append("\n" + Session.Database.Except + "\n");
                    break;
            }

            statement.Append("(");
            second.BuildSql(statement);
            statement.Append(")");

            return null;
        }
    }
}