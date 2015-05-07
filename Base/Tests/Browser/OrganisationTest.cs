namespace Browser
{
    using Allors.Domain;

    using NUnit.Framework;

    using OpenQA.Selenium;

    using Should;

    using SpecsFor.Mvc.Helpers;

    using Areas.Default.Tests.OrganisationMvc;

    public class OrganisationTest : BrowserTest
    {
        public class when_adding_an_organisation : BrowserTest
        {
            protected override void Given()
            {
            }

            protected override void When()
            {
                this.SUT.NavigateTo<OrganisationController>(c => c.Add("/"));
                
                var addForm = this.SUT.FindFormFor<Add>(); 
                addForm.Field(m => m.Name).SetValueTo("Acme");

                // Can not use addForm.Submit();

                var button = this.SUT.Browser.FindElementsByName("command")[0];
                button.Click();
            }

            [Test]
            public void then_it_displays_the_index_screen()
            {
                this.SUT.Route.ShouldMapTo<OrganisationController>(c => c.Index());
            }

            [Test]
            public void then_the_organisation_is_added()
            {
                var acme = new Organisations(this.Session).FindBy(Organisation.Meta.Name, "Acme");
                acme.ShouldNotBeNull();
            }
        }
    }
}
