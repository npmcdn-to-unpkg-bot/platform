namespace Allors.Meta
{
	using System;

	public partial class PostalCodeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Code.IsRequired = true;
        }
	}
}