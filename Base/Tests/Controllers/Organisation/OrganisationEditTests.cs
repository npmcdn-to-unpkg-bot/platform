namespace Controllers
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Web.Mvc.Models;

    using NUnit.Framework;

    using Should;

    using Areas.Default.Tests.OrganisationMvc;

    public class OrganisationEditTests : ControllersTest
    {
        [Test]
        public void Edit()
        {
            // Arrange
            var organisation = new OrganisationBuilder(this.Session).Build();
            this.Session.Derive();
            this.Session.Commit();

            var edit = new Edit
            {
                Id = organisation.Id.ToString(), 
                Description = "Hello", 
                Owner = new Select(), 
                Werknemers = new MultipleSelect()
            };

            var controller = new OrganisationController { AllorsSession = this.Session };

            // Act
            var viewResult = (ViewResult)controller.Edit(edit, Command.Save);
            
            // Assert
            viewResult.TempData.Count.ShouldEqual(0);

            organisation.Description.ShouldEqual("Hello");
        }
    }
}