namespace Allors.Meta
{
    public partial class PayCheckClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employment.IsRequired = true;
        }
	}
}