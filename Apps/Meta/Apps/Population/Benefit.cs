namespace Allors.Meta
{
    public partial class BenefitClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}