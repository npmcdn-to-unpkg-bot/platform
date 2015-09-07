namespace Allors.Meta
{
	using System;

	public partial class PickListClass
	{
	    internal override void AppsExtend()
	    {
	        new MethodType(AppsDomain.Instance, new Guid("CCBD7DB6-EC0F-4D70-9833-BC2A9E3A9292")) { ObjectType = this, Name = "Hold" };
	        new MethodType(AppsDomain.Instance, new Guid("B88AF2FA-0940-4C3B-90E7-9937DF6C05AC")) { ObjectType = this, Name = "Continue" };
	        new MethodType(AppsDomain.Instance, new Guid("41E4C5C4-2CFE-4B7F-80FD-E4C0263FDF62")) { ObjectType = this, Name = "Cancel" };
	        new MethodType(AppsDomain.Instance, new Guid("CAC524A5-47A9-4FFD-ABC2-D5D3C0ABBFDD")) { ObjectType = this, Name = "SetPicked" };

	        this.CreationDate.RoleType.IsRequired = true;
	        this.CurrentObjectState.RoleType.IsRequired = true;
	    }
	}
}