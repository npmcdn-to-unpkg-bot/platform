namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("2361c456-b624-493a-8377-2dd1e697e17a")]
	#endregion
	[Inherit(typeof(DerivationLogI12Interface))]

	[Plural("DerivationLogC1s")]
	public partial class DerivationLogC1Class : Class
	{

		public static DerivationLogC1Class Instance {get; internal set;}

		internal DerivationLogC1Class() : base(MetaPopulation.Instance)
        {
        }
	}
}