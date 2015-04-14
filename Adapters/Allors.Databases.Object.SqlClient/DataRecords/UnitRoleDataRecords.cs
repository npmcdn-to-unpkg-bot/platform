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

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    internal class UnitRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Database database;
        private readonly IRoleType roleType;
        private readonly IEnumerable<UnitRelation> relations;
 
        internal UnitRoleDataRecords(Database database, IRoleType roleType, IEnumerable<UnitRelation> relations)
        {
            this.database = database;
            this.roleType = roleType;
            this.relations = relations;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            // TODO: See Relation.SqlClient
            if (this.database.Mapping.IsObjectIdInteger)
            {
                var metaData = new[]
                                   {
                                       new SqlMetaData(
                                           this.database.SqlClientMapping.RelationTableAssociation,
                                           SqlDbType.Int),
                                       this.database.GetSqlMetaData(
                                           this.database.SqlClientMapping.RelationTableRole,
                                           this.database.SqlClientMapping.Column(this.roleType))
                                   };
                var sqlDataRecord = new SqlDataRecord(metaData);

                foreach (var relation in this.relations)
                {
                    sqlDataRecord.SetInt32(0, (int)relation.Association.Value);

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
            else
            {
                var metaData = new[]
                                   {
                                       new SqlMetaData(
                                           this.database.SqlClientMapping.RelationTableAssociation,
                                           SqlDbType.BigInt),
                                       this.database.GetSqlMetaData(
                                           this.database.SqlClientMapping.RelationTableRole,
                                           this.database.SqlClientMapping.Column(this.roleType))
                                   };
                var sqlDataRecord = new SqlDataRecord(metaData);

                foreach (var relation in this.relations)
                {
                    sqlDataRecord.SetInt64(0, (int)relation.Association.Value);

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
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}