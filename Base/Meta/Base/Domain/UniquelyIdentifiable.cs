namespace Allors.Meta
{
    #region Allors
    [Id("122ccfe1-f902-44c1-9d6c-6f6a0afa9469")]
    #endregion
    public partial class UniquelyIdentifiableInterface : Interface
    {
        #region Allors
        [Id("e1842d87-8157-40e7-b06e-4375f311f2c3")]
        [AssociationId("fe413e96-cfcf-4e8d-9f23-0fa4f457fdf1")]
        [RoleId("d73fd9a4-13ee-4fa9-8925-d93eca328bf6")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsUniqueUnit))]
        public RelationType UniqueId;

        public static UniquelyIdentifiableInterface Instance { get; internal set; }

        internal UniquelyIdentifiableInterface() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.UniqueId.RoleType.IsRequired = true;
        }
    }
}