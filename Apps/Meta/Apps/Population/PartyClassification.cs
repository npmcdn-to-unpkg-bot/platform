namespace Allors.Meta
{
	using System;

	public partial class PartyClassificationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}