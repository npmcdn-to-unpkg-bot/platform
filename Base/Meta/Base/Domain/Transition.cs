namespace Allors.Meta
{
    public partial class TransitionClass : Class
    {
        #region Allors
        [Id("c6ee1a42-05fa-462b-b04f-811f01c6b646")]
        [AssociationId("ae7fa215-20bb-4472-9d25-ee3174f40fdb")]
        [RoleId("e79fa7b8-870a-4a6e-8522-bb39437e0650")]
        #endregion
        [Indexed]
        [Type(typeof(ObjectStateInterface))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType FromState;

        #region Allors
        [Id("dd19e7f8-83b7-4ff1-b475-02c4296b47e4")]
        [AssociationId("c88c9ab2-af38-45ca-9caa-fcb5715da129")]
        [RoleId("c68eb959-1b2c-48a7-b15a-944a576944ef")]
        #endregion
        [Indexed]
        [Type(typeof(ObjectStateInterface))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType ToState;
    }
}