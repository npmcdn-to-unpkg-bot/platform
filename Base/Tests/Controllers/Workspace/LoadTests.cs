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
        public void OneObject()
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
            responseC1AllorsString[1].ShouldEqual("c1");

            var responseI1AllorsString = responseC1a.Roles.First(v => v[0].Equals("I1AllorsString"));
            responseI1AllorsString[1].ShouldEqual("i1");
        }
    }
}