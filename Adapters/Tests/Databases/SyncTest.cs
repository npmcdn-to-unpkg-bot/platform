// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.Databases
{
    using System;
    using System.IO;
    using System.Xml;

    using Allors;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Populations;

    using NUnit.Framework;

    public abstract class SyncTest
    {
        private const int RollbackCount = 3;
        private static readonly bool[] FalseTrue = { false, true };
        private static readonly int[] SaveRepeats = { 1 };

        protected abstract IProfile Profile { get; }

        protected ISession Session
        {
            get
            {
                return this.Profile.Session;
            }
        }

        protected Action[] Markers
        {
            get
            {
                return this.Profile.Markers;
            }
        }

        protected Action[] Inits
        {
            get
            {
                return this.Profile.Inits;
            }
        }

        [Test]
        public void DeleteObjects()
        {
            foreach (var init in this.Inits)
            {
                init();

                var database = this.Profile.CreateDatabase();
                database.Init();

                ObjectId c1AObjectId;
                ObjectId c2AObjectId;

                using (var session = database.CreateSession())
                {
                    var c1A = C1.Create(session);
                    var c2A = C2.Create(session);

                    c1AObjectId = c1A.Id;
                    c2AObjectId = c2A.Id;

                    c1A.C1C2one2one = c2A;

                    session.Commit();
                }

                var workspace = this.Profile.CreateWorkspace(database);

                using (var session = workspace.CreateSession())
                {
                    var workC1A = (C1)session.Instantiate(c1AObjectId);
                    var workC2A = (C2)session.Instantiate(c2AObjectId);

                    workC1A.Strategy.Delete();

                    session.Sync();

                    var changeSet = session.Checkpoint();
                    var conflicts = session.Conflicts;

                    Assert.AreEqual(0, changeSet.Deleted.Count);
                    Assert.AreEqual(0, conflicts.Length);

                    session.Commit();
                }

                using (var session = database.CreateSession())
                {
                    var c1A = (C1)session.Instantiate(c1AObjectId);
                    var c2A = (C2)session.Instantiate(c2AObjectId);

                    Assert.IsFalse(c2A.ExistC1WhereC2one2one);
                }
            }
        }

        [Test]
        public void ConflictsSyncWithCommit()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId;
                    using (var databaseSession = database.CreateSession())
                    {
                        var c1A = C1.Create(databaseSession);

                        c1AObjectId = c1A.Id;

                        c1A.C1AllorsString = "a";
                        databaseSession.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);
                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var allorsString = workC1A.C1AllorsString;

                        workspaceSession.Commit();

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);

                            c1A.C1AllorsString = "b";
                            session.Commit();
                            workspaceSession.DatabaseSession.Rollback();
                        }

                        Assert.IsTrue(workspaceSession.Conflicts.Length > 0);
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }
                }
            }
        }

        [Test]
        public virtual void ConflictsSyncWithoutCommit()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId;
                    using (var databaseSession = database.CreateSession())
                    {
                        var c1A = C1.Create(databaseSession);

                        c1AObjectId = c1A.Id;

                        c1A.C1AllorsString = "a";
                        databaseSession.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);
                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var allorsString = workC1A.C1AllorsString;

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);

                            c1A.C1AllorsString = "b";
                            session.Commit();
                            workspaceSession.DatabaseSession.Rollback();
                        }

                        Assert.IsTrue(workspaceSession.Conflicts.Length > 0);
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }
                }
            }
        }

        [Test]
        public void LifeCycleOne2ManySync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId;
                    ObjectId c1BObjectId;
                    ObjectId c1CObjectId;

                    ObjectId c2AObjectId;
                    ObjectId c2BObjectId;
                    ObjectId c2CObjectId;
                    ObjectId c2DObjectId;

                    using (var session = database.CreateSession())
                    {
                        var c1A = C1.Create(session);
                        var c1B = C1.Create(session);

                        var c2A = C2.Create(session);
                        var c2B = C2.Create(session);
                        var c2C = C2.Create(session);

                        c1AObjectId = c1A.Id;
                        c1BObjectId = c1B.Id;
                        c2AObjectId = c2A.Id;
                        c2BObjectId = c2B.Id;
                        c2CObjectId = c2C.Id;

                        c1A.AddC1C2one2many(c2A);
                        c1B.AddC1C2one2many(c2B);
                        c1B.AddC1C2one2many(c2C);

                        session.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);

                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                        var workC1C = C1.Create(workspaceSession);

                        var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                        var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                        var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                        var workC2D = C2.Create(workspaceSession);

                        c1CObjectId = workC1C.Id;
                        c2DObjectId = workC2D.Id;

                        workC1B.AddC1C2one2many(workC2A);
                        workC1C.AddC1C2one2many(workC2C);
                        workC1C.AddC1C2one2many(workC2D);

                        workC1A.Strategy.Delete();
                        workC2B.Strategy.Delete();

                        for (var i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            workC2D = (C2)workspaceSession.Instantiate(c2DObjectId);

                            Assert.IsNull(workC1A);
                            Assert.IsNull(workC2B);
                        }

                        workspaceSession.Sync();

                        Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                        c1CObjectId = workC1C.Id;
                        c2DObjectId = workC2D.Id;

                        workspaceSession.Commit();
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }

                    using (var databaseSession = database.CreateSession())
                    {
                        var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                        var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                        var c1C = C1.Instantiate(databaseSession, c1CObjectId);

                        var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                        var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                        var c2C = C2.Instantiate(databaseSession, c2CObjectId);
                        var c2D = C2.Instantiate(databaseSession, c2DObjectId);

                        Assert.AreEqual(1, c1B.C1C2one2manies.Count);
                        Assert.Contains(c2A, c1B.C1C2one2manies.BaseExtent);
                        Assert.AreEqual(2, c1C.C1C2one2manies.Count);
                        Assert.Contains(c2C, c1C.C1C2one2manies.BaseExtent);
                        Assert.Contains(c2D, c1C.C1C2one2manies.BaseExtent);

                        Assert.AreEqual(c1B, c2A.C1WhereC2one2many);
                        Assert.AreEqual(c1C, c2C.C1WhereC2one2many);
                        Assert.AreEqual(c1C, c2D.C1WhereC2one2many);

                        Assert.IsNull(c1A);
                        Assert.IsNull(c2B);
                    }
                }
            }
        }

        [Test]
        public void LifecycleOne2ManySyncRollback()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (int saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId = null;
                    ObjectId c1BObjectId = null;
                    ObjectId c1CObjectId = null;
                    ObjectId c2AObjectId = null;
                    ObjectId c2BObjectId = null;
                    ObjectId c2CObjectId = null;
                    ObjectId c2DObjectId = null;

                    using (var session = database.CreateSession())
                    {
                        C1 c1A = C1.Create(session);
                        C1 c1B = C1.Create(session);
                        C1 c1C = null;

                        C2 c2A = C2.Create(session);
                        C2 c2B = C2.Create(session);
                        C2 c2C = C2.Create(session);
                        C2 c2d = null;

                        c1AObjectId = c1A.Id;
                        c1BObjectId = c1B.Id;
                        c2AObjectId = c2A.Id;
                        c2BObjectId = c2B.Id;
                        c2CObjectId = c2C.Id;

                        c1A.AddC1C2one2many(c2A);
                        c1B.AddC1C2one2many(c2B);
                        c1B.AddC1C2one2many(c2C);

                        session.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);

                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                        var workC1C = C1.Create(workspaceSession);

                        var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                        var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                        var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                        var workC2D = C2.Create(workspaceSession);

                        workC1B.AddC1C2one2many(workC2A);
                        workC1C.AddC1C2one2many(workC2C);
                        workC1C.AddC1C2one2many(workC2D);

                        workC1A.Strategy.Delete();
                        workC2B.Strategy.Delete();

                        for (int i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(workC1A.Strategy.ObjectId);
                            workC1B = (C1)workspaceSession.Instantiate(workC1B.Strategy.ObjectId);
                            workC1C = (C1)workspaceSession.Instantiate(workC1C.Strategy.ObjectId);

                            workC2A = (C2)workspaceSession.Instantiate(workC2A.Strategy.ObjectId);
                            workC2C = (C2)workspaceSession.Instantiate(workC2C.Strategy.ObjectId);
                            workC2D = (C2)workspaceSession.Instantiate(workC2D.Strategy.ObjectId);
                        }

                        for (int i = 0; i < RollbackCount; i++)
                        {
                            workspaceSession.Commit();

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            var c1B = C1.Instantiate(workspaceSession.DatabaseSession, workC1B.Strategy.ObjectId);
                            var c1C = C1.Instantiate(workspaceSession.DatabaseSession, workC1C.Strategy.ObjectId);
                            var c2A = C2.Instantiate(workspaceSession.DatabaseSession, workC2A.Strategy.ObjectId);
                            var c2C = C2.Instantiate(workspaceSession.DatabaseSession, workC2C.Strategy.ObjectId);
                            var c2D = C2.Instantiate(workspaceSession.DatabaseSession, workC2D.Strategy.ObjectId);

                            Assert.AreEqual(1, c1B.C1C2one2manies.Count);
                            Assert.Contains(c2A, c1B.C1C2one2manies.BaseExtent);
                            Assert.AreEqual(2, c1C.C1C2one2manies.Count);
                            Assert.Contains(c2C, c1C.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2D, c1C.C1C2one2manies.BaseExtent);

                            Assert.AreEqual(c1B, c2A.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2C.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2D.C1WhereC2one2many);

                            workspaceSession.Rollback();
                        }
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }
                }
            }
        }

        [Test]
        public void LifecycleOne2OneSync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId = null;
                    ObjectId c1BObjectId = null;
                    ObjectId c1CObjectId = null;

                    ObjectId c2AObjectId = null;
                    ObjectId c2BObjectId = null;
                    ObjectId c2CObjectId = null;

                    using (var session = database.CreateSession())
                    {
                        C1 c1A = C1.Create(session);
                        C1 c1B = C1.Create(session);
                        C1 c1C = null;

                        C2 c2A = C2.Create(session);
                        C2 c2B = C2.Create(session);
                        C2 c2c = null;

                        c1AObjectId = c1A.Id;
                        c1BObjectId = c1B.Id;
                        c2AObjectId = c2A.Id;
                        c2BObjectId = c2B.Id;

                        c1A.C1C2one2one = c2A;
                        c1B.C1C2one2one = c2B;

                        session.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);

                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                        var workC1C = C1.Create(workspaceSession);

                        var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                        var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                        var workC2C = C2.Create(workspaceSession);

                        workC1C.C1C2one2one = workC2C;

                        c1CObjectId = workC1C.Strategy.ObjectId;
                        c2CObjectId = workC2C.Strategy.ObjectId;

                        workC1A.Strategy.Delete();
                        workC2B.Strategy.Delete();

                        for (int i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);

                            Assert.IsNull(workC1A);
                            Assert.IsNull(workC2B);
                        }

                        var conflicts = workspaceSession.Conflicts;

                        workspaceSession.Sync();

                        c1CObjectId = workC1C.Strategy.ObjectId;
                        c2CObjectId = workC2C.Strategy.ObjectId;

                        Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                        workspaceSession.Commit();
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }

                    using (var databaseSession = database.CreateSession())
                    {
                        var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                        var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                        var c1C = C1.Instantiate(databaseSession, c1CObjectId);
                        var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                        var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                        var c2C = C2.Instantiate(databaseSession, c2CObjectId);

                        Assert.IsFalse(c2A.ExistC1WhereC2one2one);
                        Assert.IsFalse(c1B.ExistC1C2one2one);
                        Assert.IsTrue(c1C.ExistC1C2one2one);

                        Assert.IsNull(c1A);
                        Assert.IsNull(c2B);
                        Assert.IsNotNull(c2C);
                    }
                }
            }
        }

        [Test]
        public void LifecycleOne2OneSyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId = null;
                    ObjectId c2AObjectId = null;

                    using (var session = database.CreateSession())
                    {
                        C1 c1A = C1.Create(session);
                        C2 c2A = C2.Create(session);

                        c1AObjectId = c1A.Id;
                        c2AObjectId = c2A.Id;

                        c1A.C1C2one2one = c2A;
                        session.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);

                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC2A = workC1A.C1C2one2one;

                        for (int i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(workC1A.Strategy.ObjectId);
                            workC2A = (C2)workspaceSession.Instantiate(workC2A.Strategy.ObjectId);
                        }

                        Assert.IsNotNull(workC1A);
                        Assert.IsNotNull(workC2A);

                        Assert.AreEqual(workC2A, workC1A.C1C2one2one);

                        workspaceSession.Commit();
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }

                    using (var session = database.CreateSession())
                    {
                        var c2A = (C2)session.Instantiate(c2AObjectId);
                        c2A.Strategy.Delete();
                        session.Commit();
                    }

                    workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);

                        Assert.AreEqual(2, workspaceSession.Conflicts.Length);

                        foreach (var conflict in workspaceSession.Conflicts)
                        {
                            if (conflict.ObjectId.Equals(c2AObjectId))
                            {
                                Assert.AreEqual(null, conflict.RoleType);
                            }
                            else
                            {
                                Assert.AreEqual(c1AObjectId, conflict.ObjectId);
                                Assert.AreEqual(RoleTypes.C1C2one2one, conflict.RoleType);
                            }
                        }

                        workspaceSession.Resolve(workspaceSession.Conflicts);

                        workspaceSession.Sync();

                        workspaceSession.Commit();
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }

                    using (var databaseSession = database.CreateSession())
                    {
                        var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                        var c2A = C2.Instantiate(databaseSession, c2AObjectId);

                        Assert.IsNotNull(c1A);
                        Assert.IsNull(c2A);
                    }
                }
            }
        }

        [Test]
        public void LifecycleOne2OneSyncRollback()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId = null;
                    ObjectId c1BObjectId = null;
                    ObjectId c1CObjectId = null;
                    ObjectId c2AObjectId = null;
                    ObjectId c2BObjectId = null;
                    ObjectId c2CObjectId = null;

                    using (var session = database.CreateSession())
                    {
                        var c1A = C1.Create(session);
                        var c1B = C1.Create(session);
                        C1 c1C = null;

                        var c2A = C2.Create(session);
                        var c2B = C2.Create(session);
                        C2 c2C = null;

                        c1AObjectId = c1A.Id;
                        c1BObjectId = c1B.Id;
                        c2AObjectId = c2A.Id;
                        c2BObjectId = c2B.Id;

                        c1A.C1C2one2one = c2A;
                        c1B.C1C2one2one = c2B;

                        session.Commit();
                    }

                    var workspace = this.Profile.CreateWorkspace(database);

                    var workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                        var workC1C = C1.Create(workspaceSession);

                        var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                        var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                        var workC2C = C2.Create(workspaceSession);

                        workC1C.C1C2one2one = workC2C;

                        var workC1CId = workC1C.Strategy.ObjectId;
                        var workC2CId = workC2C.Strategy.ObjectId;

                        workC1A.Strategy.Delete();
                        workC2B.Strategy.Delete();

                        for (int i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(workC1A.Strategy.ObjectId);
                            workC1B = (C1)workspaceSession.Instantiate(workC1B.Strategy.ObjectId);
                            workC1C = (C1)workspaceSession.Instantiate(workC1C.Strategy.ObjectId);

                            workC2A = (C2)workspaceSession.Instantiate(workC2A.Strategy.ObjectId);
                            workC2B = (C2)workspaceSession.Instantiate(workC2B.Strategy.ObjectId);
                            workC2C = (C2)workspaceSession.Instantiate(workC2C.Strategy.ObjectId);
                        }

                        for (var i = 0; i < RollbackCount; i++)
                        {
                            workspaceSession.Commit();

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            var c1B = C1.Instantiate(workspaceSession.DatabaseSession, workC1B.Strategy.ObjectId);
                            var c1C = C1.Instantiate(workspaceSession.DatabaseSession, workC1C.Strategy.ObjectId);
                            var c2A = C2.Instantiate(workspaceSession.DatabaseSession, workC2A.Strategy.ObjectId);

                            Assert.IsFalse(c2A.ExistC1WhereC2one2one);
                            Assert.IsFalse(c1B.ExistC1C2one2one);
                            Assert.IsTrue(c1C.ExistC1C2one2one);

                            workspaceSession.DatabaseSession.Rollback();
                            workspaceSession.Rollback();

                            workspaceSession.Rollback();
                        }
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }
                }
            }
        }

        [Test]
        public void LifecycleSync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    var database = this.Profile.CreateDatabase();
                    database.Init();

                    ObjectId c1AObjectId = null;
                    ObjectId c1BObjectId = null;
                    ObjectId c1CObjectId = null;

                    using (var session = database.CreateSession())
                    {
                        C1 c1A = C1.Create(session);
                        C1 c1B = C1.Create(session);

                        c1AObjectId = c1A.Id;
                        c1BObjectId = c1B.Id;

                        session.Commit();
                    }

                    IWorkspace workspace = this.Profile.CreateWorkspace(database);

                    IWorkspaceSession workspaceSession = workspace.CreateSession();
                    try
                    {
                        var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                        var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                        var workC1C = C1.Create(workspaceSession);

                        c1CObjectId = workC1C.Strategy.ObjectId;
                        var preSyncC1CObjectId = c1CObjectId;

                        workC1A.Strategy.Delete();

                        for (int i = 0; i < saveRepeat; i++)
                        {
                            workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                            workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);
                        }

                        workspaceSession.Sync();

                        Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                        c1CObjectId = workC1C.Strategy.ObjectId;

                        workspaceSession.Commit();

                        var c1A = C1.Instantiate(workspaceSession.DatabaseSession, c1AObjectId);
                        var c1B = C1.Instantiate(workspaceSession.DatabaseSession, c1BObjectId);
                        var c1C = C1.Instantiate(workspaceSession.DatabaseSession, c1CObjectId);

                        Assert.AreEqual(workC1C.Strategy.ObjectId, c1C.Strategy.ObjectId);
                        Assert.AreNotEqual(preSyncC1CObjectId, c1C.Strategy.ObjectId);
                        Assert.AreNotEqual(preSyncC1CObjectId, workC1C.Strategy.ObjectId);

                        Assert.IsNull(c1A);
                        Assert.IsFalse(c1B.Strategy.IsDeleted);
                        Assert.IsFalse(c1C.Strategy.IsDeleted);
                    }
                    finally
                    {
                        workspaceSession.Rollback();
                    }
                }
            }
        }

        [Test]
        public void LifecycleSyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                var database = this.Profile.CreateDatabase();
                database.Init();
                ObjectId c1AObjectId = null;

                using (var session = database.CreateSession())
                {
                    C1 c1A = C1.Create(session);
                    c1AObjectId = c1A.Id;
                    session.Commit();
                }

                var workspace = this.Profile.CreateWorkspace(database);

                using (var workspaceSession = workspace.CreateSession())
                {
                    var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                    workspaceSession.Commit();
                }

                using (var databaseSession = database.CreateSession())
                {
                    var c1A = databaseSession.Instantiate(c1AObjectId);
                    c1A.Strategy.Delete();
                    databaseSession.Commit();
                }

                using (var workspaceSession = workspace.CreateSession())
                {
                    var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);

                    Assert.IsTrue(workspaceSession.Conflicts.Length > 0);

                    var conflicts = workspaceSession.Conflicts;

                    Assert.AreEqual(1, conflicts.Length);

                    var conflict = conflicts[0];

                    Assert.AreEqual(workC1A.Strategy.ObjectId, conflict.ObjectId);
                    Assert.IsNull(conflict.RoleType);

                    workspaceSession.Resolve(conflict);

                    workspaceSession.Sync();
                }
            }
        }

        [Test]
        public void One2ManySync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;

                        ObjectId c2AObjectId = null;
                        ObjectId c2BObjectId = null;
                        ObjectId c2CObjectId = null;
                        ObjectId c2DObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);
                            C1 c1B = C1.Create(session);
                            C1 c1C = C1.Create(session);

                            C2 c2A = C2.Create(session);
                            C2 c2B = C2.Create(session);
                            C2 c2C = C2.Create(session);
                            C2 c2D = C2.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;

                            c2AObjectId = c2A.Id;
                            c2BObjectId = c2B.Id;
                            c2CObjectId = c2C.Id;
                            c2DObjectId = c2D.Id;

                            c1A.AddC1C2one2many(c2A);
                            c1B.AddC1C2one2many(c2B);
                            c1B.AddC1C2one2many(c2C);

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                            var workC2D = (C2)workspaceSession.Instantiate(c2DObjectId);

                            workC1C.AddC1C2one2many(workC2D);
                            workC1C.AddC1C2one2many(workC2C);
                            workC1B.AddC1C2one2many(workC2A);

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                                workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                                workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                                workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                                workC2D = (C2)workspaceSession.Instantiate(c2DObjectId);
                            }

                            if (refresh)
                            {
                                Extent tempExtent = workC1A.C1C2one2manies;
                                tempExtent = workC1B.C1C2one2manies;
                                tempExtent = workC1C.C1C2one2manies;
                            }

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                            var c1C = C1.Instantiate(databaseSession, c1CObjectId);

                            var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                            var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                            var c2C = C2.Instantiate(databaseSession, c2CObjectId);
                            var c2D = C2.Instantiate(databaseSession, c2DObjectId);

                            Assert.AreEqual(0, c1A.C1C2one2manies.Count);
                            Assert.AreEqual(2, c1B.C1C2one2manies.Count);
                            Assert.AreEqual(2, c1C.C1C2one2manies.Count);

                            Assert.Contains(c2A, c1B.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2B, c1B.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2C, c1C.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2D, c1C.C1C2one2manies.BaseExtent);

                            Assert.AreEqual(c1B, c2A.C1WhereC2one2many);
                            Assert.AreEqual(c1B, c2B.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2C.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2D.C1WhereC2one2many);
                        }
                    }
                }
            }
        }

        [Test]
        public void One2ManySyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;

                        ObjectId c2AObjectId = null;
                        ObjectId c2BObjectId = null;
                        ObjectId c2CObjectId = null;
                        ObjectId c2DObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);
                            C1 c1B = C1.Create(session);
                            C1 c1C = C1.Create(session);

                            C2 c2A = C2.Create(session);
                            C2 c2B = C2.Create(session);
                            C2 c2C = C2.Create(session);
                            C2 c2D = C2.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;

                            c2AObjectId = c2A.Id;
                            c2BObjectId = c2B.Id;
                            c2CObjectId = c2C.Id;
                            c2DObjectId = c2D.Id;

                            c1A.AddC1C2one2many(c2A);
                            c1B.AddC1C2one2many(c2B);
                            c1B.AddC1C2one2many(c2C);

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                            var workC2D = (C2)workspaceSession.Instantiate(c2DObjectId);

                            workC1C.AddC1C2one2many(workC2D);
                            workC1C.AddC1C2one2many(workC2C);
                            workC1B.AddC1C2one2many(workC2A);

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                                workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                                workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                                workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                                workC2D = (C2)workspaceSession.Instantiate(c2DObjectId);
                            }

                            if (refresh)
                            {
                                Extent tempExtent = workC1A.C1C2one2manies;
                                tempExtent = workC1B.C1C2one2manies;
                                tempExtent = workC1C.C1C2one2manies;
                            }
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);
                            c1A.RemoveC1C2one2manies();
                            session.Commit();
                        }

                        workspaceSession = workspace.CreateSession();
                        try
                        {
                            IConflict[] conflicts = workspaceSession.Conflicts;

                            Assert.AreEqual(1, conflicts.Length);

                            IConflict conflict = conflicts[0];

                            Assert.AreEqual(c1AObjectId, conflict.ObjectId);
                            Assert.AreEqual(RoleTypes.C1C2one2many, conflict.RoleType);

                            workspaceSession.Resolve(conflict);

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                            var c1C = C1.Instantiate(databaseSession, c1CObjectId);

                            var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                            var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                            var c2C = C2.Instantiate(databaseSession, c2CObjectId);
                            var c2D = C2.Instantiate(databaseSession, c2DObjectId);

                            Assert.AreEqual(0, c1A.C1C2one2manies.Count);
                            Assert.AreEqual(2, c1B.C1C2one2manies.Count);
                            Assert.AreEqual(2, c1C.C1C2one2manies.Count);

                            Assert.Contains(c2A, c1B.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2B, c1B.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2C, c1C.C1C2one2manies.BaseExtent);
                            Assert.Contains(c2D, c1C.C1C2one2manies.BaseExtent);

                            Assert.AreEqual(c1B, c2A.C1WhereC2one2many);
                            Assert.AreEqual(c1B, c2B.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2C.C1WhereC2one2many);
                            Assert.AreEqual(c1C, c2D.C1WhereC2one2many);
                        }
                    }
                }
            }
        }

        [Test]
        public void One2OneSync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;

                        ObjectId c2AObjectId = null;
                        ObjectId c2BObjectId = null;
                        ObjectId c2CObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);
                            C1 c1B = C1.Create(session);
                            C1 c1C = C1.Create(session);

                            C2 c2A = C2.Create(session);
                            C2 c2B = C2.Create(session);
                            C2 c2C = C2.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;

                            c2AObjectId = c2A.Id;
                            c2BObjectId = c2B.Id;
                            c2CObjectId = c2C.Id;

                            c1A.C1C2one2one = c2A;
                            c1B.C1C2one2one = c2B;

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);

                            workC1C.C1C2one2one = workC2B;
                            workC1B.C1C2one2one = workC2A;

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                                workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                                workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                                workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                            }

                            if (refresh)
                            {
                                IObject tempObject = workC1A.C1C2one2one;
                            }

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                            var c1C = C1.Instantiate(databaseSession, c1CObjectId);
                            var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                            var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                            var c2C = C2.Instantiate(databaseSession, c2CObjectId);

                            Assert.AreEqual(null, c1A.C1C2one2one);
                            Assert.AreEqual(c2A, c1B.C1C2one2one);
                            Assert.AreEqual(c2B, c1C.C1C2one2one);

                            Assert.AreEqual(c1B, c2A.C1WhereC2one2one);
                            Assert.AreEqual(c1C, c2B.C1WhereC2one2one);
                            Assert.AreEqual(null, c2C.C1WhereC2one2one);
                        }
                    }
                }
            }
        }

        [Test]
        public void One2OneSyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;

                        ObjectId c2AObjectId = null;
                        ObjectId c2BObjectId = null;
                        ObjectId c2CObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            var c1A = C1.Create(session);
                            var c1B = C1.Create(session);
                            var c1C = C1.Create(session);

                            var c2A = C2.Create(session);
                            var c2B = C2.Create(session);
                            var c2C = C2.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;

                            c2AObjectId = c2A.Id;
                            c2BObjectId = c2B.Id;
                            c2CObjectId = c2C.Id;

                            c1A.C1C2one2one = c2A;
                            c1B.C1C2one2one = c2B;

                            session.Commit();
                        }

                        var workspace = this.Profile.CreateWorkspace(database);

                        var workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);

                            workC1C.C1C2one2one = workC2B;
                            workC1B.C1C2one2one = workC2A;

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                                workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                                workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                                workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                            }

                            if (refresh)
                            {
                                IObject tempObject = workC1A.C1C2one2one;
                                tempObject = workC1B.C1C2one2one;
                                tempObject = workC1C.C1C2one2one;
                            }

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);
                            c1A.C1C2one2one = null;
                            session.Commit();
                        }

                        workspaceSession = workspace.CreateSession();
                        try
                        {
                            IConflict[] conflicts = workspaceSession.Conflicts;

                            Assert.AreEqual(1, conflicts.Length);

                            IConflict conflict = conflicts[0];

                            Assert.AreEqual(c1AObjectId, conflict.ObjectId);
                            Assert.AreEqual(RoleTypes.C1C2one2one, conflict.RoleType);

                            workspaceSession.Resolve(conflict);

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                            var c1C = C1.Instantiate(databaseSession, c1CObjectId);
                            var c2A = C2.Instantiate(databaseSession, c2AObjectId);
                            var c2B = C2.Instantiate(databaseSession, c2BObjectId);
                            var c2C = C2.Instantiate(databaseSession, c2CObjectId);

                            Assert.AreEqual(null, c1A.C1C2one2one);
                            Assert.AreEqual(c2A, c1B.C1C2one2one);
                            Assert.AreEqual(c2B, c1C.C1C2one2one);

                            Assert.AreEqual(c1B, c2A.C1WhereC2one2one);
                            Assert.AreEqual(c1C, c2B.C1WhereC2one2one);
                            Assert.AreEqual(null, c2C.C1WhereC2one2one);
                        }
                    }
                }
            }
        }

        [Test]
        public void One2OneSyncRollback()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;

                        ObjectId c2AObjectId = null;
                        ObjectId c2BObjectId = null;
                        ObjectId c2CObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);
                            C1 c1B = C1.Create(session);
                            C1 c1C = C1.Create(session);

                            C2 c2A = C2.Create(session);
                            C2 c2B = C2.Create(session);
                            C2 c2C = C2.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;

                            c2AObjectId = c2A.Id;
                            c2BObjectId = c2B.Id;
                            c2CObjectId = c2C.Id;

                            c1A.C1C2one2one = c2A;
                            c1B.C1C2one2one = c2B;

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                            var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                            var workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                            var workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);

                            workC1C.C1C2one2one = workC2B;
                            workC1B.C1C2one2one = workC2A;

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);

                                workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                                workC2B = (C2)workspaceSession.Instantiate(c2BObjectId);
                                workC2C = (C2)workspaceSession.Instantiate(c2CObjectId);
                            }

                            if (refresh)
                            {
                                IObject tempObject = workC1A.C1C2one2one;
                                tempObject = workC1B.C1C2one2one;
                                tempObject = workC1C.C1C2one2one;
                            }

                            workspaceSession.Commit();

                            for (int i = 0; i < RollbackCount; i++)
                            {
                                IConflict[] conflicts = workspaceSession.Conflicts;

                                workspaceSession.Sync();

                                Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                                var c1A = C1.Instantiate(workspaceSession.DatabaseSession, workC1A.Strategy.ObjectId);
                                var c1B = C1.Instantiate(workspaceSession.DatabaseSession, workC1B.Strategy.ObjectId);
                                var c1C = C1.Instantiate(workspaceSession.DatabaseSession, workC1C.Strategy.ObjectId);
                                var c2A = C2.Instantiate(workspaceSession.DatabaseSession, workC2A.Strategy.ObjectId);
                                var c2B = C2.Instantiate(workspaceSession.DatabaseSession, workC2B.Strategy.ObjectId);
                                var c2C = C2.Instantiate(workspaceSession.DatabaseSession, workC2C.Strategy.ObjectId);

                                Assert.AreEqual(null, c1A.C1C2one2one);
                                Assert.AreEqual(c2A, c1B.C1C2one2one);
                                Assert.AreEqual(c2B, c1C.C1C2one2one);

                                Assert.AreEqual(c1B, c2A.C1WhereC2one2one);
                                Assert.AreEqual(c1C, c2B.C1WhereC2one2one);
                                Assert.AreEqual(null, c2C.C1WhereC2one2one);

                                workspaceSession.Rollback();
                            }
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }
                    }
                }
            }
        }

        [Test]
        public void StringSync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);
                            C1 c1B = C1.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;

                            c1A.C1AllorsString = "a";
                            c1B.C1AllorsString = null;

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);

                            string c1AString = workC1A.C1AllorsString;
                            string c1BString = workC1B.C1AllorsString;

                            workC1A.C1AllorsString = null;
                            workC1B.C1AllorsString = "a";

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            }

                            if (refresh)
                            {
                                c1AString = workC1A.C1AllorsString;
                                c1BString = workC1B.C1AllorsString;
                            }

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);

                            Assert.AreEqual(null, c1A.C1AllorsString);
                            Assert.AreEqual("a", c1B.C1AllorsString);
                        }
                    }
                }
            }
        }

        [Test]
        public void StringSyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);

                            c1AObjectId = c1A.Id;

                            c1A.C1AllorsString = "x";

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);

                            workC1A.C1AllorsString = "y";

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            }

                            if (refresh)
                            {
                                string tempString = workC1A.C1AllorsString;
                            }
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);
                            c1A.C1AllorsString = "z";
                            session.Commit();
                        }

                        workspaceSession = workspace.CreateSession();
                        try
                        {
                            IConflict[] conflicts = workspaceSession.Conflicts;

                            Assert.AreEqual(1, conflicts.Length);

                            IConflict conflict = conflicts[0];

                            Assert.AreEqual(c1AObjectId, conflict.ObjectId);
                            Assert.AreEqual(RoleTypes.C1AllorsString, conflict.RoleType);

                            workspaceSession.Resolve(conflict);

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);

                            Assert.AreEqual("z", c1A.C1AllorsString);
                        }
                    }
                }
            }
        }

        [Test]
        public void BinarySync()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;
                        ObjectId c1BObjectId = null;
                        ObjectId c1CObjectId = null;
                        ObjectId c1DObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            var c1A = C1.Create(session);
                            var c1B = C1.Create(session);
                            var c1C = C1.Create(session);
                            var c1D = C1.Create(session);

                            c1AObjectId = c1A.Id;
                            c1BObjectId = c1B.Id;
                            c1CObjectId = c1C.Id;
                            c1DObjectId = c1D.Id;

                            c1A.C1AllorsBinary = new byte[] { 1, 0, 1, 0 };
                            c1B.C1AllorsBinary = null;
                            c1C.C1AllorsBinary = new byte[0];
                            c1D.C1AllorsBinary = null;

                            session.Commit();
                        }

                        var workspace = this.Profile.CreateWorkspace(database);

                        var workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            var workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                            var workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);
                            var workC1D = (C1)workspaceSession.Instantiate(c1DObjectId);

                            byte[] c1AAllorsBinary = workC1A.C1AllorsBinary;
                            byte[] c1BAllorsBinary = workC1B.C1AllorsBinary;
                            byte[] c1CAllorsBinary = workC1C.C1AllorsBinary;
                            byte[] c1DAllorsBinary = workC1D.C1AllorsBinary;

                            workC1A.C1AllorsBinary = c1BAllorsBinary;
                            workC1B.C1AllorsBinary = c1AAllorsBinary;
                            workC1C.C1AllorsBinary = c1DAllorsBinary;
                            workC1D.C1AllorsBinary = c1CAllorsBinary;

                            for (var i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                                workC1B = (C1)workspaceSession.Instantiate(c1BObjectId);
                                workC1C = (C1)workspaceSession.Instantiate(c1CObjectId);
                                workC1D = (C1)workspaceSession.Instantiate(c1DObjectId);
                            }

                            if (refresh)
                            {
                                c1AAllorsBinary = workC1A.C1AllorsBinary;
                                c1BAllorsBinary = workC1B.C1AllorsBinary;
                                c1CAllorsBinary = workC1C.C1AllorsBinary;
                                c1DAllorsBinary = workC1D.C1AllorsBinary;
                            }

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);
                            var c1B = C1.Instantiate(databaseSession, c1BObjectId);
                            var c1C = C1.Instantiate(databaseSession, c1CObjectId);
                            var c1D = C1.Instantiate(databaseSession, c1DObjectId);

                            Assert.AreEqual(null, c1A.C1AllorsBinary);
                            Assert.AreEqual(new byte[] { 1, 0, 1, 0 }, c1B.C1AllorsBinary);
                            Assert.AreEqual(null, c1C.C1AllorsBinary);
                            Assert.AreEqual(new byte[0], c1D.C1AllorsBinary);
                        }
                    }
                }
            }
        }

        [Test]
        public void BinarySyncConflicts()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var saveRepeat in SaveRepeats)
                {
                    foreach (var refresh in FalseTrue)
                    {
                        var database = this.Profile.CreateDatabase();
                        database.Init();

                        ObjectId c1AObjectId = null;

                        using (var session = database.CreateSession())
                        {
                            C1 c1A = C1.Create(session);

                            c1AObjectId = c1A.Id;

                            c1A.C1AllorsBinary = new byte[] { 0 };

                            session.Commit();
                        }

                        IWorkspace workspace = this.Profile.CreateWorkspace(database);

                        IWorkspaceSession workspaceSession = workspace.CreateSession();
                        try
                        {
                            var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);

                            workC1A.C1AllorsBinary = new byte[] { 1 };

                            for (int i = 0; i < saveRepeat; i++)
                            {
                                workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                                workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                            }

                            if (refresh)
                            {
                                byte[] tempString = workC1A.C1AllorsBinary;
                            }

                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var session = database.CreateSession())
                        {
                            var c1A = (C1)session.Instantiate(c1AObjectId);
                            c1A.C1AllorsBinary = new byte[] { 2 };
                            session.Commit();
                        }

                        workspaceSession = workspace.CreateSession();
                        try
                        {
                            IConflict[] conflicts = workspaceSession.Conflicts;

                            Assert.AreEqual(1, conflicts.Length);

                            IConflict conflict = conflicts[0];

                            Assert.AreEqual(c1AObjectId, conflict.ObjectId);
                            Assert.AreEqual(RoleTypes.C1AllorsBinary, conflict.RoleType);

                            workspaceSession.Resolve(conflict);

                            workspaceSession.Sync();

                            Assert.IsFalse(workspaceSession.Conflicts.Length > 0);

                            workspaceSession.Commit();
                        }
                        finally
                        {
                            workspaceSession.Rollback();
                        }

                        using (var databaseSession = database.CreateSession())
                        {
                            var c1A = C1.Instantiate(databaseSession, c1AObjectId);

                            Assert.AreEqual(new byte[] { 2 }, c1A.C1AllorsBinary);
                        }
                    }
                }
            }
        }

        private IWorkspaceSession SaveLoad(IDatabase database, IWorkspace workspace, IWorkspaceSession workspaceSession)
        {
            workspaceSession.Commit();

            var stringWriter = new StringWriter();
            XmlWriter writer = new XmlTextWriter(stringWriter);
            workspace.Save(writer);
            string xml = stringWriter.ToString();
            writer.Close();

            workspace = this.Profile.CreateWorkspace(database);

            var stringReader = new StringReader(xml);
            XmlReader xmlReader = new XmlTextReader(stringReader);
            workspace.Load(xmlReader);

            workspaceSession = workspace.CreateSession();
            return workspaceSession;
        }
    }
}