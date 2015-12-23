namespace Allors.Repository.Domain
{
    public partial class Singleton
    {
        #region Allors
        [Id("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236")]
        [Indexed]
        #endregion
        public StringTemplate PersonTemplate { get; set; }

        #region Allors
        [Id("3aeb4787-e1cb-460f-9e9f-fcbc3fde1aae")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl SalesAccessControl { get; set; }
        
        #region Allors
        [Id("96840be6-fb16-4450-9357-be7c010a8803")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl OperationsAccessControl { get; set; }
        
        #region Allors
        [Id("74bb8158-a222-429d-8421-3b508de5d516")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl ProcurementAccessControl { get; set; }
    }
}