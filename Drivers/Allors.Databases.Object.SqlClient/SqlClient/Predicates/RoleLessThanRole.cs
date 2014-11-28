// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleLessThanRole.cs" company="Allors bvba">
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
    using Allors.Adapters;
    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    public sealed class RoleLessThanRole : Predicate
    {
        private readonly IRoleType lessThanRole;
        private readonly IRoleType role;

        public RoleLessThanRole(ExtentFiltered extent, IRoleType role, IRoleType lessThanRole)
        {
            extent.CheckRole(role);
            CompositePredicateAssertions.ValidateRoleLessThan(role, lessThanRole);
            this.role = role;
            this.lessThanRole = lessThanRole;
        }

        public override bool BuildWhere(ExtentStatement statement, string alias)
        {
            var schema = statement.Schema;
            statement.Append(" " + alias + "." + schema.Column(this.role) + " < " + alias + "." + schema.Column(this.lessThanRole));
            return this.Include;
        }

        public override void Setup(ExtentStatement statement)
        {
            statement.UseRole(this.role);
            statement.UseRole(this.lessThanRole);
        }
    }
}