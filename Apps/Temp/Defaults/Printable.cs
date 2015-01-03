namespace Allors.Meta
{
	using System;

	public partial class PrintableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.PrintContent.IsRequired = true;

		}
	}
}