namespace Domain
{
	public class CompanyMeta
	{
		public static readonly global::Allors.R1.Meta.ObjectType ObjectType = (Allors.R1.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("b1b6361e-5ee5-434c-9c92-46c6166195c4") );

		public static readonly global::Allors.R1.Meta.RoleType Manager = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("08ab248d-bdb1-49c5-a2da-d6485f49239f"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Employee = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1a4087de-f116-4f79-9441-31faee8054f3"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType FirstPerson = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("28021756-f15f-4671-aa01-a40d3707d61a"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType NamedOneSort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("2f9fc05e-c904-4056-83f0-a7081762594a"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Owner = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("44abca14-9fb2-42a7-b8ab-a1ca87d87b2e"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType IndexedMany2ManyPerson = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("509c5341-3d87-4da4-a807-5567d897169b"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType PersonOneSort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("62b4ddac-efd7-4fc9-bbed-91c831a62f01"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType PersonManySort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("64c1be0a-0636-4da0-8404-2a93ab600cd9"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType NamedManySort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("996d27ff-3615-4a51-9214-944fac566a11"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType PersonManySort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("a9f60154-6bd1-4c76-94eb-edfd5beb6749"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType PersonOneSort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("bdf71d38-8082-4a99-9636-4f4ec26fd45c"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType NamedManySort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c1f68661-4999-4851-9224-1878258b6a58"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Many2ManyPerson = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c53bdaea-c0a5-4179-bfbb-e12de45e2ae0"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Child = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cde0a8e7-1a14-4f1a-a0ca-a305f0548df8"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType NamedOneSort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cdf04399-aa37-4ea2-9ac8-bf6d19884933"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Name = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("ce43ca5e-4dfb-4fe1-98ea-17d8382e9531"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Index = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("fdad723a-f062-492a-989c-8d8727c52679"))).RoleType;

		public static readonly global::Allors.R1.Meta.AssociationType PersonsWhereCompany = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("6cc83cb8-cb94-4716-bb7d-e25201f06b20"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompanyWhereChild = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cde0a8e7-1a14-4f1a-a0ca-a305f0548df8"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompanyWhereNamedOneSort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("2f9fc05e-c904-4056-83f0-a7081762594a"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompaniesWhereNamedManySort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("996d27ff-3615-4a51-9214-944fac566a11"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompaniesWhereNamedManySort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c1f68661-4999-4851-9224-1878258b6a58"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompanyWhereNamedOneSort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cdf04399-aa37-4ea2-9ac8-bf6d19884933"))).AssociationType;

	}
}