// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerTest.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
//   // Dual Licensed under
//   //   a) the General Public Licence v3 (GPL)
//   //   b) the Allors License
//   // The GPL License is included in the file gpl.txt.
//   // The Allors License is an addendum to your contract.
//   // Allors Platform is distributed in the hope that it will be useful,
//   // but WITHOUT ANY WARRANTY; without even the implied warranty of
//   // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   // GNU General Public License for more details.
//   // For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   Defines the ControllerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Controllers
{
    using System.Globalization;
    using System.Threading;

    using Allors;
    using Allors.Databases.Memory.IntegerId;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Workspaces.Memory.LongId;

    using NUnit.Framework;

    using Configuration = Allors.Databases.Memory.IntegerId.Configuration;

    /// <summary>
    /// The controller test.
    /// </summary>
    public class ControllersTest
    {
        /// <summary>
        /// Gets the database session.
        /// </summary>
        protected IDatabaseSession Session { get; private set; }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.Setup(true);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            this.Session.Rollback();
            this.Session = null;
        }

        /// <summary>
        /// The create workspace session.
        /// </summary>
        /// <returns>
        /// The <see cref="IWorkspaceSession"/>.
        /// </returns>
        public IWorkspaceSession CreateWorkspaceSession()
        {
            var workspace = Config.Default.CreateWorkspace();
            return workspace.CreateSession();
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="populate">
        /// The setup.
        /// </param>
        protected void Setup(bool populate)
        {
            var configuration = new Configuration { ObjectFactory = Config.ObjectFactory, WorkspaceFactory = new WorkspaceFactory() };
            Config.Default = new Database(configuration);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("nl-BE");

            var database = Config.Default;
            database.Init();

            this.Session = Config.Default.CreateSession();

            if (populate)
            {
                new Setup(this.Session).Apply();
                this.Session.Commit();
            }

            SecurityCache.Invalidate();
        }

        /// <summary>
        /// The get objects.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <returns>
        /// The <see cref="IObject[]"/>.
        /// </returns>
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