namespace Browser
{
    using Allors.Web.Identity.Models;

    using NUnit.Framework;
    using SpecsFor.Mvc.Helpers;

    using Website.Controllers;

    public class LoginTest : BrowserTest
    {
        public class when_not_using_lessons_export_filters : BrowserTest
        {
            protected override void Given()
            {
            }

            protected override void When()
            {
                this.SUT.NavigateTo<AccountController>(c => c.Login("/"));
            }

            [Test]
            public void then_it_displays_the_login_screen()
            {
                this.SUT.Route.ShouldMapTo<AccountController>(c => c.Login(new LoginViewModel(), "%2F"));
            }

            [Test]
            public void then_the_email_and_password_should_have_default_values()
            {
                this.SUT.FindFormFor<LoginViewModel>().Field(m => m.Email).ValueShouldEqual(string.Empty);
                this.SUT.FindFormFor<LoginViewModel>().Field(m => m.Password).ValueShouldEqual(string.Empty);
            }
        }
    }
}
