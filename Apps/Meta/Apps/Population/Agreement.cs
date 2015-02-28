namespace Allors.Meta
{
    public partial class AgreementInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}