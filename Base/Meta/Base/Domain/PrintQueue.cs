namespace Allors.Meta
{
    #region Allors
    [Id("b45705e3-0dc6-4296-824a-76bb6af223d3")]
    #endregion
    [Inherit(typeof(AccessControlledObjectInterface))]
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    public partial class PrintQueueClass : Class
    {
        #region Allors
        [Id("679156a1-f683-4772-b724-54b318eb3cb3")]
        [AssociationId("9124aa32-3ed5-4a1a-8988-961eea280b86")]
        [RoleId("432f8b01-0bb8-4bd2-8a41-107b6d043a40")]
        #endregion
        [Indexed]
        [Type(typeof(PrintableInterface))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Printable;

        #region Allors
        [Id("7a85e090-55cf-47f5-912e-4bd87c66a060")]
        [AssociationId("01fa325c-4b41-4cbf-9ffe-65d25e0ae694")]
        [RoleId("285adf08-7f1b-4dfe-8db5-cbf4a9d0cb59")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        public static PrintQueueClass Instance { get; internal set; }

        internal PrintQueueClass() : base(MetaPopulation.Instance)
        {
        }
    }
}