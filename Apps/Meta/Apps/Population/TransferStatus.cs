namespace Allors.Meta
{
	using System;

	public partial class TransferStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.TransferObjectState.IsRequired = true;
        }
	}
}