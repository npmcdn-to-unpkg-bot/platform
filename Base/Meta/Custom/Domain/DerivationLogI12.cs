namespace Allors.Meta
{
    public partial class DerivationLogI12Interface : Interface
    {
        #region Allors
        [Id("0b89b096-a69a-495c-acfe-b24a9b27e320")]
        [AssociationId("e178ed0f-7764-4836-bd6f-fcb7f5d62346")]
        [RoleId("007a6d25-8506-483d-9140-414c0056d812")]
        #endregion
        [Type(typeof(AllorsUniqueUnit))]
        [Plural("UniqueIds")]
        public RelationType UniqueId;
    }
}