namespace Allors.Meta
{
    public partial class CounterClass : Class
    {
        #region Allors
        [Id("309d07d9-8dea-4e99-a3b8-53c0d360bc54")]
        [AssociationId("0c807020-5397-4cdb-8380-52899b7af6b7")]
        [RoleId("ab60f6a7-d913-4377-ab47-97f0fb9d8f3b")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType Value;

        internal override void BaseExtend()
        {
            this.Value.RoleType.IsRequired = true;
        }
    }
}