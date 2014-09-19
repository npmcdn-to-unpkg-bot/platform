// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginTests.cs" company="Allors bvba">
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

namespace Allors.Security
{
    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class LoginTests : DomainTest
    {
        [Test]
        public void WhenDeletingUserThenLoginShouldAlsBeDeleted()
        {
            var user = new PersonBuilder(this.DatabaseSession).WithUserName("User").Build();
            var login = new LoginBuilder(this.DatabaseSession).WithUser(user).WithProvider("MyProvider").WithKey("XXXYYYZZZ").Build();

            this.DatabaseSession.Derive();

            user.Delete();

            this.DatabaseSession.Derive();

            Assert.IsTrue(login.Strategy.IsDeleted);
        }
    }
}
