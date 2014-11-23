//------------------------------------------------------------------------------------------------- 
// <copyright file="MergeTest.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the AssociationTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System.IO;

    using Allors.Testing.Webforms;

    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        private Application application;

        [TestFixtureSetUp]
        public virtual void Setup()
        {
            var directory = new DirectoryInfo(@"..\..\..\Allors.Testing.Webforms.Tests.WebApplication");
            var applicationFactory = new ApplicationFactory(directory, "/");
            this.application = applicationFactory.Create();
        }

        [TestFixtureTearDown]
        public virtual void Dispose()
        {
        }

        [Test]
        public void TextBox()
        {
            this.application.Test(typeof(TextBoxTest));
        }
    }
}