namespace Allors.Meta
{
    [Inherit(typeof(AccessControlledObjectInterface))]
    public partial class LanguageClass : Class
    {
        #region Allors
        [Id("be482902-beb5-4a76-8ad0-c1b1c1c0e5c4")]
        [AssociationId("d3369fa9-afb7-4d5a-b476-3e4d43cce0fd")]
        [RoleId("308d73b0-1b65-40a9-88f1-288848849c51")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        #region Allors
        [Id("d2a32d9f-21cc-4f9d-b0d3-a9b75da66907")]
        [AssociationId("6c860e73-d12e-4e35-897e-ed9f8fd8eba0")]
        [RoleId("84f904a6-8dcc-4089-bda6-34325ade6367")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType IsoCode;

        #region Allors
        [Id("f091b264-e6b1-4a57-bbfb-8225cbe8190c")]
        [AssociationId("6650af3b-f537-4c2f-afff-6773552315cd")]
        [RoleId("5e9fcced-727d-42a2-95e6-a0f9d8be4ec7")]
        #endregion
        [Indexed]
        [Type(typeof(LocalisedTextClass))]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType LocalisedName;

        internal override void BaseExtend()
        {
            this.IsoCode.RoleType.IsRequired = true;
            this.Name.RoleType.IsRequired = true;
        }
    }
}