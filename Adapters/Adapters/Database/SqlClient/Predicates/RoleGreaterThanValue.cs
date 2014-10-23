//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsPredicateRoleGreaterThanValueSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsPredicateRoleGreaterThanValueSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;

    using Allors.Meta;

    internal sealed class AllorsPredicateRoleGreaterThanValueSql : AllorsPredicateSql
    {
        private readonly object obj;
        private readonly IRoleType role;

        internal AllorsPredicateRoleGreaterThanValueSql(AllorsExtentFilteredSql extent, IRoleType role, Object allorsObject)
        {
            extent.CheckRole(role);
            CompositePredicateAssertions.ValidateRoleGreaterThan(role, allorsObject);
            this.role = role;
            this.obj = allorsObject;
        }

        internal override bool BuildWhere(AllorsExtentFilteredSql extent, Schema schema, AllorsExtentStatementSql statement, IObjectType type, string alias)
        {
            statement.Append(" " + this.role.SingularFullName + "_R." + Schema.ColumnNameForRole + " > " + statement.AddParameter(this.obj));
            return this.Include;
        }

        internal override void Setup(AllorsExtentFilteredSql extent, AllorsExtentStatementSql statement)
        {
            statement.UseRole(this.role);
        }
    }
}