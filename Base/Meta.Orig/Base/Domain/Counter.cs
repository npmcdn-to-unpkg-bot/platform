namespace Allors.Meta
{
    #region Allors
    [Id("0568354f-e3d9-439e-baac-b7dce31b956a")]
    #endregion
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    public partial class CounterClass : Class
    {
        #region Allors
        [Id("309d07d9-8dea-4e99-a3b8-53c0d360bc54")]
        [AssociationId("0c807020-5397-4cdb-8380-52899b7af6b7")]
        [RoleId("ab60f6a7-d913-4377-ab47-97f0fb9d8f3b")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType Value;

        public static CounterClass Instance { get; internal set; }

        internal CounterClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.Value.RoleType.IsRequired = true;
        }
    }
}