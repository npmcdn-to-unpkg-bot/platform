// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationTableForCompositeRelations.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient.IntegerId
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Adapters;

    using Microsoft.SqlServer.Server;

    public class RelationTableForCompositeRelations : IEnumerable<SqlDataRecord>
    {
        private readonly Schema schema;
        private readonly IEnumerable<CompositeRelation> relations;
 
        public RelationTableForCompositeRelations(Schema schema, IEnumerable<CompositeRelation> relations)
        {
            this.schema = schema;
            this.relations = relations;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var metaData = new[]
                {
                    new SqlMetaData(this.schema.RelationTableAssociation, SqlDbType.Int), 
                    new SqlMetaData(this.schema.RelationTableRole, SqlDbType.Int)
                };
            var sqlDataRecord = new SqlDataRecord(metaData);

            foreach (var relation in this.relations)
            {
                sqlDataRecord.SetInt32(0, (int)relation.Association.Value);
                sqlDataRecord.SetInt32(1, (int)relation.Role.Value);
                yield return sqlDataRecord;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}