namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("849393ed-cff6-40da-9b4d-483f045f2e99")]
	#endregion
	[Inherit(typeof(ObjectStateInterface))]

	[Plural("OrderObjectStates")]
	public partial class OrderObjectStateClass : Class
	{

		public static OrderObjectStateClass Instance {get; internal set;}

		internal OrderObjectStateClass() : base(MetaPopulation.Instance)
        {
        }
	}
}