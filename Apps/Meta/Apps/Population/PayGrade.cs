namespace Allors.Meta
{
	using System;

	public partial class PayGradeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}