namespace Domain
{
	public class SandboxMeta
	{
		public static readonly global::Allors.Meta.MetaObject ObjectType = (Allors.Meta.MetaObject)global::Domain.M.D.Find( new System.Guid("73970b0f-1ff4-4d39-aad8-fdbfbaae472f") );

		public static readonly global::Allors.Meta.MetaRole InvisibleMany = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("0e0ee030-8fb5-42fb-82b5-5daade2aca9d"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole InvisibleOne = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("122b0376-8d1a-4d46-b8a0-9f4ea94c9e96"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole InvisibleValue = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("5eec5096-d8ba-424e-988f-b50828fc7b51"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole Test = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("856a0161-2a46-428a-bae5-95d6a86a89e8"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole AllorsString = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("c82d1693-7b88-4fab-8389-a43185c832ed"))).RoleType;

		public static readonly global::Allors.Meta.MetaAssociation SandboxesWhereInvisibleMany = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("0e0ee030-8fb5-42fb-82b5-5daade2aca9d"))).AssociationType;
		public static readonly global::Allors.Meta.MetaAssociation SandboxWhereInvisibleOne = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("122b0376-8d1a-4d46-b8a0-9f4ea94c9e96"))).AssociationType;

	}
}