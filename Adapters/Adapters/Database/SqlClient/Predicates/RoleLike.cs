//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsPredicateRoleLikeSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsPredicateRoleLikeSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;

    using Allors.Meta;

    internal sealed class AllorsPredicateRoleLikeSql : AllorsPredicateSql
    {
        private readonly IRoleType role;
        private readonly string str;

        internal AllorsPredicateRoleLikeSql(AllorsExtentFilteredSql extent, IRoleType role, String str)
        {
            extent.CheckRole(role);
            CompositePredicateAssertions.ValidateRoleLikeFilter(role, str);
            this.role = role;
            this.str = str;
        }

        internal override bool BuildWhere(AllorsExtentFilteredSql extent, Schema schema, AllorsExtentStatementSql statement, IObjectType type, string alias)
        {
            statement.Append(" " + role.SingularFullName + "_R." + Schema.ColumnNameForRole + " LIKE " + statement.AddParameter(this.str));
            return this.Include;
        }

        internal override void Setup(AllorsExtentFilteredSql extent, AllorsExtentStatementSql statement)
        {
            statement.UseRole(this.role);
        }
    }
}