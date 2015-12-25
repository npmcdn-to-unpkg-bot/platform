namespace Allors.Meta
{
    public partial class AccessControlClass : Class
    {
        #region Allors
        [Id("0dbbff5c-3dca-4257-b2da-442d263dcd86")]
        [AssociationId("92e8d639-9205-422b-b4ff-c7e8c2980abf")]
        [RoleId("2d9b7157-5409-40d3-bd3e-6911df9aface")]
        #endregion
        [Indexed]
        [Type(typeof(UserGroupClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType SubjectGroup;

        #region Allors
        [Id("37dd1e27-ba75-404c-9410-c6399d28317c")]
        [AssociationId("3d74101d-97bc-46fb-9548-96cb7aa01b39")]
        [RoleId("e0303a17-bf7a-4a7f-bb4b-0a447c56aaaf")]
        #endregion
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Subject;

        #region Allors
        [Id("69a9dae8-678d-4c1c-a464-2e5aa5caf39e")]
        [AssociationId("ec79e57d-be81-430a-b12f-08ffd1e94af3")]
        [RoleId("370d3d12-3164-4753-8a72-1c604bda1b64")]
        #endregion
        [Indexed]
        [Type(typeof(RoleClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Role;

        #region Allors
        [Id("5E218F37-3B07-4002-87A4-7581A53F01BA")]
        [AssociationId("BE94D5F0-DF53-4118-987A-11BCE8593A1B")]
        [RoleId("46D65185-9E0D-4347-A38F-6AFA2431C241")]
        #endregion
        [Derived]
        [Indexed]
        [Type(typeof(PermissionClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType EffectivePermission;

        #region Allors
        [Id("50ECAE85-E5A9-467E-99A3-78703D954B2F")]
        [AssociationId("01590AEA-D75C-45BE-AF4B-BF56545A4008")]
        [RoleId("BAC6C53C-E103-42CB-B36D-2FAA00EBF574")]
        #endregion
        [Derived]
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType EffectiveUser;

        internal override void BaseExtend()
        {
            this.Role.RoleType.IsRequired = true;
        }
    }
}