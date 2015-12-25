namespace Allors.Meta
{
    public partial class SingletonClass : Class
    {
        #region Allors
        [Id("64aed238-7009-4157-8395-7eb58ebf7889")]
        [AssociationId("2f79ecfe-5fd4-44d1-9c39-457bb3dc6815")]
        [RoleId("d861c8f8-7362-4805-9941-661a99ab11ac")]
        #endregion
        [Indexed]
        [Type(typeof(PrintQueueClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType DefaultPrintQueue;

        #region Allors
        [Id("9c1634ab-be99-4504-8690-ed4b39fec5bc")]
        [AssociationId("45a4205d-7c02-40d4-8d97-6d7d59e05def")]
        [RoleId("1e051b37-cf30-43ed-a623-dd2928d6d0a3")]
        #endregion
        [Indexed]
        [Type(typeof(LocaleClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType DefaultLocale;

        #region Allors
        [Id("9e5a3413-ed33-474f-adf2-149ad5a80719")]
        [AssociationId("33d5d8b9-3472-48d8-ab1a-83d00d9cb691")]
        [RoleId("e75a8956-4d02-49ba-b0cf-747b7a9f350d")]
        #endregion
        [Indexed]
        [Type(typeof(LocaleClass))]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType Locale;

        #region Allors
        [Id("f16652b0-b712-43d7-8d4e-34a22487514d")]
        [AssociationId("c92466b5-55ba-496a-8880-2821f32f8f8e")]
        [RoleId("3a12d798-40c3-40e0-ba9f-9d01b1e39e89")]
        #endregion
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType Guest;

        #region Allors
        [Id("f579494b-e550-4be6-9d93-84618ac78704")]
        [AssociationId("33f17e75-99cc-417e-99f3-c29080f08f0a")]
        [RoleId("ca9e3469-583c-4950-ba2c-1bc3a0fc3e96")]
        #endregion
        [Indexed]
        [Type(typeof(SecurityTokenClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType DefaultSecurityToken;

        #region Allors
        [Id("F7E50CAC-AB57-4EBE-B765-D63804924C48")]
        [AssociationId("CB47A309-ED8F-47D1-879F-478E63B350D8")]
        [RoleId("C955B6EF-57B7-404F-BBA5-FA7AEBF706F6")]
        #endregion
        [Indexed]
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType DefaultGuestAccessControl;

        #region Allors
        [Id("829AA4A4-8232-4625-8CAB-DB7DC96DA53F")]
        [AssociationId("56F18F8B-380B-4236-9A85-ED989C1A6E44")]
        [RoleId("A3B765ED-BBF6-4BC4-9551-6338705EF03E")]
        #endregion
        [Indexed]
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType DefaultAdministratorsAccessControl;
    }
}