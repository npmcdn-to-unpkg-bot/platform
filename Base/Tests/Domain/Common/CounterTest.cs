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

namespace Domain
{
    using System;
    using System.Data;

    using Allors;
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
            var counterBuilder = new CounterBuilder(this.Session).Build();

            Assert.IsTrue(counterBuilder.ExistUniqueId);
            Assert.AreNotEqual(Guid.Empty, counterBuilder.UniqueId);

            Assert.AreEqual(0, counterBuilder.Value);

            var secondCounterBuilder = new CounterBuilder(this.Session).Build();

            Assert.AreNotEqual(counterBuilder.UniqueId, secondCounterBuilder.UniqueId);
        }

        [Test]
        public void NonShared()
        {
            this.SaveApplication();

            try
            {
                var configuration = new Allors.Databases.Object.SqlClient.Configuration
                {
                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    WorkspaceFactory = new WorkspaceFactory()
                };
                Config.Default = new Allors.Databases.Object.SqlClient.Database(configuration);
                Config.Serializable = null;

                this.SetUp(true);

                var id = Guid.NewGuid();

                new CounterBuilder(this.Session).WithUniqueId(id).Build();
                this.Session.Derive(true);
                this.Session.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.Session, id));
                Assert.AreEqual(2, Counters.NextValue(this.Session, id));
                Assert.AreEqual(3, Counters.NextValue(this.Session, id));
                Assert.AreEqual(4, Counters.NextValue(this.Session, id));
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
                var configuration = new Allors.Databases.Object.SqlClient.Configuration
                                        {
                                            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                            ObjectFactory = Config.ObjectFactory,
                                            WorkspaceFactory = new WorkspaceFactory()
                                        };
                Config.Default = new Allors.Databases.Object.SqlClient.Database(configuration);
                Config.Serializable = null;

                this.SetUp(true);

                var id = Guid.NewGuid();

                new CounterBuilder(this.Session).WithUniqueId(id).Build();
                this.Session.Derive(true);
                this.Session.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.Session, id));
                Assert.AreEqual(2, Counters.NextValue(this.Session, id));
                Assert.AreEqual(3, Counters.NextValue(this.Session, id));
                Assert.AreEqual(4, Counters.NextValue(this.Session, id));
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
                Config.Default = new Allors.Databases.Object.SqlClient.Database(new Allors.Databases.Object.SqlClient.Configuration
                                                                                                    {
                                                                                                        ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                                                                                        ObjectFactory = Config.ObjectFactory,
                                                                                                        WorkspaceFactory = workspaceFactory
                                                                                                    });
                Config.Serializable = new Allors.Databases.Object.SqlClient.Database(new Allors.Databases.Object.SqlClient.Configuration
                                                                                                {
                                                                                                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                                                                                                    ObjectFactory = Config.ObjectFactory,
                                                                                                    WorkspaceFactory = workspaceFactory,
                                                                                                    IsolationLevel = IsolationLevel.Serializable
                                                                                                });

                this.SetUp(true);

                var id = Guid.NewGuid();

                new CounterBuilder(this.Session).WithUniqueId(id).Build();
                this.Session.Derive(true);
                this.Session.Commit();

                Assert.AreEqual(1, Counters.NextValue(this.Session, id));
                Assert.AreEqual(2, Counters.NextValue(this.Session, id));
                Assert.AreEqual(3, Counters.NextValue(this.Session, id));
                Assert.AreEqual(4, Counters.NextValue(this.Session, id));
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