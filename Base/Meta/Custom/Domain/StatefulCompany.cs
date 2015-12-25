namespace Allors.Meta
{
    public partial class StatefulCompanyClass : Class
    {
        #region Allors
        [Id("6c848eeb-7b42-45ea-81ac-fa983e1e0fa9")]
        [AssociationId("be566287-a26d-46fb-a4f2-1fc8bf1c1de4")]
        [RoleId("2a482b25-a154-4306-87f3-b6cd7af3c80d")]
        #endregion
        [Indexed]
        [Type(typeof(PersonClass))]
        [Plural("Employees")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Employee;

        #region Allors
        [Id("6e429d87-ea80-465e-9aa6-0f7d546b6bb3")]
        [AssociationId("de607129-6f68-4db6-a6ca-6ba53cae698d")]
        [RoleId("94570d2c-2a5e-451f-905e-6ca61a469a31")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        [Plural("Names")]
        public RelationType Name;

        #region Allors
        [Id("9940e8ed-189e-42c6-b0d1-7c01920b9fac")]
        [AssociationId("de4a92c8-4e08-4f37-9d6c-321dcce89e1c")]
        [RoleId("3becaaa8-7b49-4616-8d79-a7bf04d9e666")]
        #endregion
        [Indexed]
        [Type(typeof(PersonClass))]
        [Plural("Managers")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Manager;
    }
}