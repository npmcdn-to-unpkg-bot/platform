namespace Allors.Meta
{
	using System;

	public partial class PositionReportingStructureClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Position.IsRequired = true;
            this.Roles.ManagedByPosition.IsRequired = true;
        }
	}
}