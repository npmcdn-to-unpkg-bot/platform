namespace Allors.Meta
{
    public partial class ObjectStateInterface : Interface
    {
        #region Allors
        [Id("59338f0b-40e7-49e8-ba1a-3ecebf96aebe")]
        [AssociationId("fca0f3f6-bdd6-4405-93b3-35dd769bff0e")]
        [RoleId("c338f087-559c-4239-9c6a-1f691e58ed16")]
        #endregion
        [Indexed]
        [Type(typeof(PermissionClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType DeniedPermission;

        #region Allors
        [Id("b86f9e42-fe10-4302-ab7c-6c6c7d357c39")]
        [AssociationId("052ec640-3150-458a-99d5-0edce6eb6149")]
        [RoleId("945cbba6-4b09-4b87-931e-861b147c3823")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        internal override void BaseExtend()
        {
            this.Name.AddGroup(Groups.Workspace);
        }
    }
}