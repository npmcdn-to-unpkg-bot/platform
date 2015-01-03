namespace Allors.Meta
{
	using System;

	public partial class PartRevisionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Part.IsRequired = true;
            this.Roles.SupersededByPart.IsRequired = true;
        }
	}
}