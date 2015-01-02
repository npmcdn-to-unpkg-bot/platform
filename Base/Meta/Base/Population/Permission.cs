namespace Allors.Meta
{
	public partial class PermissionClass
	{
        internal override void BaseExtend()
        {
            this.Roles.OperandTypePointer.IsRequired = true;
            this.Roles.ConcreteClassPointer.IsRequired = true;
            this.Roles.OperationEnum.IsRequired = true;
        }
    }
}