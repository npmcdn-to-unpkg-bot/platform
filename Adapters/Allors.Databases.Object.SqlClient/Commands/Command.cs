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

namespace Allors.Databases.Object.SqlClient.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;

    using Microsoft.SqlServer.Server;

    internal abstract class Command
    {
        internal void AddInObject(SqlCommand command, string parameterName, SqlDbType parameterType, object value)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = parameterName;
            sqlParameter.SqlDbType = parameterType;
            sqlParameter.Value = Normalize(value);
            
            command.Parameters.Add(sqlParameter);
        }

        internal void SetInObject(SqlCommand command, string parameterName, object value)
        {
            command.Parameters[parameterName].Value = Normalize(value);
        }

        internal void AddInTable(SqlCommand command, string parameterType, IEnumerable<SqlDataRecord> array)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = parameterType;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = array;

            command.Parameters.Add(sqlParameter);
        }

        internal void SetInTable(SqlCommand command, IEnumerable<SqlDataRecord> array)
        {
            command.Parameters[Mapping.ParamNameForTableType].Value = array;
        }

        internal int GetCachId(SqlDataReader reader, int i)
        {
            return reader.GetInt32(i);
        }

        internal Guid GetClassId(SqlDataReader reader, int i)
        {
            return reader.GetGuid(i);
        }

        private static object Normalize(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}