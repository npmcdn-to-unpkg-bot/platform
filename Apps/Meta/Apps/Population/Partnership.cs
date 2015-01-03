namespace Allors.Meta
{
	using System;

	public partial class PartnershipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Partner.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
		}
	}
}