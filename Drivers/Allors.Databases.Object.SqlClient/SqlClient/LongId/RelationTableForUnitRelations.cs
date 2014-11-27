// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationTableForUnitRelations.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.SqlClient.LongId
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Allors.R1.Meta;

    using Microsoft.SqlServer.Server;

    public class RelationTableForUnitRelations : IEnumerable<SqlDataRecord>
    {
        private readonly Database database;
        private readonly RoleType roleType;
        private readonly IEnumerable<UnitRelation> relations;

        public RelationTableForUnitRelations(Database database, RoleType roleType, IEnumerable<UnitRelation> relations)
        {
            this.database = database;
            this.roleType = roleType;
            this.relations = relations;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var objectArrayElement = this.database.SqlClientSchema.ObjectTableObject;
            var metaData = new[]
                {
                    new SqlMetaData(objectArrayElement, SqlDbType.BigInt),
                    this.database.GetSqlMetaData(objectArrayElement, this.database.SqlClientSchema.Column(this.roleType))
                };
            var sqlDataRecord = new SqlDataRecord(metaData);

            foreach (var relation in this.relations)
            {
                sqlDataRecord.SetInt64(0, (long)relation.Association.Value);

                if (relation.Role == null)
                {
                    sqlDataRecord.SetValue(1, DBNull.Value);
                }
                else
                {
                    sqlDataRecord.SetValue(1, relation.Role);
                }

                yield return sqlDataRecord;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}