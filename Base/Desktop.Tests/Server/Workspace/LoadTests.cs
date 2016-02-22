using System.Web.Mvc;

namespace Desktop.Tests.Server.Workspace
{
    using System.Linq;

    using Allors;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Workspace;

    using NUnit.Framework;

    using Should;

    using Web.Controllers;

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

            var loadRequest = new SyncRequest
            {
                Objects = new[] { c1a.Id.ToString() }
            };

            var controller = new AngularController { AllorsSession = this.Session };

            // Act
            var jsonResult = (JsonResult)controller.Load(loadRequest);
            var loadResponse = (SyncResponse)jsonResult.Data;
            
            // Assert
            loadResponse.Objects.Length.ShouldEqual(1);

            var responseC1a = loadResponse.Objects[0];

            var roles = responseC1a.Roles;
            roles.Length.ShouldEqual(5);

            // Roles
            var responseC1AllorsString = roles.First(v => v[0].Equals("C1AllorsString"));
            responseC1AllorsString.Length.ShouldEqual(3);
            responseC1AllorsString[1].ShouldEqual("r");
            responseC1AllorsString[2].ShouldEqual("c1");

            var responseI1AllorsString = roles.First(v => v[0].Equals("I1AllorsString"));
            responseI1AllorsString.Length.ShouldEqual(3);
            responseI1AllorsString[1].ShouldEqual("r");
            responseI1AllorsString[2].ShouldEqual("i1");

            // Null's
            var responseC1AllorsBoolean = roles.First(v => v[0].Equals("C1AllorsBinary"));
            responseC1AllorsBoolean.Length.ShouldEqual(2);
            responseC1AllorsBoolean[1].ShouldEqual("r");

            var responseC1One2One = roles.First(v => v[0].Equals("C1C1One2One"));
            responseC1One2One.Length.ShouldEqual(2);
            responseC1One2One[1].ShouldEqual("r");

            var responseC1One2Many = roles.First(v => v[0].Equals("C1C1One2Manies"));
            responseC1One2Many.Length.ShouldEqual(2);
            responseC1One2Many[1].ShouldEqual("r");

            // Methods
            var methods = responseC1a.Methods;
            methods.Length.ShouldEqual(1);

            var responseClassMethod = methods.First(v => v[0].Equals("ClassMethod"));
            responseClassMethod.Length.ShouldEqual(2);
            responseClassMethod[1].ShouldEqual(string.Empty);
        }

        [Test]
        public void Administrator()
        {
            // Arrange
            var administrator = new People(this.Session).FindBy(M.Person.UserName, Users.AdministratorUserName);
           
            this.Session.Derive(true);

            var c1a = new C1Builder(this.Session)
               .WithC1AllorsString("c1")
               .WithI1AllorsString("i1")
               .WithI12AllorsString("i12")
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var loadRequest = new SyncRequest
            {
                Objects = new[] { c1a.Id.ToString() }
            };

            var controller = new AngularController { AllorsSession = this.Session, AllorsUser = administrator};

            // Act
            var jsonResult = (JsonResult)controller.Load(loadRequest);
            var loadResponse = (SyncResponse)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(1);

            var responseC1a = loadResponse.Objects[0];

            var roles = responseC1a.Roles;
            roles.Length.ShouldEqual(5);

            // Roles
            var responseC1AllorsString = roles.First(v => v[0].Equals("C1AllorsString"));
            responseC1AllorsString.Length.ShouldEqual(3);
            responseC1AllorsString[1].ShouldEqual("rw");
            responseC1AllorsString[2].ShouldEqual("c1");

            var responseI1AllorsString = roles.First(v => v[0].Equals("I1AllorsString"));
            responseI1AllorsString.Length.ShouldEqual(3);
            responseI1AllorsString[1].ShouldEqual("rw");
            responseI1AllorsString[2].ShouldEqual("i1");

            // Null's
            var responseC1AllorsBoolean = roles.First(v => v[0].Equals("C1AllorsBinary"));
            responseC1AllorsBoolean.Length.ShouldEqual(2);
            responseC1AllorsBoolean[1].ShouldEqual("rw");

            var responseC1One2One = roles.First(v => v[0].Equals("C1C1One2One"));
            responseC1One2One.Length.ShouldEqual(2);
            responseC1One2One[1].ShouldEqual("rw");

            var responseC1One2Many = roles.First(v => v[0].Equals("C1C1One2Manies"));
            responseC1One2Many.Length.ShouldEqual(2);
            responseC1One2Many[1].ShouldEqual("rw");

            // Methods
            var methods = responseC1a.Methods;
            methods.Length.ShouldEqual(1);

            var responseClassMethod = methods.First(v => v[0].Equals("ClassMethod"));
            responseClassMethod.Length.ShouldEqual(2);
            responseClassMethod[1].ShouldEqual("x");
        }
    }
}