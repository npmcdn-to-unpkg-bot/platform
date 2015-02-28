namespace Allors.Meta
{
    public partial class JournalClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
            this.Roles.JournalType.IsRequired = true;
            this.Roles.ContraAccount.IsRequired = true;
            this.Roles.BlockUnpaidTransactions.IsRequired = true;
            this.Roles.CloseWhenInBalance.IsRequired = true;
            this.Roles.UseAsDefault.IsRequired = true;
        }
	}
}