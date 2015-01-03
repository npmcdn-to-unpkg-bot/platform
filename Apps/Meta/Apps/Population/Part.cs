namespace Allors.Meta
{
	using System;

	public partial class PartInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.InventoryItemKind.IsRequired = true;
            this.Roles.OwnedByParty.IsRequired = true;
        }
	}
}