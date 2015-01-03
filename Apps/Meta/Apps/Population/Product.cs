namespace Allors.Meta
{
	using System;

	public partial class ProductInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.VatRate.IsRequired = true;
            this.Roles.SoldBy.IsRequired = true;
        }
	}
}