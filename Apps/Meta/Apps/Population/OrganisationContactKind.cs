namespace Allors.Meta
{
	using System;

	public partial class OrganisationContactKindClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}