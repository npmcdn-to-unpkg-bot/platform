namespace Allors.Meta
{
    public partial class OneClass : Class
    {
        #region Allors
        [Id("448878af-c992-4256-baa7-239335a26bc6")]
        [AssociationId("2c9236ed-892e-4005-9730-5a14f03f71e1")]
        [RoleId("355b2e85-e597-4f88-9dca-45cbfbde527f")]
        #endregion
        [Indexed]
        [Type(typeof(TwoClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Two;
    }
}