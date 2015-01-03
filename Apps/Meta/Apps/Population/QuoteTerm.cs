namespace Allors.Meta
{
	using System;

	public partial class QuoteTermClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.TermType.IsRequired = true;
        }
	}
}