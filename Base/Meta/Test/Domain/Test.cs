namespace Allors.Meta
{
    #region Allors
		[Inherit(typeof(BaseDomain))]

	[Id("af96e2b7-3bb5-4cd1-b02c-39a67c99a11a")]
	#endregion
	public partial class TestDomain : Domain
	{
		public static TestDomain Instance { get; internal set; }

		private TestDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}