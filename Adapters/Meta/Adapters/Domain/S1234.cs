namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("c3c0ecf3-9f8d-4701-854f-8ddea1bd69fd")]
	#endregion
  	public partial class S1234Interface: Interface
	{
		#region Allors
		[Id("012a43d3-e1e0-4693-a771-1526c29b7ac4")]
		[AssociationId("88f61f13-20e0-4ef0-a42c-80ee1c8e001b")]
		[RoleId("e77d108f-e8db-4e8c-89ae-574c7362a159")]
		#endregion
		[Type(typeof(AllorsFloatUnit))]
		public RelationType AllorsDouble;

		#region Allors
		[Id("2ac36edd-d718-4252-b7cf-74849e1fca6e")]
		[AssociationId("c932a3da-ab1f-4a99-9d04-7d2a00425328")]
		[RoleId("54eac29a-4aee-4fd2-b6e5-4f70e9d04c2f")]
		#endregion
		[Type(typeof(AllorsDecimalUnit))]
		[Precision(19)]
		[Scale(2)]
		public RelationType AllorsDecimal;

		#region Allors
		[Id("46263379-afd4-4472-bb05-057fb88163ab")]
		[AssociationId("1675c94a-b570-4d27-a765-652ba71cbb4e")]
		[RoleId("e7793a66-e307-420e-9fb6-6a057fcf8094")]
		#endregion
		[Type(typeof(AllorsIntegerUnit))]
		public RelationType AllorsInteger;

		#region Allors
		[Id("4b846355-000b-4651-bff2-51f1275c1461")]
		[AssociationId("e5bba0bd-a176-48c5-8b8f-b172b0712804")]
		[RoleId("f9d2c447-1b98-4473-b9e3-d4c3a5cdf954")]
		#endregion
		[Type(typeof(S1234Interface))]
		[Plural("S1234many2one")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType S1234many2one;

		#region Allors
		[Id("58a56dee-c613-4d76-ab99-5608e7709cd8")]
		[AssociationId("1066981e-8c9b-44fa-b759-fa3bf62bc195")]
		[RoleId("9a580256-37b9-45f5-9c77-6c13454e8fb1")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2one2ones")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType C2one2one;

		#region Allors
		[Id("73302b50-8526-40ae-a202-5b17e1093629")]
		[AssociationId("4548670a-0f02-4760-b325-229d3f74213e")]
		[RoleId("21509a16-a977-4fe8-a413-34c6da8d77c0")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType C2many2many;

		#region Allors
		[Id("8fb24e1c-9e04-4b3d-8a97-153d3c0ea7ec")]
		[AssociationId("468742f0-6fb4-4ca6-9b35-9a08f208ca8a")]
		[RoleId("67068383-1966-4bcc-a0da-3fe42278a263")]
		#endregion
		[Type(typeof(S1234Interface))]
		[Plural("S1234one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType S1234one2many;

		#region Allors
		[Id("94a49847-273f-4e9b-b07b-d615d994757a")]
		[AssociationId("a9421230-08a7-4ad0-8206-4732ac3f3413")]
		[RoleId("3d8fee19-54b4-4ee9-8de6-8fc267fd4daf")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType C2one2many;

		#region Allors
		[Id("a2e7c6f6-ca0d-4fb3-9431-8dd1be7ebdb7")]
		[AssociationId("44861773-ca9e-462f-b05b-0f2309208ebf")]
		[RoleId("6534a951-0ee2-4574-b54f-3f2fdf8f694b")]
		#endregion
		[Type(typeof(S1234Interface))]
		[Plural("S1234many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType S1234many2many;

		#region Allors
		[Id("b299db28-1107-4120-946c-fbdad2271c5c")]
		[AssociationId("385dc31e-b277-484c-b448-9c532a0196ba")]
		[RoleId("4716308c-67b8-492b-a9d5-0be18ade8344")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("ClassNames")]
		public RelationType ClassName;

		#region Allors
		[Id("c13e8484-75a3-40be-afd5-44a31aca3771")]
		[AssociationId("da6964f3-54fa-4467-bacd-48764783f413")]
		[RoleId("29672f22-7d09-4bca-b8a2-4170a8a8a8b1")]
		#endregion
		[Type(typeof(AllorsDateTimeUnit))]
		public RelationType AllorsDateTime;

		#region Allors
		[Id("c2fac2fc-14c6-4aa3-89ff-afba1316d06d")]
		[AssociationId("b334c35e-f8aa-4047-b8c1-111cd570b26a")]
		[RoleId("e6dc50eb-0d05-4f9a-82c2-9af6bbc82eda")]
		#endregion
		[Type(typeof(S1234Interface))]
		[Plural("S1234one2one")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType S1234one2one;

		#region Allors
		[Id("df9eb36a-366f-4a5a-a750-f2f23f681c74")]
		[AssociationId("479c2fd7-dda6-4efa-b031-e0584926f66a")]
		[RoleId("4e227376-5396-4838-bc23-f6bf4f530b75")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2many2ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType C2many2one;

		#region Allors
		[Id("e6164217-2f54-4134-8c53-4a45caa9dd11")]
		[AssociationId("f14300c4-bacf-4541-914e-ec5781ada1d9")]
		[RoleId("986c626f-1bcb-480c-9e2d-0da297340fc9")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;

		#region Allors
		[Id("ef45cd72-2e16-47df-b949-c803a554b307")]
		[AssociationId("f5db9b49-cc7c-42d2-b818-8f9d0a16e2ae")]
		[RoleId("ace6cdc5-e66f-40dd-bd37-45db231cc9e8")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		public RelationType AllorsBoolean;



		public static S1234Interface Instance {get; internal set;}

		internal S1234Interface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "S1234";
			this.PluralName = "S1234s";
        }
	}
}