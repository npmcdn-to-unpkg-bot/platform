namespace Allors.Meta
{
    public partial class MetaObjectState
    {
        internal override void BaseExtend()
        {
            this.Name.AddGroup(Groups.Workspace);
        }
    }
}