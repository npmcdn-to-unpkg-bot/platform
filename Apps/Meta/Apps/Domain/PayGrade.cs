namespace Allors.Meta
{
    public partial class PayGradeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}