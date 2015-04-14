// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectTableForStrategies.cs" company="Allors bvba">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Microsoft.SqlServer.Server;

    internal class CompositesRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;
        private readonly IEnumerable<Reference> strategies;

        internal CompositesRoleDataRecords(Mapping mapping, IEnumerable<Reference> strategies)
        {
            this.mapping = mapping;
            this.strategies = strategies;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            // TODO: See Relation.SqlClient
            if (this.mapping.IsObjectIdInteger)
            {
                var objectArrayElement = this.mapping.ObjectTableObject;
                var metaData = new SqlMetaData(objectArrayElement, SqlDbType.Int);
                var sqlDataRecord = new SqlDataRecord(metaData);
                foreach (var strategy in this.strategies)
                {
                    sqlDataRecord.SetInt32(0, (int)strategy.ObjectId.Value);
                    yield return sqlDataRecord;
                }
            }
            else
            {
                var objectArrayElement = this.mapping.ObjectTableObject;
                var metaData = new SqlMetaData(objectArrayElement, SqlDbType.BigInt);
                var sqlDataRecord = new SqlDataRecord(metaData);
                foreach (var strategy in this.strategies)
                {
                    sqlDataRecord.SetInt64(0, (int)strategy.ObjectId.Value);
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