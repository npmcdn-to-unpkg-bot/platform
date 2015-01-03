namespace Allors.Meta
{
	using System;

	public partial class RequirementCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AssociatedProfessional.IsRequired = true;
            this.Roles.CommunicationEvent.IsRequired = true;
            this.Roles.Requirement.IsRequired = true;
        }
	}
}