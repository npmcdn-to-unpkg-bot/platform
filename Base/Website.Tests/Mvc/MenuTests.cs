// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodsTests.cs" company="Allors bvba">
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
//   Defines the PersonTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Linq;
    using System.Security.Principal;

    using Allors.Web.Mvc;

    using Moq;

    using NUnit.Framework;

    using Website.Controllers;

    [TestFixture]
    public class MenuTests : DomainTest
    {
        [Test]
        public void ActiveRootMenu()
        {
            var menu = new Menu
                           {
                               new MenuItem().Action<HomeController>(c => c.Index())
                                    .Add(new MenuItem().Action<HomeController>(c => c.Contact())),
                               new MenuItem().Action<HomeController>(c => c.About())
                           };

            var menuForView = new MenuForUser(menu, "Home", "Index", null);
            foreach (var menuItemForView in menuForView)
            {
                var isActive = menuItemForView.ControllerName.Equals("Home") && menuItemForView.ActionName.Equals("Index"); 
                Assert.AreEqual(isActive, menuItemForView.IsActive);
            }
        }

        [Test]
        public void ActiveSubMenu()
        {
            var menu = new Menu
                           {
                               new MenuItem().Action<HomeController>(c => c.Index())
                                        .Add(new MenuItem().Action<HomeController>(c => c.Contact())),
                               new MenuItem().Action<HomeController>(c => c.About())
                           };

            var menuForView = new MenuForUser(menu, "Home", "Contact", null);
            foreach (var menuItemForView in menuForView)
            {
                var isActive = (menuItemForView.ControllerName.Equals("Home") && menuItemForView.ActionName.Equals("Index")) ||
                               (menuItemForView.ControllerName.Equals("Home") && menuItemForView.ActionName.Equals("Contact"));
                Assert.AreEqual(isActive, menuItemForView.IsActive);
            }
        }
        
        [Test]
        public void AuthorizeOnClassWithUser()
        {
            var menu = new Menu
                           {
                               new MenuItem().Action<DataController>(c => c.Index())
                                        .Add(new MenuItem().Action<HomeController>(c => c.Contact())),
                               new MenuItem().Action<HomeController>(c => c.About())
                           };

            var identityMock = new Mock<IIdentity>();
            identityMock.Setup(identity => identity.IsAuthenticated).Returns(true);

            var userMock = new Mock<IPrincipal>();
            userMock.Setup(user => user.Identity).Returns(identityMock.Object);

            var menuForView = new MenuForUser(menu, "Home", "Contact", userMock.Object);

            var menuItemsForView = menuForView.ToArray();

            Assert.AreEqual(2, menuItemsForView.Length);
        }

        [Test]
        public void AuthorizeOnClassWithoutUser()
        {
            var menu = new Menu
                           {
                               new MenuItem().Action<DataController>(c => c.Index())
                                        .Add(new MenuItem().Action<HomeController>(c => c.Contact())),
                               new MenuItem().Action<HomeController>(c => c.About())
                           };

            var menuForView = new MenuForUser(menu, "Home", "Contact", null);

            var menuItemsForView = menuForView.ToArray();

            Assert.AreEqual(1, menuItemsForView.Length);

            var menuItemForView = menuItemsForView[0];

            Assert.AreEqual("Home", menuItemForView.ControllerName);
            Assert.AreEqual("About", menuItemForView.ActionName);
        }
    }
}