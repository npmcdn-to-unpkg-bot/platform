// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitRoleDataRecords.cs" company="Allors bvba">
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

namespace Allors.Databases.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    public class UnitRoleDataRecords : IEnumerable<SqlDataRecord>
    {
        private readonly Mapping mapping;

        private readonly IRoleType roleType;
        private readonly IList<ObjectId> associations;
        private readonly Dictionary<ObjectId, object> roleByAssociation;

        public UnitRoleDataRecords(Mapping mapping, IRoleType roleType, IList<ObjectId> associations, Dictionary<ObjectId, object> roleByAssociation)
        {
            this.mapping = mapping;
            this.roleType = roleType;
            this.associations = associations;
            this.roleByAssociation = roleByAssociation;
        }

        public IEnumerator<SqlDataRecord> GetEnumerator()
        {
            var sqlDataRecord = new SqlDataRecord(this.mapping.GetSqlMetaData(this.roleType));
            var unitTypeTag = ((IUnit)this.roleType.ObjectType).UnitTag;

            if (this.mapping.IsObjectIdInteger)
            {
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsBinary:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetValue(1, this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;

                    case UnitTags.AllorsBoolean:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetBoolean(1, (bool)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsDecimal:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetDecimal(1, (decimal)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsFloat:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetDouble(1, (double)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsInteger:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetInt32(1, (int)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsString:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetString(1, (string)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;

                    case UnitTags.AllorsUnique:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt32(0, (int)association.Value);
                            sqlDataRecord.SetGuid(1, (Guid)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    default:
                        throw new NotSupportedException("Unit type tag " + unitTypeTag + " is not supported.");
                }

            }
            else
            {
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsBinary:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetValue(1, this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;

                    case UnitTags.AllorsBoolean:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetBoolean(1, (bool)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsDecimal:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetDecimal(1, (decimal)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsFloat:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetDouble(1, (double)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsInteger:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetInt32(1, (int)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    case UnitTags.AllorsString:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetString(1, (string)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;

                    case UnitTags.AllorsUnique:
                        foreach (var association in this.associations)
                        {
                            sqlDataRecord.SetInt64(0, (long)association.Value);
                            sqlDataRecord.SetGuid(1, (Guid)this.roleByAssociation[association]);
                            yield return sqlDataRecord;
                        }

                        break;
                    default:
                        throw new NotSupportedException("Unit type tag " + unitTypeTag + " is not supported.");
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}