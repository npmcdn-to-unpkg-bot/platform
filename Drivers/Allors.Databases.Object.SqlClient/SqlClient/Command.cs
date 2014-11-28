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
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.Meta;

    internal class Command : IDisposable
    {
        private readonly SqlCommand command;

        public Command(ICommandFactory commandFactory, string commandText)
        {
            this.command = commandFactory.CreateSqlCommand(commandText);
        }

        public byte[] GetBinary(DbDataReader reader, int i)
        {
            return (byte[])reader.GetValue(i);
        }

        public bool GetBoolean(DbDataReader reader, int i)
        {
            return reader.GetBoolean(i);
        }

        public decimal GetDecimal(DbDataReader reader, int i)
        {
            return reader.GetDecimal(i);
        }

        public double GetFloat(DbDataReader reader, int i)
        {
            return reader.GetDouble(i);
        }

        public int GetInteger(DbDataReader reader, int i)
        {
            return reader.GetInt32(i);
        }

        public string GetString(DbDataReader reader, int i)
        {
            return reader.GetString(i);
        }

        public Guid GetUnique(DbDataReader reader, int i)
        {
            return reader.GetGuid(i);
        }

        public object GetValue(DbDataReader reader, UnitTags unitTypeTag, int i)
        {
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    return this.GetString(reader, i);
                case UnitTags.AllorsInteger:
                    return this.GetInteger(reader, i);
                case UnitTags.AllorsFloat:
                    return this.GetFloat(reader, i);
                case UnitTags.AllorsDecimal:
                    return this.GetDecimal(reader, i);
                case UnitTags.AllorsBoolean:
                    return this.GetBoolean(reader, i);
                case UnitTags.AllorsUnique:
                    return this.GetUnique(reader, i);
                case UnitTags.AllorsBinary:
                    return this.GetBinary(reader, i);
                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }
        }

        public void ExecuteNonQuery()
        {
            this.command.ExecuteNonQuery();
        }

        public DbDataReader ExecuteReader()
        {
            return ((DbCommand)this.command).ExecuteReader();
        }

        public void AddInParameter(string parameterName, object value)
        {
            var sqlParameter = this.command.Parameters.Contains(parameterName) ? this.command.Parameters[parameterName] : null;
            if (sqlParameter == null)
            {
                sqlParameter = this.command.CreateParameter();
                sqlParameter.ParameterName = parameterName;
                this.command.Parameters.Add(sqlParameter);
            }

            this.SetParameterValue(parameterName, value);
        }

        public void Dispose()
        {
            this.command.Dispose();
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