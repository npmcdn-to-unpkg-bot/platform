namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("592260cc-365c-4769-b067-e95dd49609f5")]
	#endregion
	[Inherit(typeof(PaymentInterface))]

	[Plural("Receipts")]
	public partial class ReceiptClass : Class
	{

		public static ReceiptClass Instance {get; internal set;}

		internal ReceiptClass() : base(MetaPopulation.Instance)
        {
        }
	}
}