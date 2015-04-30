// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllersFixture.cs" company="Allors bvba">
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
// --------------------------------------------------------------------------------------------------------------------

namespace Controllers
{
    using System.Globalization;
    using System.Threading;

    using Allors;
    using Allors.Databases.Memory.IntegerId;
    using Allors.Workspaces.Memory.IntegerId;

    using NUnit.Framework;

    using Configuration = Allors.Databases.Memory.IntegerId.Configuration;

    [SetUpFixture]
    public class ControllersFixture
    {
        [SetUp]
        public void SetUp()
        {
            var configuration = new Configuration { ObjectFactory = Config.ObjectFactory, WorkspaceFactory = new WorkspaceFactory() };
            Config.Default = new Database(configuration);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }
    }
}