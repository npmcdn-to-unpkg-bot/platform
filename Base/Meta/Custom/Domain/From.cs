namespace Allors.Meta
{
    public partial class FromClass : Class
    {
        #region Allors
        [Id("d9a9896d-e175-410a-9916-9261d83aa229")]
        [AssociationId("a963f593-cad0-4fa9-96a3-3853f0f7d7c6")]
        [RoleId("775a29b8-6e21-4545-9881-d52f6eb7db8b")]
        #endregion
        [Indexed]
        [Type(typeof(ToClass))]
        [Plural("Tos")]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType To;
    }
}