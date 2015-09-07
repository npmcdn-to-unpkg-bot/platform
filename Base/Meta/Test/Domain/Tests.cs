namespace Allors.Meta
{
    #region Allors
		[Inherit(typeof(BaseDomain))]

	[Id("af96e2b7-3bb5-4cd1-b02c-39a67c99a11a")]
	#endregion
	public partial class TestsDomain : Domain
	{
		public static TestsDomain Instance { get; internal set; }

		private TestsDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}