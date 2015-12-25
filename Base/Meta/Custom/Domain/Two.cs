namespace Allors.Meta
{
    public partial class TwoClass : Class
    {
        #region Allors
        [Id("8930c13c-ad5a-4b0e-b3bf-d7cdf6f5b867")]
        [AssociationId("fd97db6d-d946-47ba-a2a0-88b732457b96")]
        [RoleId("39eda296-4e8d-492b-b0c1-756ffcf9a493")]
        #endregion
        [Indexed]
        [Type(typeof(SharedInterface))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Shared;
    }
}