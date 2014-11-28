// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleContainedInEnumerable.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Text;

    using Allors.Adapters;
    using Allors.Adapters.Database.Sql;

    using Meta;

    public sealed class RoleContainedInEnumerable : In
    {
        private readonly IEnumerable<IObject> enumerable;
        private readonly IRoleType role;

        public RoleContainedInEnumerable(ExtentFiltered extent, IRoleType role, IEnumerable<IObject> enumerable)
        {
            extent.CheckRole(role);
            CompositePredicateAssertions.ValidateRoleContainedIn(role, this.enumerable);
            this.role = role;
            this.enumerable = enumerable;
        }

        public override bool BuildWhere(ExtentStatement statement, string alias)
        {
            var schema = statement.Schema;

            var inStatement = new StringBuilder("0");
            foreach (var inObject in this.enumerable)
            {
                inStatement.Append(",");
                inStatement.Append(inObject.Id.ToString());
            }

            if ((this.role.IsMany && this.role.RelationType.AssociationType.IsMany) || !this.role.RelationType.ExistExclusiveLeafClasses)
            {
                statement.Append(" (" + this.role.SingularFullName + "_R." + schema.RoleId + " IS NOT NULL AND ");
                statement.Append(" " + this.role.SingularFullName + "_R." + schema.AssociationId + " IN (");
                statement.Append(inStatement.ToString());
                statement.Append(" ))");
            }
            else
            {
                if (this.role.IsMany)
                {
                    statement.Append(" (" + this.role.SingularFullName + "_R." + schema.ObjectId + " IS NOT NULL AND ");
                    statement.Append(" " + this.role.SingularFullName + "_R." + schema.ObjectId + " IN (");
                    statement.Append(inStatement.ToString());
                    statement.Append(" ))");
                }
                else
                {
                    statement.Append(" (" + schema.Column(this.role) + " IS NOT NULL AND ");
                    statement.Append(" " + schema.Column(this.role) + " IN (");
                    statement.Append(inStatement.ToString());
                    statement.Append(" ))");
                }
            }

            return this.Include;
        }

        public override void Setup(ExtentStatement statement)
        {
            statement.UseRole(this.role);
        }
    }
}