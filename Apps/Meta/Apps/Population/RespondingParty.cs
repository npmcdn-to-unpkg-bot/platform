namespace Allors.Meta
{
	using System;

	public partial class RespondingPartyClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Party.IsRequired = true;
        }
	}
}