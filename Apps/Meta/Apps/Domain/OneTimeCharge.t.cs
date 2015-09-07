namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("5835aca6-214b-41cf-aefe-e361dda026d7")]
	#endregion
	[Inherit(typeof(PriceComponentInterface))]

	[Plural("OneTimeCharges")]
	public partial class OneTimeChargeClass : Class
	{

		public static OneTimeChargeClass Instance {get; internal set;}

		internal OneTimeChargeClass() : base(MetaPopulation.Instance)
        {
        }
	}
}