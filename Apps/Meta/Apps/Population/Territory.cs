namespace Allors.Meta
{
	using System;

	public partial class TerritoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}