namespace Allors.Meta
{
    public partial class PartySkillClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Skill.IsRequired = true;
        }
	}
}