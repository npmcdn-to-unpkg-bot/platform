namespace Allors.Meta
{
	using System;

	public partial class TrainingClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}