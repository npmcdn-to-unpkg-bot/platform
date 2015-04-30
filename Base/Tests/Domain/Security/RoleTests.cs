// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleTests.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Domain
{
    using Allors;
    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class RoleTests : DomainTest
    {
        [Test]
        public void GivenNoRolesWhenCreatingARoleWithoutANameThenRoleIsInvalid()
        {
            new RoleBuilder(this.Session).Build();

            var derivationLog = this.Session.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(1, derivationLog.Errors.Length);

            var derivationError = derivationLog.Errors[0];

            Assert.AreEqual(1, derivationError.Relations.Length);
            Assert.AreEqual(typeof(DerivationErrorRequired), derivationError.GetType());
            Assert.AreEqual((RoleType)Roles.Meta.Name, derivationError.Relations[0].RoleType);
        }

        [Test]
        public void GivenARoleWhenCreatingARoleWithTheSameNameThenRoleIsInvalid()
        {
            new RoleBuilder(this.Session)
                .WithName("Same")
                .Build();

            new RoleBuilder(this.Session)
                .WithName("Same")
                .Build();

            var derivationLog = this.Session.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(2, derivationLog.Errors.Length);

            foreach (var derivationError in derivationLog.Errors)
            {
                Assert.AreEqual(1, derivationError.Relations.Length);
                Assert.AreEqual(typeof(DerivationErrorUnique), derivationError.GetType());
                Assert.AreEqual((RoleType)Roles.Meta.Name, derivationError.Relations[0].RoleType);
            }
        }

        [Test]
        public void GivenNoRolesWhenCreatingARoleWithoutAUniqueIdThenRoleIsValid()
        {
            var role = new RoleBuilder(this.Session)
                .WithName("Role")
                .Build();

            Assert.IsTrue(role.ExistUniqueId);

            var derivationLog = this.Session.Derive();

            Assert.IsFalse(derivationLog.HasErrors);
        }
    }
}
