namespace Allors.Meta
{
    public partial class MetaUser
    {
        internal override void CustomExtend()
        {
            this.UserEmail.AddGroup(Groups.Workspace);

            this.UserName.AddGroup(Groups.Workspace);
        }
    }
}