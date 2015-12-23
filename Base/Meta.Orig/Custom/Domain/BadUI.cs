namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("bb1b0a2e-66d1-4e09-860f-52dc7145029e")]
	#endregion
	[Plural("BadUIs")]
	public partial class BadUIClass : Class
	{
		#region Allors
		[Id("8a999086-ca90-40a1-90ae-475d231bb1eb")]
		[AssociationId("0ce20e7c-7be0-4c07-a179-e8d0e77f3de1")]
		[RoleId("4ab20876-f8fc-4d39-87d7-8758f044587b")]
		#endregion
		[Indexed]
		[Type(typeof(PersonClass))]
		[Plural("PersonsMany")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType PersonMany;

		#region Allors
		[Id("9ebbb9d1-2ca7-4a7f-9f18-f25c05fd28c1")]
		[AssociationId("37c64e26-a391-4c7b-98fb-53ccb5fbc795")]
		[RoleId("4d2c7c20-b9c7-451b-b6b1-8552322ceddd")]
		#endregion
		[Indexed]
		[Type(typeof(OrganisationClass))]
		[Plural("CompanyOnes")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType CompanyOne;

		#region Allors
		[Id("a4db0d75-3dff-45ac-9c1d-623bca046b4a")]
		[AssociationId("5ed577d8-f048-42b8-9fb4-38b88ebf35f1")]
		[RoleId("c1b45f09-59fe-4484-8999-e2a3d9147919")]
		#endregion
		[Indexed]
		[Type(typeof(PersonClass))]
		[Plural("PersonOnes")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType PersonOne;

		#region Allors
		[Id("a8621048-48b5-43c4-b10b-17225958d177")]
		[AssociationId("718eaf0b-1b62-43b2-b336-c9820d806b28")]
		[RoleId("1663525b-5add-4a96-a596-5d736d466985")]
		#endregion
		[Indexed]
		[Type(typeof(OrganisationClass))]
		[Plural("CompanyManies")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType CompanyMany;

		#region Allors
		[Id("c93a102e-ecdb-4189-a0fc-eeea8b4b85d4")]
		[AssociationId("2225f3e0-1304-4a55-9b89-29563fe52e3c")]
		[RoleId("7f2dc0db-4628-45a8-8cc5-2cc1b87e0eb3")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;



		public static BadUIClass Instance {get; internal set;}

		internal BadUIClass() : base(MetaPopulation.Instance)
        {
        }
	}
}