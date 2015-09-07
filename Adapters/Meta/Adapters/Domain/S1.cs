namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("15c3bb71-075d-48ad-8a00-250c2f627092")]
	#endregion
	[Inherit(typeof(S1234Interface))]

  	public partial class S1Interface: Interface
	{
		#region Allors
		[Id("294e7ce3-1b0b-490a-a5e8-6149885d4943")]
		[AssociationId("35b9e89a-2962-47a6-87be-5c3e6a5c553a")]
		[RoleId("eeb24332-c825-4e1b-9d65-4e7f93062aae")]
		#endregion
		[Type(typeof(AllorsDecimalUnit))]
		[Precision(19)]
		[Scale(2)]
		public RelationType AllorsDecimal;

		#region Allors
		[Id("4cd28d56-ffd6-461c-b9ed-ca0e4bae51df")]
		[AssociationId("a0dd3d9e-d722-43c4-be7c-27a63995a4bb")]
		[RoleId("592c39ed-47f8-4f42-8ab4-26279627c5d4")]
		#endregion
		[Type(typeof(AllorsIntegerUnit))]
		public RelationType AllorsInteger;

		#region Allors
		[Id("55ab6cfa-651b-48ec-bc33-ad3a381d2260")]
		[AssociationId("3abf073d-bbbb-4f97-bfa3-df6b07c233d2")]
		[RoleId("e6b89bb6-dfd9-4605-afff-21f1360bc5cb")]
		#endregion
		[Type(typeof(AllorsBinaryUnit))]
		[Size(-1)]
		public RelationType AllorsBinary;

		#region Allors
		[Id("645c20ac-5b4f-40db-8d11-d2b07123dabe")]
		[AssociationId("2d19869d-592e-4e14-bc5f-3557266ed8c1")]
		[RoleId("7121814d-81c8-4224-a77c-f873cae73b74")]
		#endregion
		[Type(typeof(AllorsUniqueUnit))]
		public RelationType AllorsUnique;

		#region Allors
		[Id("678b14c4-b5ae-48e3-ac06-2459cab66c34")]
		[AssociationId("69651335-2e01-432f-af18-4f61c9c0edcf")]
		[RoleId("22b95a80-c349-46a3-88cf-01a8fa6faeee")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(100000)]
		[Plural("StringsLarge")]
		public RelationType StringLarge;

		#region Allors
		[Id("6a166388-5bca-4cd9-bfee-0da27cbc3073")]
		[AssociationId("0532c685-8ee2-44c3-b7a1-46f3717c76d5")]
		[RoleId("ac61e38a-fdfb-41a1-88f1-565b77079122")]
		#endregion
		[Type(typeof(S2Interface))]
		[Plural("S2many2ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType S2many2one;

		#region Allors
		[Id("6ee98698-15dc-4998-88c3-d2a4d1c19e8c")]
		[AssociationId("0b75718e-1648-438c-b0fe-70e4f05623c8")]
		[RoleId("e88fdde4-a3e8-4ee0-85d6-7e5fc3047b48")]
		#endregion
		[Type(typeof(S2Interface))]
		[Plural("S2one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType S2one2many;

		#region Allors
		[Id("701ca57d-241f-470c-b690-9045c0f76c8f")]
		[AssociationId("1310f3cf-dfc3-4a28-846a-1b2a32e73930")]
		[RoleId("0c610a6d-839f-4578-9ede-23afa29c3205")]
		#endregion
		[Type(typeof(AllorsFloatUnit))]
		public RelationType AllorsDouble;

		#region Allors
		[Id("70815e0c-11d4-41ac-b0b2-105f8ede6d27")]
		[AssociationId("76f42563-7820-49e4-90dc-24d7a6c96254")]
		[RoleId("c5bc1216-fcfb-4e84-bd7c-5ad9f64de637")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;

		#region Allors
		[Id("77afee4a-08b7-4231-aa73-575145efd1e3")]
		[AssociationId("fd26d004-ed7a-4561-b370-737c37e2c3b3")]
		[RoleId("bb03281d-aa61-4411-a696-df294b5c6bfe")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1many2ones")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType C1many2one;

		#region Allors
		[Id("8f5485ba-5a82-4d01-809e-52b467f958d8")]
		[AssociationId("57120147-1a4b-4328-8ca0-4cd44e5a157e")]
		[RoleId("8c79f471-118e-4f05-9069-7be505b2af71")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1one2ones")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType C1one2one;

		#region Allors
		[Id("9fbcf7ce-3b59-458d-ab5e-9c48dd3842b3")]
		[AssociationId("6e426f4b-3dc1-4c13-a03f-3875997f7ba5")]
		[RoleId("75f3149d-52c9-4aca-bee7-1d24a2a5751b")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		public RelationType AllorsBoolean;

		#region Allors
		[Id("c0cfe3ee-d184-40bd-8354-b0b0bd4e641c")]
		[AssociationId("8e91bf46-bbb9-4b70-93ba-1f0ebd24c38e")]
		[RoleId("3c0b8c2c-bd01-4fa7-9902-50ecec0a76ee")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType C1many2many;

		#region Allors
		[Id("c6f49460-a259-44de-b674-4d0585fe00cd")]
		[AssociationId("9c577162-b64e-4457-bb57-dbfe690bb36d")]
		[RoleId("75d4eda9-54d4-49ae-8ba6-a1dc9af34937")]
		#endregion
		[Type(typeof(S2Interface))]
		[Plural("S2many2manies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType S2many2many;

		#region Allors
		[Id("dc22175f-185d-4cd3-b492-74b0a9389c91")]
		[AssociationId("d6e64cd0-1f37-4e70-acba-fbe0faee8f07")]
		[RoleId("1505b48d-b4ce-406c-a5cb-69151b3d391e")]
		#endregion
		[Type(typeof(S2Interface))]
		[Plural("S2one2ones")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType S2one2one;

		#region Allors
		[Id("e263ac2b-822d-4aa4-8a8c-67db3f2b4bb0")]
		[AssociationId("dfb5ca2c-a44f-49a4-b967-c7eeab9d66fa")]
		[RoleId("919db239-03a7-488f-81fd-4930a41fa42b")]
		#endregion
		[Type(typeof(AllorsDateTimeUnit))]
		public RelationType AllorsDateTime;

		#region Allors
		[Id("ef918b82-87f4-4591-bf19-2fd5a1019ece")]
		[AssociationId("37261db2-118c-4c9a-a184-4956fe1e4c29")]
		[RoleId("eed5fe88-d325-4567-b9db-95d139741920")]
		#endregion
		[Type(typeof(C1Class))]
		[Plural("C1one2manies")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType C1one2many;



		public static S1Interface Instance {get; internal set;}

		internal S1Interface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "S1";
			this.PluralName = "S1s";
        }
	}
}