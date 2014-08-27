namespace Domain
{
	public class PersonMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Domain.M.D.Find( new System.Guid("6a082a25-a8f2-4acd-a1a3-ba4461b729f1") );

		public static readonly global::Allors.Meta.RoleType NextPerson = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("25ff791d-9547-41ba-ac34-f2fe501ef217"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Company = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("6cc83cb8-cb94-4716-bb7d-e25201f06b20"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Name = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("ce43ca5e-4dfb-4fe1-98ea-17d8382e9531"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Index = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("fdad723a-f062-492a-989c-8d8727c52679"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType CompaniesWhereManager = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("08ab248d-bdb1-49c5-a2da-d6485f49239f"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType EmployerWhereEmployee = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1a4087de-f116-4f79-9441-31faee8054f3"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType PersonWhereNextPerson = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("25ff791d-9547-41ba-ac34-f2fe501ef217"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompanyWhereFirstPerson = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("28021756-f15f-4671-aa01-a40d3707d61a"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWhereOwner = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("44abca14-9fb2-42a7-b8ab-a1ca87d87b2e"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWhereIndexedMany2ManyPerson = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("509c5341-3d87-4da4-a807-5567d897169b"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompanyWherePersonOneSort1 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("62b4ddac-efd7-4fc9-bbed-91c831a62f01"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWherePersonManySort1 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("64c1be0a-0636-4da0-8404-2a93ab600cd9"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWherePersonManySort2 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("a9f60154-6bd1-4c76-94eb-edfd5beb6749"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompanyWherePersonOneSort2 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("bdf71d38-8082-4a99-9636-4f4ec26fd45c"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWhereMany2ManyPerson = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c53bdaea-c0a5-4179-bfbb-e12de45e2ae0"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompanyWhereNamedOneSort2 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("2f9fc05e-c904-4056-83f0-a7081762594a"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWhereNamedManySort1 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("996d27ff-3615-4a51-9214-944fac566a11"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompaniesWhereNamedManySort2 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c1f68661-4999-4851-9224-1878258b6a58"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType CompanyWhereNamedOneSort1 = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cdf04399-aa37-4ea2-9ac8-bf6d19884933"))).AssociationType;

	}
}