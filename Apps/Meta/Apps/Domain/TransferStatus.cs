namespace Allors.Meta
{
    public partial class TransferStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.TransferObjectState.IsRequired = true;
        }
	}
}