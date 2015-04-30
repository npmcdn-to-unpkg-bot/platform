namespace Controllers
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Web.Identity;
    using Allors.Web.Identity.Models;

    using NUnit.Framework;

    using Should;

    using Website.Controllers;

    public class LoginEditTests : ControllersTest
    {
        [Test]
        public void Edit()
        {
            // Arrange
            var person = new PersonBuilder(this.Session).Build();
            this.Session.Derive(true);
            this.Session.Commit();

            var userManager = new IdentityUserManager(new UserStore());
            var signinManager = new IdentitySignInManager(userManager, null);

            var model = new AddPhoneNumberViewModel
            {
                Number = "+32 15 494949"
            };

            var controller = new ManageController(userManager, signinManager);

            // Act
            var viewResult = (ViewResult)controller.AddPhoneNumber(model);
            
            // Assert
            viewResult.TempData.Count.ShouldEqual(0);
        }
    }
}