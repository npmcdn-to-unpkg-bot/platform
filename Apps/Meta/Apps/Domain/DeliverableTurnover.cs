namespace Allors.Meta
{
    public partial class DeliverableTurnoverClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;

            this.ConcreteRoles.Description.IsRequiredOverride = true;
		}
	}
}