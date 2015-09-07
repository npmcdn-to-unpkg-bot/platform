namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("a917f763-e54a-4693-bf7b-d8e7aead8fe6")]
	#endregion
	[Inherit(typeof(AccessControlledObjectInterface))]
	[Inherit(typeof(AgreementTermInterface))]

	[Plural("InvoiceTerms")]
	public partial class InvoiceTermClass : Class
	{

		public static InvoiceTermClass Instance {get; internal set;}

		internal InvoiceTermClass() : base(MetaPopulation.Instance)
        {
        }
	}
}