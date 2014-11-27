//------------------------------------------------------------------------------------------------- 
// <copyright file="DatabaseSession.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the Session type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;

    public class DatabaseSession : Adapters.Database.Sql.DatabaseSession, ICommandFactory
    {
        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private SessionCommands sessionCommands;

        internal DatabaseSession(Database database)
        {
            this.database = database;
        }
        
        public override Allors.IDatabase Database
        {
            get { return this.database; }
        }

        public override Adapters.Database.Sql.Database SqlDatabase
        {
            get { return this.database; }
        }

        public Database SqlClientDatabase
        {
            get { return this.database; }
        }

        public override Adapters.Database.Sql.SessionCommands SessionCommands
        {
            get
            {
                return this.sessionCommands ?? (this.sessionCommands = new SessionCommands(this));
            }
        }

        public virtual SqlCommand CreateSqlCommand(string commandText)
        {
            var command = this.CreateSqlCommand();
            command.CommandText = commandText;
            return command;
        }

        public virtual SqlCommand CreateSqlCommand()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.SqlDatabase.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.SqlDatabase.IsolationLevel);
            }

            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.SqlDatabase.CommandTimeout;
            return command;
        }

        public override Adapters.Database.Sql.Command CreateCommand(string commandText)
        {
            return new Command(this, commandText);
        }

        protected override IFlush CreateFlush(Dictionary<Reference, Roles> unsyncedRolesByReference)
        {
            return new Flush(this, unsyncedRolesByReference);
        }

        protected override void SqlCommit()
        {
            try
            {
                this.sessionCommands = null;
                if (this.transaction != null)
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    this.connection.Close();
                }

                this.connection = null;
            }
        }

        protected override void SqlRollback()
        {
            try
            {
                this.sessionCommands = null;
                if (this.transaction != null)
                {
                    this.transaction.Rollback();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    this.connection.Close();
                }

                this.connection = null;
            }
        }
    }
}