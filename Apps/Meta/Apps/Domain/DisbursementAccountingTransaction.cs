namespace Allors.Meta
{
    public partial class DisbursementAccountingTransactionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Disbursement.IsRequired = true;
        }
	}
}