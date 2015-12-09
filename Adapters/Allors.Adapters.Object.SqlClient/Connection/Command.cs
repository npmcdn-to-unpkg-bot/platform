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

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Meta;

    public abstract class Command : IDisposable
    {
        protected Mapping Mapping { get; }

        protected SqlCommand SqlCommand { get; }

        protected internal Command(Mapping mapping, SqlCommand command)
        {
            this.Mapping = mapping;
            this.SqlCommand = command;
        }

        internal SqlParameterCollection Parameters => this.SqlCommand.Parameters;

        internal CommandType CommandType {
            get
            {
                return this.SqlCommand.CommandType;
            }

            set
            {
                this.SqlCommand.CommandType = value;
            }
        }

        internal string CommandText {
            get
            {
                return this.SqlCommand.CommandText;
            }

            set
            {
                this.SqlCommand.CommandText = value;
            }
        }

        public void Dispose()
        {
            this.SqlCommand.Dispose();
        }

        internal SqlParameter CreateParameter()
        {
            return this.SqlCommand.CreateParameter();
        }

        internal void AddInParameter(string parameterName, object value)
        {
            var sqlParameter = this.SqlCommand.Parameters.Contains(parameterName) ? this.SqlCommand.Parameters[parameterName] : null;
            if (sqlParameter == null)
            {
                sqlParameter = this.SqlCommand.CreateParameter();
                sqlParameter.ParameterName = parameterName;
                if (value is DateTime)
                {
                    sqlParameter.SqlDbType = SqlDbType.DateTime2;
                }

                this.SqlCommand.Parameters.Add(sqlParameter);
            }

            if (value == null || value == DBNull.Value)
            {
                this.SqlCommand.Parameters[parameterName].Value = DBNull.Value;
            }
            else
            {
                this.SqlCommand.Parameters[parameterName].Value = value;
            }
        }

        internal void AddObjectParameter(ObjectId objectId)
        {
            var sqlParameter = this.SqlCommand.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForObject;
            sqlParameter.SqlDbType = this.Mapping.SqlDbTypeForObject;
            sqlParameter.Value = objectId.Value;

            this.SqlCommand.Parameters.Add(sqlParameter);
        }

        internal object ExecuteScalar()
        {
            this.OnExecute();
            try
            {
                return this.SqlCommand.ExecuteScalar();
            }
            finally
            {
                this.OnExecuted();
            }
        }

        internal void ExecuteNonQuery()
        {
            this.OnExecute();

            try
            {
                this.SqlCommand.ExecuteNonQuery();
            }
            finally
            {
                this.OnExecuted();
            }
        }

        internal SqlDataReader ExecuteReader()
        {
            this.OnExecute();

            try
            {
                return this.SqlCommand.ExecuteReader();
            }
            finally
            {
                this.OnExecuted();
            }

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

        #region Events

        protected abstract void OnExecute();

        protected abstract void OnExecuted();

        #endregion
    }
}