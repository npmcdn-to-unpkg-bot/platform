namespace Allors.Meta
{
	using System;

	public partial class CustomerShipmentClass
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("9E89A8AD-2EFE-4A21-815B-9598D7D7C1F7")){ObjectType=this, Name="Hold"};
            new MethodType(AppsDomain.Instance, new Guid("1A64504B-0115-4D4D-BBE0-35792A8BCA1A")){ObjectType=this, Name="PutOnHold"};
            new MethodType(AppsDomain.Instance, new Guid("9DD73148-A1C0-4631-91AF-E13116FC0102")){ObjectType=this, Name="Cancel"};
            new MethodType(AppsDomain.Instance, new Guid("6E09CAC6-327F-49DD-B4AB-07D075C7579E")){ObjectType=this, Name="Continue"};
            new MethodType(AppsDomain.Instance, new Guid("1B56BF7E-08BE-49B1-92A1-4CE89B329D77")){ObjectType=this, Name="Ship"};
            new MethodType(AppsDomain.Instance, new Guid("9AFF4390-9B51-4C33-A0CF-125FED33E34F")){ObjectType=this, Name="ProcessOnContinue"};
            new MethodType(AppsDomain.Instance, new Guid("BD7F0406-29E2-4A10-AE55-C2849D257B01")){ObjectType=this, Name="SetPicked"};
            new MethodType(AppsDomain.Instance, new Guid("F484244D-BB1D-4158-9A4D-40267D4B7D5B")){ObjectType=this, Name="SetPacked"};
            
			this.Roles.ShipmentValue.IsRequired = true;

            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.ReleasedManually.IsRequired = true;
            this.Roles.HeldManually.IsRequired = true;
            this.Roles.WithoutCharges.IsRequired = true;

	        this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
            this.ConcreteRoles.ShipFromAddress.IsRequiredOverride = true;
            this.ConcreteRoles.ShipmentMethod.IsRequiredOverride = true;
            this.ConcreteRoles.Carrier.IsRequiredOverride = true;
            this.ConcreteRoles.EstimatedShipDate.IsRequiredOverride = true;
            this.ConcreteRoles.ShipFromParty.IsRequiredOverride = true;
        }
	}
}