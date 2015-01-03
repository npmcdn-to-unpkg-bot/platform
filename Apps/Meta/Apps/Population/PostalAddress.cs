namespace Allors.Meta
{
	using System;

	public partial class PostalAddressClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.FormattedFullAddress.IsRequired = true;

            this.Roles.Address1.IsRequired = true;
		}
	}
}