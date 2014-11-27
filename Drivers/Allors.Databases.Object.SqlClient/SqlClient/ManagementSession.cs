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

    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Databases.Object.SqlClient.Commands.Procedure;
    using Allors.Databases.Object.SqlClient.Commands.Text;

    public class ManagementSession : Adapters.Database.Sql.ManagementSession, ICommandFactory
    {
        private readonly Database database;
        
        private SqlTransaction transaction;
        private SqlConnection connection;
       
        private ILoadObjectsFactory loadObjectsFactory;
        private ILoadCompositeRelationsFactory loadCompositeRelationsFactory;
        private ILoadUnitRelationsFactory loadUnitRelationsFactory;

        public ManagementSession(Database database)
        {
            this.database = database;
        }
        
        ~ManagementSession()
        {
            this.Dispose();
        }

        public override ILoadObjectsFactory LoadObjectsFactory
        {
            get
            {
                return this.loadObjectsFactory ?? (this.loadObjectsFactory = new LoadObjectsFactory(this));
            }
        }

        public override ILoadCompositeRelationsFactory LoadCompositeRelationsFactory
        {
            get
            {
                return this.loadCompositeRelationsFactory ?? (this.loadCompositeRelationsFactory = new LoadCompositeRelationsFactory(this));
            }
        }

        public override ILoadUnitRelationsFactory LoadUnitRelationsFactory
        {
            get
            {
                return this.loadUnitRelationsFactory ?? (this.loadUnitRelationsFactory = new LoadUnitRelationsFactory(this));
            }
        }

        public override Adapters.Database.Sql.Database Database
        {
            get
            {
                return this.database;
            }
        }

        public Database SqlClientDatabase
        {
            get
            {
                return this.database;
            }
        }

        public override void ExecuteSql(string sql)
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

        public override Adapters.Database.Sql.Command CreateCommand(string commandText)
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

        public override void Commit()
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

        public override void Rollback()
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