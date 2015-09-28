namespace Allors.Meta
{
    #region Allors

    [Id("c799ca62-a554-467d-9aa2-1663293bb37f")]

    #endregion
    [Inherit(typeof(UserInterface))]
    [Inherit(typeof(AccessControlledObjectInterface))]
    [Inherit(typeof(UniquelyIdentifiableInterface))]
    public partial class PersonClass : Class
    {
        #region Allors

        [Id("ed4b710a-fe24-4143-bb96-ed1bd9beae1a")]
        [AssociationId("1ea9eca4-eed0-4f61-8903-c99feae961ad")]
        [RoleId("f10ea049-6d24-4ca2-8efa-032fcf3000b3")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType FirstName;

        #region Allors

        [Id("8a3e4664-bb40-4208-8e90-a1b5be323f27")]
        [AssociationId("9b48ff56-afef-4501-ac97-6173731ff2c9")]
        [RoleId("ace04ad8-bf64-4fc3-8216-14a720d3105d")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType LastName;

        #region Allors

        [Id("eb18bb28-da9c-47b4-a091-2f8f2303dcb6")]
        [AssociationId("e3a4d7b2-c5f1-4101-9aab-a0135d37a5a5")]
        [RoleId("a86fc7a6-dedd-4da9-a250-75c9ec730d22")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType MiddleName;

        internal PersonClass()
            : base(MetaPopulation.Instance)
        {
        }

        public static PersonClass Instance { get; internal set; }
    }
}