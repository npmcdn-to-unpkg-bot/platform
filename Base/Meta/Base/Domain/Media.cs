namespace Allors.Meta
{
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    [Inherit(typeof(AccessControlledObjectInterface))]
    [Inherit(typeof(DeletableInterface))]
    public partial class MediaClass : Class
    {
        #region Allors
        [Id("49481792-06f0-49a1-b32f-28d265815a24")]
        [AssociationId("7ca17a9e-0b68-445f-8080-84b08ca0eb2d")]
        [RoleId("f1008c56-b375-4aa8-ac7e-c1f7ef9b2080")]
        #endregion
        [Indexed]
        [Type(typeof(MediaTypeClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType MediaType;

        #region Allors
        [Id("67082a51-1502-490b-b8db-537799e550bd")]
        [AssociationId("e8537dcf-1bd7-46c4-a37c-077bee6a78a1")]
        [RoleId("02fe1ce8-c019-4a40-bd6f-b38d2f47a288")]
        #endregion
        [Derived]
        [Indexed]
        [Type(typeof(MediaContentClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType MediaContent;

        internal override void BaseExtend()
        {
            this.MediaType.RoleType.IsRequired = true;
            this.MediaContent.RoleType.IsRequired = true;
        }
    }
}