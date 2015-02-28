namespace Allors.Meta
{
    public partial class LotClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.LotNumber.IsRequired = true;
        }
	}
}