namespace Allors.Meta
{
    #region Allors
    [Id("7CA39473-BBA1-49AF-A343-F3CC7ABE14D5")]
    #endregion
    [Inherit(typeof(DeletableInterface))]
    public partial class AsyncDerivationClass : Class
    {
        #region Allors
        [Id("040AE9FF-C5BA-490A-88B0-0E044C116039")]
        [AssociationId("A48F2F25-7A77-42B2-B91D-6F169189897B")]
        [RoleId("4E02FBA4-EAE4-4C7D-B174-E2326B81FDC4")]
        #endregion
        [Indexed]
        [Type(typeof(AsyncDerivableInterface))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType AsyncDerivable;

        public static AsyncDerivationClass Instance { get; internal set; }

        internal AsyncDerivationClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
        }
    }
}