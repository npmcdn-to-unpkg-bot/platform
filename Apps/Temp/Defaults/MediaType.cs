namespace Allors.Meta
{
	using System;

	public partial class MediaTypeClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.DefaultFileExtension.IsRequired = true;

		}
	}
}