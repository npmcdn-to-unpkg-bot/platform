namespace Allors.Meta
{
    public partial class OrderRequirementCommitmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.OrderItem.IsRequired = true;
            this.Roles.Requirement.IsRequired = true;
        }
	}
}