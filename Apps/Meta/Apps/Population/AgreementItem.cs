namespace Allors.Meta
{
    public partial class AgreementItemInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}