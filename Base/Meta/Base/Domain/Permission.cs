namespace Allors.Meta
{
    public partial class MetaPermission
    {
        internal override void BaseExtend()
        {
            this.OperandTypePointer.RoleType.IsRequired = true;
            this.ConcreteClassPointer.RoleType.IsRequired = true;
            this.OperationEnum.RoleType.IsRequired = true;
        }
    }
}