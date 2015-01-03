namespace Allors.Meta
{
	using System;

	public partial class EmploymentApplicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Person.IsRequired = true;
            this.Roles.ApplicationDate.IsRequired = true;
            this.Roles.Position.IsRequired = true;
        }
	}
}