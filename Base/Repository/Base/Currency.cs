namespace Allors.Repository.Domain
{

    #region Allors
    [Id("fd397adf-40b4-4ef8-b449-dd5a24273df3")]
    #endregion
    [Plural("Currencies")]
    public partial class Currency :  Object, AccessControlledObject 
    {
        #region inherited properties
        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("294a4bdc-f03a-47a2-a649-419e6b9021a3")]
        [AssociationId("f9eec7c6-c4cd-4d8c-a5f7-44855328cd7e")]
        [RoleId("09d74027-4457-4788-803c-24b857245565")]
        [Required]
        [Size(256)]
        #endregion
        public string IsoCode { get; set; }

        #region Allors
        [Id("74c8308b-1b76-4218-9532-f01c9d1e146b")]
        [AssociationId("2cb43671-c648-4bd4-ac08-7302c29246e7")]
        [RoleId("e7c93764-d634-4187-97ed-9248ea56bab2")]
        [Indexed]
        [Required]
        [Size(256)]
        #endregion
        public string Name { get; set; }
        
        #region Allors
        [Id("82797074-8d6c-4d61-a885-34ae7133a503")]
        [AssociationId("0d4524d0-503f-494d-87a4-cbc239b278e1")]
        [RoleId("43e13383-ea7f-4aa1-872c-eec0b53a998e")]
        [Required]
        [Size(256)]
        #endregion
        public string Symbol { get; set; }
        
        #region Allors
        [Id("e9fc0472-cf7a-4e02-b061-cb42b6f5c273")]
        [AssociationId("06b8f2b2-91f0-4b89-ae19-b47de4524556")]
        [RoleId("e1301b8f-25cc-4ace-884e-79af1d303f53")]
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