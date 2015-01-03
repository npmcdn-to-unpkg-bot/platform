namespace Allors.Meta
{
	using System;

	public partial class SalesTerritoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}