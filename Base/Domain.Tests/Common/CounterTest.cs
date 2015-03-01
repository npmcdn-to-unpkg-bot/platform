// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CounterTest.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   Defines the ApplicationTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;
    using System.Data;

    using Allors.Domain;
    using Allors.Workspaces.Memory.IntegerId;

    using NUnit.Framework;

    [TestFixture]
    public class CounterTest : DomainTest
    {
        private IDatabase previousDatabase;
        private IDatabase previousSerializableDatabase;

        [Test]
        public void Meta()
        {
            var counterBuilder = new CounterBuilder(this.DatabaseSession).Build();

            Assert.IsTrue(counterBuilder.ExistUniqueId);
            Assert.AreNotEqual(Guid.Empty, counterBuilder.UniqueId);

            Assert.AreEqual(0, counterBuilder.Value);

            var secondCounterBuilder = new CounterBuilder(this.DatabaseSession).Build();

            Assert.AreNotEqual(counterBuilder.UniqueId, secondCounterBuilder.UniqueId);
        }

        [Test]
        public void NonShared()
        {
            this.SaveApplication();

            try
            {
                var configuration = new Databases.Object.SqlClient.IntegerId.Configuration
                {
                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    WorkspaceFactory = new WorkspaceFactory()
                };
                Config.Default = new Databases.Object.SqlClient.IntegerId.Database(configuration);
                Config.Serializable = null;

                this.Init(true);

                var id = Guid.NewGuid();

                new CounterBuilder(DatabaseSession).WithUniqueId(id).Build();
                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(2, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(3, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(4, Counters.NextValue(this.DatabaseSession, id));
            }
            finally
            {
                this.RestoreApplication();
            }
        }

        [Test]
        public void Shared()
        {
            this.SaveApplication();

            try
            {
                var configuration = new Databases.Object.SqlClient.IntegerId.Configuration
                                        {
                                            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                            ObjectFactory = Config.ObjectFactory,
                                            WorkspaceFactory = new WorkspaceFactory()
                                        };
                Config.Default = new Databases.Object.SqlClient.IntegerId.Database(configuration);
                Config.Serializable = null;

                this.Init(true);

                var id = Guid.NewGuid();

                new CounterBuilder(DatabaseSession).WithUniqueId(id).Build();
                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(2, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(3, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(4, Counters.NextValue(this.DatabaseSession, id));
            }
            finally
            {
                this.RestoreApplication();
            }
        }

        [Test]
        public void Serializable()
        {
            this.SaveApplication();

            try
            {
                var workspaceFactory = new WorkspaceFactory();
                Config.Default = new Databases.Object.SqlClient.IntegerId.Database(new Databases.Object.SqlClient.IntegerId.Configuration
                                                                                                    {
                                                                                                        ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                                                                                        ObjectFactory = Config.ObjectFactory,
                                                                                                        WorkspaceFactory = workspaceFactory
                                                                                                    });
                Config.Serializable = new Databases.Object.SqlClient.IntegerId.Database(new Databases.Object.SqlClient.IntegerId.Configuration
                                                                                                {
                                                                                                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                                                                                    ObjectFactory = Config.ObjectFactory,
                                                                                                    WorkspaceFactory = workspaceFactory,
                                                                                                    IsolationLevel = IsolationLevel.Serializable
                                                                                                });

                this.Init(true);

                var id = Guid.NewGuid();

                new CounterBuilder(DatabaseSession).WithUniqueId(id).Build();
                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(2, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(3, Counters.NextValue(this.DatabaseSession, id));
                Assert.AreEqual(4, Counters.NextValue(this.DatabaseSession, id));
            }
            finally
            {
                this.RestoreApplication();
            }
        }

        private void SaveApplication()
        {
            this.previousDatabase = Config.Default;
            this.previousSerializableDatabase = Config.Serializable;
        }

        private void RestoreApplication()
        {
            Config.Default = this.previousDatabase;
            Config.Serializable = this.previousSerializableDatabase;
        }
    }
}