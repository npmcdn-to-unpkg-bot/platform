namespace Allors.Repository.Domain
{
    using System;

    public partial class Singleton :  Object, AccessControlledObject 
    {
        #region Allors
        [Id("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236")]
        [AssociationId("04bc6904-bd6e-4401-9720-088ebf1fb392")]
        [RoleId("7ab62a77-c098-4ad6-836d-53ae820df951")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Indexed]
        #endregion
        public StringTemplate PersonTemplate { get; set; }
        #region Allors
        [Id("3aeb4787-e1cb-460f-9e9f-fcbc3fde1aae")]
        [AssociationId("174745f3-eae9-451c-acf8-b082ecfa52c8")]
        [RoleId("4459b703-7d0e-4f78-a239-a1038c288f96")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl SalesAccessControl { get; set; }
        #region Allors
        [Id("96840be6-fb16-4450-9357-be7c010a8803")]
        [AssociationId("061e9a69-39f7-4760-b192-7fd45dc493d2")]
        [RoleId("e2c738d1-6c4c-455b-9eb7-d59bcab88328")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl OperationsAccessControl { get; set; }
        #region Allors
        [Id("74bb8158-a222-429d-8421-3b508de5d516")]
        [AssociationId("be102333-b04a-4942-a7e2-9ef303d39bff")]
        [RoleId("a20aef2f-4626-446f-9931-aed22e7a5043")]
        [Multiplicity(Multiplicity.OneToOne)]
        #endregion
        public AccessControl ProcurementAccessControl { get; set; }
    }
}