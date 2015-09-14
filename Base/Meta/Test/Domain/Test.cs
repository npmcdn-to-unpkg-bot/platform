namespace Allors.Meta
{
    #region Allors
	[Id("af96e2b7-3bb5-4cd1-b02c-39a67c99a11a")]
    #endregion
    [Inherit(typeof(BaseDomain))]
    public partial class TestDomain : Domain
	{
		public static TestDomain Instance { get; internal set; }

		private TestDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}