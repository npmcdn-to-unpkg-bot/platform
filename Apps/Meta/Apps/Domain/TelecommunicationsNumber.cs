namespace Allors.Meta
{
    public partial class TelecommunicationsNumberClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ContactNumber.IsRequired = true;
        }
	}
}