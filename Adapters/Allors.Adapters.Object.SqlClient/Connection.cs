// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Connection.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Object.SqlClient
{
    using System.Data;
    using System.Data.SqlClient;

    public class Connection
    {
        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;
        
        public Connection(Database database)
        {
            this.database = database;
        }

        public Command CreateProcedureCommand(string commandText)
        {
            var command = this.InternalCreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            return new Command(this.database.Mapping, command);
        }

        public Command CreateCommand()
        {
            return new Command(this.database.Mapping, this.InternalCreateCommand());
        }

        public void Commit()
        {
            try
            {
                this.transaction?.Commit();
            }
            finally
            {
                this.transaction = null;

                this.connection?.Close();
                this.connection = null;
            }
        }

        public void Rollback()
        {
            try
            {
                this.transaction?.Rollback();
            }
            finally
            {
                this.transaction = null;

                this.connection?.Close();
                this.connection = null;
            }
        }

        public Command CreateCommand(string commandText)
        {
            var command = this.InternalCreateCommand();
            command.CommandText = commandText;
            return new Command(this.database.Mapping, command);
        }
        
        private SqlCommand InternalCreateCommand()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.database.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.database.IsolationLevel);
            }

            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.database.CommandTimeout;
            return command;
        }

    }
}