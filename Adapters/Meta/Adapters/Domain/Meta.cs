namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("0900c2a6-c90c-4d75-be21-32cc796260d1")]
	#endregion
	public partial class AdaptersDomain : Domain
	{
		public static AdaptersDomain Instance { get; internal set; }

		private AdaptersDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}