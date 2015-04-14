// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectTableForObjectIds.cs" company="Allors bvba">
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

    internal class ObjectDataRecord : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;
        private readonly IEnumerable<ObjectId> objectIds;

        internal ObjectDataRecord(Mapping mapping, IEnumerable<ObjectId> objectIds)
        {
            this.mapping = mapping;
            this.objectIds = objectIds;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var objectArrayElement = this.mapping.ObjectTableObject;
            var metaData = new SqlMetaData(objectArrayElement, SqlDbType.Int);
            var sqlDataRecord = new SqlDataRecord(metaData);

            if (this.mapping.IsObjectIdInteger)
            {
                foreach (var objectId in this.objectIds)
                {
                    sqlDataRecord.SetInt32(0, (int)objectId.Value);
                    yield return sqlDataRecord;
                }
            }
            else
            {
                foreach (var objectId in this.objectIds)
                {
                    sqlDataRecord.SetInt64(0, (long)objectId.Value);
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