// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface InternalAccountingTransactionBuilder : AccountingTransactionBuilder , global::System.IDisposable
	{	
		InternalOrganisation InternalOrganisation {get;}

		
	}

	public partial class InternalAccountingTransactions : global::Allors.ObjectsBase<InternalAccountingTransaction>
	{
		public static readonly InternalAccountingTransactionMeta Meta = InternalAccountingTransactionMeta.Instance;

		public InternalAccountingTransactions(Allors.ISession session) : base(session)
		{
		}

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}