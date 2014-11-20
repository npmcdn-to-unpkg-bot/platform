//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsPredicateRoleBetweenValueSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsPredicateRoleBetweenValueSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Database.SqlClient
{
    using System;

    using Allors.Meta;
    using Allors.Populations;

    internal sealed class AllorsPredicateRoleBetweenValueSql : AllorsPredicateSql
    {
        private readonly object first;
        private readonly IRoleType role;
        private readonly object second;

        internal AllorsPredicateRoleBetweenValueSql(AllorsExtentFilteredSql extent, IRoleType role, Object first, Object second)
        {
            extent.CheckRole(role);
            PredicateAssertions.ValidateRoleBetween(role, first, second);
            this.role = role;
            this.first = first;
            this.second = second;
        }

        internal override bool BuildWhere(AllorsExtentFilteredSql extent, Mapping mapping, AllorsExtentStatementSql statement, IObjectType type, string alias)
        {
            statement.Append(" " + this.role.SingularFullName + "_R." + Mapping.ColumnNameForRole + " BETWEEN " + statement.AddParameter(this.first) + " AND " + statement.AddParameter(this.second) + " ");
            return this.Include;
        }

        internal override void Setup(AllorsExtentFilteredSql extent, AllorsExtentStatementSql statement)
        {
            statement.UseRole(this.role);
        }
    }
}