namespace Allors.Meta
{
    public partial class MetaObjectState
    {
        internal override void BaseExtend()
        {
            this.Name.RelationType.AddGroup(Groups.Workspace);
        }
    }
}