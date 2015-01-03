namespace Allors.Meta
{
	using System;

	public partial class PrintableInterface
	{
	    internal override void BaseExtend()
	    {
	        this.Roles.PrintContent.IsRequired = true;
	    }
	}
}