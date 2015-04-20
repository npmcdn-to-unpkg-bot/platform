// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Allors bvba">
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
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Meta;

    internal class Command : IDisposable
    {
        private readonly SqlCommand command;

        internal Command(SqlCommand command)
        {
            this.command = command;
        }

        public void Dispose()
        {
            this.command.Dispose();
        }

        internal object GetValue(SqlDataReader reader, UnitTags unitTypeTag, int i)
        {
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    return reader.GetString(i);
                case UnitTags.AllorsInteger:
                    return reader.GetInt32(i);
                case UnitTags.AllorsFloat:
                    return reader.GetDouble(i);
                case UnitTags.AllorsDecimal:
                    return reader.GetDecimal(i);
                case UnitTags.AllorsBoolean:
                    return reader.GetBoolean(i);
                case UnitTags.AllorsDateTime:
                    return reader.GetDateTime(i);
                case UnitTags.AllorsUnique:
                    return reader.GetGuid(i);
                case UnitTags.AllorsBinary:
                    return reader.GetValue(i);
                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }
        }

        internal void ExecuteNonQuery()
        {
            this.command.ExecuteNonQuery();
        }

        internal SqlDataReader ExecuteReader()
        {
            return this.command.ExecuteReader();
        }

        internal void AddInParameter(string parameterName, object value)
        {
            var sqlParameter = this.command.Parameters.Contains(parameterName) ? this.command.Parameters[parameterName] : null;
            if (sqlParameter == null)
            {
                sqlParameter = this.command.CreateParameter();
                sqlParameter.ParameterName = parameterName;
                if (value is DateTime)
                {
                    sqlParameter.SqlDbType = SqlDbType.DateTime2;    
                }

                this.command.Parameters.Add(sqlParameter);
            }

            this.SetParameterValue(parameterName, value);
        }

        private void SetParameterValue(string parameterName, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                this.command.Parameters[parameterName].Value = DBNull.Value;
            }
            else
            {
                this.command.Parameters[parameterName].Value = value;
            }
        }
    }
}