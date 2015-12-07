namespace Allors.Meta
{
    #region Allors
    [Id("7979a17c-0829-46df-a0d4-1b01775cfaac")]
    #endregion
    public partial class LocalisedInterface : Interface
    {
        #region Allors
        [Id("8c005a4e-5ffe-45fd-b279-778e274f4d83")]
        [AssociationId("6684d98b-cd43-4612-bf9d-afefe02a0d43")]
        [RoleId("d43b92ac-9e6f-4238-9625-1e889be054cf")]
        #endregion
        [Indexed]
        [Type(typeof(LocaleClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Locale;

        public static LocalisedInterface Instance { get; internal set; }

        internal LocalisedInterface() : base(MetaPopulation.Instance)
        {
        }
    }
}