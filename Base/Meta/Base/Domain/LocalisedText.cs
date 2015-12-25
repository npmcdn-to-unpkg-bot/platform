namespace Allors.Meta
{
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

        internal override void BaseExtend()
        {
            this.Text.RoleType.IsRequired = true;
        }
    }
}