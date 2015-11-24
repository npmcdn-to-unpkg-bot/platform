namespace Allors.Meta
{
    public partial class SingletonClass : Class
    {
        #region Allors
        [Id("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236")]
        [AssociationId("04bc6904-bd6e-4401-9720-088ebf1fb392")]
        [RoleId("7ab62a77-c098-4ad6-836d-53ae820df951")]
        #endregion
        [Indexed]
        [Type(typeof(StringTemplateClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType PersonTemplate;
        
        #region Allors
        [Id("3AEB4787-E1CB-460F-9E9F-FCBC3FDE1AAE")]
        [AssociationId("174745F3-EAE9-451C-ACF8-B082ECFA52C8")]
        [RoleId("4459B703-7D0E-4F78-A239-A1038C288F96")]
        #endregion
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType SalesAccessControl;

        #region Allors
        [Id("96840BE6-FB16-4450-9357-BE7C010A8803")]
        [AssociationId("061E9A69-39F7-4760-B192-7FD45DC493D2")]
        [RoleId("E2C738D1-6C4C-455B-9EB7-D59BCAB88328")]
        #endregion
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType OperationsAccessControl;

        #region Allors
        [Id("74BB8158-A222-429D-8421-3B508DE5D516")]
        [AssociationId("BE102333-B04A-4942-A7E2-9EF303D39BFF")]
        [RoleId("A20AEF2F-4626-446F-9931-AED22E7A5043")]
        #endregion
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType ProcurementAccessControl;
    }
}