namespace Domain
{
	public class S2Meta
	{
		public static readonly global::Allors.Meta.ObjectType ObjectType = (Allors.Meta.ObjectType)global::Domain.M.D.Find( new System.Guid("feeb7027-7c6c-4cb5-8718-93e6e8a4afd8") );

		public static readonly global::Allors.Meta.RoleType S2AllorsString = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1c758737-140a-49f0-badc-29658b4bc55f"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsInteger = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("1f5a6afe-f458-43db-bea0-8c90074b5abf"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsDouble = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("74dd2b7b-e647-4967-9838-46c701baf3a7"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsBoolean = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("9a191c76-bd05-498f-91da-33184c72fe90"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsDecimal = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("9d70a5f5-ed72-4ba3-98ac-e50752f8fb79"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsDateTime = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("a305d91a-5fe1-467d-9f24-6cce5dd30b1d"))).RoleType;
		public static readonly global::Allors.Meta.RoleType S2AllorsLong = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("bbb8d0fa-fe1e-49a6-a18d-0c790e52bb0c"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType C1sWhereS2many2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("2cee32ad-4e62-4112-9775-f84b0298e93a"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType I1WhereS2one2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("68549750-b8f9-4a29-a078-803e7348e142"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType S1sWhereS2many2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("6a166388-5bca-4cd9-bfee-0da27cbc3073"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType S1WhereS2one2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("6ee98698-15dc-4998-88c3-d2a4d1c19e8c"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType C1WhereS2one2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("92cbd254-9763-41e1-9c73-4a378aab4b8e"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType I1sWhereS2many2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("973d6e4f-57ff-454a-9621-bd5dccb65525"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType I1WhereS2one2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("a77bcd80-82df-4b76-a1bc-8e78106d7d53"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType C1sWhereS2many2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("b2071550-cc1b-4543-b98f-006e7564a74b"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType S1sWhereS2many2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("c6f49460-a259-44de-b674-4d0585fe00cd"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType S1WhereS2one2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("dc22175f-185d-4cd3-b492-74b0a9389c91"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType C1WhereS2one2many = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("f47b9392-1391-416e-9a49-23ab0627133e"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType I1sWhereS2many2one = ((Allors.Meta.RelationType)global::Domain.M.D.Find( new System.Guid("fe51c02e-ed28-4628-9da1-7bc2131c8992"))).AssociationType;

	}
}