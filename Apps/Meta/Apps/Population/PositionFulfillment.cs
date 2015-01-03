namespace Allors.Meta
{
	using System;

	public partial class PositionFulfillmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Person.IsRequired = true;
            this.Roles.Position.IsRequired = true;
        }
	}
}