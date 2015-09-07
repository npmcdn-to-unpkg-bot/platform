namespace Allors.Meta
{
    public partial class OwnBankAccountClass
    {
        internal override void AppsExtend()
        {
            this.Roles.BankAccount.IsRequired = true;
        }
    }
}