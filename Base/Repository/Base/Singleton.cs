namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("313b97a5-328c-4600-9dd2-b5bc146fb13b")]
    #endregion
    public partial class Singleton :  Object, AccessControlledObject 
    {
        #region inherited properties
        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("64aed238-7009-4157-8395-7eb58ebf7889")]
        [AssociationId("2f79ecfe-5fd4-44d1-9c39-457bb3dc6815")]
        [RoleId("d861c8f8-7362-4805-9941-661a99ab11ac")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public PrintQueue DefaultPrintQueue { get; set; }
        #region Allors
        [Id("9c1634ab-be99-4504-8690-ed4b39fec5bc")]
        [AssociationId("45a4205d-7c02-40d4-8d97-6d7d59e05def")]
        [RoleId("1e051b37-cf30-43ed-a623-dd2928d6d0a3")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Indexed]
        #endregion
        public Locale DefaultLocale { get; set; }
        #region Allors
        [Id("9e5a3413-ed33-474f-adf2-149ad5a80719")]
        [AssociationId("33d5d8b9-3472-48d8-ab1a-83d00d9cb691")]
        [RoleId("e75a8956-4d02-49ba-b0cf-747b7a9f350d")]
        [Multiplicity(Multiplicity.OneToMany)]
        [Indexed]
        #endregion
        public Locale[] Locales { get; set; }
        #region Allors
        [Id("f16652b0-b712-43d7-8d4e-34a22487514d")]
        [AssociationId("c92466b5-55ba-496a-8880-2821f32f8f8e")]
        [RoleId("3a12d798-40c3-40e0-ba9f-9d01b1e39e89")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public User Guest { get; set; }
        #region Allors
        [Id("f579494b-e550-4be6-9d93-84618ac78704")]
        [AssociationId("33f17e75-99cc-417e-99f3-c29080f08f0a")]
        [RoleId("ca9e3469-583c-4950-ba2c-1bc3a0fc3e96")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Indexed]
        #endregion
        public SecurityToken DefaultSecurityToken { get; set; }
        #region Allors
        [Id("f7e50cac-ab57-4ebe-b765-d63804924c48")]
        [AssociationId("cb47a309-ed8f-47d1-879f-478e63b350d8")]
        [RoleId("c955b6ef-57b7-404f-bba5-fa7aebf706f6")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public AccessControl DefaultGuestAccessControl { get; set; }
        #region Allors
        [Id("829aa4a4-8232-4625-8cab-db7dc96da53f")]
        [AssociationId("56f18f8b-380b-4236-9a85-ed989c1a6e44")]
        [RoleId("a3b765ed-bbf6-4bc4-9551-6338705ef03e")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public AccessControl DefaultAdministratorsAccessControl { get; set; }
    
        #region inherited methods
        
        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}

        #endregion
    }
}