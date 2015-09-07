namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("d4cfdb68-9128-4afc-8670-192e55115499")]
	#endregion
	[Inherit(typeof(PriceComponentInterface))]

	[Plural("ManufacturerSuggestedRetailPrices")]
	public partial class ManufacturerSuggestedRetailPriceClass : Class
	{

		public static ManufacturerSuggestedRetailPriceClass Instance {get; internal set;}

		internal ManufacturerSuggestedRetailPriceClass() : base(MetaPopulation.Instance)
        {
        }
	}
}