namespace Allors.Meta
{
    #region Allors
    [Id("a53f1aed-0e3f-4c3c-9600-dc579cccf893")]
    #endregion
    [Inherit(typeof(DeletableInterface))]
    public partial class SecurityTokenClass : Class
    {
        #region Allors
        [Id("6503574b-8bab-4da8-a19d-23a9bcffe01e")]
        [AssociationId("cae9e5c2-afa1-46f4-b930-69d4e810038f")]
        [RoleId("ab2b4b9c-87dd-4712-b123-f5f9271c6193")]
        #endregion
        [Indexed]
        [Type(typeof(AccessControlClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType AccessControl;

        public static SecurityTokenClass Instance { get; internal set; }

        internal SecurityTokenClass() : base(MetaPopulation.Instance)
        {
        }
    }
}