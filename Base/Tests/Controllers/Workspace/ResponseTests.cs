using System.Linq;
using Allors.Web;
using Website.Controllers;

namespace Controllers.Workspace
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Web.Workspace;

    using NUnit.Framework;

    using Should;

    public class ResponseTests : ControllersTest
    {
        [Test]
        public void NoRoles()
        {
            // Arrange
            var user = new PersonBuilder(this.Session)
                .WithFirstName("Koen")
                .WithLastName("Van Exem")
                .WithUserName("kvex")
                .WithUserEmail("koen@vanexem.be")
                .Build();

            this.Session.Derive();
            this.Session.Commit();

            var controller = new AngularController { AllorsSession = this.Session , AuthenticatedUser = user};

            // Act
            var jsonResult = (JsonResult)controller.Main();
            var loadResponse = (Response)jsonResult.Data;
            
            // Assert
            loadResponse.Objects.Length.ShouldEqual(1);

            var obj = loadResponse.Objects[0];
            obj[0].ShouldEqual(user.Id.ToString());
            obj[1].ShouldEqual(user.Strategy.ObjectVersion.ToString());

            loadResponse.NamedObjects.Length.ShouldEqual(1);

            var namedObject = loadResponse.NamedObjects[0];
            namedObject[0].ShouldEqual("root");
            namedObject[1].ShouldEqual(user.Id.ToString());

        }

        [Test]
        public void OneRole()
        {
            // Arrange
            var media = new MediaBuilder(this.Session)
                .Build();

            var user = new PersonBuilder(this.Session)
                .WithFirstName("Koen")
                .WithLastName("Van Exem")
                .WithUserName("kvex")
                .WithUserEmail("koen@vanexem.be")
                .WithPhoto(media)
                .Build();

            this.Session.Derive();
            this.Session.Commit();

            // Bump version number
            user.MiddleName = "x"; 

            this.Session.Derive();
            this.Session.Commit();


            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = user };

            // Act
            var jsonResult = (JsonResult)controller.Main();
            var loadResponse = (Response)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(2);

            var userObject = loadResponse.Objects.First(v => v[0].Equals(user.Id.ToString()));
            userObject[1].ShouldEqual(user.Strategy.ObjectVersion.ToString());

            var mediaObject = loadResponse.Objects.First(v => v[0].Equals(media.Id.ToString()));
            mediaObject[1].ShouldEqual(media.Strategy.ObjectVersion.ToString());

            loadResponse.NamedObjects.Length.ShouldEqual(1);

            var namedObject = loadResponse.NamedObjects[0];
            namedObject[0].ShouldEqual("root");
            namedObject[1].ShouldEqual(user.Id.ToString());

        }

        [Test]
        public void ManyRole()
        {
            // Arrange
            var koen = new PersonBuilder(this.Session)
                .WithFirstName("Koen")
                .WithLastName("Van Exem")
                .WithUserName("kvex")
                .WithUserEmail("koen@vanexem.be")
                .Build();

            var john = new PersonBuilder(this.Session)
                .WithFirstName("John")
                .WithLastName("Doe")
                .WithUserName("jod")
                .WithUserEmail("john@doe.com")
                .Build();

            var jane = new PersonBuilder(this.Session)
                .WithFirstName("Jane")
                .WithLastName("Doe")
                .WithUserName("jad")
                .WithUserEmail("jane@doe.com")
                .Build();

            var organisation = new OrganisationBuilder(this.Session)
                .WithName("Acme")
                .WithOwner(koen)
                .WithEmployee(john)
                .WithEmployee(jane)
                .Build();

            this.Session.Derive();
            this.Session.Commit();

            // Bump version number
            organisation.Description = "Acme industries";

            this.Session.Derive();
            this.Session.Commit();
            
            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = koen };

            // Act
            var jsonResult = (JsonResult)controller.Employees();
            var loadResponse = (Response)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(3);

            var responseOrganisation = loadResponse.Objects.First(v => v[0].Equals(organisation.Id.ToString()));
            responseOrganisation[1].ShouldEqual(organisation.Strategy.ObjectVersion.ToString());

            var responseJohn = loadResponse.Objects.First(v => v[0].Equals(john.Id.ToString()));
            responseJohn[1].ShouldEqual(john.Strategy.ObjectVersion.ToString());

            var responseJane = loadResponse.Objects.First(v => v[0].Equals(jane.Id.ToString()));
            responseJane[1].ShouldEqual(jane.Strategy.ObjectVersion.ToString());

            loadResponse.NamedObjects.Length.ShouldEqual(1);

            var namedObject = loadResponse.NamedObjects[0];
            namedObject[0].ShouldEqual("root");
            namedObject[1].ShouldEqual(organisation.Id.ToString());

        }

        [Test]
        public void ManyAndOneRole()
        {
            // Arrange
            var koen = new PersonBuilder(this.Session)
                .WithFirstName("Koen")
                .WithLastName("Van Exem")
                .WithUserName("kvex")
                .WithUserEmail("koen@vanexem.be")
                .Build();

            var johnMedia = new MediaBuilder(this.Session)
                .Build();

            var john = new PersonBuilder(this.Session)
                .WithFirstName("John")
                .WithLastName("Doe")
                .WithUserName("jod")
                .WithUserEmail("john@doe.com")
                .WithPhoto(johnMedia)
                .Build();

            var janeMedia = new MediaBuilder(this.Session)
                 .Build();

            var jane = new PersonBuilder(this.Session)
                .WithFirstName("Jane")
                .WithLastName("Doe")
                .WithUserName("jad")
                .WithUserEmail("jane@doe.com")
                .WithPhoto(janeMedia)
                .Build();

            var organisation = new OrganisationBuilder(this.Session)
                .WithName("Acme")
                .WithOwner(koen)
                .WithEmployee(john)
                .WithEmployee(jane)
                .Build();

            this.Session.Derive();
            this.Session.Commit();

            // Bump version number
            organisation.Description = "Acme industries";

            this.Session.Derive();
            this.Session.Commit();

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = koen };

            // Act
            var jsonResult = (JsonResult)controller.Shareholders();
            var loadResponse = (Response)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(3);

            var responseOrganisation = loadResponse.Objects.First(v => v[0].Equals(organisation.Id.ToString()));
            responseOrganisation[1].ShouldEqual(organisation.Strategy.ObjectVersion.ToString());

            var responseJohn = loadResponse.Objects.First(v => v[0].Equals(john.Id.ToString()));
            responseJohn[1].ShouldEqual(john.Strategy.ObjectVersion.ToString());

            var responseJane = loadResponse.Objects.First(v => v[0].Equals(jane.Id.ToString()));
            responseJane[1].ShouldEqual(jane.Strategy.ObjectVersion.ToString());

            loadResponse.NamedObjects.Length.ShouldEqual(1);

            var namedObject = loadResponse.NamedObjects[0];
            namedObject[0].ShouldEqual("root");
            namedObject[1].ShouldEqual(organisation.Id.ToString());
        }
    }
}