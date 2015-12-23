namespace Allors.Meta
{
    #region Allors
    [Id("7fded183-3337-4196-afb0-3266377944bc")]
    #endregion
    [Inherit(typeof(DeletableInterface))]
    [Inherit(typeof(AccessControlledObjectInterface))]
    public partial class PermissionClass : Class
    {
        #region Allors
        [Id("097bb620-f068-440e-8d02-ef9d8be1d0f0")]
        [AssociationId("3442728c-164a-477c-87be-19a789229585")]
        [RoleId("3fd81194-2f6f-43e7-9c6b-91f5e3e118ac")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsUniqueUnit))]
        public RelationType OperandTypePointer;

        #region Allors
        [Id("29b80857-e51b-4dec-b859-887ed74b1626")]
        [AssociationId("8ffed1eb-b64e-4341-bbb6-348ed7f06e83")]
        [RoleId("cadaca05-55ba-4a13-8758-786ff29c8e46")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsUniqueUnit))]
        public RelationType ConcreteClassPointer;

        #region Allors
        [Id("9d73d437-4918-4f20-b9a7-3ce23a04bd7b")]
        [AssociationId("891734d6-4855-4b33-8b3b-f46fd6103149")]
        [RoleId("d29ce0ed-fba8-409d-8675-dc95e1566cfb")]
        #endregion
        [Indexed]
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType OperationEnum;

        public static PermissionClass Instance { get; internal set; }

        internal PermissionClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.OperandTypePointer.RoleType.IsRequired = true;
            this.ConcreteClassPointer.RoleType.IsRequired = true;
            this.OperationEnum.RoleType.IsRequired = true;
        }
    }
}