namespace Allors.Meta
{
	using System;

	public partial class RequirementInterface
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("B96906C0-83CB-48D5-A67C-8E3E05073B14")){ObjectType= this, Name = "Reopen"};

	        this.Description.RoleType.IsRequired = true;
            this.CurrentObjectState.RoleType.IsRequired = true;
        }
	}
}