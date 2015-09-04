// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessControlListTests.cs" Organisation="Allors bvba">
//   Copyright 2002-2011 Allors bvba.
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
    public class AccessControlListTests : DomainTest
    {
        [Test]
        public void GivenAnAuthenticationPopulatonWhenCreatingAnAccessListForGuestThenPermissionIsDenied()
        {
            var guest = new PersonBuilder(this.Session).WithUserName("guest").WithLastName("Guest").Build();
            var administrator = new PersonBuilder(this.Session).WithUserName("admin").WithLastName("Administrator").Build();
            var user = new PersonBuilder(this.Session).WithUserName("user").WithLastName("User").Build();

            Singleton.Instance(this.Session).Guest = guest;
            new UserGroups(this.Session).FindBy(UserGroups.Meta.Name, "Administrators").AddMember(administrator);

            this.Session.Derive(true);
            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                foreach (AccessControlledObject aco in this.GetObjects(session, Organisations.Meta.ObjectType))
                {
                    // When
                    var accessList = new AccessControlList(aco, guest);

                    // Then
                    Assert.IsFalse(accessList.CanExecute(Organisations.Meta.JustDoIt));
                }

                session.Rollback();
            }
        }

        [Test]
        public void GivenAUserAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.Session).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();

            this.Session.Derive(true);
            this.Session.Commit();

            new AccessControlBuilder(this.Session).WithSubject(person).WithRole(databaseRole).Build();

            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.Session).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.Session.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAUserGroupAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.Session).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();
            new UserGroupBuilder(this.Session).WithName("Group").WithMember(person).Build();

            this.Session.Derive(true);
            this.Session.Commit();

            new AccessControlBuilder(this.Session).WithSubject(person).WithRole(databaseRole).Build();

            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.Session).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.Session.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAnotherUserAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.Session).WithName("Role").WithPermission(readOrganisationName).Build();

            Assert.IsFalse(this.Session.Derive().HasErrors);

            var person = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();
            var anotherPerson = new PersonBuilder(this.Session).WithFirstName("Jane").WithLastName("Doe").Build();

            this.Session.Derive(true);
            this.Session.Commit();

            new AccessControlBuilder(this.Session).WithSubject(anotherPerson).WithRole(databaseRole).Build();
            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.Session).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.Session.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAnotherUserGroupAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.Session).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();
            new UserGroupBuilder(this.Session).WithName("Group").WithMember(person).Build();
            var anotherUserGroup = new UserGroupBuilder(this.Session).WithName("AnotherGroup").Build();

            this.Session.Derive(true);
            this.Session.Commit();

            new AccessControlBuilder(this.Session).WithSubjectGroup(anotherUserGroup).WithRole(databaseRole).Build();

            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.Session).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.Session.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAnAccessListWithWriteOperationsWhenUsingTheHasWriteOperationBeforeAnyOtherMehodThenHasWriteOperationReturnTrue()
        {
            var guest = new PersonBuilder(this.Session).WithUserName("guest").WithLastName("Guest").Build();
            var administrator = new PersonBuilder(this.Session).WithUserName("admin").WithLastName("Administrator").Build();

            Singleton.Instance(this.Session).Guest = guest;
            new UserGroups(this.Session).FindBy(UserGroups.Meta.Name, "Administrators").AddMember(administrator);

            this.Session.Derive(true);
            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                AccessControlledObject aco = new OrganisationBuilder(session).Build();
                var accessList = new AccessControlList(aco, administrator);

                Assert.IsTrue(accessList.HasWriteOperation);

                session.Rollback();
            }
        }

        [Test]
        public void DeniedPermissions()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.Session).WithName("Role").WithPermission(readOrganisationName).Build();
            var person = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();

            this.Session.Derive(true);
            this.Session.Commit();

            new AccessControlBuilder(this.Session).WithRole(databaseRole).WithSubject(person).Build();
            this.Session.Commit();

            var sessions = new ISession[] { this.Session };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.Session).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.Session.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));

                organisation.AddDeniedPermission(readOrganisationName);

                accessList = new AccessControlList(organisation, person);

                Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        private Permission FindPermission(ObjectType objectType, RoleType roleType, Operation operation)
        {
            Extent<Permission> permissions = this.Session.Extent<Permission>();
            permissions.Filter.AddEquals(Permissions.Meta.ConcreteClassPointer, objectType.Id);
            permissions.Filter.AddEquals(Permissions.Meta.OperandTypePointer, roleType.Id);
            permissions.Filter.AddEquals(Permissions.Meta.OperationEnum, operation);
            return permissions.First;
        }

        private Permission FindPermission(RoleType roleType, Operation operation)
        {
            return this.FindPermission(roleType.AssociationType.ObjectType, roleType, operation);
        }
    }
}
