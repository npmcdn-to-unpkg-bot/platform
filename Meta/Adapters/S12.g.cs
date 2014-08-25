namespace Domain
{
	public class S12Meta
	{
		public static readonly global::Allors.Meta.MetaObject ObjectType = (Allors.Meta.MetaObject)global::Domain.M.D.Find( new System.Guid("c5747a64-f468-4d0d-80f3-6463bd32b0ca") );

		public static readonly global::Allors.Meta.MetaRole S12AllorsString = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("06fabe71-737a-4cff-ac10-2d15dafce503"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12AllorsDateTime = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("2eb9e232-4ed4-4997-a21a-f11bb0fe3b0e"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12C2many2many = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("39f50108-df59-455d-8371-fc07f3dbb7ef"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12C2many2one = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("61e8c425-407e-408b-9f2e-c95548833004"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12C2one2one = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("830117d4-fbe1-4944-bacf-54331e8451d7"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12C2one2many = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("a3aac482-aad0-4b59-9361-51b23867e5a2"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12AllorsBoolean = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("a97eca8e-807b-4a06-9587-6240f6150203"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12AllorsDouble = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("acc4ae39-2d5c-4485-be22-87b27e84b627"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12AllorsInteger = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("d07313ca-fd8d-4c74-928e-41274aa28de9"))).RoleType;
		public static readonly global::Allors.Meta.MetaRole S12AllorsDecimal = ((Allors.Meta.MetaRelation)global::Domain.M.D.Find( new System.Guid("f7ace363-89bd-4ea5-a865-4a6e3de2d723"))).RoleType;

	}
}