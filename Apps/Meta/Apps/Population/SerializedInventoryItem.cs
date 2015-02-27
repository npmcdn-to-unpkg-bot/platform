namespace Allors.Meta
{
    public partial class SerializedInventoryItemClass
    {
        internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.SerialNumber.IsRequired = true;
            this.Roles.SerialNumber.IsUnique = true;
        }
    }
}