//------------------------------------------------------------------------------------------------- 
// <copyright file="AccessControlTests.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the AccessControlTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Security
{
    using System.Collections;

    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class AccessControlTests : DomainTest
    {
        [Test]
        public void GivenNoAccessControlWhenCreatingAAccessControlWithoutARoleThenAccessControlIsInvalid()
        {
            var userGroup = new UserGroupBuilder(this.DatabaseSession).WithName("UserGroup").Build();
            var securityToken = new SecurityTokenBuilder(this.DatabaseSession).Build();

            new AccessControlBuilder(this.DatabaseSession)
                .WithSubjectGroup(userGroup)
                .WithObject(securityToken)
                .Build();

            var derivationLog = this.DatabaseSession.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(1, derivationLog.Errors.Length);

            var derivationError = derivationLog.Errors[0];

            Assert.AreEqual(1, derivationError.Relations.Length);
            Assert.AreEqual(typeof(DerivationErrorRequired), derivationError.GetType());
            Assert.AreEqual((RoleType)AccessControls.Meta.Role, derivationError.Relations[0].RoleType);
        }

        [Test]
        public void GivenNoAccessControlWhenCreatingAAccessControlWithoutAUserOrUserGroupThenAccessControlIsInvalid()
        {
            var securityToken = new SecurityTokenBuilder(this.DatabaseSession).Build();
            var role = new RoleBuilder(this.DatabaseSession).WithName("Role").Build();

            new AccessControlBuilder(this.DatabaseSession)
                .WithObject(securityToken)
                .WithRole(role)
                .Build();

            var derivationLog = this.DatabaseSession.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(1, derivationLog.Errors.Length);

            var derivationError = derivationLog.Errors[0];

            Assert.AreEqual(2, derivationError.Relations.Length);
            Assert.AreEqual(typeof(DerivationErrorAtLeastOne), derivationError.GetType());
            Assert.IsTrue(new ArrayList(derivationError.RoleTypes).Contains((RoleType)AccessControls.Meta.Subject));
            Assert.IsTrue(new ArrayList(derivationError.RoleTypes).Contains((RoleType)AccessControls.Meta.SubjectGroup));
        }

        [Test]
        public void GivenNoAccessControlWhenCreatingAAccessControlWithoutATokenThenAccessControlIsInvalid()
        {
            var role = new RoleBuilder(this.DatabaseSession).WithName("Role").Build();
            var user = new PersonBuilder(this.DatabaseSession).WithUserName("user").WithLastName("Doe").Build();

            new AccessControlBuilder(this.DatabaseSession)
                .WithSubject(user)
                .WithRole(role)
                .Build();

            var derivationLog = this.DatabaseSession.Derive();

            Assert.IsTrue(derivationLog.HasErrors);
            Assert.AreEqual(1, derivationLog.Errors.Length);

            var derivationError = derivationLog.Errors[0];

            Assert.AreEqual(1, derivationError.Relations.Length);
            Assert.AreEqual(typeof(DerivationErrorRequired), derivationError.GetType());
            Assert.IsTrue(new ArrayList(derivationError.RoleTypes).Contains((RoleType)AccessControls.Meta.Object));
        }
    }
}
