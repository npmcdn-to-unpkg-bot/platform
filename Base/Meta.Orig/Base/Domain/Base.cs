namespace Allors.Meta
{
    #region Allors
    [Id("770538dd-7b19-4694-bdce-cf04dcf9cf62")]
    #endregion
    [Inherit(typeof(CoreDomain))]

    public partial class BaseDomain : Domain
    {
        public static BaseDomain Instance { get; internal set; }

        private BaseDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
    }
}