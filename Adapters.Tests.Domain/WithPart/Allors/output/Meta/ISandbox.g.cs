namespace Domain
{
	public class ISandboxMeta
	{
		public static readonly global::Allors.R1.Meta.ObjectType ObjectType = (Allors.R1.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("7ba2ab26-491b-49eb-944c-26f6bb66e50f") );

		public static readonly global::Allors.R1.Meta.RoleType InvisibleValue = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("38361bff-62b3-4607-8291-cfdaeedbd36d"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType InvisibleMany = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("796ab057-88a0-4d71-bc4a-2673a209161b"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType InvisibleOne = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("dba5deb2-880d-47f4-adae-0b3125ff1379"))).RoleType;

		public static readonly global::Allors.R1.Meta.AssociationType ISandboxesWhereInvisibleMany = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("796ab057-88a0-4d71-bc4a-2673a209161b"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType ISandboxWhereInvisibleOne = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("dba5deb2-880d-47f4-adae-0b3125ff1379"))).AssociationType;

	}
}