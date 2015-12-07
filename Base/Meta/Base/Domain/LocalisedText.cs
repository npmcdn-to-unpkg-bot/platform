namespace Allors.Meta
{
    #region Allors
    [Id("020f5d4d-4a59-4d7b-865a-d72fc70e4d97")]
    #endregion
    [Inherit(typeof(AccessControlledObjectInterface))]
    [Inherit(typeof(LocalisedInterface))]
    public partial class LocalisedTextClass : Class
    {
        #region Allors
        [Id("50dc85f0-3d22-4bc1-95d9-153674b89f7a")]
        [AssociationId("accd061b-20b9-4a24-bb2c-c2f7276f43ab")]
        [RoleId("8d3f68e1-fa6e-414f-aa4d-25fcc2c975d6")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(-1)]
        public RelationType Text;

        public static LocalisedTextClass Instance { get; internal set; }

        internal LocalisedTextClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.Text.RoleType.IsRequired = true;
        }
    }
}