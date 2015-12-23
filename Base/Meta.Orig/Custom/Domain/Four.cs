namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("1248e212-ca71-44aa-9e87-6e83dae9d4fd")]
	#endregion
	[Inherit(typeof(SharedInterface))]

	[Plural("Fours")]
	public partial class FourClass : Class
	{

		public static FourClass Instance {get; internal set;}

		internal FourClass() : base(MetaPopulation.Instance)
        {
        }
	}
}