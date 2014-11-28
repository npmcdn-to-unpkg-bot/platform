// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Equals.cs" company="Allors bvba">
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

    public sealed class Equals : Predicate
    {
        private readonly IObject obj;

        public Equals(IObject obj)
        {
            PredicateAssertions.ValidateEquals(obj);
            this.obj = obj;
        }

        public override void Setup(ExtentStatement statement)
        {
        }

        public override bool BuildWhere(ExtentStatement statement, string alias)
        {
            var schema = statement.Schema;
            statement.Append(" (" + alias + "." + schema.ObjectId + "=" + statement.AddParameter(this.obj) + ") ");
            return this.Include;
        }
    }
}