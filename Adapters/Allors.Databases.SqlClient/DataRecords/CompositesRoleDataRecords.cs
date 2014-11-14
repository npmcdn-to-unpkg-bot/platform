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

namespace Allors.Adapters.Database.SqlClient.IntegerId
{
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.SqlServer.Server;

    public class CompositesRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;
        private readonly Dictionary<ObjectId, ObjectId[]> roleByAssociation;

        public CompositesRoleDataRecords(Mapping mapping, Dictionary<ObjectId, ObjectId[]> roleByAssociation)
        {
            this.mapping = mapping;
            this.roleByAssociation = roleByAssociation;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var metaData = new[]
                {
                    new SqlMetaData(Mapping.TableTypeColumnNameForAssociation, this.mapping.SqlDbTypeForId), 
                    new SqlMetaData(Mapping.TableTypeColumnNameForRole, this.mapping.SqlDbTypeForId)
                };
            var sqlDataRecord = new SqlDataRecord(metaData);
            foreach (var entry in this.roleByAssociation)
            {
                var association = entry.Key;
                var roles = entry.Value;

                foreach (var role in roles)
                {
                    sqlDataRecord.SetValue(0, association.Value);
                    sqlDataRecord.SetValue(1, role.Value);
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