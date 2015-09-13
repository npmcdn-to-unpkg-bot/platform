using System.ComponentModel.DataAnnotations;

namespace Allors.Meta
{
	#region Allors
	[Id("3a5dcec7-308f-48c7-afee-35d38415aa0b")]
	#endregion
	[Inherit(typeof(UniquelyIdentifiableInterface))]
	[Inherit(typeof(AccessControlledObjectInterface))]

	[Plural("Organisations")]
	public partial class OrganisationClass : Class
	{
        [Id("CBF9121E-A5E5-45C6-99FE-52FA80DC3220")]
        public MethodType JustDoIt;

        #region Allors
        [Id("01dd273f-cbca-4ee7-8c2d-827808aba481")]
		[AssociationId("ffc3b92f-860a-4e45-90e1-b9ba7ab27a27")]
		[RoleId("e567907e-ca61-4ec1-ab06-62dbb84e5d57")]
		#endregion
		[Indexed]
		[Type(typeof(AllorsStringUnit))]
		[Size(-1)]
		[Plural("Informations")]
		public RelationType Information;

		#region Allors
		[Id("15f33fa4-c878-45a0-b40c-c5214bce350b")]
		[AssociationId("4fdd9abb-f2e7-4f07-860e-27b4207224bd")]
		[RoleId("45bef644-dfcf-417a-9356-3c1cfbcada1b")]
		#endregion
		[Indexed]
		[Type(typeof(PersonClass))]
		[Plural("Shareholders")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType Shareholder;

		#region Allors
		[Id("17e55fcd-2c82-462b-8e31-b4a515acdaa9")]
		[AssociationId("e6fc633a-de9d-42a5-af03-b2359b2c2ea4")]
		[RoleId("6ab3328a-0fe1-4e98-b10d-eee420a90ffb")]
		#endregion
		[Indexed]
		[Type(typeof(ImageClass))]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType Image;

		#region Allors
		[Id("2cc74901-cda5-4185-bcd8-d51c745a8437")]
		[AssociationId("896a4589-4caf-4cd2-8365-c4200b12f519")]
		[RoleId("baa30557-79ff-406d-b374-9d32519b2de7")]
		#endregion
		[Indexed]
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("Names")]
		public RelationType Name;

		#region Allors
		[Id("2cfea5d4-e893-4264-a966-a68716839acd")]
		[AssociationId("c3c93567-1d78-42ea-a8cf-77549cd1a235")]
		[RoleId("d5965473-66cd-44b2-8048-a521c9cdadd0")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(-1)]
		[Plural("Descriptions")]
		public RelationType Description;

		#region Allors
		[Id("49b96f79-c33d-4847-8c64-d50a6adb4985")]
		[AssociationId("b031ef1a-0102-4b19-b85d-aa9c404596c3")]
		[RoleId("b95c7b34-a295-4600-82c8-826cc2186a00")]
		#endregion
		[Type(typeof(PersonClass))]
		[Plural("Employees")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType Employee;

		#region Allors
		[Id("5fa25b53-e2a7-44c8-b6ff-f9575abb911d")]
		[AssociationId("6a382c73-c6a2-4d8b-bc85-4623ede54298")]
		[RoleId("1c3dec18-978c-470a-8857-5210b9267185")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("Incorporateds")]
		public RelationType Incorporated;

		#region Allors
		[Id("68c61cea-4e6e-4ed5-819b-7ec794a10870")]
		[AssociationId("8494ad76-3422-4799-b5a6-caa077e53aca")]
		[RoleId("90489246-8590-4578-8b8d-716a25abd27d")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("AreSupplier")]
		public RelationType IsSupplier;

		#region Allors
		[Id("7046c2b4-d458-4343-8446-d23d9c837c84")]
		[AssociationId("0671f523-a557-41e1-9d05-0e89d8d1ae2d")]
		[RoleId("c84a6696-a1e9-4794-86c3-50e1f009c845")]
		#endregion
		[Type(typeof(AllorsDateTimeUnit))]
		[Plural("IncorporationDates")]
		public RelationType IncorporationDate;

		#region Allors
		[Id("73f23588-1444-416d-b43c-b3384ca87bfc")]
		[AssociationId("d1a098bf-a3d8-4b71-948f-a77ae82f02db")]
		[RoleId("a365f0ee-a94f-4435-a7b1-c92ac804a845")]
		#endregion
		[Indexed]
		[Type(typeof(AddressInterface))]
		[Plural("Addresses")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType Address;

		#region Allors
		[Id("845ff004-516f-4ad5-9870-3d0e966a9f7d")]
		[AssociationId("3820f65f-0e79-4f30-a973-5d17dca6ad33")]
		[RoleId("58d7df91-fbc5-4bcb-9398-a9957949402b")]
		#endregion
		[Indexed]
		[Type(typeof(PersonClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType Owner;

        #region Allors
        [Id("DBEF262D-7184-4B98-8F1F-CF04E884BB92")]
        [AssociationId("ED76A631-00C4-4753-B3D4-B3A53B9ECF4A")]
        [RoleId("19DE0627-FB1C-4F55-9B65-31D8008D0A48")]
        #endregion
        [Indexed]
        [Type(typeof(PersonClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType Manager;

        #region Allors
        [Id("b201d2a0-2335-47a1-aa8d-8416e89a9fec")]
		[AssociationId("e332003a-0287-4aab-9d95-257146ee4f1c")]
		[RoleId("b1f5b479-e4d0-46de-8ad4-347076d9f180")]
		#endregion
		[Indexed]
		[Type(typeof(ImageClass))]
		[Plural("Logos")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Logo;

		#region Allors
		[Id("bac702b8-7874-45c3-a410-102e1caea4a7")]
		[AssociationId("8c2ce648-3942-4ead-9772-308c29bc905e")]
		[RoleId("26a60588-3c90-4f4e-9bb6-8f45fe8f9606")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("Sizes")]
		public RelationType Size;

		#region Allors
		[Id("ddcea177-0ed9-4247-93d3-2090496c130c")]
		[AssociationId("944d024b-81eb-442f-8f50-387a588d2373")]
		[RoleId("2c3bc00d-6715-4c1b-be78-753f7f306df0")]
		#endregion
		[Indexed]
		[Type(typeof(AddressInterface))]
		[Plural("MainAddresses")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType MainAddress;
        
		public static OrganisationClass Instance {get; internal set;}

        public Tree AngularEmployees { get; private set; }
        public Tree AngularShareholders { get; private set; }

        internal OrganisationClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void TestExtend()
        {
            this.Description.RoleType.DataTypeAttribute = new DataTypeAttribute(DataType.MultilineText);

            this.Information.RoleType.DisplayAttribute = new DisplayAttribute { Name = "Ik ben het label" };
            this.Information.RoleType.DataTypeAttribute = new DataTypeAttribute(DataType.Html);

            var organisation = this;
            this.AngularEmployees = new Tree(organisation)
                .Add(organisation.Employee);

            var person = PersonClass.Instance;
            this.AngularShareholders = new Tree(organisation)
                .Add(organisation.Shareholder, new Tree(person)
                        .Add(person.Photo));
        }
    }
}