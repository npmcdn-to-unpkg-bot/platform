namespace Allors.Meta
{
	using System;

	public partial class WorkEffortInterface
	{
	    internal override void AppsExtend()
	    {
	        new MethodType(AppsDomain.Instance, new Guid("A8D6C356-6AB3-47EA-A0F7-25FBFB711A81")) { ObjectType = this, Name = "Confirm" };
            new MethodType(AppsDomain.Instance, new Guid("860F33C9-7CD9-427D-9FFD-93B1274C9EB2")) { ObjectType = this, Name = "Finish" };
            new MethodType(AppsDomain.Instance, new Guid("0A66E9CA-89A8-4D5A-B63F-E061CDBC0A2E")) { ObjectType = this, Name = "Cancel" };
            new MethodType(AppsDomain.Instance, new Guid("A1189C0F-8E2E-41B7-B61E-36525B3895B5")) { ObjectType = this, Name = "Reopen" };


            this.Roles.Description.IsRequired = true;
            this.Roles.CurrentObjectState.IsRequired = true;
        }
	}
}