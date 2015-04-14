// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleBetweenValue.cs" company="Allors bvba">
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
    using Allors.Populations;

    using Meta;

    internal sealed class RoleBetweenValue : Predicate
    {
        private readonly IRoleType roleType;
        private readonly object first;
        private readonly object second;

        internal RoleBetweenValue(ExtentFiltered extent, IRoleType roleType, object first, object second)
        {
            extent.CheckRole(roleType);
            PredicateAssertions.ValidateRoleBetween(roleType, first, second);
            this.roleType = roleType;
            this.first = roleType.Normalize(first);
            this.second = roleType.Normalize(second);
        }

        internal override bool BuildWhere(ExtentStatement statement, string alias)
        {
            var schema = statement.Mapping;
            statement.Append(" (" + alias + "." + schema.Column(this.roleType) + " BETWEEN " + statement.AddParameter(this.first) + " AND " + statement.AddParameter(this.second) + ")");
            return this.Include;
        }

        internal override void Setup(ExtentStatement statement)
        {
            statement.UseRole(this.roleType);
        }
    }
}