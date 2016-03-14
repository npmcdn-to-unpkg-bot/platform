namespace Allors.Repository.Domain
{
    #region Allors
    [Id("6c20422e-cb3e-4402-bb40-dacaf584405e")]
    #endregion
    public partial class MediaContent : AccessControlledObject, Deletable
    {
        #region inherited properties
        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }
        #endregion

        #region Allors
        [Id("890598a9-0be4-49ee-8dd8-3581ee9355e6")]
        [AssociationId("3cf7f10e-dc56-4a50-95a5-fe7fae0be291")]
        [RoleId("70823e7d-5829-4db7-99e0-f6c5f2b0e87b")]
        #endregion
        [Required]
        [Indexed]
        [Size(1024)]
        public string Type { get; set; }

        #region Allors
        [Id("0756d508-44b7-405e-bf92-bc09e5702e63")]
        [AssociationId("76e6547b-8dcf-4e69-ae2d-c8f8c33989e9")]
        [RoleId("85170945-b020-485b-bb6f-c4122992ebfd")]
        #endregion
        [Required]
        [Size(-1)]
        public byte[] Data { get; set; }
        
        #region inherited methods
        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}

        public void Delete(){}
        #endregion
    }
}