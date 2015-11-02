namespace Allors.Meta
{
    #region Allors
	[Id("af96e2b7-3bb5-4cd1-b02c-39a67c99a11a")]
    #endregion
    [Inherit(typeof(BaseDomain))]
    public partial class CustomDomain : Domain
	{
		public static CustomDomain Instance { get; internal set; }

		private CustomDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}