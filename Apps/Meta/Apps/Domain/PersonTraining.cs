namespace Allors.Meta
{
    public partial class PersonTrainingClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Training.IsRequired = true;

            this.ConcreteRoles.ThroughDate.IsRequiredOverride = true;
        }
	}
}