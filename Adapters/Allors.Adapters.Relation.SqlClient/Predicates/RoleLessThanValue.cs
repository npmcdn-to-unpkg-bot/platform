//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsPredicateRoleLessThanValueSql.cs" company="Allors bvba">
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
// <summary>Defines the AllorsPredicateRoleLessThanValueSql type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Relation.SqlClient
{
    using System;

    using Allors.Meta;
    using Adapters;

    internal sealed class AllorsPredicateRoleLessThanValueSql : AllorsPredicateSql
    {
        private readonly object obj;
        private readonly IRoleType roleType;

        internal AllorsPredicateRoleLessThanValueSql(AllorsExtentFilteredSql extent, IRoleType roleType, Object obj)
        {
            extent.CheckRole(roleType);
            PredicateAssertions.ValidateRoleLessThan(roleType, obj);
            this.roleType = roleType;
            this.obj = roleType.ObjectType is IUnit ? roleType.Normalize(obj) : obj;
        }

        internal override bool BuildWhere(AllorsExtentFilteredSql extent, Mapping mapping, AllorsExtentStatementSql statement, IObjectType type, string alias)
        {
            statement.Append(" " + this.roleType.SingularFullName + "_R." + Mapping.ColumnNameForRole + " < " + statement.AddParameter(this.obj));
            return this.Include;
        }

        internal override void Setup(AllorsExtentFilteredSql extent, AllorsExtentStatementSql statement)
        {
            statement.UseRole(this.roleType);
        }
    }
}