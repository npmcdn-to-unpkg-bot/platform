namespace Domain
{
	public class NamedMeta
	{
		public static readonly global::Allors.R1.Meta.ObjectType ObjectType = (Allors.R1.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("fcaa52e3-4a90-4981-b45d-d158e2589506") );

		public static readonly global::Allors.R1.Meta.RoleType Name = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("ce43ca5e-4dfb-4fe1-98ea-17d8382e9531"))).RoleType;
		public static readonly global::Allors.R1.Meta.RoleType Index = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("fdad723a-f062-492a-989c-8d8727c52679"))).RoleType;

		public static readonly global::Allors.R1.Meta.AssociationType CompanyWhereNamedOneSort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("2f9fc05e-c904-4056-83f0-a7081762594a"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompaniesWhereNamedManySort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("996d27ff-3615-4a51-9214-944fac566a11"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompaniesWhereNamedManySort2 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c1f68661-4999-4851-9224-1878258b6a58"))).AssociationType;
		public static readonly global::Allors.R1.Meta.AssociationType CompanyWhereNamedOneSort1 = ((Allors.R1.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("cdf04399-aa37-4ea2-9ac8-bf6d19884933"))).AssociationType;

	}
}