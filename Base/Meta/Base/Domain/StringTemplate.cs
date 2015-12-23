namespace Allors.Meta
{
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    [Inherit(typeof(LocalisedInterface))]
    public partial class StringTemplateClass : Class
    {
        #region Allors
        [Id("2f88f9f8-3c22-40d3-885c-2abd43af96cc")]
        [AssociationId("9ad9b285-2a91-4bd9-90dd-8f963ef0a465")]
        [RoleId("3fcb83d0-11c5-48ba-ba9c-5126f0b4e9f4")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(-1)]
        [Plural("Bodies")]
        public RelationType Body;

        #region Allors
        [Id("c501103b-037a-4961-93df-2dbb74b88a76")]
        [AssociationId("1bcdddcc-e462-4d59-af2d-7346245cb271")]
        [RoleId("37bd5d22-89f1-47a4-b6bd-8841e194b213")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;

            var locale = this.ConcreteRoleTypeByRoleType[LocalisedInterface.Instance.Locale.RoleType];
            locale.IsRequiredOverride = true;
        }
    }
}