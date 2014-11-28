// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagementSession.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient.Commands.Procedure;
    using Allors.Databases.Object.SqlClient.Commands.Text;

    internal class ManagementSession : ICommandFactory, IDisposable
    {
        private readonly Database database;
        
        private SqlTransaction transaction;
        private SqlConnection connection;
       
        private LoadObjectsFactory loadObjectsFactory;
        private LoadCompositeRelationsFactory loadCompositeRelationsFactory;
        private LoadUnitRelationsFactory loadUnitRelationsFactory;

        internal ManagementSession(Database database)
        {
            this.database = database;
        }
        
        ~ManagementSession()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Rollback();
        }

        internal LoadObjectsFactory LoadObjectsFactory
        {
            get
            {
                return this.loadObjectsFactory ?? (this.loadObjectsFactory = new LoadObjectsFactory(this));
            }
        }

        internal LoadCompositeRelationsFactory LoadCompositeRelationsFactory
        {
            get
            {
                return this.loadCompositeRelationsFactory ?? (this.loadCompositeRelationsFactory = new LoadCompositeRelationsFactory(this));
            }
        }

        internal LoadUnitRelationsFactory LoadUnitRelationsFactory
        {
            get
            {
                return this.loadUnitRelationsFactory ?? (this.loadUnitRelationsFactory = new LoadUnitRelationsFactory(this));
            }
        }

        internal Database Database
        {
            get
            {
                return this.database;
            }
        }

        internal Database SqlClientDatabase
        {
            get
            {
                return this.database;
            }
        }

        internal void ExecuteSql(string sql)
        {
            this.LazyConnect();
            using (DbCommand command = this.CreateSqlCommand(sql))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message + "\n-----" + sql + "\n-----");
                    throw;
                }
            }
        }

        internal Command CreateCommand(string commandText)
        {
            return new Command(this, commandText);
        }

        public SqlCommand CreateSqlCommand(string sql)
        {
            this.LazyConnect();
            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.SqlClientDatabase.CommandTimeout;
            command.CommandText = sql;
            return command;
        }

        internal void Commit()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        internal void Rollback()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Rollback();
                }
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        private void LazyConnect()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.SqlClientDatabase.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction();
            }
        }

        private void LazyDisconnect()
        {
            try
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }
            }
            finally
            {
                this.connection = null;
                this.transaction = null;
            }
        }
    }
}