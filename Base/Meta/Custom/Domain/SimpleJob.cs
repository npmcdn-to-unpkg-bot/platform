namespace Allors.Meta
{
    public partial class SimpleJobClass : Class
    {
        #region Allors
        [Id("7cd27660-13c6-4a15-8fd8-5775920cfd28")]
        [AssociationId("da384d02-5d30-4df5-acb5-ca36c895ef53")]
        [RoleId("44b9e3cc-e584-48c0-bfec-916ab14e5f03")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        [Plural("Indeces")]
        public RelationType Index;
    }
}