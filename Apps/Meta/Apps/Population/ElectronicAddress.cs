namespace Allors.Meta
{
	using System;

	public partial class ElectronicAddressInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.ElectronicAddressString.IsRequired = true;
        }
	}
}