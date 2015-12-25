namespace Allors.Meta
{
    [Plural("Countries")]
    public partial class CountryClass : Class
    {
        #region Allors
        [Id("62009cef-7424-4ec0-8953-e92b3cd6639d")]
        [AssociationId("323173ee-385c-4f74-8b78-ff05735460f8")]
        [RoleId("4ca5a640-5d9e-4910-95ed-6872c7ea13d2")]
        #endregion
        [Indexed]
        [Type(typeof(CurrencyClass))]
        [Plural("Currencies")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Currency;

        #region Allors
        [Id("6b9c977f-b394-440e-9781-5d56733b60da")]
        [AssociationId("6e3532ae-3528-4114-9274-54fc08effd0d")]
        [RoleId("60f1f9a3-13d1-485f-bc77-fda1f9ef1815")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        #region Allors
        [Id("8236a702-a76d-4bb5-9afd-acacb1508261")]
        [AssociationId("9b682612-50f9-43f3-abde-4d0cb5156f0d")]
        [RoleId("99c52c13-ef50-4f68-a32f-fef660aa3044")]
        #endregion
        [Indexed]
        [Type(typeof(LocalisedTextClass))]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType LocalisedName;

        #region Allors
        [Id("f93acc4e-f89e-4610-ada9-e58f21c165bc")]
        [AssociationId("ea0efe67-89f2-4317-97e7-f0e14358e718")]
        [RoleId("4fe997d6-d221-432b-9f09-4f77735c109b")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(2)]
        public RelationType IsoCode;

        internal override void BaseExtend()
        {
            this.IsoCode.RoleType.IsRequired = true;
            this.IsoCode.RoleType.IsUnique = true;
            this.Name.RoleType.IsRequired = true;
        }
    }
}