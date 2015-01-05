// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fixture.cs" company="Allors bvba">
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

namespace Allors
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Xml;

    using Allors.Domain;
    using Allors.Workspaces.Memory.IntegerId;

    using NUnit.Framework;


    [SetUpFixture]
    public class Fixture
    {
        public static string MinimalXml { get; private set; }

        public static string DefaultXml { get; private set; }

        [SetUp]
        public void SetUp()
        {
            var configuration = new Databases.Memory.IntegerId.Configuration { ObjectFactory = Config.ObjectFactory, WorkspaceFactory = new WorkspaceFactory() };
            Config.Default = new Databases.Memory.IntegerId.Database(configuration);

            SearchDatas.SkipDerivation = true;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("nl-BE");

            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                try
                {
                    new Setup(session).Apply();
                }
                catch (Exception e)
                {
                    Debugger.Break();
                }

                session.Commit();

                using (var stringWriter = new StringWriter())
                {
                    using (var writer = new XmlTextWriter(stringWriter))
                    {
                        database.Save(writer);
                        MinimalXml = stringWriter.ToString();
                    }
                }

                var singleton = Singleton.Instance(session);
                singleton.Guest = new PersonBuilder(session).WithUserName("guest").WithLastName("guest").Build();

                var administrator = new PersonBuilder(session).WithUserName("administrator").WithLastName("Administrator").Build();
                var administrators = new UserGroups(session).Administrators;
                administrators.AddMember(administrator);

                session.Derive(true);
                session.Commit();

                SearchDatas.Derive(database);

                using (var stringWriter = new StringWriter())
                {
                    using (var writer = new XmlTextWriter(stringWriter))
                    {
                        database.Save(writer);
                        DefaultXml = stringWriter.ToString();
                    }
                }

                SearchDatas.SkipDerivation = false;
            }
        }
    }
}