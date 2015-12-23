namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("4a0eca4b-281f-488d-9c7e-497de882c044")]
    #endregion
    public partial class Language :  Object, AccessControlledObject 
    {
        #region inherited properties
        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("be482902-beb5-4a76-8ad0-c1b1c1c0e5c4")]
        [AssociationId("d3369fa9-afb7-4d5a-b476-3e4d43cce0fd")]
        [RoleId("308d73b0-1b65-40a9-88f1-288848849c51")]
        [Indexed]
        [Required]
        [Size(256)]
        #endregion
        public string Name { get; set; }
        #region Allors
        [Id("d2a32d9f-21cc-4f9d-b0d3-a9b75da66907")]
        [AssociationId("6c860e73-d12e-4e35-897e-ed9f8fd8eba0")]
        [RoleId("84f904a6-8dcc-4089-bda6-34325ade6367")]
        [Required]
        [Size(256)]
        #endregion
        public string IsoCode { get; set; }
        #region Allors
        [Id("f091b264-e6b1-4a57-bbfb-8225cbe8190c")]
        [AssociationId("6650af3b-f537-4c2f-afff-6773552315cd")]
        [RoleId("5e9fcced-727d-42a2-95e6-a0f9d8be4ec7")]
        [Multiplicity(Multiplicity.OneToMany)]
        [Indexed]
        #endregion
        public LocalisedText[] LocalisedNames { get; set; }


        #region inherited methods


        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}

        #endregion

    }
}