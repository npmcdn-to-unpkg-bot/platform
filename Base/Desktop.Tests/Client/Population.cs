namespace Desktop.Tests.Client
{
    using Allors;
    using Allors.Domain;

    public class Population
    {
        private ISession session;

        public Person Administrator { get; }

        public Population(ISession session)
        {
            this.session = session;

            // Persons
            this.Administrator = new Persons(this.session).FindBy(Persons.Meta.UserName, @"administrator");

            this.session.Derive(true);
            this.session.Commit();
        }
    }
}