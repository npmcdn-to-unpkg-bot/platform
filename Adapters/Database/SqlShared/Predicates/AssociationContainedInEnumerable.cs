// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssociationContainedInEnumerable.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Text;

    using Meta;

    public sealed class AssociationContainedInEnumerable : In
    {
        private readonly AssociationType association;
        private readonly IEnumerable<IObject> enumerable;

        public AssociationContainedInEnumerable(ExtentFiltered extent, AssociationType association, IEnumerable<IObject> enumerable)
        {
            extent.CheckAssociation(association);
            CompositePredicateAssertions.AssertAssociationContainedIn(association, this.enumerable);
            this.association = association;
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

            if ((this.association.IsMany && this.association.RelationTypeWhereAssociationType.RoleType.IsMany) || !this.association.RelationTypeWhereAssociationType.ExistExclusiveRootClasses)
            {
                statement.Append(" (" + this.association.Name + "_A." + schema.AssociationId + " IS NOT NULL AND ");
                statement.Append(" " + this.association.Name + "_A." + schema.AssociationId + " IN (\n");
                statement.Append(inStatement.ToString());
                statement.Append(" ))\n");
            }
            else
            {
                if (this.association.RelationTypeWhereAssociationType.RoleType.IsMany)
                {
                    statement.Append(" (" + alias + "." + schema.Column(this.association) + " IS NOT NULL AND ");
                    statement.Append(" " + alias + "." + schema.Column(this.association) + " IN (\n");
                    statement.Append(inStatement.ToString());
                    statement.Append(" ))\n");
                }
                else
                {
                    statement.Append(" (" + this.association.Name + "_A." + schema.ObjectId + " IS NOT NULL AND ");
                    statement.Append(" " + this.association.Name + "_A." + schema.ObjectId + " IN (\n");
                    statement.Append(inStatement.ToString());
                    statement.Append(" ))\n");
                }
            }

            return this.Include;
        }

        public override void Setup(ExtentStatement statement)
        {
            statement.UseAssociation(this.association);
        }
    }
}