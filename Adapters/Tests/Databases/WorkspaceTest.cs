// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
//
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
//
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
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
    using Allors.Populations;

    using NUnit.Framework;

    public abstract class WorkspaceTest
    {
        protected abstract IProfile Profile { get; }

        protected IDatabase Database 
        { 
            get
            {
                return (IDatabase)this.Profile.Population;
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
        public void AssocationUnit()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var connectedC1 = C1.Create(workspaceSession.DatabaseSession);

                    connectedC1.C1AllorsString = "OK!";

                    workspaceSession.DatabaseSession.Commit();

                    var workC1 = C1.Instantiate(workspaceSession, connectedC1.Id);

                    Assert.IsTrue(workC1.ExistC1AllorsString);
                    Assert.AreEqual("OK!", workC1.C1AllorsString);
                }
            }
        }

        [Test]
        public void AssocationOne()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var connectedC1 = C1.Create(workspaceSession.DatabaseSession);
                    var connectedC2 = C2.Create(workspaceSession.DatabaseSession);

                    connectedC1.C1C2one2one = connectedC2;

                    workspaceSession.DatabaseSession.Commit();

                    var workC1 = C1.Instantiate(workspaceSession, connectedC1.Id);
                    var workC2 = C2.Instantiate(workspaceSession, connectedC2.Id);

                    Assert.IsTrue(workC1.ExistC1C2one2one);
                    Assert.AreEqual(workC2, workC1.C1C2one2one);

                    Assert.IsTrue(workC2.ExistC1WhereC2one2one);
                    Assert.AreEqual(workC1, workC2.C1WhereC2one2one);
                }
            }
        }

        [Test]
        public void AssocationMany2One()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var connectedC1 = C1.Create(workspaceSession.DatabaseSession);
                    var connectedC2 = C2.Create(workspaceSession.DatabaseSession);

                    connectedC1.C1C2many2one = connectedC2;

                    workspaceSession.DatabaseSession.Commit();

                    var workC2 = C2.Instantiate(workspaceSession, connectedC2.Id);

                    Assert.IsTrue(workC2.ExistC1sWhereC2many2one);
                }
            }
        }

        [Test]
        public void AssocationMany2Many()
        {
            foreach (var init in this.Inits)
            {
                init();
                
                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var connectedC1 = C1.Create(workspaceSession.DatabaseSession);
                    var connectedC2 = C2.Create(workspaceSession.DatabaseSession);

                    connectedC1.AddC1C2many2many(connectedC2);

                    workspaceSession.DatabaseSession.Commit();

                    var workC2 = C2.Instantiate(workspaceSession, connectedC2.Id);

                    Assert.IsTrue(workC2.ExistC1sWhereC2many2many);
                }
            }
        }

        [Test]
        public void SaveAndLoad()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c2A = C2.Create(workspaceSession.DatabaseSession);

                    c1A.C1AllorsString = "a";
                    c1A.C1C2one2one = c2A;
                    c1A.AddC1C2many2many(c2A);

                    workspaceSession.DatabaseSession.Commit();

                    var workC1A = C1.Instantiate(workspaceSession, c1A.Id);
                    workC1A.RemoveC1AllorsString();
                    workC1A.RemoveC1C2one2one();
                    workC1A.RemoveC1C2many2manies();

                    var workC2A = C2.Instantiate(workspaceSession, c2A.Id);

                    var workC1B = C1.Create(workspaceSession);

                    workC1B.C1AllorsString = "string";
                    workC1B.C1C2one2one = workC2A;
                    workC1B.AddC1C2one2many(workC2A);

                    workC1B.Delete();

                    var stringWriter = new StringWriter();
                    var writer = new XmlTextWriter(stringWriter);
                    workspaceSession.Workspace.Save(writer);
                    writer.Close();

                    workspaceSession.DatabaseSession.Commit();

                    var xml = stringWriter.ToString();
                    Console.Out.WriteLine(xml);

                    var workspace2 = this.CreateWorkspace();
                    var stringReader = new StringReader(stringWriter.ToString());
                    var reader = new XmlTextReader(stringReader);
                    workspace2.Load(reader);
                    reader.Close();

                    using (var workSpace2Session = workspace2.CreateSession())
                    {
                        workC1A = C1.Instantiate(workSpace2Session, c1A.Id);

                        Assert.IsFalse(workC1A.ExistC1AllorsString);
                        Assert.IsFalse(workC1A.ExistC1C2one2one);
                        Assert.IsFalse(workC1A.ExistC1C2many2manies);

                        workC1B = C1.Instantiate(workSpace2Session, workC1B.Id);
                        Assert.IsNull(workC1B);
                    }
                }
            }
        }

        [Test]
        public void String()
        {
            foreach (var init in this.Inits)
            {
                init();
          
                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1 = C1.Create(workspaceSession.DatabaseSession);
                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);

                    c2A.C2AllorsString = "a";
                    c2B.C2AllorsString = "b";
                    c2C.C2AllorsString = null;

                    var workC1 = (C1)workspaceSession.Instantiate(c1.Strategy.ObjectId);
                    var workC2A = (C2)workspaceSession.Instantiate(c2A.Strategy.ObjectId);
                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId.ToString());
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId.ToString());

                    Assert.IsNotNull(workC1);
                    Assert.AreEqual(null, workC1.C1AllorsString);

                    Assert.IsNotNull(workC2A);
                    Assert.AreEqual("a", workC2A.C2AllorsString);

                    Assert.IsNotNull(workC2B);
                    Assert.AreEqual("b", workC2B.C2AllorsString);

                    Assert.IsNotNull(workC2C);
                    Assert.AreEqual(null, workC2C.C2AllorsString);

                    // Id & Population
                    Assert.AreEqual(c1.Strategy.ObjectId, workC1.Strategy.ObjectId);
                    Assert.AreEqual(c2A.Strategy.ObjectId, workC2A.Strategy.ObjectId);
                    Assert.AreEqual(c2B.Strategy.ObjectId, workC2B.Strategy.ObjectId);
                    Assert.AreEqual(c2C.Strategy.ObjectId, workC2C.Strategy.ObjectId);

                    Assert.AreEqual(workspaceSession.Workspace, workC1.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2C.Strategy.Session.Population);

                    Assert.AreEqual(0, workspaceSession.Conflicts.Length);

                    workspaceSession.DatabaseSession.Commit();
                    var workspaceSession2 = this.SaveLoad(
                        workspaceSession.DatabaseSession.Database,
                        workspaceSession.Workspace,
                        workspaceSession);

                    Assert.AreEqual(0, workspaceSession2.Conflicts.Length);

                    workspaceSession2.Rollback();
                }
            }
        }

        [Test]
        public void One2One()
        {
            foreach (var init in this.Inits)
            {
                init();

                                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c1B = C1.Create(workspaceSession.DatabaseSession);
                    var c1C = C1.Create(workspaceSession.DatabaseSession);
                    var c1D = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);

                    c1A.C1C2one2one = c2A;
                    c1B.C1C2one2one = c2B;
                    c1C.C1C2one2one = c2C;

                    var workC1A = (C1)workspaceSession.Instantiate(c1A.Strategy.ObjectId);
                    var workC1D = (C1)workspaceSession.Instantiate(c1D.Strategy.ObjectId);

                    var workC2A = workC1A.C1C2one2one;
                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId);
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId);
                    var workC2D = (C2)workspaceSession.Instantiate(c2D.Strategy.ObjectId);

                    var workC1B = workC2B.C1WhereC2one2one;
                    var workC1C = workC2C.C1WhereC2one2one;

                    Assert.AreEqual(workC1A, workC2A.C1WhereC2one2one);
                    Assert.AreEqual(workC2B, workC1B.C1C2one2one);

                    // Assert.AreEqual(workC2C, workC1C.C1C2one2one); Don't use Role
                    Assert.AreEqual(null, workC1D.C1C2one2one);
                    Assert.AreEqual(null, workC2D.C1WhereC2one2one);

                    // Exist, Id & Population
                    Assert.IsNotNull(workC1A);
                    Assert.IsNotNull(workC1B);
                    Assert.IsNotNull(workC1C);
                    Assert.IsNotNull(workC1D);
                    Assert.IsNotNull(workC2A);
                    Assert.IsNotNull(workC2B);
                    Assert.IsNotNull(workC2C);
                    Assert.IsNotNull(workC2D);

                    Assert.AreEqual(c1A.Strategy.ObjectId, workC1A.Strategy.ObjectId);
                    Assert.AreEqual(c1B.Strategy.ObjectId, workC1B.Strategy.ObjectId);
                    Assert.AreEqual(c1C.Strategy.ObjectId, workC1C.Strategy.ObjectId);
                    Assert.AreEqual(c1D.Strategy.ObjectId, workC1D.Strategy.ObjectId);
                    Assert.AreEqual(c2A.Strategy.ObjectId, workC2A.Strategy.ObjectId);
                    Assert.AreEqual(c2B.Strategy.ObjectId, workC2B.Strategy.ObjectId);
                    Assert.AreEqual(c2C.Strategy.ObjectId, workC2C.Strategy.ObjectId);
                    Assert.AreEqual(c2D.Strategy.ObjectId, workC2D.Strategy.ObjectId);

                    Assert.AreEqual(workspaceSession.Workspace, workC1A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2D.Strategy.Session.Population);
                }
            }
        }

        [Test]
        public void One2OneUpdated()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c1B = C1.Create(workspaceSession.DatabaseSession);
                    var c1C = C1.Create(workspaceSession.DatabaseSession);
                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);

                    c1A.C1C2one2one = c2A;
                    c1B.C1C2one2one = c2B;

                    var workC1A = (C1)workspaceSession.Instantiate(c1A.Strategy.ObjectId);
                    var workC1B = (C1)workspaceSession.Instantiate(c1B.Strategy.ObjectId);
                    var workC1C = (C1)workspaceSession.Instantiate(c1C.Strategy.ObjectId);
                    var workC2A = (C2)workspaceSession.Instantiate(c2A.Strategy.ObjectId.ToString());
                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId.ToString());
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId.ToString());

                    workC1B.C1C2one2one = workC2A;
                    workC1C.C1C2one2one = workC2C;

                    Assert.AreEqual(workC1B, workC2A.C1WhereC2one2one);
                    Assert.AreEqual(null, workC2B.C1WhereC2one2one);
                    Assert.AreEqual(workC1C, workC2C.C1WhereC2one2one);
                }
            }
        }

        [Test]
        public void One2Many()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c1B = C1.Create(workspaceSession.DatabaseSession);
                    var c1C = C1.Create(workspaceSession.DatabaseSession);
                    var c1D = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);
                    var c2E = C2.Create(workspaceSession.DatabaseSession);

                    c1A.AddC1C2one2many(c2A);
                    c1B.AddC1C2one2many(c2B);
                    c1C.AddC1C2one2many(c2C);
                    c1C.AddC1C2one2many(c2D);

                    var workC1A = (C1)workspaceSession.Instantiate(c1A.Strategy.ObjectId);
                    var workC1D = (C1)workspaceSession.Instantiate(c1D.Strategy.ObjectId);

                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId);
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId.ToString());
                    C2 workC2D = null;
                    var workC2E = (C2)workspaceSession.Instantiate(c2E.Strategy.ObjectId);

                    var workC1B = workC2B.C1WhereC2one2many;
                    var workC1C = workC2C.C1WhereC2one2many;
                    var workC2A = workC1A.C1C2one2manies[0];

                    var workC2Cd = workC1C.C1C2one2manies;
                    foreach (C2 workObject in workC2Cd)
                    {
                        if (workObject.Strategy.ObjectId.Equals(c2D.Strategy.ObjectId))
                        {
                            workC2D = workObject;
                        }
                    }

                    Assert.AreEqual(workC1A, workC2A.C1WhereC2one2many);
                    Assert.AreEqual(workC1B, workC2B.C1WhereC2one2many);
                    Assert.AreEqual(workC1C, workC2C.C1WhereC2one2many);
                    Assert.AreEqual(workC1C, workC2D.C1WhereC2one2many);
                    Assert.AreEqual(null, workC2E.C1WhereC2one2many);

                    Assert.AreEqual(1, workC1A.C1C2one2manies.Count);

                    // Assert.AreEqual(1, workC1B.C1C2one2manies.Count); Don't use Role!
                    Assert.AreEqual(2, workC1C.C1C2one2manies.Count);
                    Assert.AreEqual(0, workC1D.C1C2one2manies.Count);

                    Assert.Contains(workC2C, workC1C.C1C2one2manies.BaseExtent);
                    Assert.Contains(workC2D, workC1C.C1C2one2manies.BaseExtent);

                    // Exist, Id & Population
                    Assert.IsNotNull(workC1A);
                    Assert.IsNotNull(workC1B);
                    Assert.IsNotNull(workC1C);
                    Assert.IsNotNull(workC1D);
                    Assert.IsNotNull(workC2A);
                    Assert.IsNotNull(workC2B);
                    Assert.IsNotNull(workC2C);
                    Assert.IsNotNull(workC2D);
                    Assert.IsNotNull(workC2E);

                    Assert.AreEqual(c1A.Strategy.ObjectId, workC1A.Strategy.ObjectId);
                    Assert.AreEqual(c1B.Strategy.ObjectId, workC1B.Strategy.ObjectId);
                    Assert.AreEqual(c1C.Strategy.ObjectId, workC1C.Strategy.ObjectId);
                    Assert.AreEqual(c1D.Strategy.ObjectId, workC1D.Strategy.ObjectId);
                    Assert.AreEqual(c2A.Strategy.ObjectId, workC2A.Strategy.ObjectId);
                    Assert.AreEqual(c2B.Strategy.ObjectId, workC2B.Strategy.ObjectId);
                    Assert.AreEqual(c2C.Strategy.ObjectId, workC2C.Strategy.ObjectId);
                    Assert.AreEqual(c2D.Strategy.ObjectId, workC2D.Strategy.ObjectId);
                    Assert.AreEqual(c2E.Strategy.ObjectId, workC2E.Strategy.ObjectId);

                    Assert.AreEqual(workspaceSession.Workspace, workC1A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2E.Strategy.Session.Population);
                }
            }
        }

        [Test]
        public void Many2Many()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c1B = C1.Create(workspaceSession.DatabaseSession);
                    var c1C = C1.Create(workspaceSession.DatabaseSession);
                    var c1D = C1.Create(workspaceSession.DatabaseSession);
                    var c1E = C1.Create(workspaceSession.DatabaseSession);
                    var c1F = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);
                    var c2E = C2.Create(workspaceSession.DatabaseSession);
                    var c2F = C2.Create(workspaceSession.DatabaseSession);

                    c1A.AddC1C2many2many(c2A);
                    c1B.AddC1C2many2many(c2B);

                    c1C.AddC1C2many2many(c2C);
                    c1C.AddC1C2many2many(c2D);
                    c1D.AddC1C2many2many(c2D);
                    c1D.AddC1C2many2many(c2E);

                    var workC1A = (C1)workspaceSession.Instantiate(c1A.Strategy.ObjectId);
                    C1 workC1C = null;
                    C1 workC1D = null;
                    C1 workC1E = (C1)workspaceSession.Instantiate(c1E.Strategy.ObjectId);
                    C1 workC1F = (C1)workspaceSession.Instantiate(c1F.Strategy.ObjectId);

                    var workC2A = workC1A.C1C2many2manies[0];
                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId);
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId);
                    var workC2D = (C2)workspaceSession.Instantiate(c2D.Strategy.ObjectId);
                    C2 workC2E = null;
                    var workC2F = (C2)workspaceSession.Instantiate(c2F.Strategy.ObjectId);

                    var workC1B = workC2B.C1sWhereC2many2many[0];

                    var workC1Cd = workC2D.C1sWhereC2many2many;
                    foreach (C1 workObject in workC1Cd)
                    {
                        if (workObject.Strategy.ObjectId.Equals(c1C.Strategy.ObjectId))
                        {
                            workC1C = workObject;
                        }

                        if (workObject.Strategy.ObjectId.Equals(c1D.Strategy.ObjectId))
                        {
                            workC1D = workObject;
                        }
                    }

                    var workC2De = workC1D.C1C2many2manies;
                    foreach (C2 workObject in workC2De)
                    {
                        if (workObject.Strategy.ObjectId.Equals(c2E.Strategy.ObjectId))
                        {
                            workC2E = workObject;
                        }
                    }

                    Assert.AreEqual(1, workC2A.C1sWhereC2many2many.Count);
                    Assert.AreEqual(workC1A, workC2A.C1sWhereC2many2many[0]);

                    // Assert.AreEqual(workC2B, workC1B.C1C2many2many[0]); Don't use Role
                    // Assert.AreEqual(2, workC1C.C1C2many2many.Count); Don't use Role
                    Assert.AreEqual(2, workC1D.C1C2many2manies.Count);
                    Assert.Contains(workC2D, workC1D.C1C2many2manies.BaseExtent);
                    Assert.Contains(workC2E, workC1D.C1C2many2manies.BaseExtent);

                    Assert.AreEqual(1, workC2C.C1sWhereC2many2many.Count);
                    Assert.AreEqual(2, workC2D.C1sWhereC2many2many.Count);
                    Assert.Contains(workC1C, workC2D.C1sWhereC2many2many.BaseExtent);
                    Assert.Contains(workC1D, workC2D.C1sWhereC2many2many.BaseExtent);
                    Assert.AreEqual(1, workC2E.C1sWhereC2many2many.Count);

                    Assert.AreEqual(0, workC1F.C1C2many2manies.Count);
                    Assert.AreEqual(0, workC2F.C1sWhereC2many2many.Count);

                    // Exist, Id & Population
                    Assert.IsNotNull(workC1A);
                    Assert.IsNotNull(workC1B);
                    Assert.IsNotNull(workC1C);
                    Assert.IsNotNull(workC1D);
                    Assert.IsNotNull(workC1E);
                    Assert.IsNotNull(workC1F);
                    Assert.IsNotNull(workC2A);
                    Assert.IsNotNull(workC2B);
                    Assert.IsNotNull(workC2C);
                    Assert.IsNotNull(workC2D);
                    Assert.IsNotNull(workC2E);
                    Assert.IsNotNull(workC2F);

                    Assert.AreEqual(c1A.Strategy.ObjectId, workC1A.Strategy.ObjectId);
                    Assert.AreEqual(c1B.Strategy.ObjectId, workC1B.Strategy.ObjectId);
                    Assert.AreEqual(c1C.Strategy.ObjectId, workC1C.Strategy.ObjectId);
                    Assert.AreEqual(c1D.Strategy.ObjectId, workC1D.Strategy.ObjectId);
                    Assert.AreEqual(c1E.Strategy.ObjectId, workC1E.Strategy.ObjectId);
                    Assert.AreEqual(c1F.Strategy.ObjectId, workC1F.Strategy.ObjectId);
                    Assert.AreEqual(c2A.Strategy.ObjectId, workC2A.Strategy.ObjectId);
                    Assert.AreEqual(c2B.Strategy.ObjectId, workC2B.Strategy.ObjectId);
                    Assert.AreEqual(c2C.Strategy.ObjectId, workC2C.Strategy.ObjectId);
                    Assert.AreEqual(c2D.Strategy.ObjectId, workC2D.Strategy.ObjectId);
                    Assert.AreEqual(c2E.Strategy.ObjectId, workC2E.Strategy.ObjectId);
                    Assert.AreEqual(c2F.Strategy.ObjectId, workC2F.Strategy.ObjectId);

                    Assert.AreEqual(workspaceSession.Workspace, workC1A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1E.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1F.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2E.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2F.Strategy.Session.Population);
                }
            }
        }

        [Test]
        public void Many2One()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1A = C1.Create(workspaceSession.DatabaseSession);
                    var c1B = C1.Create(workspaceSession.DatabaseSession);
                    var c1C = C1.Create(workspaceSession.DatabaseSession);
                    var c1D = C1.Create(workspaceSession.DatabaseSession);
                    var c1E = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);

                    c1A.C1C2many2one = c2A;
                    c1B.C1C2many2one = c2B;
                    c1C.C1C2many2one = c2C;
                    c1D.C1C2many2one = c2C;

                    var workC1A = (C1)workspaceSession.Instantiate(c1A.Strategy.ObjectId);
                    C1 workC1C = null;
                    var workC1D = (C1)workspaceSession.Instantiate(c1D.Strategy.ObjectId);
                    var workC1E = (C1)workspaceSession.Instantiate(c1E.Strategy.ObjectId);

                    var workC2A = workC1A.C1C2many2one;
                    var workC2B = (C2)workspaceSession.Instantiate(c2B.Strategy.ObjectId);
                    var workC2C = (C2)workspaceSession.Instantiate(c2C.Strategy.ObjectId);
                    var workC2D = (C2)workspaceSession.Instantiate(c2D.Strategy.ObjectId);

                    var workC1B = workC2B.C1sWhereC2many2one[0];
                    var workC1Cd = workC2C.C1sWhereC2many2one;
                    foreach (C1 workObject in workC1Cd)
                    {
                        if (workObject.Strategy.ObjectId.Equals(c1C.Strategy.ObjectId))
                        {
                            workC1C = workObject;
                        }
                    }

                    Assert.AreEqual(1, workC2A.C1sWhereC2many2one.Count);
                    Assert.AreEqual(workC1A, workC2A.C1sWhereC2many2one[0]);

                    // Assert.AreEqual(workC2B, workC1B.C1C2many2one); Don't use Role
                    Assert.AreEqual(workC2C, workC1C.C1C2many2one);
                    Assert.AreEqual(workC2C, workC1D.C1C2many2one);
                    Assert.AreEqual(2, workC2C.C1sWhereC2many2one.Count);
                    Assert.Contains(workC1C, workC2C.C1sWhereC2many2one.BaseExtent);
                    Assert.Contains(workC1D, workC2C.C1sWhereC2many2one.BaseExtent);

                    Assert.AreEqual(null, workC1E.C1C2many2one);
                    Assert.AreEqual(0, workC2D.C1sWhereC2many2one.Count);

                    // Exist, Id & Population
                    Assert.IsNotNull(workC1A);
                    Assert.IsNotNull(workC1B);
                    Assert.IsNotNull(workC1C);
                    Assert.IsNotNull(workC1D);
                    Assert.IsNotNull(workC1E);
                    Assert.IsNotNull(workC2A);
                    Assert.IsNotNull(workC2B);
                    Assert.IsNotNull(workC2C);
                    Assert.IsNotNull(workC2D);

                    Assert.AreEqual(c1A.Strategy.ObjectId, workC1A.Strategy.ObjectId);
                    Assert.AreEqual(c1B.Strategy.ObjectId, workC1B.Strategy.ObjectId);
                    Assert.AreEqual(c1C.Strategy.ObjectId, workC1C.Strategy.ObjectId);
                    Assert.AreEqual(c1D.Strategy.ObjectId, workC1D.Strategy.ObjectId);
                    Assert.AreEqual(c1E.Strategy.ObjectId, workC1E.Strategy.ObjectId);
                    Assert.AreEqual(c2A.Strategy.ObjectId, workC2A.Strategy.ObjectId);
                    Assert.AreEqual(c2B.Strategy.ObjectId, workC2B.Strategy.ObjectId);
                    Assert.AreEqual(c2C.Strategy.ObjectId, workC2C.Strategy.ObjectId);
                    Assert.AreEqual(c2D.Strategy.ObjectId, workC2D.Strategy.ObjectId);

                    Assert.AreEqual(workspaceSession.Workspace, workC1A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1D.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC1E.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2A.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2B.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2C.Strategy.Session.Population);
                    Assert.AreEqual(workspaceSession.Workspace, workC2D.Strategy.Session.Population);
                }
            }
        }

        [Test]
        public void EmptyRollback()
        {
            foreach (var init in this.Inits)
            {
                init();
                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    workspaceSession.Rollback();
                    workspaceSession.Rollback();
                }
            }
        }

        [Test]
        public void EmptyCommit()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    workspaceSession.Commit();
                    workspaceSession.Commit();
                }
            }
        }

        [Test]
        public void AutoInstantiate()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1 = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);
                    var c2E = C2.Create(workspaceSession.DatabaseSession);
                    var c2F = C2.Create(workspaceSession.DatabaseSession);

                    workspaceSession.DatabaseSession.Commit();

                    var workC1 = C1.Instantiate(workspaceSession, c1.Id);

                    workC1.C1C2one2one = c2A;
                    workC1.C1C2many2one = c2B;
                    workC1.AddC1C2one2many(c2C);
                    workC1.AddC1C2many2many(c2D);

                    var workC2A = workC1.C1C2one2one;
                    var workC2B = workC1.C1C2many2one;
                    var workC2C = workC1.C1C2one2manies[0];
                    var workC2D = workC1.C1C2many2manies[0];

                    Assert.AreEqual(workspaceSession, workC2A.Strategy.Session);
                    Assert.AreEqual(workspaceSession, workC2B.Strategy.Session);
                    Assert.AreEqual(workspaceSession, workC2C.Strategy.Session);
                    Assert.AreEqual(workspaceSession, workC2D.Strategy.Session);

                    workC1.RemoveC1C2one2many(workC2C);
                    workC1.RemoveC1C2many2many(workC2D);

                    workC1.C1C2one2manies = new[] { c2E };
                    workC1.C1C2many2manies = new[] { c2F };

                    var workC2E = workC1.C1C2one2manies[0];
                    var workC2F = workC1.C1C2many2manies[0];

                    Assert.AreEqual(workspaceSession, workC2E.Strategy.Session);
                    Assert.AreEqual(workspaceSession, workC2F.Strategy.Session);
                }
            }
        }

        [Test]
        public virtual void AutoInstantiateDifferentDatabaseSession()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1 = C1.Create(workspaceSession.DatabaseSession);

                    var c2A = C2.Create(workspaceSession.DatabaseSession);
                    var c2B = C2.Create(workspaceSession.DatabaseSession);
                    var c2C = C2.Create(workspaceSession.DatabaseSession);
                    var c2D = C2.Create(workspaceSession.DatabaseSession);
                    var c2E = C2.Create(workspaceSession.DatabaseSession);
                    var c2F = C2.Create(workspaceSession.DatabaseSession);

                    workspaceSession.DatabaseSession.Commit();

                    using (var differentDatabaseSession = workspaceSession.DatabaseSession.Database.CreateSession())
                    {
                        c2A = C2.Instantiate(differentDatabaseSession, c2A.Id);
                        c2B = C2.Instantiate(differentDatabaseSession, c2B.Id);
                        c2C = C2.Instantiate(differentDatabaseSession, c2C.Id);
                        c2D = C2.Instantiate(differentDatabaseSession, c2D.Id);
                        c2E = C2.Instantiate(differentDatabaseSession, c2D.Id);
                        c2F = C2.Instantiate(differentDatabaseSession, c2D.Id);

                        var workC1 = C1.Instantiate(workspaceSession, c1.Id);

                        workC1.C1C2one2one = c2A;
                        workC1.C1C2many2one = c2B;
                        workC1.AddC1C2one2many(c2C);
                        workC1.AddC1C2many2many(c2D);

                        var workC2A = workC1.C1C2one2one;
                        var workC2B = workC1.C1C2many2one;
                        var workC2C = workC1.C1C2one2manies[0];
                        var workC2D = workC1.C1C2many2manies[0];

                        Assert.AreEqual(workspaceSession, workC2A.Strategy.Session);
                        Assert.AreEqual(workspaceSession, workC2B.Strategy.Session);
                        Assert.AreEqual(workspaceSession, workC2C.Strategy.Session);
                        Assert.AreEqual(workspaceSession, workC2D.Strategy.Session);

                        workC1.RemoveC1C2one2many(workC2C);
                        workC1.RemoveC1C2many2many(workC2D);

                        workC1.C1C2one2manies = new[] { c2E };
                        workC1.C1C2many2manies = new[] { c2F };

                        var workC2E = workC1.C1C2one2manies[0];
                        var workC2F = workC1.C1C2many2manies[0];

                        Assert.AreEqual(workspaceSession, workC2E.Strategy.Session);
                        Assert.AreEqual(workspaceSession, workC2F.Strategy.Session);
                    }
                }
            }
        }

        [Test]
        public void DeletedOneAssociation()
        {
            foreach (var init in this.Inits)
            {
                init();

                                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1 = C1.Create(workspaceSession.DatabaseSession);
                    var c2 = C2.Create(workspaceSession.DatabaseSession);
                    c1.C1C2one2one = c2;

                    workspaceSession.DatabaseSession.Commit();
                    workspaceSession.Commit();

                    var workC1 = C1.Instantiate(workspaceSession, c1.Id);
                    var workC2 = C2.Instantiate(workspaceSession, c2.Id);

                    workC1.Delete();

                    Assert.IsFalse(workC2.ExistC1WhereC2one2one);
                }
            }
        }

        [Test]
        public void DeletedManyAssociation()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = this.CreateWorkspace();
                using (var workspaceSession = workspace.CreateSession())
                {
                    var c1 = C1.Create(workspaceSession.DatabaseSession);
                    var c2 = C2.Create(workspaceSession.DatabaseSession);
                    c1.C1C2many2one = c2;

                    workspaceSession.DatabaseSession.Commit();
                    workspaceSession.Commit();

                    var workC1 = C1.Instantiate(workspaceSession, c1.Id);
                    var workC2 = C2.Instantiate(workspaceSession, c2.Id);

                    workC1.Delete();

                    Assert.IsFalse(workC2.ExistC1sWhereC2many2one);
                }
            }
        }

        [Test]
        public void SerializationOne2Many()
        {
            foreach (var init in this.Inits)
            {
                init();

                var database = this.Profile.CreateDatabase();
                database.Init();

                ObjectId c1AObjectId;
                ObjectId c1BObjectId;
                ObjectId c2AObjectId;

                using (var session = database.CreateSession())
                {
                    var c1A = C1.Create(session);
                    var c1B = C1.Create(session);
                    var c2A = C2.Create(session);

                    c1AObjectId = c1A.Id;
                    c1BObjectId = c1B.Id;
                    c2AObjectId = c2A.Id;

                    c1A.AddC1C2one2many(c2A);

                    session.Commit();
                }

                var workspace = this.Profile.CreateWorkspace(database);

                var workspaceSession = workspace.CreateSession();
                try
                {
                    var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                    var workC1B = (C1)workspaceSession.Instantiate(c1AObjectId);
                    var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);

                    workC1A.RemoveC1C2one2manies();
                    workC1B.AddC1C2one2many(workC2A);

                    workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                    workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                    workC1B = workC2A.C1WhereC2one2many;

                    workspaceSession.Commit();
                }
                finally
                {
                    workspaceSession.Rollback();
                }
            }
        }

        [Test]
        public void SerializationOne2One()
        {
            foreach (var init in this.Inits)
            {
                init();

                var database = this.Profile.CreateDatabase();
                database.Init();

                ObjectId c1AObjectId;
                ObjectId c1BObjectId;
                ObjectId c2AObjectId;

                using (var session = database.CreateSession())
                {
                    var c1A = C1.Create(session);
                    var c1B = C1.Create(session);
                    var c2A = C2.Create(session);

                    c1AObjectId = c1A.Id;
                    c1BObjectId = c1B.Id;
                    c2AObjectId = c2A.Id;

                    c1A.C1C2one2one = c2A;

                    session.Commit();
                }

                var workspace = this.Profile.CreateWorkspace(database);

                var workspaceSession = workspace.CreateSession();
                try
                {
                    var workC1A = (C1)workspaceSession.Instantiate(c1AObjectId);
                    var workC1B = (C1)workspaceSession.Instantiate(c1AObjectId);
                    var workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);

                    workC1A.RemoveC1C2one2manies();
                    workC1B.C1C2one2one = workC2A;

                    workspaceSession = this.SaveLoad(database, workspace, workspaceSession);

                    workC2A = (C2)workspaceSession.Instantiate(c2AObjectId);
                    workC1B = workC2A.C1WhereC2one2one;

                    workspaceSession.Commit();
                }
                finally
                {
                    workspaceSession.Rollback();
                }
            }
        }

        protected IWorkspace CreateWorkspace()
        {
            return this.Profile.CreateWorkspace(this.Database);
        }

        private IWorkspaceSession SaveLoad(IDatabase database, IWorkspace workspace, IWorkspaceSession workspaceSession)
        {
            workspaceSession.Commit();

            var stringWriter = new StringWriter();
            XmlWriter writer = new XmlTextWriter(stringWriter);
            workspace.Save(writer);
            var xml = stringWriter.ToString();
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