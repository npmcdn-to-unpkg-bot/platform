namespace Allors.Meta
{
	using System;

	public partial class PerformanceNoteClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.Employee.IsRequired = true;
        }
	}
}