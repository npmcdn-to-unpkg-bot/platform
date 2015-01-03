namespace Allors.Meta
{
	using System;

	public partial class NeededSkillClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Skill.IsRequired = true;
            this.Roles.SkillLevel.IsRequired = true;
        }
	}
}