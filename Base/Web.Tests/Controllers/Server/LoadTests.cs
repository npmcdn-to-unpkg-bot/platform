using System.Linq;
using Allors.Web;
using Allors.Web.Server;
using Newtonsoft.Json;

namespace Controllers
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
 
    using NUnit.Framework;

    using Should;

    public class LoadTests : ControllersTest
    {
        [Test]
        public void Edit()
        {
            // Arrange
            var organisation = new OrganisationBuilder(this.Session).Build();
            this.Session.Derive();
            this.Session.Commit();

            var loadRequest = new LoadRequest
            {
            };

            var controller = new ServerController { AllorsSession = this.Session };

            // Act
            var jsonResult = (JsonResult)controller.Load(loadRequest);
            var loadResponse = (LoadResponse)jsonResult.Data;

            // Assert
            loadResponse.Objects.Length.ShouldEqual(0);
        }
    }
}