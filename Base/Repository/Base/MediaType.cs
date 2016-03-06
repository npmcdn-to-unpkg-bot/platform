namespace Allors.Repository.Domain
{
    #region Allors
    [Id("aa7d61f8-6618-47a0-9cf2-e75dd81dbd5b")]
    #endregion
    public partial class MediaType :  Object, AccessControlledObject 
    {
        #region inherited properties
        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("19e52bd9-26cb-4e74-9c28-9f01e684f3da")]
        [AssociationId("b1928c18-ef98-4cee-b03c-660221046486")]
        [RoleId("7223c1e2-d722-440b-8345-ab4cfe56d0e9")]
        [Size(256)]
        #endregion
        public string DefaultFileExtension { get; set; }

        #region Allors
        [Id("5fcee025-29fd-42d8-ad5a-75cb88d8aef0")]
        [AssociationId("0353bfc3-552c-43c7-bfe2-666d2a8199dc")]
        [RoleId("437caa53-1838-4cc4-a403-d65cf3b64358")]
        [Required]
        [Size(256)]
        #endregion
        public string Name { get; set; }
        
        #region inherited methods


        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}

        #endregion
    }
}