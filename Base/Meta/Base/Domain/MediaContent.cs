namespace Allors.Meta
{
    public partial class MediaContentClass : Class
    {
        #region Allors
        [Id("0756d508-44b7-405e-bf92-bc09e5702e63")]
        [AssociationId("76e6547b-8dcf-4e69-ae2d-c8f8c33989e9")]
        [RoleId("85170945-b020-485b-bb6f-c4122992ebfd")]
        #endregion
        [Type(typeof(AllorsBinaryUnit))]
        [Size(-1)]
        public RelationType Value;

        #region Allors
        [Id("890598a9-0be4-49ee-8dd8-3581ee9355e6")]
        [AssociationId("3cf7f10e-dc56-4a50-95a5-fe7fae0be291")]
        [RoleId("70823e7d-5829-4db7-99e0-f6c5f2b0e87b")]
        #endregion
        [Derived]
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(1024)]
        [Plural("Hashes")]
        public RelationType Hash;

        internal override void BaseExtend()
        {
            this.Value.RoleType.IsRequired = true;
        }
    }
}