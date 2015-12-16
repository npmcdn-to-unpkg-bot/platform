namespace Allors.Meta
{
    #region Allors
    [Id("7847DD12-33CE-4D3F-B093-B0844C4DDB8F")]
    #endregion
    public partial class AsyncDerivableInterface : Interface
    {
        #region Allors
        [Id("60679E40-4E7B-4DEB-AAAE-E3A90582A4A2")]
        #endregion
        public MethodType AsyncDerive;

        public static AsyncDerivableInterface Instance { get; internal set; }

        internal AsyncDerivableInterface() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
        }
    }
}