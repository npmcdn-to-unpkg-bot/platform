namespace Domain
{
	public class A2Meta
	{
		public static readonly global::Allors.Meta.ObjectType ObjectType = (Allors.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("460d0222-0a23-4610-853c-fddaa7fd2bee") );

		public static readonly global::Allors.Meta.RoleType A2AllorsString = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("74cc2ab0-19c4-48b4-890d-e14a2ff78765"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType A1sWhereA2Many2Many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("39bb9b08-ff8c-4dd1-bf6a-87dde81998c5"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType A1WhereA2One2One = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("b9edde7c-979a-4902-9cc6-332182eaef3e"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType A1sWhereA2Many2One = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cc0fe732-70e4-46ea-92b7-9cf218ae956c"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType A1WhereA2One2Many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("d69cd491-de29-4bae-aeb2-5254cb4f37d8"))).AssociationType;

	}
}