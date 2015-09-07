namespace Allors.Meta
{
	public partial class BankAccountClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Iban.IsRequired = true;
            this.Roles.Iban.IsUnique = true;
        }
	}
}