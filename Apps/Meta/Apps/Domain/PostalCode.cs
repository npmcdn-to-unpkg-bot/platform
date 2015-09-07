namespace Allors.Meta
{
    public partial class PostalCodeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Code.IsRequired = true;
        }
	}
}