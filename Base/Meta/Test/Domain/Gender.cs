namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("270f0dc8-1bc2-4a42-9617-45e93d5403c8")]
	#endregion
	[Inherit(typeof(EnumerationInterface))]

	[Plural("Genders")]
	public partial class GenderClass : Class
	{

		public static GenderClass Instance {get; internal set;}

		internal GenderClass() : base(MetaPopulation.Instance)
        {
        }
	}
}