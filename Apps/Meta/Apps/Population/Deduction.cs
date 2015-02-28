namespace Allors.Meta
{
    public partial class DeductionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.DeductionType.IsRequired = true;
        }
	}
}