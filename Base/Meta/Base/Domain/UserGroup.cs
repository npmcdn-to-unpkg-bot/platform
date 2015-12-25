namespace Allors.Meta
{
    public partial class UserGroupClass : Class
    {
        #region Allors
        [Id("2f8cf270-a153-4e0d-b844-991d577222d4")]
        [AssociationId("46f531f2-b211-4f2a-902d-7198cda9c50d")]
        [RoleId("a1b92c88-79d9-4a4f-bb99-0fde4e251a28")]
        #endregion
        [Indexed]
        [Type(typeof(RoleClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType Role;

        #region Allors
        [Id("585bb5cf-9ba4-4865-9027-3667185abc4f")]
        [AssociationId("1e2d1e31-ed80-4435-8850-7663d9c5f41d")]
        [RoleId("c552f0b7-95ce-4d45-aaea-56bc8365eee4")]
        #endregion
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Member;

        #region Allors
        [Id("e94e7f05-78bd-4291-923f-38f82d00e3f4")]
        [AssociationId("75859e2c-c1a3-4f4c-bb37-4064d0aa81d0")]
        [RoleId("9d3c1eec-bf10-4a79-a37f-bc6a20ff2a79")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType Name;

        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
            this.Name.RoleType.IsUnique = true;
        }
    }
}