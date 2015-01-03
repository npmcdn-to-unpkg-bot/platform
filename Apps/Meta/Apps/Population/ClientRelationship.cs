namespace Allors.Meta
{
	using System;

	public partial class ClientRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Client.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
        }
	}
}