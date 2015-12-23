namespace Allors.Meta
{
    public partial class PlaceClass : Class
    {
        #region Allors
        [Id("1bf1cc1e-75bf-4a3f-87bd-a2fae2697855")]
        [AssociationId("dce03fde-fbb1-45e7-b78d-9484fa6487ff")]
        [RoleId("d88eaaa2-2622-48ef-960a-1b506d95f238")]
        #endregion
        [Indexed]
        [Type(typeof(CountryClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Country;

        #region Allors
        [Id("d029f486-4bb8-43a1-8356-98b9bee10de4")]
        [AssociationId("1454029b-b016-41e1-b142-cea20c7b36d1")]
        [RoleId("dccca416-913b-406a-9405-c5d037af2fd8")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        [Plural("Cities")]
        public RelationType City;

        #region Allors
        [Id("d80d7c6a-138a-43dd-9748-8ffb89b1dabb")]
        [AssociationId("944c752e-742c-426b-9ac9-c405080d4a8d")]
        [RoleId("b54fcc51-e294-4732-82bf-a1117a4e2219")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        [Plural("PostalCodes")]
        public RelationType PostalCode;
    }
}