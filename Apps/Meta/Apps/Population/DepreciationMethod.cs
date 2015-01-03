namespace Allors.Meta
{
	using System;

	public partial class DepreciationMethodClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Formula.IsRequired = true;
        }
	}
}