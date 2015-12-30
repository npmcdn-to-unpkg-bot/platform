namespace Allors.Meta
{
    public partial class MetaOrganisation
    {
        public Tree FilterResponse;

        public Tree EditResponse;
        public Tree AngularEmployees { get; private set; }

        public Tree AngularShareholders { get; private set; }

        internal override void CustomExtend()
        {
            var organisation = this;
            var person = PersonClass.Instance;

            this.FilterResponse = new Tree(organisation)
                .Add(organisation.Owner)
                .Add(organisation.Employee);

            this.EditResponse = new Tree(organisation)
                .Add(organisation.Owner)
                .Add(organisation.Employee);
            
            this.AngularEmployees = new Tree(organisation)
                .Add(organisation.Employee);

            this.AngularShareholders = new Tree(organisation)
                .Add(organisation.Shareholder,
                    new Tree(person)
                        .Add(person.Photo));
        }
    }
}