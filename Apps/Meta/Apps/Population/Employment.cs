namespace Allors.Meta
{
	using System;

	public partial class EmploymentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employer.IsRequired = true;
            this.Roles.Employee.IsRequired = true;
        }
	}
}