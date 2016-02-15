namespace Allors.Meta
{
    public partial class MetaPerson
    {
        public Tree AngularHome;

        internal override void CustomExtend()
        {
            this.Delete.AddGroup("Workspace");

            this.FirstName.RelationType.AddGroup(Groups.Workspace);
            this.LastName.RelationType.AddGroup(Groups.Workspace);
            this.MiddleName.RelationType.AddGroup(Groups.Workspace);
            
            var person = this;
            this.AngularHome = new Tree(person)
                    .Add(person.Photo);
        }
    }
}