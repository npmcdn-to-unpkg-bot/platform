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

namespace Allors.Security
{
    
    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class AccessControlListTests : DomainTest
    {
        [Test]
        public void GivenAnAuthenticationPopulatonWhenCreatingAnAccessListForGuestThenPermissionIsDenied()
        {
            var guest = new PersonBuilder(this.DatabaseSession).WithUserName("guest").WithLastName("Guest").Build();
            var administrator = new PersonBuilder(this.DatabaseSession).WithUserName("admin").WithLastName("Administrator").Build();
            var user = new PersonBuilder(this.DatabaseSession).WithUserName("user").WithLastName("User").Build();

            Singleton.Instance(this.DatabaseSession).Guest = guest;
            new UserGroups(this.DatabaseSession).FindBy(UserGroups.Meta.Name, "Administrators").AddMember(administrator);

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
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
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubject(person).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAUserGroupAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();
            new UserGroupBuilder(this.DatabaseSession).WithName("Group").WithMember(person).Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubject(person).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAnotherUserAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();
            var anotherPerson = new PersonBuilder(this.DatabaseSession).WithFirstName("Jane").WithLastName("Doe").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubject(anotherPerson).WithRole(databaseRole).Build();
            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAnotherUserGroupAndAnAccessControlledObjectWhenGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();
            new UserGroupBuilder(this.DatabaseSession).WithName("Group").WithMember(person).Build();
            var anotherUserGroup = new UserGroupBuilder(this.DatabaseSession).WithName("AnotherGroup").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubjectGroup(anotherUserGroup).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

                var accessList = new AccessControlList(organisation, person);

                Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));

                session.Rollback();
            }
        }

        [Test]
        public void GivenAWorkspaceNewAccessControlledObjectWhenGettingTheAccessControlListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubject(person).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var workspaceSession = this.CreateWorkspaceSession();

            var organisation = new OrganisationBuilder(workspaceSession).WithName("Organisation").Build();

            var token = new SecurityTokenBuilder(workspaceSession).Build();
            organisation.AddSecurityToken(token);

            var role = (Role)workspaceSession.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
            var accessControl = (AccessControl)workspaceSession.Instantiate(role.AccessControlsWhereRole.First);
            accessControl.AddObject(token);

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            var accessList = new AccessControlList(organisation, person);
            accessList.CanRead(Organisations.Meta.Name);
        }

        [Test]
        public void GivenAUserGroupWhenSettingTheTokenInTheWorkspaceAndGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();
            var userGroup = new UserGroupBuilder(this.DatabaseSession).WithName("Group").WithMember(person).Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubjectGroup(userGroup).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var workspaceSession = this.CreateWorkspaceSession();

            var organisation = new OrganisationBuilder(workspaceSession).WithName("Organisation").Build();

            var token = new SecurityTokenBuilder(workspaceSession).Build();
            organisation.AddSecurityToken(token);

            var role = (Role)workspaceSession.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
            var accessControl = (AccessControl)workspaceSession.Instantiate(role.AccessControlsWhereRole.First);
            accessControl.AddObject(token);

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            var accessList = new AccessControlList(organisation, person);

            Assert.IsTrue(accessList.CanRead(Organisations.Meta.Name));
        }

        [Test]
        public void GivenAnotherUserWhenSettingTheTokenInTheWorkspaceAndGettingTheAccessControlListThenUserHasNoAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();
            var anotherPerson = new PersonBuilder(this.DatabaseSession).WithFirstName("Jane").WithLastName("Doe").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubject(anotherPerson).WithRole(databaseRole).Build();

            this.DatabaseSession.Commit();

            var workspaceSession = this.CreateWorkspaceSession();

            var organisation = new OrganisationBuilder(workspaceSession).WithName("Organisation").Build();

            var token = new SecurityTokenBuilder(workspaceSession).Build();
            organisation.AddSecurityToken(token);

            var role = (Role)workspaceSession.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
            var accessControl = (AccessControl)workspaceSession.Instantiate(role.AccessControlsWhereRole.First);
            accessControl.AddObject(token);

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            var accessList = new AccessControlList(organisation, person);

            Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));
        }

        [Test]
        public void GivenAnotherUserGroupWhenSettingTheTokenInTheWorkspaceAndGettingTheAccessListThenUserHasAccessToThePermissionsInTheRole()
        {
            var readOrganisationName = this.FindPermission(Organisations.Meta.Name, Operation.Read);
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();
            
            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var person = new PersonBuilder(this.DatabaseSession).WithLastName("Person").Build();
            new UserGroupBuilder(this.DatabaseSession).WithName("AGroup").WithMember(person).Build();
            var anotherUserGroup = new UserGroupBuilder(this.DatabaseSession).WithName("AnotherGroup").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithSubjectGroup(anotherUserGroup).WithRole(databaseRole).Build();

            var workspaceSession = this.CreateWorkspaceSession();

            var organisation = new OrganisationBuilder(workspaceSession).WithName("Organisation").Build();

            var token = new SecurityTokenBuilder(workspaceSession).Build();
            organisation.AddSecurityToken(token);

            var role = (Role)workspaceSession.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
            var accessControl = (AccessControl)workspaceSession.Instantiate(role.AccessControlsWhereRole.First);
            accessControl.AddObject(token);

            var accessList = new AccessControlList(organisation, person);

            Assert.IsFalse(accessList.CanRead(Organisations.Meta.Name));
        }

        [Test]
        public void GivenAnAccessListWithWriteOperationsWhenUsingTheHasWriteOperationBeforeAnyOtherMehodThenHasWriteOperationReturnTrue()
        {
            var guest = new PersonBuilder(this.DatabaseSession).WithUserName("guest").WithLastName("Guest").Build();
            var administrator = new PersonBuilder(this.DatabaseSession).WithUserName("admin").WithLastName("Administrator").Build();

            Singleton.Instance(this.DatabaseSession).Guest = guest;
            new UserGroups(this.DatabaseSession).FindBy(UserGroups.Meta.Name, "Administrators").AddMember(administrator);

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
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
            var databaseRole = new RoleBuilder(this.DatabaseSession).WithName("Role").WithPermission(readOrganisationName).Build();
            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").WithLastName("Doe").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            new AccessControlBuilder(this.DatabaseSession).WithRole(databaseRole).WithSubject(person).Build();
            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var organisation = new OrganisationBuilder(session).WithName("Organisation").Build();

                var token = new SecurityTokenBuilder(session).Build();
                organisation.AddSecurityToken(token);

                var role = (Role)session.Instantiate(new Roles(this.DatabaseSession).FindBy(Roles.Meta.Name, "Role"));
                var accessControl = (AccessControl)session.Instantiate(role.AccessControlsWhereRole.First);
                accessControl.AddObject(token);

                Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

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
            Extent<Permission> permissions = this.DatabaseSession.Extent<Permission>();
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
