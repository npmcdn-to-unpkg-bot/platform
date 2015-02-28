namespace Allors.Meta
{
    public partial class DepreciationMethodClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Formula.IsRequired = true;
        }
	}
}