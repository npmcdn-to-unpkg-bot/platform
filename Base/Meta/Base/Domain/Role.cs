namespace Allors.Meta
{
    [Inherit(typeof(AccessControlledObjectInterface))]
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    public partial class RoleClass : Class
    {
        #region Allors
        [Id("51e56ae1-72dc-443f-a2a3-f5aa3650f8d2")]
        [AssociationId("47af1a0f-497d-4a19-887b-79e5fb77c8bd")]
        [RoleId("7e6a71b0-2194-47f8-b562-cb4a15e335b6")]
        #endregion
        [Indexed]
        [Type(typeof(PermissionClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Permission;

        #region Allors
        [Id("934bcbbe-5286-445c-a1bd-e2fcc786c448")]
        [AssociationId("05785884-ca83-43de-a6f3-86d3fa7ec82a")]
        [RoleId("8d87c74f-53ed-4e1d-a2ea-12190ac233d2")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
            this.Name.RoleType.IsUnique = true;
        }
    }
}