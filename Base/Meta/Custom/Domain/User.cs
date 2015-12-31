namespace Allors.Meta
{
    public partial class MetaUser
    {
        internal override void CustomExtend()
        {
            this.UserEmail.RelationType.AddGroup(Groups.Workspace);

            this.UserName.RelationType.AddGroup(Groups.Workspace);
        }
    }
}