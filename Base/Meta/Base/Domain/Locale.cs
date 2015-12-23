namespace Allors.Meta
{
    [Inherit(typeof(AccessControlledObjectInterface))]
    public partial class LocaleClass : Class
    {
        #region Allors
        [Id("2a2c6f77-e6a2-4eab-bfe3-5d35a8abd7f7")]
        [AssociationId("09422255-fa17-41d8-991b-d21d7b37c6c5")]
        [RoleId("647db2b3-997b-4c3a-9ae2-d49b410933c1")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        #region Allors
        [Id("d8cac34a-9bb2-4190-bd2a-ec0b87e04cf5")]
        [AssociationId("af501892-3c83-41d1-826b-f5c4cb1de7fe")]
        [RoleId("ed32b12a-00ad-420b-9dfa-f1c6ce773fcd")]
        #endregion
        [Indexed]
        [Type(typeof(LanguageClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Language;

        #region Allors
        [Id("ea778b77-2929-4ab4-ad99-bf2f970401a9")]
        [AssociationId("bb5904f5-feb0-47eb-903a-0351d55f0d8c")]
        [RoleId("b2fc6e06-3881-427e-b4cc-8457a65f8076")]
        #endregion
        [Indexed]
        [Type(typeof(CountryClass))]
        [Plural("Countries")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Country;

        internal override void BaseExtend()
        {
            this.Language.RoleType.IsRequired = true;
            this.Country.RoleType.IsRequired = true;
        }
    }
}