namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("72c07e8a-03f5-4da8-ab37-236333d4f74e")]
	#endregion
	[Inherit(typeof(I2Interface))]
	[Inherit(typeof(I23Interface))]
	[Inherit(typeof(I12Interface))]

	public partial class C2Class : Class
	{
		#region Allors
		[Id("07eaa992-322a-40e9-bf2c-aa33b69f54cd")]
		[AssociationId("610f93b1-e108-4a4f-8285-a5fca8600ee3")]
		[RoleId("54370233-2451-4ba1-bbd6-e573cc885b84")]
		#endregion
		[Type(typeof(AllorsDecimalUnit))]
		[Precision(19)]
		[Scale(2)]
		public RelationType AllorsDecimal;

		#region Allors
		[Id("0947eb06-5511-475f-8d68-06cfb812678e")]
		[AssociationId("9a759774-19ae-437d-b5cb-320720a901db")]
		[RoleId("402141f3-3124-42fa-904f-9ed5c0ed1e6a")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType C1many2many;

		#region Allors
		[Id("0ecc2d3b-f813-44db-b349-3e67d7e0b2d7")]
		[AssociationId("86606949-6646-4b1e-bf65-acea7f33fd55")]
		[RoleId("c9ad84d3-8328-4432-bd15-b4d88cb4f357")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2many2ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType C2many2one;

		#region Allors
		[Id("262ad367-a52c-4d8b-94e2-b477bb098423")]
		[AssociationId("3ab98f7a-f38f-45ff-a15e-205974aaf8c6")]
		[RoleId("8aaedaa7-bea2-43cf-b0be-1401ef1b92d4")]
		#endregion
		[Type(typeof(AllorsFloatUnit))]
		public RelationType AllorsDouble;

		#region Allors
		[Id("42f9f4b6-3b35-4168-93cb-35171dbf83f4")]
		[AssociationId("bb11267f-3214-4456-8a49-1a350dbabb4f")]
		[RoleId("15d1699c-3c37-4f75-8b91-6082288053ec")]
		#endregion
		[Type(typeof(AllorsIntegerUnit))]
		public RelationType AllorsInteger;

		#region Allors
		[Id("49d04b6f-6393-49f6-bb6b-2dd634d6b9ee")]
		[AssociationId("43fff719-2350-41dd-91cb-e48f44ef3887")]
		[RoleId("4440c0ed-5057-4ee6-a690-f92f70dfa715")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType C2many2many;

		#region Allors
		[Id("61daaaae-dd22-405e-aa98-6321d2f8af04")]
		[AssociationId("f8465b8a-da28-4aa3-a09c-b5ce6f02324c")]
		[RoleId("261c56b2-6096-44dd-bf1d-4750ef29b9d6")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		public RelationType AllorsBoolean;

		#region Allors
		[Id("7ee9d97c-8ae3-438c-adfd-6a35b3ff645b")]
		[AssociationId("56c55810-c2d0-4f58-b03e-9deeac23c369")]
		[RoleId("5a4899a0-730b-455e-98cb-e1f2b778fea6")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1many2ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType C1many2one;

		#region Allors
		[Id("9540e8d3-9fe3-4aea-9918-fc31210f2622")]
		[AssociationId("dbac8142-7278-4a5c-9b99-be065fa4bf60")]
		[RoleId("ab18bbdb-96b8-4a03-a797-246bcd8bb16b")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1one2ones")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType C1one2one;

		#region Allors
		[Id("9c7cde3f-9b61-4c79-a5d7-afe1067262ce")]
		[AssociationId("41604445-0c6e-4d20-ae20-43619232438d")]
		[RoleId("757e03f2-be92-4af7-ba0b-fa0ce9f5bda2")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;

		#region Allors
		[Id("9e9d1c6a-f647-4922-b5f4-874b8b6c1907")]
		[AssociationId("a0d30124-e214-4b76-9442-af893f14baca")]
		[RoleId("5e574659-c186-4ab6-9e64-79eb8d32017c")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2one2ones")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType C2one2one;

		#region Allors
		[Id("a95948a7-3f12-4b85-8823-82dea87740c0")]
		[AssociationId("72bd6031-935f-4280-8048-188ca54e602e")]
		[RoleId("6248a324-04b8-423e-92ac-5ba38be3b72f")]
		#endregion
		[Type(typeof(C2Class))]
		[Plural("C2one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType C2one2many;

		#region Allors
		[Id("ce23482d-3a22-4202-98e7-5934fd9abd2d")]
		[AssociationId("e83be403-6722-450f-81ba-bd58bf2f8941")]
		[RoleId("0443f179-b2dd-49da-9e15-d0b348aa2dd9")]
		#endregion
		[Type(typeof(AllorsDateTimeUnit))]
		public RelationType AllorsDateTime;

		#region Allors
		[Id("d82be8f5-673a-466b-8abb-077be0bc6eb5")]
		[AssociationId("de720379-fc76-44b6-887c-00fe7916e4e5")]
		[RoleId("2e985306-893b-4686-bb39-775e1a0f87a7")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType C1one2many;

		#region Allors
		[Id("d92643c0-854c-40f8-92c8-93a0245e33c2")]
		[AssociationId("3df80a07-eb84-46e0-8c0f-53e64cf29d2c")]
		[RoleId("6d2f1196-5a1a-439c-b743-fed5540ac49c")]
		#endregion
		[Indexed]
		[Type(typeof(C3Class))]
		[Plural("C3Many2Manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType C3Many2Many;

		#region Allors
		[Id("f3482f88-4408-4e2e-b179-7f757bf0eb3d")]
		[AssociationId("9602ea68-e7f4-4c89-bad6-875813a4d59a")]
		[RoleId("e1087401-fa0a-49ee-8242-0836c2476d06")]
		#endregion
		[Indexed]
		[Type(typeof(C3Class))]
		[Plural("C3Many2Ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType C3Many2One;



		public static C2Class Instance {get; internal set;}

		internal C2Class() : base(MetaPopulation.Instance)
        {
			this.SingularName = "C2";
			this.PluralName = "C2s";
        }
	}
}