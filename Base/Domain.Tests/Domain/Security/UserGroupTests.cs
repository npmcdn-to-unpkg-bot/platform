// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGroupTests.cs" company="Allors bvba">
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

namespace Domain
{
    using Allors;
    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class UserGroupTests : DomainTest
    {
        [Test]
        public void GivenNoUserGroupWhenCreatingAUserGroupWithoutANameThenUserGroupIsInvalid()
        {
            new UserGroupBuilder(this.Session).Build();

            var derivationLog = this.Session.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(1, derivationLog.Errors.Length);

            var derivationError = derivationLog.Errors[0];

            Assert.AreEqual(1, derivationError.Relations.Length);
            Assert.AreEqual(typeof(DerivationErrorRequired), derivationError.GetType());
            Assert.AreEqual((RoleType)UserGroups.Meta.Name, derivationError.Relations[0].RoleType);
        }

        [Test]
        public void GivenAUserGroupWhenCreatingAUserGroupWithTheSameNameThenUserGroupIsInvalid()
        {
            new UserGroupBuilder(this.Session).WithName("Same").Build();
            new UserGroupBuilder(this.Session).WithName("Same").Build();

            var derivationLog = this.Session.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(2, derivationLog.Errors.Length);

            foreach (var derivationError in derivationLog.Errors)
            {
                Assert.AreEqual(1, derivationError.Relations.Length);
                Assert.AreEqual(typeof(DerivationErrorUnique), derivationError.GetType());
                Assert.AreEqual((RoleType)UserGroups.Meta.Name, derivationError.Relations[0].RoleType);
            }
        }
    }
}
