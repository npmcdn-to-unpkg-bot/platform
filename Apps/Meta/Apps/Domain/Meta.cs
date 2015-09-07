namespace Allors.Meta
{
	using System;
	#region Allors
	[Id("770538dd-7b19-4694-bdce-cf04dcf9cf62")]
	#endregion
	public partial class BaseDomain : Domain
	{
		public static BaseDomain Instance { get; internal set; }

		private BaseDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}

	#region Allors
		[Inherit(typeof(BaseDomain))]

	[Id("4ea604af-7fcc-49f8-8b3b-6be712cea6d9")]
	#endregion
	public partial class AppsDomain : Domain
	{
		public static AppsDomain Instance { get; internal set; }

		private AppsDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}