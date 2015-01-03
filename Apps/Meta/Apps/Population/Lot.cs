namespace Allors.Meta
{
	using System;

	public partial class LotClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.LotNumber.IsRequired = true;
        }
	}
}