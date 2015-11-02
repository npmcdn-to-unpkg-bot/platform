namespace Allors.Meta
{
    public partial class UserInterface
    {
        internal override void CustomExtend()
        {
            this.UserEmail.AddGroup(Groups.Workspace);

            this.UserName.AddGroup(Groups.Workspace);
        }
    }
}