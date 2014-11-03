//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsPredicateAssociationContainsSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsPredicateAssociationContainsSql type.</summary>
//-------------------------------------------------------------------------------------------------

using Allors.Meta;

namespace Allors.Adapters.Database.SqlClient
{
    internal sealed class AllorsPredicateAssociationContainsSql : AllorsPredicateSql
    {
        private readonly IObject allorsObject;
        private readonly IAssociationType association;

        internal AllorsPredicateAssociationContainsSql(AllorsExtentFilteredSql extent, IAssociationType association, IObject allorsObject)
        {
            extent.CheckAssociation(association);
            CompositePredicateAssertions.AssertAssociationContains(association, allorsObject);
            this.association = association;
            this.allorsObject = allorsObject;
        }

        internal override bool BuildWhere(AllorsExtentFilteredSql extent, Schema schema, AllorsExtentStatementSql statement, IObjectType type, string alias)
        {
            statement.Append("\n");
            statement.Append("EXISTS(\n");
            statement.Append("SELECT " + Schema.ColumnNameForObject + "\n");
            statement.Append("FROM " + schema.SchemaName + "." + schema.GetTableName(this.association) + "\n");
            statement.Append("WHERE " + Schema.ColumnNameForAssociation + "=" + this.allorsObject.Strategy.ObjectId + "\n");
            statement.Append("AND " + Schema.ColumnNameForRole + "=" + Schema.ColumnNameForObject + "\n");
            statement.Append(")");
            return this.Include;
        }

        internal override void Setup(AllorsExtentFilteredSql extent, AllorsExtentStatementSql statement)
        {
            statement.UseAssociation(this.association);
        }
    }
}