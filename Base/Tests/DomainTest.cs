//------------------------------------------------------------------------------------------------- 
// <copyright file="DomainTest.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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
// <summary>Defines the DomainTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors
{
    using System.IO;
    using System.Xml;

    
    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    public class DomainTest
    {
        private IDatabaseSession databaseSession;

        public IDatabaseSession DatabaseSession
        {
            get { return this.databaseSession; }
        }

        [SetUp]
        public virtual void Init()
        {
            this.Init(true);
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.databaseSession.Rollback();
            this.databaseSession = null;
        }

        public IWorkspaceSession CreateWorkspaceSession()
        {
            var workspace = Databases.Default.CreateWorkspace();
            return workspace.CreateSession();
        }

        protected void Init(bool setup)
        {
            if (setup)
            {
                var stringReader = new StringReader(Fixture.DefaultXml);
                var reader = new XmlTextReader(stringReader);
                Databases.Default.Load(reader);
            }

            this.databaseSession = Databases.Default.CreateSession();
            
            SecurityCache.Invalidate();
        }

        protected IObject[] GetObjects(ISession session, Composite objectType)
        {
            if (session is IDatabaseSession)
            {
                return session.Extent(objectType);
            }

            var workspaceSess = (IWorkspaceSession)session;
            return workspaceSess.LocalExtent(objectType);
        }
    }
}