namespace Allors.Meta
{
	using System;

	public partial class PayCheckClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employment.IsRequired = true;
        }
	}
}