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

using Allors;

namespace Allors.Adapters.Relation.SqlClient
{
    using System.Collections;
    using System.Collections.Generic;

    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    public class CompositeRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;
        private readonly IRoleType roleType;
        private readonly IList<ObjectId> associations;
        private readonly Dictionary<ObjectId, ObjectId> roleByAssociation;

        public CompositeRoleDataRecords(Mapping mapping, IRoleType roleType, IList<ObjectId> associations, Dictionary<ObjectId, ObjectId> roleByAssociation)
        {
            this.mapping = mapping;
            this.roleType = roleType;
            this.associations = associations;
            this.roleByAssociation = roleByAssociation;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var sqlDataRecord = new SqlDataRecord(this.mapping.GetSqlMetaData(this.roleType));
            if (this.mapping.IsObjectIdInteger)
            {
                foreach (var association in this.associations)
                {
                    sqlDataRecord.SetInt32(0, (int)association.Value);
                    sqlDataRecord.SetValue(1, this.roleByAssociation[association].Value);
                    yield return sqlDataRecord;
                }
            }
            else
            {
                foreach (var association in this.associations)
                {
                    sqlDataRecord.SetInt64(0, (long)association.Value);
                    sqlDataRecord.SetValue(1, this.roleByAssociation[association].Value);
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