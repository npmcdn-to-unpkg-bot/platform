namespace Allors.Meta
{
	using System;

	public partial class ResumeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ResumeDate.IsRequired = true;
            this.Roles.ResumeText.IsRequired = true;
        }
	}
}