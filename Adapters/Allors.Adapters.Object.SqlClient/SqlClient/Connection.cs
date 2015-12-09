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
    using System.Data.SqlClient;

    public class Connection
    {
        protected readonly Database Database;

        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        
        public Connection(Database database)
        {
            this.Database = database;
        }

        public void Commit()
        {
            try
            {
                this.sqlTransaction?.Commit();
            }
            finally
            {
                this.sqlTransaction = null;

                this.sqlConnection?.Close();
                this.sqlConnection = null;
            }
        }

        public void Rollback()
        {
            try
            {
                this.sqlTransaction?.Rollback();
            }
            finally
            {
                this.sqlTransaction = null;

                this.sqlConnection?.Close();
                this.sqlConnection = null;
            }
        }

        public Command CreateCommand()
        {
            if (this.sqlConnection == null)
            {
                this.sqlConnection = new SqlConnection(this.Database.ConnectionString);
                this.sqlConnection.Open();
                this.sqlTransaction = this.sqlConnection.BeginTransaction(this.Database.IsolationLevel);
            }

            var sqlCommand = this.sqlConnection.CreateCommand();
            sqlCommand.Transaction = this.sqlTransaction;
            sqlCommand.CommandTimeout = this.Database.CommandTimeout;

            return this.CreateCommand(this.Database.Mapping, sqlCommand);
        }

        protected virtual Command CreateCommand(Mapping mapping, SqlCommand sqlCommand)
        {
            return new Command(mapping, sqlCommand);
        }
    }
}