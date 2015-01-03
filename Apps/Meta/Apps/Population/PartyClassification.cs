namespace Allors.Meta
{
	using System;

	public partial class PartyClassificationInterface
	{
        internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}