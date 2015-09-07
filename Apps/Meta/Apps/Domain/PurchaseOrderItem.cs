namespace Allors.Meta
{
    using System;

    public partial class PurchaseOrderItemClass
    {
        internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("3F65C670-B891-4979-B664-D47D45833AF5")) { ObjectType = this, Name = "Complete" };

            this.QuantityReceived.RoleType.IsRequired = true;

            this.CurrentObjectState.RoleType.IsRequired = true;
        }
    }
}