namespace Allors.Meta
{
    public partial class EstimatedProductCostInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Cost.IsRequired = true;
            this.Roles.Currency.IsRequired = true;
        }
	}
}