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

            this.FirstName.AddGroup(Groups.Workspace);
            this.LastName.AddGroup(Groups.Workspace);
            this.MiddleName.AddGroup(Groups.Workspace);

            this.AngularHome = new Tree(person)
                    .Add(person.Photo);

            this.MainResponse = new Tree(person);

            this.SettingsResponse = new Tree(person);
        }
    }
}