// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleBetweenRole.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Sql
{
    using Allors.Meta;

    public sealed class RoleBetweenRole : Predicate
    {
        private readonly RoleType first;
        private readonly RoleType role;
        private readonly RoleType second;

        public RoleBetweenRole(ExtentFiltered extent, RoleType role, RoleType first, RoleType second)
        {
            extent.CheckRole(role);
            CompositePredicateAssertions.ValidateRoleBetween(role, first, second);
            this.role = role;
            this.first = first;
            this.second = second;
        }

        public override bool BuildWhere(ExtentStatement statement, string alias)
        {
            var schema = statement.Schema;
            statement.Append(" (" + alias + "." + schema.Column(this.role) + " BETWEEN " + alias + "." + schema.Column(this.first) + " AND " + alias + "." + schema.Column(this.second) + ")");
            return this.Include;
        }

        public override void Setup(ExtentStatement statement)
        {
            statement.UseRole(this.role);
            statement.UseRole(this.first);
            statement.UseRole(this.second);
        }
    }
}