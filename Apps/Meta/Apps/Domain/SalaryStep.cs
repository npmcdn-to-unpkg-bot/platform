namespace Allors.Meta
{
    public partial class SalaryStepClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ModifiedDate.IsRequired = true;
            this.Roles.Amount.IsRequired = true;
        }
	}
}