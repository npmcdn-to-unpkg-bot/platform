namespace Allors.Meta
{
    public partial class MetaOrganisation
    {
        public Tree EditResponse;

        public Tree AngularEmployees { get; private set; }

        public Tree AngularShareholders { get; private set; }

        internal override void CustomExtend()
        {
            var organisation = this;
            var person = MetaPerson.Instance;

            this.EditResponse = new Tree(organisation)
                .Add(organisation.Owner)
                .Add(organisation.Employees);
            
            this.AngularEmployees = new Tree(organisation)
                .Add(organisation.Employees);

            this.AngularShareholders = new Tree(organisation)
                .Add(organisation.Shareholders,
                    new Tree(person)
                        .Add(person.Photo));
        }
    }
}