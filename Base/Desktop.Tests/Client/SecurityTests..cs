using Allors.Domain;

namespace Desktop.Tests.Client
{
    using Allors;
    using Client.Pages;
    using NUnit.Framework;

    using Should;

    public class SecurityTests : Test
    {
        private Population population;

        private Person johnDoe;
        private Person janeDoe;

        private Organisation organisation;
        private UserGroup administrators;
        private UserGroup guests;

        public OrganisationEditPage Page { get; private set; }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            this.population = new Population(this.Session);

            this.johnDoe = new PersonBuilder(this.Session)
                .WithUserName("johndoe")
                .WithFirstName("John")
                .WithLastName("Doe")
                .Build();

            this.janeDoe = new PersonBuilder(this.Session)
                .WithUserName("janedoe")
                .WithFirstName("Jane")
                .WithLastName("Doe")
                .Build();

            this.organisation = new OrganisationBuilder(this.Session)
                .WithName("Acme")
                .Build();

            this.administrators = new UserGroups(this.Session).Administrators;
            this.guests = new UserGroups(this.Session).Guests;

            this.guests.AddMember(this.johnDoe);

            this.Session.Derive(true);
            this.Session.Commit();
            
            this.Driver.Navigate().GoToUrl(Test.AppUrl);
            this.Page = new OrganisationEditPage(this.Driver);
        }
        
        [Test]
        public void MakeAdministratorAfterVisitingPage()
        {
            this.Page.Login(this.johnDoe);

            this.Session.Derive(true);
            this.Session.Commit();

            this.Page
                .GoToOrganisation()
                .Edit(this.organisation)
                .EnterName("Acme 2")
                .Save()
                .SaveSuccessful.ShouldBeFalse();

            this.Session.Rollback();

            this.organisation.Name.ShouldEqual("Acme");

            this.Page.GoToHome();
            
            this.administrators.AddMember(this.johnDoe);
            this.Session.Derive(true);
            this.Session.Commit();

            this.Page
                .GoToOrganisation()
                .Edit(this.organisation)
                .EnterName("Acme 2")
                .Save();

            this.Page.SaveSuccessful.ShouldBeTrue();

            this.Session.Rollback();

            this.organisation.Name.ShouldEqual("Acme 2");
        }
    }
}