// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisconnectedTest.cs" company="Allors bvba">
//   Copyright 2002-2010 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Database.Special.MySqlClient.LongId.RepeatableRead
{
    using Allors.Workspace;

    using NUnit.Framework;
    

    [TestFixture]
    public class WorkspaceTest : Special.WorkspaceTest
    {
        private readonly Profile profile = new Profile();

        private IWorkspaceSession workspaceSession;

        protected override IWorkspaceSession WorkspaceSession
        {
            get 
            {
                return this.workspaceSession ??
                       (this.workspaceSession = this.profile.CreateWorkspace().CreateSession());
            }
        }

        protected override Special.Profile Profile
        {
            get
            {
                return this.profile;
            }
        }

        protected override IWorkspace CreateWorkspace()
        {
            return this.profile.CreateWorkspace();
        }

        [SetUp]
        protected void Init()
        {
            this.profile.Init();
        }

        [TearDown]
        protected void Dispose()
        {
            if (this.WorkspaceSession != null)
            {
                this.workspaceSession.DatabaseSession.Commit();
                this.workspaceSession.Commit();
                this.workspaceSession = null;
                this.profile.Dispose();
            }
        }
    }
}