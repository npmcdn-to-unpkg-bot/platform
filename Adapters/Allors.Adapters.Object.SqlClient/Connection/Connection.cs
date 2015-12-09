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

    public abstract class Connection
    {
        protected readonly Database Database;

        protected SqlConnection SqlConnection { get; private set; }

        protected SqlTransaction SqlTransaction { get; private set; }

        protected Connection(Database database)
        {
            this.Database = database;
        }

        internal Command CreateCommand()
        {
            if (this.SqlConnection == null)
            {
                this.OnCreateSqlConnection();
                this.SqlConnection = new SqlConnection(this.Database.ConnectionString);
                this.OnCreatedSqlConnection();

                this.OnOpenSqlConnection();
                this.SqlConnection.Open();
                this.OnOpenedSqlConnection();

                this.OnCreateSqlTransaction();
                this.SqlTransaction = this.SqlConnection.BeginTransaction(this.Database.IsolationLevel);
                this.OnCreatedSqlTransaction();
            }

            this.OnCreateSqlCommand();
            var sqlCommand = this.SqlConnection.CreateCommand();
            sqlCommand.Transaction = this.SqlTransaction;
            sqlCommand.CommandTimeout = this.Database.CommandTimeout;
            this.OnCreatedSqlCommand();

            return this.CreateCommand(this.Database.Mapping, sqlCommand);
        }

        internal void Commit()
        {
            try
            {
                if (this.SqlTransaction != null)
                {
                    this.OnCommit();
                    this.SqlTransaction.Commit();
                    this.OnCommitted();
                }
            }
            finally
            {
                this.SqlTransaction = null;

                if (this.SqlConnection != null)
                {
                    this.OnCloseSqlConnection();
                    this.SqlConnection?.Close();
                    this.OnClosedSqlConnection();
                }

                this.SqlConnection = null;
            }
        }

        internal void Rollback()
        {
            try
            {
                if (this.SqlTransaction != null)
                {
                    this.OnRollback();
                    this.SqlTransaction?.Rollback();
                    this.OnRolledback();
                }
            }
            finally
            {
                this.SqlTransaction = null;

                if (this.SqlConnection != null)
                {
                    this.OnCloseSqlConnection();
                    this.SqlConnection?.Close();
                    this.OnClosedSqlConnection();
                }

                this.SqlConnection = null;
            }
        }

        protected abstract Command CreateCommand(Mapping mapping, SqlCommand sqlCommand);

        #region Events

        protected abstract void OnCreateSqlConnection();

        protected abstract void OnCreatedSqlConnection();

        protected abstract void OnOpenSqlConnection();

        protected abstract void OnOpenedSqlConnection();

        protected abstract void OnCloseSqlConnection();

        protected abstract void OnClosedSqlConnection();

        protected abstract void OnCreateSqlTransaction();

        protected abstract void OnCreatedSqlTransaction();

        protected abstract void OnCommit();

        protected abstract void OnCommitted();

        protected abstract void OnRollback();

        protected abstract void OnRolledback();

        protected abstract void OnCreateSqlCommand();

        protected abstract void OnCreatedSqlCommand();

        #endregion
    }
}