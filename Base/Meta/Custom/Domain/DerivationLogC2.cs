namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("c7563dd3-77b2-43ff-92f9-a4f98db36acf")]
	#endregion
	[Inherit(typeof(DerivationLogI12Interface))]

	[Plural("DerivationLogC2s")]
	public partial class DerivationLogC2Class : Class
	{

		public static DerivationLogC2Class Instance {get; internal set;}

		internal DerivationLogC2Class() : base(MetaPopulation.Instance)
        {
        }
	}
}