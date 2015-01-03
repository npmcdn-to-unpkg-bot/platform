namespace Allors.Meta
{
	using System;

	public partial class ProductDeliverySkillRequirementClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Service.IsRequired = true;
            this.Roles.Skill.IsRequired = true;
        }
	}
}