namespace Domain
{
	public class UserMeta
	{
		public static readonly global::Allors.Meta.ObjectType ObjectType = (Allors.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("0d6bc154-112b-4a58-aa96-3b2a96f82523") );

		public static readonly global::Allors.Meta.RoleType Select = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1ffa3cb7-41f0-406a-a3a5-2f3a4c5ad59c"))).RoleType;
		public static readonly global::Allors.Meta.RoleType From = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("bc6b71a8-2a66-4b57-9c86-ecf521b973ba"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType UsersWhereSelect = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1ffa3cb7-41f0-406a-a3a5-2f3a4c5ad59c"))).AssociationType;

	}
}