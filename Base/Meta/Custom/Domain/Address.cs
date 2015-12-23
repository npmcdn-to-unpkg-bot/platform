namespace Allors.Meta
{
    [Plural("Addresses")]
    public partial class AddressInterface : Interface
    {
        #region Allors
        [Id("36e7d935-a9c7-484d-8551-9bdc5bdeab68")]
        [AssociationId("113a8abd-e587-45a3-b118-92e60182c94b")]
        [RoleId("4f7016f6-1b87-4ac4-8363-7f8210108928")]
        #endregion
        [Indexed]
        [Type(typeof(PlaceClass))]
        [Plural("Places")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Place;
    }
}