namespace Allors.Meta
{
	#region Allors
	[Id("15ea889f-21d6-4682-aca2-c2987f592f0e")]
	#endregion
	public partial class LT32UnitGT32CompositeClass : Class
	{
		public static LT32UnitGT32CompositeClass Instance {get; internal set;}

		internal LT32UnitGT32CompositeClass() : base(MetaPopulation.Instance)
        {
        }
	}
}