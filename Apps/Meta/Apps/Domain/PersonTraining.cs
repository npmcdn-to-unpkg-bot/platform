namespace Allors.Meta
{
    public partial class PersonTrainingClass
	{
	    internal override void AppsExtend()
        {
            this.Training.RoleType.IsRequired = true;

            this.ConcreteRoles.ThroughDate.IsRequiredOverride = true;
        }
	}
}