namespace Allors.Meta
{
	using System;

	public partial class ProfessionalAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Professional.IsRequired = true;
            this.Roles.EngagementItem.IsRequired = true;
        }
	}
}