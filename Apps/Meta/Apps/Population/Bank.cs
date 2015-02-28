namespace Allors.Meta
{
    public partial class BankClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.Country.IsRequired = true;
            this.Roles.Bic.IsRequired = true;
        }
	}
}