namespace Allors.Meta
{
	using System;

	public partial class OrganisationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
            
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
        }
	}
}