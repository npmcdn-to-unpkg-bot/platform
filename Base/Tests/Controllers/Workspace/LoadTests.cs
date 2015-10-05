namespace Controllers.Workspace
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Web.Workspace;

    using System.Linq;
    using Website.Controllers;

    using NUnit.Framework;

    using Should;

    public class LoadTests : ControllersTest
    {
        [Test]
        public void Guest()
        {
            // Arrange
            var c1a = new C1Builder(this.Session)
               .WithC1AllorsString("c1")
               .WithI1AllorsString("i1")
               .WithI12AllorsString("i12")
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var loadRequest = new LoadRequest
            {
                Objects = new []{ c1a.Id.ToString() }
            };

            var controller = new AngularController { AllorsSession = this.Session };

            // Act
            var jsonResult = (JsonResult)controller.Load(loadRequest);
            var loadResponse = (LoadResponse)jsonResult.Data;
            
            // Assert
            loadResponse.Objects.Length.ShouldEqual(1);

            var responseC1a = loadResponse.Objects[0];

            responseC1a.Roles.Length.ShouldEqual(2);

            var responseC1AllorsString = responseC1a.Roles.First(v => v[0].Equals("C1AllorsString"));
            responseC1AllorsString[1].ShouldEqual("r");
            responseC1AllorsString[2].ShouldEqual("c1");

            var responseI1AllorsString = responseC1a.Roles.First(v => v[0].Equals("I1AllorsString"));
            responseI1AllorsString[1].ShouldEqual("r");
            responseI1AllorsString[2].ShouldEqual("i1");
        }

        [Test]
        public void Administrator()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .WithC1AllorsString("c1")
               .WithI1AllorsString("i1")
               .WithI12AllorsString("i12")
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var loadRequest = new LoadRequest
            {
                Objects = new[] { c1a.Id.ToString() }
            };

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = administrator};

            // Act
            var jsonResult = (JsonResult)controller.Load(loadRequest);
            var loadResponse = (LoadResponse)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(1);

            var responseC1a = loadResponse.Objects[0];

            responseC1a.Roles.Length.ShouldEqual(2);

            var responseC1AllorsString = responseC1a.Roles.First(v => v[0].Equals("C1AllorsString"));
            responseC1AllorsString.Length.ShouldEqual(2);
            responseC1AllorsString[1].ShouldEqual("rw");
            responseC1AllorsString[2].ShouldEqual("c1");

            var responseC1AllorsBoolean = responseC1a.Roles.First(v => v[0].Equals("C1AllorsBoolean"));
            responseC1AllorsBoolean.Length.ShouldEqual(2);
            responseC1AllorsBoolean[1].ShouldEqual("rw");

            var responseI1AllorsString = responseC1a.Roles.First(v => v[0].Equals("I1AllorsString"));
            responseI1AllorsString.Length.ShouldEqual(2);
            responseI1AllorsString[1].ShouldEqual("rw");
            responseI1AllorsString[2].ShouldEqual("i1");

        }

    }
}