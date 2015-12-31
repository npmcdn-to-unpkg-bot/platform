namespace Allors.Meta
{
    public partial class MetaPerson
    {
        public Tree MainResponse;

        public Tree SettingsResponse;

        public Tree AngularHome { get; private set; }

        internal override void CustomExtend()
        {
            var person = this;

            this.FirstName.RelationType.AddGroup(Groups.Workspace);
            this.LastName.RelationType.AddGroup(Groups.Workspace);
            this.MiddleName.RelationType.AddGroup(Groups.Workspace);

            this.AngularHome = new Tree(person)
                    .Add(person.Photo);

            this.MainResponse = new Tree(person);

            this.SettingsResponse = new Tree(person);
        }
    }
}