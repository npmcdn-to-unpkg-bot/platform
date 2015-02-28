namespace Allors.Meta
{
    public partial class ResumeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ResumeDate.IsRequired = true;
            this.Roles.ResumeText.IsRequired = true;
        }
	}
}