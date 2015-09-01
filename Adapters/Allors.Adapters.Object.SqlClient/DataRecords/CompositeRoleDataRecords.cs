// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeRoleDataRecords.cs" company="Allors bvba">
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

namespace Allors.Adapters.Object.SqlClient
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Microsoft.SqlServer.Server;

    internal class CompositeRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;
        private readonly IEnumerable<CompositeRelation> relations;
 
        internal CompositeRoleDataRecords(Mapping mapping, IEnumerable<CompositeRelation> relations)
        {
            this.mapping = mapping;
            this.relations = relations;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            // TODO: See Relation.SqlClient
            if (this.mapping.IsObjectIdInteger)
            {
                var metaData = new[]
                                   {
                                       new SqlMetaData(this.mapping.TableTypeColumnNameForAssociation, SqlDbType.Int),
                                       new SqlMetaData(this.mapping.TableTypeColumnNameForRole, SqlDbType.Int)
                                   };
                var sqlDataRecord = new SqlDataRecord(metaData);

                foreach (var relation in this.relations)
                {
                    sqlDataRecord.SetInt32(0, (int)relation.Association.Value);
                    sqlDataRecord.SetInt32(1, (int)relation.Role.Value);
                    yield return sqlDataRecord;
                }
            }
            else
            {
                var metaData = new[]
                {
                    new SqlMetaData(this.mapping.TableTypeColumnNameForAssociation, SqlDbType.BigInt), 
                    new SqlMetaData(this.mapping.TableTypeColumnNameForRole, SqlDbType.BigInt)
                };
                var sqlDataRecord = new SqlDataRecord(metaData);

                foreach (var relation in this.relations)
                {
                    sqlDataRecord.SetInt64(0, (int)relation.Association.Value);
                    sqlDataRecord.SetInt64(1, (int)relation.Role.Value);
                    yield return sqlDataRecord;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}