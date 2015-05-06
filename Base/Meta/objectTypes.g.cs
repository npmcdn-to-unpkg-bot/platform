namespace Allors.Meta
{
	using System;

	public partial class DependentClass : Class
	{
		public static DependentClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal DependentClass() : base(TestsDomain.Instance, new Guid("0cb8d2a7-4566-432f-9882-893b05a77f44"))
        {
			this.SingularName = "Dependent";
			this.PluralName = "Dependents";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("c0a77982-76d7-4760-bf24-53a6df3859b4"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var DependentDependee = new RelationType(TestsDomain.Instance, new Guid("8859af04-ba38-42ce-8ac9-f428c3f92f31"), new Guid("cd3972e6-8ad4-4b01-9381-4d18718c7538"), new Guid("d6b1d6b6-539b-4b12-9363-18e7e9ab632c"));
			DependentDependee.AssignedMultiplicity = Multiplicity.OneToOne;
			DependentDependee.IsIndexed = true;

			DependentDependee.AssociationType.ObjectType = this;

			DependentDependee.RoleType.ObjectType = DependeeClass.Instance;;
			this.Roles.Dependee = DependentDependee.RoleType;

			var DependentCounter = new RelationType(TestsDomain.Instance, new Guid("9884955e-74ed-4f9d-9362-8e0274c53bf9"), new Guid("5b97e356-9bcd-4c4e-be7a-ef577eef5f14"), new Guid("d067129b-8440-4fc7-80d3-832ce569fe54"));
			DependentCounter.AssignedMultiplicity = Multiplicity.OneToOne;
			DependentCounter.AssociationType.ObjectType = this;

			DependentCounter.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			DependentCounter.RoleType.AssignedSingularName = "Counter";
			DependentCounter.RoleType.AssignedPluralName = "Counters";
			this.Roles.Counter = DependentCounter.RoleType;

			var DependentSubcounter = new RelationType(TestsDomain.Instance, new Guid("e971733a-c381-4b5e-8e62-6bbd6d285bd7"), new Guid("6269351a-5e08-4b10-a895-ff2f669b259f"), new Guid("2b916cdb-93a6-42f1-b4e6-625b941c1874"));
			DependentSubcounter.AssignedMultiplicity = Multiplicity.OneToOne;
			DependentSubcounter.AssociationType.ObjectType = this;

			DependentSubcounter.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			DependentSubcounter.RoleType.AssignedSingularName = "Subcounter";
			DependentSubcounter.RoleType.AssignedPluralName = "Subcounters";
			this.Roles.Subcounter = DependentSubcounter.RoleType;


		}

		internal override void SetRelationTypes()
		{
			DependentClass.Instance.ConcreteRoles.Dependee = DependentClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Dependee]; 

			DependentClass.Instance.ConcreteRoles.Counter = DependentClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Counter]; 

			DependentClass.Instance.ConcreteRoles.Subcounter = DependentClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Subcounter]; 


		}

		internal class RolesType
		{
			internal RoleType Dependee;
			internal RoleType Counter;
			internal RoleType Subcounter;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Dependee;
			internal ConcreteRoleType Counter;
			internal ConcreteRoleType Subcounter;

		}
	}public partial class FourClass : Class
	{
		public static FourClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal FourClass() : base(TestsDomain.Instance, new Guid("1248e212-ca71-44aa-9e87-6e83dae9d4fd"))
        {
			this.SingularName = "Four";
			this.PluralName = "Fours";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("1a077ff9-b309-4982-8d79-2b176394eee4"))
			{
				Subtype = Instance,
				Supertype = SharedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
		}
	}public partial class AddressInterface: Interface
	{
		public static AddressInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal AddressInterface() : base(TestsDomain.Instance, new Guid("130aa2ff-4f14-4ad7-8a27-f80e8aebfa00"))
        {
			this.SingularName = "Address";
			this.PluralName = "Addresses";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var AddressPlace = new RelationType(TestsDomain.Instance, new Guid("36e7d935-a9c7-484d-8551-9bdc5bdeab68"), new Guid("113a8abd-e587-45a3-b118-92e60182c94b"), new Guid("4f7016f6-1b87-4ac4-8363-7f8210108928"));
			AddressPlace.AssignedMultiplicity = Multiplicity.ManyToOne;
			AddressPlace.IsIndexed = true;

			AddressPlace.AssociationType.ObjectType = this;

			AddressPlace.RoleType.ObjectType = PlaceClass.Instance;;
			AddressPlace.RoleType.AssignedSingularName = "Place";
			AddressPlace.RoleType.AssignedPluralName = "Places";
			this.Roles.Place = AddressPlace.RoleType;


		}

		internal override void SetRelationTypes()
		{
			HomeAddressClass.Instance.ConcreteRoles.Place = HomeAddressClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Place]; 
			MailboxAddressClass.Instance.ConcreteRoles.Place = MailboxAddressClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Place]; 


		}

		internal class RolesType
		{
			internal RoleType Place;

		}
	}public partial class FirstClass : Class
	{
		public static FirstClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal FirstClass() : base(TestsDomain.Instance, new Guid("1937b42e-954b-4ef9-bc63-5b8ae7903e9d"))
        {
			this.SingularName = "First";
			this.PluralName = "Firsts";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var FirstSecond = new RelationType(TestsDomain.Instance, new Guid("24886999-11f0-408f-b094-14b36ac4129b"), new Guid("e48ab2ee-c7a5-4d9a-b3ab-263f6aa4cdd1"), new Guid("cf5c725d-e567-44de-ab5b-b47bb0bf8647"));
			FirstSecond.AssignedMultiplicity = Multiplicity.OneToOne;
			FirstSecond.IsIndexed = true;

			FirstSecond.AssociationType.ObjectType = this;

			FirstSecond.RoleType.ObjectType = SecondClass.Instance;;
			this.Roles.Second = FirstSecond.RoleType;

			var FirstCreateCycle = new RelationType(TestsDomain.Instance, new Guid("b0274351-3403-4384-afb6-2cb49cd03893"), new Guid("ec145229-e33a-4807-a0dd-48778cc88ac7"), new Guid("12c46bf1-eed0-4e2a-b704-5d40032b4911"));
			FirstCreateCycle.AssignedMultiplicity = Multiplicity.OneToOne;
			FirstCreateCycle.AssociationType.ObjectType = this;

			FirstCreateCycle.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			FirstCreateCycle.RoleType.AssignedSingularName = "CreateCycle";
			FirstCreateCycle.RoleType.AssignedPluralName = "CreateCycles";
			this.Roles.CreateCycle = FirstCreateCycle.RoleType;

			var FirstIsDerived = new RelationType(TestsDomain.Instance, new Guid("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278"), new Guid("ae9f23b5-20a7-4ecc-b642-503d75c486f1"), new Guid("eb6b0565-1440-4b9b-aa23-51cfae3f93dd"));
			FirstIsDerived.AssignedMultiplicity = Multiplicity.OneToOne;
			FirstIsDerived.AssociationType.ObjectType = this;

			FirstIsDerived.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			FirstIsDerived.RoleType.AssignedSingularName = "IsDerived";
			FirstIsDerived.RoleType.AssignedPluralName = "IsDeriveds";
			this.Roles.IsDerived = FirstIsDerived.RoleType;


		}

		internal override void SetRelationTypes()
		{
			FirstClass.Instance.ConcreteRoles.Second = FirstClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Second]; 

			FirstClass.Instance.ConcreteRoles.CreateCycle = FirstClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.CreateCycle]; 

			FirstClass.Instance.ConcreteRoles.IsDerived = FirstClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsDerived]; 


		}

		internal class RolesType
		{
			internal RoleType Second;
			internal RoleType CreateCycle;
			internal RoleType IsDerived;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Second;
			internal ConcreteRoleType CreateCycle;
			internal ConcreteRoleType IsDerived;

		}
	}public partial class I2Interface: Interface
	{
		public static I2Interface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal I2Interface() : base(TestsDomain.Instance, new Guid("19bb2bc3-d53a-4d15-86d0-b250fdbcb0a0"))
        {
			this.SingularName = "I2";
			this.PluralName = "I2s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("0b3994ce-e159-4130-85c3-01bd2bda95fb"))
			{
				Subtype = Instance,
				Supertype = I12Interface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var I2I2Many2One = new RelationType(TestsDomain.Instance, new Guid("01d9ff41-d503-421e-93a6-5563e1787543"), new Guid("359ca62a-c74c-4936-a62d-9b8774174e8d"), new Guid("141b832f-7321-43b8-8033-dbad3f80edc3"));
			I2I2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I2I2Many2One.IsIndexed = true;

			I2I2Many2One.AssociationType.ObjectType = this;

			I2I2Many2One.RoleType.ObjectType = I2Interface.Instance;;
			I2I2Many2One.RoleType.AssignedSingularName = "I2Many2One";
			I2I2Many2One.RoleType.AssignedPluralName = "I2Many2Ones";
			this.Roles.I2I2Many2One = I2I2Many2One.RoleType;

			var I2C1Many2One = new RelationType(TestsDomain.Instance, new Guid("1f763206-c575-4e34-9e6b-997d434d3f42"), new Guid("923f6373-cbf8-46b1-9b4b-185015ff59ac"), new Guid("9edd1eb9-2b9a-4375-a669-68c1859eace2"));
			I2C1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I2C1Many2One.IsIndexed = true;

			I2C1Many2One.AssociationType.ObjectType = this;

			I2C1Many2One.RoleType.ObjectType = C1Class.Instance;;
			I2C1Many2One.RoleType.AssignedSingularName = "C1Many2One";
			I2C1Many2One.RoleType.AssignedPluralName = "C1Many2Ones";
			this.Roles.I2C1Many2One = I2C1Many2One.RoleType;

			var I2I12Many2One = new RelationType(TestsDomain.Instance, new Guid("23e9c15f-097f-4452-9bac-d7cf2a65134a"), new Guid("278afe09-d0e7-4a41-a60b-b3a01fd14c93"), new Guid("e538ab5e-80f2-4a34-81e7-c9b92414dda1"));
			I2I12Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I2I12Many2One.IsIndexed = true;

			I2I12Many2One.AssociationType.ObjectType = this;

			I2I12Many2One.RoleType.ObjectType = I12Interface.Instance;;
			I2I12Many2One.RoleType.AssignedSingularName = "I12Many2One";
			I2I12Many2One.RoleType.AssignedPluralName = "I12Many2Ones";
			this.Roles.I2I12Many2One = I2I12Many2One.RoleType;

			var I2AllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("35040d7c-ab7f-4a99-9d09-e01e24ca3cb9"), new Guid("d1f0ae79-1820-47a5-8869-496c3578a53d"), new Guid("0d2c6dbe-9bb2-414c-8f19-5381fe69ac64"));
			I2AllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsBoolean.AssociationType.ObjectType = this;

			I2AllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.I2AllorsBoolean = I2AllorsBoolean.RoleType;

			var I2C1One2Many = new RelationType(TestsDomain.Instance, new Guid("40b8edb3-e8c4-46c0-855b-4b18e0e8d7f3"), new Guid("078e1b17-f239-44b2-87d6-6350dd37ac1d"), new Guid("805d7871-bc51-4572-be01-e47ac8fef22a"));
			I2C1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I2C1One2Many.IsIndexed = true;

			I2C1One2Many.AssociationType.ObjectType = this;

			I2C1One2Many.RoleType.ObjectType = C1Class.Instance;;
			I2C1One2Many.RoleType.AssignedSingularName = "C1One2Many";
			I2C1One2Many.RoleType.AssignedPluralName = "C1One2Manies";
			this.Roles.I2C1One2Many = I2C1One2Many.RoleType;

			var I2C1One2One = new RelationType(TestsDomain.Instance, new Guid("49736daf-d0bd-4216-97fa-958cfa21a4f0"), new Guid("02a80ccd-31c9-422c-8ad9-d96916dd7741"), new Guid("6ac5d426-9156-4467-8a04-85ccb6c964e2"));
			I2C1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I2C1One2One.IsIndexed = true;

			I2C1One2One.AssociationType.ObjectType = this;

			I2C1One2One.RoleType.ObjectType = C1Class.Instance;;
			I2C1One2One.RoleType.AssignedSingularName = "C1One2One";
			I2C1One2One.RoleType.AssignedPluralName = "C1One2Ones";
			this.Roles.I2C1One2One = I2C1One2One.RoleType;

			var I2AllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("4f095abd-8803-4610-87f0-2847ddd5e9f4"), new Guid("5371c058-628e-4a1c-b654-ad0b7013eb17"), new Guid("ec80b71e-a933-4eb3-ab14-00b26c3bc805"));
			I2AllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsDecimal.AssociationType.ObjectType = this;

			I2AllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			I2AllorsDecimal.RoleType.Scale = 2;
			I2AllorsDecimal.RoleType.Precision = 19;
			this.Roles.I2AllorsDecimal = I2AllorsDecimal.RoleType;

			var I2I2Many2any = new RelationType(TestsDomain.Instance, new Guid("5ebbc734-23dd-494f-af2d-8e75caaa3e26"), new Guid("4d6c09d6-5644-47bb-a50a-464350053833"), new Guid("3aab87f3-2eab-4f81-9c1b-fd2e162a93b8"));
			I2I2Many2any.AssignedMultiplicity = Multiplicity.ManyToMany;
			I2I2Many2any.IsIndexed = true;

			I2I2Many2any.AssociationType.ObjectType = this;

			I2I2Many2any.RoleType.ObjectType = I2Interface.Instance;;
			I2I2Many2any.RoleType.AssignedSingularName = "I2Many2any";
			I2I2Many2any.RoleType.AssignedPluralName = "I2Many2Manies";
			this.Roles.I2Many2any = I2I2Many2any.RoleType;

			var I2AllorsBinary = new RelationType(TestsDomain.Instance, new Guid("62a8a93d-3744-49de-9f9a-9997b6ef4da6"), new Guid("f9be65e7-6e36-42df-bb85-5198d0c12b74"), new Guid("e3ae23bc-5934-4c0d-a709-adb00110772d"));
			I2AllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsBinary.AssociationType.ObjectType = this;

			I2AllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			I2AllorsBinary.RoleType.Size = -1;
			this.Roles.I2AllorsBinary = I2AllorsBinary.RoleType;

			var I2AllorsUnique = new RelationType(TestsDomain.Instance, new Guid("663559c4-ef64-4e78-89b4-bfa00691c627"), new Guid("9513c57f-478a-423e-ba15-b9132bc28cd0"), new Guid("3f03fb6f-b0ba-4c78-b86a-9c4a1c574dd4"));
			I2AllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsUnique.AssociationType.ObjectType = this;

			I2AllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.I2AllorsUnique = I2AllorsUnique.RoleType;

			var I2I1Many2One = new RelationType(TestsDomain.Instance, new Guid("6bb406bc-627b-444c-9c16-df9878e05e9c"), new Guid("16647879-8af1-4f1c-8ef5-2cec85aa31f4"), new Guid("edee2f1c-3e94-45b5-80f4-160faa2074c4"));
			I2I1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I2I1Many2One.IsIndexed = true;

			I2I1Many2One.AssociationType.ObjectType = this;

			I2I1Many2One.RoleType.ObjectType = I1Interface.Instance;;
			I2I1Many2One.RoleType.AssignedSingularName = "I1Many2One";
			I2I1Many2One.RoleType.AssignedPluralName = "I1Many2Ones";
			this.Roles.I2I1Many2One = I2I1Many2One.RoleType;

			var I2AllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("81d9eb2f-55a7-4d1c-853d-4369eb691ba5"), new Guid("db4d3b11-77bd-408e-ad41-4a03272a88e1"), new Guid("bdcffe2b-ffa7-4eb1-be24-8d8ab0b4dce2"));
			I2AllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsDateTime.AssociationType.ObjectType = this;

			I2AllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.I2AllorsDateTime = I2AllorsDateTime.RoleType;

			var I2I12One2Many = new RelationType(TestsDomain.Instance, new Guid("83dc0581-e04a-4f51-a44e-4fef63d44356"), new Guid("b1c5cbb7-3d5f-48b8-b182-aa8a0cc3e72a"), new Guid("9598153e-9c1c-438a-a8a8-9822092a6a07"));
			I2I12One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I2I12One2Many.IsIndexed = true;

			I2I12One2Many.AssociationType.ObjectType = this;

			I2I12One2Many.RoleType.ObjectType = I12Interface.Instance;;
			I2I12One2Many.RoleType.AssignedSingularName = "I12One2Many";
			I2I12One2Many.RoleType.AssignedPluralName = "I12One2Manies";
			this.Roles.I2I12One2Many = I2I12One2Many.RoleType;

			var I2I12One2One = new RelationType(TestsDomain.Instance, new Guid("87499e99-ed77-44c1-89d6-b4f570b6f217"), new Guid("e5201e06-3fbf-4b9c-aa65-1ee4ee9fabfb"), new Guid("e4c9f00e-7c3d-4b58-92f0-ccce24b55589"));
			I2I12One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I2I12One2One.IsIndexed = true;

			I2I12One2One.AssociationType.ObjectType = this;

			I2I12One2One.RoleType.ObjectType = I12Interface.Instance;;
			I2I12One2One.RoleType.AssignedSingularName = "I12One2One";
			I2I12One2One.RoleType.AssignedPluralName = "I12One2Ones";
			this.Roles.I2I12One2One = I2I12One2One.RoleType;

			var I2C2Many2Many = new RelationType(TestsDomain.Instance, new Guid("92fdb313-0b90-48f6-b054-a4ab38f880ba"), new Guid("a45ffec8-5e4e-4b21-9d68-9b0050472ed2"), new Guid("17e159a2-f5a6-4828-9fef-796fcc9085e8"));
			I2C2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I2C2Many2Many.IsIndexed = true;

			I2C2Many2Many.AssociationType.ObjectType = this;

			I2C2Many2Many.RoleType.ObjectType = C2Class.Instance;;
			I2C2Many2Many.RoleType.AssignedSingularName = "C2Many2Many";
			I2C2Many2Many.RoleType.AssignedPluralName = "C2Many2Manies";
			this.Roles.I2C2Many2Many = I2C2Many2Many.RoleType;

			var I2I1Many2Many = new RelationType(TestsDomain.Instance, new Guid("9bed0518-1946-4e23-9d4b-e4cda439984c"), new Guid("7b4a8937-258c-4129-a282-89d5ab924d68"), new Guid("2e78a543-949f-4130-b659-80a9a60ad6ab"));
			I2I1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I2I1Many2Many.IsIndexed = true;

			I2I1Many2Many.AssociationType.ObjectType = this;

			I2I1Many2Many.RoleType.ObjectType = I1Interface.Instance;;
			I2I1Many2Many.RoleType.AssignedSingularName = "I1Many2Many";
			I2I1Many2Many.RoleType.AssignedPluralName = "I1Many2Manies";
			this.Roles.I2I1Many2Many = I2I1Many2Many.RoleType;

			var I2C2Many2One = new RelationType(TestsDomain.Instance, new Guid("9f361b97-0b04-496d-ac60-718760c2a4e2"), new Guid("c51f6fd4-c290-41b6-b594-19e9bcbbee6a"), new Guid("f60f8fa4-4e73-472d-b0b0-67f202c1e969"));
			I2C2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I2C2Many2One.IsIndexed = true;

			I2C2Many2One.AssociationType.ObjectType = this;

			I2C2Many2One.RoleType.ObjectType = C2Class.Instance;;
			I2C2Many2One.RoleType.AssignedSingularName = "C2Many2One";
			I2C2Many2One.RoleType.AssignedPluralName = "C2Many2Ones";
			this.Roles.I2C2Many2One = I2C2Many2One.RoleType;

			var I2AllorsString = new RelationType(TestsDomain.Instance, new Guid("9f91841c-f63f-4ffa-bee6-62e100f3cd15"), new Guid("3164fd30-297e-4e2a-86d6-fad6754f1d59"), new Guid("7afb53c1-2fe3-44b6-b1d2-d5a9f6100076"));
			I2AllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsString.AssociationType.ObjectType = this;

			I2AllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			I2AllorsString.RoleType.Size = 256;
			this.Roles.I2AllorsString = I2AllorsString.RoleType;

			var I2C2One2Many = new RelationType(TestsDomain.Instance, new Guid("b39fdd23-d7dd-473f-9705-df2f29be5ffe"), new Guid("8ddc9cbf-8e5c-4166-a2b0-6127c142da78"), new Guid("7cdd2b76-6c35-4e81-a1da-f5d0a300014b"));
			I2C2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I2C2One2Many.IsIndexed = true;

			I2C2One2Many.AssociationType.ObjectType = this;

			I2C2One2Many.RoleType.ObjectType = C2Class.Instance;;
			I2C2One2Many.RoleType.AssignedSingularName = "C2One2Many";
			I2C2One2Many.RoleType.AssignedPluralName = "C2One2Manies";
			this.Roles.I2C2One2Many = I2C2One2Many.RoleType;

			var I2I1One2One = new RelationType(TestsDomain.Instance, new Guid("b640bf16-0dc0-4203-aa76-f456371239ae"), new Guid("257fa0c6-43ea-4fe9-8142-dbc172d1e138"), new Guid("26deb364-bd5e-4b5d-b28a-19689ab3c00d"));
			I2I1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I2I1One2One.IsIndexed = true;

			I2I1One2One.AssociationType.ObjectType = this;

			I2I1One2One.RoleType.ObjectType = I1Interface.Instance;;
			I2I1One2One.RoleType.AssignedSingularName = "I1One2One";
			I2I1One2One.RoleType.AssignedPluralName = "I1One2Ones";
			this.Roles.I2I1One2One = I2I1One2One.RoleType;

			var I2I1One2Many = new RelationType(TestsDomain.Instance, new Guid("bbb01166-2671-4ca1-8b1e-12e6ae8aeb03"), new Guid("ee0766c7-0ef6-4ca0-b4a1-c399bc8df823"), new Guid("d8f011c4-3057-4384-9045-9c34b13db5c3"));
			I2I1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I2I1One2Many.IsIndexed = true;

			I2I1One2Many.AssociationType.ObjectType = this;

			I2I1One2Many.RoleType.ObjectType = I1Interface.Instance;;
			I2I1One2Many.RoleType.AssignedSingularName = "I1One2Many";
			I2I1One2Many.RoleType.AssignedPluralName = "I1One2Manies";
			this.Roles.I2I1One2Many = I2I1One2Many.RoleType;

			var I2I12Many2Many = new RelationType(TestsDomain.Instance, new Guid("cb9f21e0-a841-45de-8ba4-991b4ceca616"), new Guid("1127ff1b-1657-4e18-bdc9-bc90cd8a3c15"), new Guid("d838e921-ff63-4e4f-afd8-42dc29d23555"));
			I2I12Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I2I12Many2Many.IsIndexed = true;

			I2I12Many2Many.AssociationType.ObjectType = this;

			I2I12Many2Many.RoleType.ObjectType = I12Interface.Instance;;
			I2I12Many2Many.RoleType.AssignedSingularName = "I12Many2Many";
			I2I12Many2Many.RoleType.AssignedPluralName = "I12Many2Manies";
			this.Roles.I2I12Many2Many = I2I12Many2Many.RoleType;

			var I2I2One2One = new RelationType(TestsDomain.Instance, new Guid("cc4c704c-ab7e-45d4-baa9-b67cfff9448e"), new Guid("d15cb643-1ace-4dfe-b0af-e02e4273bbbb"), new Guid("12c2c263-7839-4734-9307-bcde6930a2b7"));
			I2I2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I2I2One2One.IsIndexed = true;

			I2I2One2One.AssociationType.ObjectType = this;

			I2I2One2One.RoleType.ObjectType = I2Interface.Instance;;
			I2I2One2One.RoleType.AssignedSingularName = "I2One2One";
			I2I2One2One.RoleType.AssignedPluralName = "I2One2Ones";
			this.Roles.I2I2One2One = I2I2One2One.RoleType;

			var I2AllorsInteger = new RelationType(TestsDomain.Instance, new Guid("d30dd036-6d28-48df-873b-3a76da8c029e"), new Guid("012e0afc-ebc7-4ae4-9fa0-49c72f3daebf"), new Guid("69c063b7-156f-4b7f-89eb-10c7eaf39ad5"));
			I2AllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsInteger.AssociationType.ObjectType = this;

			I2AllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.I2AllorsInteger = I2AllorsInteger.RoleType;

			var I2I2One2Many = new RelationType(TestsDomain.Instance, new Guid("deb9cbd3-386f-4599-802c-be50945b9f1d"), new Guid("3fcc8e73-5f3c-4ce0-8f45-daa813278d7e"), new Guid("c7d68f0d-24b1-40c9-9431-78763b776bee"));
			I2I2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I2I2One2Many.IsIndexed = true;

			I2I2One2Many.AssociationType.ObjectType = this;

			I2I2One2Many.RoleType.ObjectType = I2Interface.Instance;;
			I2I2One2Many.RoleType.AssignedSingularName = "I2One2Many";
			I2I2One2Many.RoleType.AssignedPluralName = "I2One2Manies";
			this.Roles.I2I2One2Many = I2I2One2Many.RoleType;

			var I2C1Many2Many = new RelationType(TestsDomain.Instance, new Guid("f364c9fe-ad36-4305-80fd-4921451c70a5"), new Guid("db6935b0-684c-48ce-97d0-6b7183a73adb"), new Guid("6ed084f6-8809-46d9-a3ec-4b086ddafb0a"));
			I2C1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I2C1Many2Many.IsIndexed = true;

			I2C1Many2Many.AssociationType.ObjectType = this;

			I2C1Many2Many.RoleType.ObjectType = C1Class.Instance;;
			I2C1Many2Many.RoleType.AssignedSingularName = "C1Many2Many";
			I2C1Many2Many.RoleType.AssignedPluralName = "C1Many2Manies";
			this.Roles.I2C1Many2Many = I2C1Many2Many.RoleType;

			var I2C2One2One = new RelationType(TestsDomain.Instance, new Guid("f85c2d97-10b9-478d-9b82-2700d95d5cb1"), new Guid("bfb08e5e-afc6-4f27-975f-5fb9af5bacc4"), new Guid("666c65ad-8bf7-40be-a51a-e69d3e0bfe01"));
			I2C2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I2C2One2One.IsIndexed = true;

			I2C2One2One.AssociationType.ObjectType = this;

			I2C2One2One.RoleType.ObjectType = C2Class.Instance;;
			I2C2One2One.RoleType.AssignedSingularName = "C2One2One";
			I2C2One2One.RoleType.AssignedPluralName = "C2One2Ones";
			this.Roles.I2C2One2One = I2C2One2One.RoleType;

			var I2AllorsDouble = new RelationType(TestsDomain.Instance, new Guid("fbad33e7-ede1-41fc-97e9-ddf33a0f6459"), new Guid("c138d77b-e8bf-4945-962e-f74e338caad4"), new Guid("12ea1f33-0eed-4476-9cab-1fd62ed146a3"));
			I2AllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			I2AllorsDouble.AssociationType.ObjectType = this;

			I2AllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.I2AllorsDouble = I2AllorsDouble.RoleType;


		}

		internal override void SetRelationTypes()
		{
			C2Class.Instance.ConcreteRoles.I2I2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I2Many2One]; 

			C2Class.Instance.ConcreteRoles.I2C1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C1Many2One]; 

			C2Class.Instance.ConcreteRoles.I2I12Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I12Many2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsBoolean = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsBoolean]; 

			C2Class.Instance.ConcreteRoles.I2C1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C1One2Many]; 

			C2Class.Instance.ConcreteRoles.I2C1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C1One2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsDecimal = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsDecimal]; 

			C2Class.Instance.ConcreteRoles.I2Many2any = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2Many2any]; 

			C2Class.Instance.ConcreteRoles.I2AllorsBinary = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsBinary]; 

			C2Class.Instance.ConcreteRoles.I2AllorsUnique = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsUnique]; 

			C2Class.Instance.ConcreteRoles.I2I1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I1Many2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsDateTime = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsDateTime]; 

			C2Class.Instance.ConcreteRoles.I2I12One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I12One2Many]; 

			C2Class.Instance.ConcreteRoles.I2I12One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I12One2One]; 

			C2Class.Instance.ConcreteRoles.I2C2Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C2Many2Many]; 

			C2Class.Instance.ConcreteRoles.I2I1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I1Many2Many]; 

			C2Class.Instance.ConcreteRoles.I2C2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C2Many2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsString = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsString]; 

			C2Class.Instance.ConcreteRoles.I2C2One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C2One2Many]; 

			C2Class.Instance.ConcreteRoles.I2I1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I1One2One]; 

			C2Class.Instance.ConcreteRoles.I2I1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I1One2Many]; 

			C2Class.Instance.ConcreteRoles.I2I12Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I12Many2Many]; 

			C2Class.Instance.ConcreteRoles.I2I2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I2One2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsInteger = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsInteger]; 

			C2Class.Instance.ConcreteRoles.I2I2One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2I2One2Many]; 

			C2Class.Instance.ConcreteRoles.I2C1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C1Many2Many]; 

			C2Class.Instance.ConcreteRoles.I2C2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2C2One2One]; 

			C2Class.Instance.ConcreteRoles.I2AllorsDouble = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I2AllorsDouble]; 


		}

		internal class RolesType
		{
			internal RoleType I2I2Many2One;
			internal RoleType I2C1Many2One;
			internal RoleType I2I12Many2One;
			internal RoleType I2AllorsBoolean;
			internal RoleType I2C1One2Many;
			internal RoleType I2C1One2One;
			internal RoleType I2AllorsDecimal;
			internal RoleType I2Many2any;
			internal RoleType I2AllorsBinary;
			internal RoleType I2AllorsUnique;
			internal RoleType I2I1Many2One;
			internal RoleType I2AllorsDateTime;
			internal RoleType I2I12One2Many;
			internal RoleType I2I12One2One;
			internal RoleType I2C2Many2Many;
			internal RoleType I2I1Many2Many;
			internal RoleType I2C2Many2One;
			internal RoleType I2AllorsString;
			internal RoleType I2C2One2Many;
			internal RoleType I2I1One2One;
			internal RoleType I2I1One2Many;
			internal RoleType I2I12Many2Many;
			internal RoleType I2I2One2One;
			internal RoleType I2AllorsInteger;
			internal RoleType I2I2One2Many;
			internal RoleType I2C1Many2Many;
			internal RoleType I2C2One2One;
			internal RoleType I2AllorsDouble;

		}
	}public partial class DerivationLogC1Class : Class
	{
		public static DerivationLogC1Class Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal DerivationLogC1Class() : base(TestsDomain.Instance, new Guid("2361c456-b624-493a-8377-2dd1e697e17a"))
        {
			this.SingularName = "DerivationLogC1";
			this.PluralName = "DerivationLogC1s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("e1abeb9f-d257-409c-8c1a-b79e3193f050"))
			{
				Subtype = Instance,
				Supertype = DerivationLogI12Interface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType UniqueId;

		}
	}public partial class S1Interface: Interface
	{
		public static S1Interface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal S1Interface() : base(TestsDomain.Instance, new Guid("253b0d71-9eaa-4d87-9094-3b549d8446b3"))
        {
			this.SingularName = "S1";
			this.PluralName = "S1s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}
	}public partial class HomeAddressClass : Class
	{
		public static HomeAddressClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal HomeAddressClass() : base(TestsDomain.Instance, new Guid("2561e93c-5b85-44fb-a924-a1c0d1f78846"))
        {
			this.SingularName = "HomeAddress";
			this.PluralName = "HomeAddresses";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("ab97d574-18bc-45cd-881d-87e2b024ceef"))
			{
				Subtype = Instance,
				Supertype = AddressInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var HomeAddressStreet = new RelationType(TestsDomain.Instance, new Guid("6f0f42c4-9b47-47c2-a632-da8e08116be4"), new Guid("652a00b8-f708-4804-80b6-c1fe3211acf2"), new Guid("fc273b47-d98a-4afd-90ba-574fbdbfb395"));
			HomeAddressStreet.AssignedMultiplicity = Multiplicity.OneToOne;
			HomeAddressStreet.AssociationType.ObjectType = this;

			HomeAddressStreet.RoleType.ObjectType = AllorsStringUnit.Instance;;
			HomeAddressStreet.RoleType.AssignedSingularName = "Street";
			HomeAddressStreet.RoleType.AssignedPluralName = "Street";
			HomeAddressStreet.RoleType.Size = 256;
			this.Roles.Street = HomeAddressStreet.RoleType;

			var HomeAddressHouseNumber = new RelationType(TestsDomain.Instance, new Guid("b181d077-e897-4add-9456-67b9760d32e8"), new Guid("5eca1733-0f01-4141-b0d0-d7a2bfd90388"), new Guid("d29dbed0-a68a-4075-b893-55e16e6335fd"));
			HomeAddressHouseNumber.AssignedMultiplicity = Multiplicity.OneToOne;
			HomeAddressHouseNumber.AssociationType.ObjectType = this;

			HomeAddressHouseNumber.RoleType.ObjectType = AllorsStringUnit.Instance;;
			HomeAddressHouseNumber.RoleType.AssignedSingularName = "HouseNumber";
			HomeAddressHouseNumber.RoleType.AssignedPluralName = "HouseNumbers";
			HomeAddressHouseNumber.RoleType.Size = 256;
			this.Roles.HouseNumber = HomeAddressHouseNumber.RoleType;


		}

		internal override void SetRelationTypes()
		{
			HomeAddressClass.Instance.ConcreteRoles.Street = HomeAddressClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Street]; 

			HomeAddressClass.Instance.ConcreteRoles.HouseNumber = HomeAddressClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.HouseNumber]; 


		}

		internal class RolesType
		{
			internal RoleType Street;
			internal RoleType HouseNumber;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Street;
			internal ConcreteRoleType HouseNumber;
			internal ConcreteRoleType Place;

		}
	}public partial class PlaceClass : Class
	{
		public static PlaceClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal PlaceClass() : base(TestsDomain.Instance, new Guid("268f63d2-17da-4f29-b0d0-76db611598c6"))
        {
			this.SingularName = "Place";
			this.PluralName = "Places";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var PlaceCountry = new RelationType(TestsDomain.Instance, new Guid("1bf1cc1e-75bf-4a3f-87bd-a2fae2697855"), new Guid("dce03fde-fbb1-45e7-b78d-9484fa6487ff"), new Guid("d88eaaa2-2622-48ef-960a-1b506d95f238"));
			PlaceCountry.AssignedMultiplicity = Multiplicity.ManyToOne;
			PlaceCountry.IsIndexed = true;

			PlaceCountry.AssociationType.ObjectType = this;

			PlaceCountry.RoleType.ObjectType = CountryClass.Instance;;
			this.Roles.Country = PlaceCountry.RoleType;

			var PlaceCity = new RelationType(TestsDomain.Instance, new Guid("d029f486-4bb8-43a1-8356-98b9bee10de4"), new Guid("1454029b-b016-41e1-b142-cea20c7b36d1"), new Guid("dccca416-913b-406a-9405-c5d037af2fd8"));
			PlaceCity.AssignedMultiplicity = Multiplicity.OneToOne;
			PlaceCity.AssociationType.ObjectType = this;

			PlaceCity.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PlaceCity.RoleType.AssignedSingularName = "City";
			PlaceCity.RoleType.AssignedPluralName = "Cities";
			PlaceCity.RoleType.Size = 256;
			this.Roles.City = PlaceCity.RoleType;

			var PlacePostalCode = new RelationType(TestsDomain.Instance, new Guid("d80d7c6a-138a-43dd-9748-8ffb89b1dabb"), new Guid("944c752e-742c-426b-9ac9-c405080d4a8d"), new Guid("b54fcc51-e294-4732-82bf-a1117a4e2219"));
			PlacePostalCode.AssignedMultiplicity = Multiplicity.OneToOne;
			PlacePostalCode.AssociationType.ObjectType = this;

			PlacePostalCode.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PlacePostalCode.RoleType.AssignedSingularName = "PostalCode";
			PlacePostalCode.RoleType.AssignedPluralName = "PostalCodes";
			PlacePostalCode.RoleType.Size = 256;
			this.Roles.PostalCode = PlacePostalCode.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PlaceClass.Instance.ConcreteRoles.Country = PlaceClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Country]; 

			PlaceClass.Instance.ConcreteRoles.City = PlaceClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.City]; 

			PlaceClass.Instance.ConcreteRoles.PostalCode = PlaceClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PostalCode]; 


		}

		internal class RolesType
		{
			internal RoleType Country;
			internal RoleType City;
			internal RoleType PostalCode;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Country;
			internal ConcreteRoleType City;
			internal ConcreteRoleType PostalCode;

		}
	}public partial class GenderClass : Class
	{
		public static GenderClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal GenderClass() : base(TestsDomain.Instance, new Guid("270f0dc8-1bc2-4a42-9617-45e93d5403c8"))
        {
			this.SingularName = "Gender";
			this.PluralName = "Genders";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("2c5e6389-9a31-4ac8-aeeb-9e9a1b8f98a1"))
			{
				Subtype = Instance,
				Supertype = EnumerationInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType LocalisedName;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType IsActive;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;
			internal ConcreteRoleType UniqueId;

		}
	}public partial class DependeeClass : Class
	{
		public static DependeeClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal DependeeClass() : base(TestsDomain.Instance, new Guid("2cc9bde1-80da-4159-bb20-219074266101"))
        {
			this.SingularName = "Dependee";
			this.PluralName = "Dependees";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var DependeeSubdependee = new RelationType(TestsDomain.Instance, new Guid("1b8e0350-c446-48dc-85c0-71130cc1490e"), new Guid("97c6a03f-f0c7-4c7d-b40f-1353e34431bd"), new Guid("89b8f5f6-5589-42ad-ac9e-1d984c02f7ea"));
			DependeeSubdependee.AssignedMultiplicity = Multiplicity.OneToOne;
			DependeeSubdependee.IsIndexed = true;

			DependeeSubdependee.AssociationType.ObjectType = this;

			DependeeSubdependee.RoleType.ObjectType = SubdependeeClass.Instance;;
			this.Roles.Subdependee = DependeeSubdependee.RoleType;

			var DependeeSubcounter = new RelationType(TestsDomain.Instance, new Guid("c1e86449-e5a8-4911-97c7-b03de9142f98"), new Guid("2786b8ca-2d71-44cc-8e1e-1896ac5e6c5c"), new Guid("af75f294-b20d-4304-8804-32ef9c0a324a"));
			DependeeSubcounter.AssignedMultiplicity = Multiplicity.OneToOne;
			DependeeSubcounter.AssociationType.ObjectType = this;

			DependeeSubcounter.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			DependeeSubcounter.RoleType.AssignedSingularName = "Subcounter";
			DependeeSubcounter.RoleType.AssignedPluralName = "Subcounters";
			this.Roles.Subcounter = DependeeSubcounter.RoleType;

			var DependeeCounter = new RelationType(TestsDomain.Instance, new Guid("d58d1f28-3abd-4294-abde-885bdd16f466"), new Guid("9a867244-8ea3-402b-9a9c-a78727dbee78"), new Guid("5f570211-688e-4050-bf54-997d22a529d5"));
			DependeeCounter.AssignedMultiplicity = Multiplicity.OneToOne;
			DependeeCounter.AssociationType.ObjectType = this;

			DependeeCounter.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			DependeeCounter.RoleType.AssignedSingularName = "Counter";
			DependeeCounter.RoleType.AssignedPluralName = "Counters";
			this.Roles.Counter = DependeeCounter.RoleType;

			var DependeeDeleteDependent = new RelationType(TestsDomain.Instance, new Guid("e73b8fc5-0148-486a-9379-cfb051b303d2"), new Guid("db615c1c-3d08-4faa-b19f-740bd7102fbd"), new Guid("bde110ae-8242-4d98-bdc3-feeed8fde742"));
			DependeeDeleteDependent.AssignedMultiplicity = Multiplicity.OneToOne;
			DependeeDeleteDependent.AssociationType.ObjectType = this;

			DependeeDeleteDependent.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			DependeeDeleteDependent.RoleType.AssignedSingularName = "DeleteDependent";
			DependeeDeleteDependent.RoleType.AssignedPluralName = "DeleteDependents";
			this.Roles.DeleteDependent = DependeeDeleteDependent.RoleType;


		}

		internal override void SetRelationTypes()
		{
			DependeeClass.Instance.ConcreteRoles.Subdependee = DependeeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Subdependee]; 

			DependeeClass.Instance.ConcreteRoles.Subcounter = DependeeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Subcounter]; 

			DependeeClass.Instance.ConcreteRoles.Counter = DependeeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Counter]; 

			DependeeClass.Instance.ConcreteRoles.DeleteDependent = DependeeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeleteDependent]; 


		}

		internal class RolesType
		{
			internal RoleType Subdependee;
			internal RoleType Subcounter;
			internal RoleType Counter;
			internal RoleType DeleteDependent;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Subdependee;
			internal ConcreteRoleType Subcounter;
			internal ConcreteRoleType Counter;
			internal ConcreteRoleType DeleteDependent;

		}
	}public partial class SimpleJobClass : Class
	{
		public static SimpleJobClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal SimpleJobClass() : base(TestsDomain.Instance, new Guid("320985b6-d571-4b6c-b940-e02c04ad37d3"))
        {
			this.SingularName = "SimpleJob";
			this.PluralName = "SimpleJobs";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var SimpleJobIndex = new RelationType(TestsDomain.Instance, new Guid("7cd27660-13c6-4a15-8fd8-5775920cfd28"), new Guid("da384d02-5d30-4df5-acb5-ca36c895ef53"), new Guid("44b9e3cc-e584-48c0-bfec-916ab14e5f03"));
			SimpleJobIndex.AssignedMultiplicity = Multiplicity.OneToOne;
			SimpleJobIndex.AssociationType.ObjectType = this;

			SimpleJobIndex.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			SimpleJobIndex.RoleType.AssignedSingularName = "Index";
			SimpleJobIndex.RoleType.AssignedPluralName = "Indeces";
			this.Roles.Index = SimpleJobIndex.RoleType;


		}

		internal override void SetRelationTypes()
		{
			SimpleJobClass.Instance.ConcreteRoles.Index = SimpleJobClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Index]; 


		}

		internal class RolesType
		{
			internal RoleType Index;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Index;

		}
	}public partial class ThirdClass : Class
	{
		public static ThirdClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ThirdClass() : base(TestsDomain.Instance, new Guid("39116edf-34cf-45a6-ac09-2e4f98f28e14"))
        {
			this.SingularName = "Third";
			this.PluralName = "Thirds";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var ThirdIsDerived = new RelationType(TestsDomain.Instance, new Guid("6ab5a7af-a0f0-4940-9be3-6f6430a9e728"), new Guid("a18d4c53-ba36-4936-8650-0d90182e5948"), new Guid("7866ac81-e84d-40c6-b9c0-5a038b1e838f"));
			ThirdIsDerived.AssignedMultiplicity = Multiplicity.OneToOne;
			ThirdIsDerived.AssociationType.ObjectType = this;

			ThirdIsDerived.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			ThirdIsDerived.RoleType.AssignedSingularName = "IsDerived";
			ThirdIsDerived.RoleType.AssignedPluralName = "IsDeriveds";
			this.Roles.IsDerived = ThirdIsDerived.RoleType;


		}

		internal override void SetRelationTypes()
		{
			ThirdClass.Instance.ConcreteRoles.IsDerived = ThirdClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsDerived]; 


		}

		internal class RolesType
		{
			internal RoleType IsDerived;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType IsDerived;

		}
	}public partial class OrganisationClass : Class
	{
		public static OrganisationClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal OrganisationClass() : base(TestsDomain.Instance, new Guid("3a5dcec7-308f-48c7-afee-35d38415aa0b"))
        {
			this.SingularName = "Organisation";
			this.PluralName = "Organisations";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("2324bc9f-79a1-44f7-9041-00ed74e789e3"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};
			new Inheritance(TestsDomain.Instance, new Guid("f356965f-c8a7-40f3-9956-149b3b1c6863"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var OrganisationInformation = new RelationType(TestsDomain.Instance, new Guid("01dd273f-cbca-4ee7-8c2d-827808aba481"), new Guid("ffc3b92f-860a-4e45-90e1-b9ba7ab27a27"), new Guid("e567907e-ca61-4ec1-ab06-62dbb84e5d57"));
			OrganisationInformation.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationInformation.IsIndexed = true;

			OrganisationInformation.AssociationType.ObjectType = this;

			OrganisationInformation.RoleType.ObjectType = AllorsStringUnit.Instance;;
			OrganisationInformation.RoleType.AssignedSingularName = "Information";
			OrganisationInformation.RoleType.AssignedPluralName = "Informations";
			OrganisationInformation.RoleType.Size = -1;
			this.Roles.Information = OrganisationInformation.RoleType;

			var CompanyShareholder = new RelationType(TestsDomain.Instance, new Guid("15f33fa4-c878-45a0-b40c-c5214bce350b"), new Guid("4fdd9abb-f2e7-4f07-860e-27b4207224bd"), new Guid("45bef644-dfcf-417a-9356-3c1cfbcada1b"));
			CompanyShareholder.AssignedMultiplicity = Multiplicity.ManyToMany;
			CompanyShareholder.IsIndexed = true;

			CompanyShareholder.AssociationType.ObjectType = this;

			CompanyShareholder.RoleType.ObjectType = PersonClass.Instance;;
			CompanyShareholder.RoleType.AssignedSingularName = "Shareholder";
			CompanyShareholder.RoleType.AssignedPluralName = "Shareholders";
			this.Roles.Shareholder = CompanyShareholder.RoleType;

			var OrganisationName = new RelationType(TestsDomain.Instance, new Guid("2cc74901-cda5-4185-bcd8-d51c745a8437"), new Guid("896a4589-4caf-4cd2-8365-c4200b12f519"), new Guid("baa30557-79ff-406d-b374-9d32519b2de7"));
			OrganisationName.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationName.IsIndexed = true;

			OrganisationName.AssociationType.ObjectType = this;

			OrganisationName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			OrganisationName.RoleType.AssignedSingularName = "Name";
			OrganisationName.RoleType.AssignedPluralName = "Names";
			OrganisationName.RoleType.Size = 256;
			this.Roles.Name = OrganisationName.RoleType;

			var OrganisationDescription = new RelationType(TestsDomain.Instance, new Guid("2cfea5d4-e893-4264-a966-a68716839acd"), new Guid("c3c93567-1d78-42ea-a8cf-77549cd1a235"), new Guid("d5965473-66cd-44b2-8048-a521c9cdadd0"));
			OrganisationDescription.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationDescription.AssociationType.ObjectType = this;

			OrganisationDescription.RoleType.ObjectType = AllorsStringUnit.Instance;;
			OrganisationDescription.RoleType.AssignedSingularName = "Description";
			OrganisationDescription.RoleType.AssignedPluralName = "Descriptions";
			OrganisationDescription.RoleType.Size = -1;
			this.Roles.Description = OrganisationDescription.RoleType;

			var CompanyEmployee = new RelationType(TestsDomain.Instance, new Guid("49b96f79-c33d-4847-8c64-d50a6adb4985"), new Guid("b031ef1a-0102-4b19-b85d-aa9c404596c3"), new Guid("b95c7b34-a295-4600-82c8-826cc2186a00"));
			CompanyEmployee.AssignedMultiplicity = Multiplicity.OneToMany;
			CompanyEmployee.AssociationType.ObjectType = this;

			CompanyEmployee.RoleType.ObjectType = PersonClass.Instance;;
			CompanyEmployee.RoleType.AssignedSingularName = "Employee";
			CompanyEmployee.RoleType.AssignedPluralName = "Employees";
			this.Roles.Employee = CompanyEmployee.RoleType;

			var OrganisationIncorporated = new RelationType(TestsDomain.Instance, new Guid("5fa25b53-e2a7-44c8-b6ff-f9575abb911d"), new Guid("6a382c73-c6a2-4d8b-bc85-4623ede54298"), new Guid("1c3dec18-978c-470a-8857-5210b9267185"));
			OrganisationIncorporated.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationIncorporated.AssociationType.ObjectType = this;

			OrganisationIncorporated.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			OrganisationIncorporated.RoleType.AssignedSingularName = "Incorporated";
			OrganisationIncorporated.RoleType.AssignedPluralName = "Incorporateds";
			this.Roles.Incorporated = OrganisationIncorporated.RoleType;

			var OrganisationIsSupplier = new RelationType(TestsDomain.Instance, new Guid("68c61cea-4e6e-4ed5-819b-7ec794a10870"), new Guid("8494ad76-3422-4799-b5a6-caa077e53aca"), new Guid("90489246-8590-4578-8b8d-716a25abd27d"));
			OrganisationIsSupplier.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationIsSupplier.AssociationType.ObjectType = this;

			OrganisationIsSupplier.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			OrganisationIsSupplier.RoleType.AssignedSingularName = "IsSupplier";
			OrganisationIsSupplier.RoleType.AssignedPluralName = "AreSupplier";
			this.Roles.IsSupplier = OrganisationIsSupplier.RoleType;

			var OrganisationIncorporationDate = new RelationType(TestsDomain.Instance, new Guid("7046c2b4-d458-4343-8446-d23d9c837c84"), new Guid("0671f523-a557-41e1-9d05-0e89d8d1ae2d"), new Guid("c84a6696-a1e9-4794-86c3-50e1f009c845"));
			OrganisationIncorporationDate.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationIncorporationDate.AssociationType.ObjectType = this;

			OrganisationIncorporationDate.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			OrganisationIncorporationDate.RoleType.AssignedSingularName = "IncorporationDate";
			OrganisationIncorporationDate.RoleType.AssignedPluralName = "IncorporationDates";
			this.Roles.IncorporationDate = OrganisationIncorporationDate.RoleType;

			var CompanyAddress = new RelationType(TestsDomain.Instance, new Guid("73f23588-1444-416d-b43c-b3384ca87bfc"), new Guid("d1a098bf-a3d8-4b71-948f-a77ae82f02db"), new Guid("a365f0ee-a94f-4435-a7b1-c92ac804a845"));
			CompanyAddress.AssignedMultiplicity = Multiplicity.OneToMany;
			CompanyAddress.IsIndexed = true;

			CompanyAddress.AssociationType.ObjectType = this;

			CompanyAddress.RoleType.ObjectType = AddressInterface.Instance;;
			CompanyAddress.RoleType.AssignedSingularName = "Address";
			CompanyAddress.RoleType.AssignedPluralName = "Addresses";
			this.Roles.Address = CompanyAddress.RoleType;

			var CompanyOwner = new RelationType(TestsDomain.Instance, new Guid("845ff004-516f-4ad5-9870-3d0e966a9f7d"), new Guid("3820f65f-0e79-4f30-a973-5d17dca6ad33"), new Guid("58d7df91-fbc5-4bcb-9398-a9957949402b"));
			CompanyOwner.AssignedMultiplicity = Multiplicity.OneToOne;
			CompanyOwner.IsIndexed = true;

			CompanyOwner.AssociationType.ObjectType = this;

			CompanyOwner.RoleType.ObjectType = PersonClass.Instance;;
			CompanyOwner.RoleType.AssignedSingularName = "Owner";
			CompanyOwner.RoleType.AssignedPluralName = "Owners";
			this.Roles.Owner = CompanyOwner.RoleType;

			var OrganisationLogo = new RelationType(TestsDomain.Instance, new Guid("b201d2a0-2335-47a1-aa8d-8416e89a9fec"), new Guid("e332003a-0287-4aab-9d95-257146ee4f1c"), new Guid("b1f5b479-e4d0-46de-8ad4-347076d9f180"));
			OrganisationLogo.AssignedMultiplicity = Multiplicity.ManyToOne;
			OrganisationLogo.IsIndexed = true;

			OrganisationLogo.AssociationType.ObjectType = this;

			OrganisationLogo.RoleType.ObjectType = MediaClass.Instance;;
			OrganisationLogo.RoleType.AssignedSingularName = "Logo";
			OrganisationLogo.RoleType.AssignedPluralName = "Logos";
			this.Roles.Logo = OrganisationLogo.RoleType;

			var OrganisationSize = new RelationType(TestsDomain.Instance, new Guid("bac702b8-7874-45c3-a410-102e1caea4a7"), new Guid("8c2ce648-3942-4ead-9772-308c29bc905e"), new Guid("26a60588-3c90-4f4e-9bb6-8f45fe8f9606"));
			OrganisationSize.AssignedMultiplicity = Multiplicity.OneToOne;
			OrganisationSize.AssociationType.ObjectType = this;

			OrganisationSize.RoleType.ObjectType = AllorsStringUnit.Instance;;
			OrganisationSize.RoleType.AssignedSingularName = "Size";
			OrganisationSize.RoleType.AssignedPluralName = "Sizes";
			OrganisationSize.RoleType.Size = 256;
			this.Roles.Size = OrganisationSize.RoleType;

			var OrganisationMainAddress = new RelationType(TestsDomain.Instance, new Guid("ddcea177-0ed9-4247-93d3-2090496c130c"), new Guid("944d024b-81eb-442f-8f50-387a588d2373"), new Guid("2c3bc00d-6715-4c1b-be78-753f7f306df0"));
			OrganisationMainAddress.AssignedMultiplicity = Multiplicity.ManyToOne;
			OrganisationMainAddress.IsIndexed = true;

			OrganisationMainAddress.AssociationType.ObjectType = this;

			OrganisationMainAddress.RoleType.ObjectType = AddressInterface.Instance;;
			OrganisationMainAddress.RoleType.AssignedSingularName = "MainAddress";
			OrganisationMainAddress.RoleType.AssignedPluralName = "MainAddresses";
			this.Roles.MainAddress = OrganisationMainAddress.RoleType;


		}

		internal override void SetRelationTypes()
		{
			OrganisationClass.Instance.ConcreteRoles.Information = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Information]; 

			OrganisationClass.Instance.ConcreteRoles.Shareholder = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Shareholder]; 

			OrganisationClass.Instance.ConcreteRoles.Name = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			OrganisationClass.Instance.ConcreteRoles.Description = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Description]; 

			OrganisationClass.Instance.ConcreteRoles.Employee = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Employee]; 

			OrganisationClass.Instance.ConcreteRoles.Incorporated = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Incorporated]; 

			OrganisationClass.Instance.ConcreteRoles.IsSupplier = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsSupplier]; 

			OrganisationClass.Instance.ConcreteRoles.IncorporationDate = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IncorporationDate]; 

			OrganisationClass.Instance.ConcreteRoles.Address = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Address]; 

			OrganisationClass.Instance.ConcreteRoles.Owner = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Owner]; 

			OrganisationClass.Instance.ConcreteRoles.Logo = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Logo]; 

			OrganisationClass.Instance.ConcreteRoles.Size = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Size]; 

			OrganisationClass.Instance.ConcreteRoles.MainAddress = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MainAddress]; 


		}

		internal class RolesType
		{
			internal RoleType Information;
			internal RoleType Shareholder;
			internal RoleType Name;
			internal RoleType Description;
			internal RoleType Employee;
			internal RoleType Incorporated;
			internal RoleType IsSupplier;
			internal RoleType IncorporationDate;
			internal RoleType Address;
			internal RoleType Owner;
			internal RoleType Logo;
			internal RoleType Size;
			internal RoleType MainAddress;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Information;
			internal ConcreteRoleType Shareholder;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType Description;
			internal ConcreteRoleType Employee;
			internal ConcreteRoleType Incorporated;
			internal ConcreteRoleType IsSupplier;
			internal ConcreteRoleType IncorporationDate;
			internal ConcreteRoleType Address;
			internal ConcreteRoleType Owner;
			internal ConcreteRoleType Logo;
			internal ConcreteRoleType Size;
			internal ConcreteRoleType MainAddress;
			internal ConcreteRoleType UniqueId;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class SubdependeeClass : Class
	{
		public static SubdependeeClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal SubdependeeClass() : base(TestsDomain.Instance, new Guid("46a437d1-455b-4ddd-b83c-068938c352bd"))
        {
			this.SingularName = "Subdependee";
			this.PluralName = "Subdependees";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var SubdependeeSubcounter = new RelationType(TestsDomain.Instance, new Guid("194930f9-9c3f-458d-93ec-3d7bea4cd538"), new Guid("63ed21ba-b310-43fc-afed-a3eeea918204"), new Guid("6765f2b5-bf55-4713-a693-946fc0846b27"));
			SubdependeeSubcounter.AssignedMultiplicity = Multiplicity.OneToOne;
			SubdependeeSubcounter.AssociationType.ObjectType = this;

			SubdependeeSubcounter.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			SubdependeeSubcounter.RoleType.AssignedSingularName = "Subcounter";
			SubdependeeSubcounter.RoleType.AssignedPluralName = "Subcounters";
			this.Roles.Subcounter = SubdependeeSubcounter.RoleType;


		}

		internal override void SetRelationTypes()
		{
			SubdependeeClass.Instance.ConcreteRoles.Subcounter = SubdependeeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Subcounter]; 


		}

		internal class RolesType
		{
			internal RoleType Subcounter;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Subcounter;

		}
	}public partial class UnitClass : Class
	{
		public static UnitClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal UnitClass() : base(TestsDomain.Instance, new Guid("4e501cd6-807c-4f10-b60b-acd1d80042cd"))
        {
			this.SingularName = "Unit";
			this.PluralName = "Units";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("3f713f76-d79f-477d-adff-a6b438df4c5e"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var UnitAllorsBinary = new RelationType(TestsDomain.Instance, new Guid("24771d5b-f920-4820-aff7-ea6391b4a45c"), new Guid("fe3aa333-e011-4a1e-85dc-ded48329cf00"), new Guid("4d4428fc-bac0-47af-ab5e-7c7b87880206"));
			UnitAllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsBinary.AssociationType.ObjectType = this;

			UnitAllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			UnitAllorsBinary.RoleType.Size = -1;
			this.Roles.AllorsBinary = UnitAllorsBinary.RoleType;

			var UnitAllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("4d6a80f5-0fa7-4867-91f8-37aa92b6707b"), new Guid("13f88cf7-aaec-48a1-a896-401df84da34b"), new Guid("a462ce40-5885-48c6-b327-7e4c096a99fa"));
			UnitAllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsDateTime.AssociationType.ObjectType = this;

			UnitAllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.AllorsDateTime = UnitAllorsDateTime.RoleType;

			var UnitAllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("5a788ebe-65e9-4d5e-853a-91bb4addabb5"), new Guid("7620281d-3d8a-470a-9258-7a6d1b818b46"), new Guid("b5dd13eb-8923-4a66-94df-af5fadb42f1c"));
			UnitAllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsBoolean.AssociationType.ObjectType = this;

			UnitAllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.AllorsBoolean = UnitAllorsBoolean.RoleType;

			var UnitAllorsDouble = new RelationType(TestsDomain.Instance, new Guid("74a35820-ef8c-4373-9447-6215ee8279c0"), new Guid("e5f7a565-372a-42ed-8da5-ffe6dd599f70"), new Guid("4a95fb0d-6849-499e-a140-6c942fb06f4d"));
			UnitAllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsDouble.AssociationType.ObjectType = this;

			UnitAllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.AllorsDouble = UnitAllorsDouble.RoleType;

			var UnitAllorsInteger = new RelationType(TestsDomain.Instance, new Guid("b817ba76-876e-44ea-8e5a-51d552d4045e"), new Guid("80683240-71d5-4329-abd0-87c367b44fec"), new Guid("07070cb0-6e65-4a00-8754-50cf594ed9e1"));
			UnitAllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsInteger.AssociationType.ObjectType = this;

			UnitAllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.AllorsInteger = UnitAllorsInteger.RoleType;

			var UnitAllorsString = new RelationType(TestsDomain.Instance, new Guid("c724c733-972a-411c-aecb-e865c2628a90"), new Guid("e4917fda-a605-4f6f-8f63-579ec688b629"), new Guid("f27c150a-ce8d-4ff3-9507-ccb0b91aa0c2"));
			UnitAllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsString.AssociationType.ObjectType = this;

			UnitAllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			UnitAllorsString.RoleType.Size = 256;
			this.Roles.AllorsString = UnitAllorsString.RoleType;

			var UnitAllorsUnique = new RelationType(TestsDomain.Instance, new Guid("ed58ae4c-24e0-4dd1-8b1c-0909df1e0fcd"), new Guid("f117e164-ce37-4c12-a79e-38cda962adae"), new Guid("25dd4abf-c6da-4739-aed0-8528d1c00b8b"));
			UnitAllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsUnique.AssociationType.ObjectType = this;

			UnitAllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.AllorsUnique = UnitAllorsUnique.RoleType;

			var UnitAllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("f746da51-ea2d-4e22-9ecb-46d4dbc1b084"), new Guid("3936ee9b-3bd6-44de-9340-4047749a6c2c"), new Guid("1408cd42-3125-48c7-86d7-4a5f71e75e25"));
			UnitAllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			UnitAllorsDecimal.AssociationType.ObjectType = this;

			UnitAllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			UnitAllorsDecimal.RoleType.Scale = 2;
			UnitAllorsDecimal.RoleType.Precision = 19;
			this.Roles.AllorsDecimal = UnitAllorsDecimal.RoleType;


		}

		internal override void SetRelationTypes()
		{
			UnitClass.Instance.ConcreteRoles.AllorsBinary = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsBinary]; 

			UnitClass.Instance.ConcreteRoles.AllorsDateTime = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsDateTime]; 

			UnitClass.Instance.ConcreteRoles.AllorsBoolean = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsBoolean]; 

			UnitClass.Instance.ConcreteRoles.AllorsDouble = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsDouble]; 

			UnitClass.Instance.ConcreteRoles.AllorsInteger = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsInteger]; 

			UnitClass.Instance.ConcreteRoles.AllorsString = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsString]; 

			UnitClass.Instance.ConcreteRoles.AllorsUnique = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsUnique]; 

			UnitClass.Instance.ConcreteRoles.AllorsDecimal = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsDecimal]; 


		}

		internal class RolesType
		{
			internal RoleType AllorsBinary;
			internal RoleType AllorsDateTime;
			internal RoleType AllorsBoolean;
			internal RoleType AllorsDouble;
			internal RoleType AllorsInteger;
			internal RoleType AllorsString;
			internal RoleType AllorsUnique;
			internal RoleType AllorsDecimal;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType AllorsBinary;
			internal ConcreteRoleType AllorsDateTime;
			internal ConcreteRoleType AllorsBoolean;
			internal ConcreteRoleType AllorsDouble;
			internal ConcreteRoleType AllorsInteger;
			internal ConcreteRoleType AllorsString;
			internal ConcreteRoleType AllorsUnique;
			internal ConcreteRoleType AllorsDecimal;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class SharedInterface: Interface
	{
		public static SharedInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal SharedInterface() : base(TestsDomain.Instance, new Guid("5c3876c3-c3be-46aa-a598-a68b964d329e"))
        {
			this.SingularName = "Shared";
			this.PluralName = "Shareds";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}
	}public partial class OneClass : Class
	{
		public static OneClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal OneClass() : base(TestsDomain.Instance, new Guid("5d9b9cad-3720-47c3-9693-289698bf3dd0"))
        {
			this.SingularName = "One";
			this.PluralName = "Ones";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("ae3ba09f-3c0f-4dc8-8147-1fed71aa96be"))
			{
				Subtype = Instance,
				Supertype = SharedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var OneTwo = new RelationType(TestsDomain.Instance, new Guid("448878af-c992-4256-baa7-239335a26bc6"), new Guid("2c9236ed-892e-4005-9730-5a14f03f71e1"), new Guid("355b2e85-e597-4f88-9dca-45cbfbde527f"));
			OneTwo.AssignedMultiplicity = Multiplicity.ManyToOne;
			OneTwo.IsIndexed = true;

			OneTwo.AssociationType.ObjectType = this;

			OneTwo.RoleType.ObjectType = TwoClass.Instance;;
			this.Roles.Two = OneTwo.RoleType;


		}

		internal override void SetRelationTypes()
		{
			OneClass.Instance.ConcreteRoles.Two = OneClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Two]; 


		}

		internal class RolesType
		{
			internal RoleType Two;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Two;

		}
	}public partial class FromClass : Class
	{
		public static FromClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal FromClass() : base(TestsDomain.Instance, new Guid("6217b428-4ad0-4f7f-ad4b-e334cf0b3ab1"))
        {
			this.SingularName = "From";
			this.PluralName = "Froms";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var FromTo = new RelationType(TestsDomain.Instance, new Guid("d9a9896d-e175-410a-9916-9261d83aa229"), new Guid("a963f593-cad0-4fa9-96a3-3853f0f7d7c6"), new Guid("775a29b8-6e21-4545-9881-d52f6eb7db8b"));
			FromTo.AssignedMultiplicity = Multiplicity.OneToMany;
			FromTo.IsIndexed = true;

			FromTo.AssociationType.ObjectType = this;

			FromTo.RoleType.ObjectType = ToClass.Instance;;
			FromTo.RoleType.AssignedSingularName = "To";
			FromTo.RoleType.AssignedPluralName = "Tos";
			this.Roles.To = FromTo.RoleType;


		}

		internal override void SetRelationTypes()
		{
			FromClass.Instance.ConcreteRoles.To = FromClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.To]; 


		}

		internal class RolesType
		{
			internal RoleType To;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType To;

		}
	}public partial class StatefulCompanyClass : Class
	{
		public static StatefulCompanyClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal StatefulCompanyClass() : base(TestsDomain.Instance, new Guid("62859bfb-7949-4f7f-a428-658447576d0a"))
        {
			this.SingularName = "StatefulCompany";
			this.PluralName = "StatefulCompanies";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var StatefulCompanyEmployee = new RelationType(TestsDomain.Instance, new Guid("6c848eeb-7b42-45ea-81ac-fa983e1e0fa9"), new Guid("be566287-a26d-46fb-a4f2-1fc8bf1c1de4"), new Guid("2a482b25-a154-4306-87f3-b6cd7af3c80d"));
			StatefulCompanyEmployee.AssignedMultiplicity = Multiplicity.ManyToOne;
			StatefulCompanyEmployee.IsIndexed = true;

			StatefulCompanyEmployee.AssociationType.ObjectType = this;

			StatefulCompanyEmployee.RoleType.ObjectType = PersonClass.Instance;;
			StatefulCompanyEmployee.RoleType.AssignedSingularName = "Employee";
			StatefulCompanyEmployee.RoleType.AssignedPluralName = "Employees";
			this.Roles.Employee = StatefulCompanyEmployee.RoleType;

			var StatefulCompanyName = new RelationType(TestsDomain.Instance, new Guid("6e429d87-ea80-465e-9aa6-0f7d546b6bb3"), new Guid("de607129-6f68-4db6-a6ca-6ba53cae698d"), new Guid("94570d2c-2a5e-451f-905e-6ca61a469a31"));
			StatefulCompanyName.AssignedMultiplicity = Multiplicity.OneToOne;
			StatefulCompanyName.AssociationType.ObjectType = this;

			StatefulCompanyName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			StatefulCompanyName.RoleType.AssignedSingularName = "Name";
			StatefulCompanyName.RoleType.AssignedPluralName = "Names";
			StatefulCompanyName.RoleType.Size = 256;
			this.Roles.Name = StatefulCompanyName.RoleType;

			var StatefulCompanyManager = new RelationType(TestsDomain.Instance, new Guid("9940e8ed-189e-42c6-b0d1-7c01920b9fac"), new Guid("de4a92c8-4e08-4f37-9d6c-321dcce89e1c"), new Guid("3becaaa8-7b49-4616-8d79-a7bf04d9e666"));
			StatefulCompanyManager.AssignedMultiplicity = Multiplicity.ManyToOne;
			StatefulCompanyManager.IsIndexed = true;

			StatefulCompanyManager.AssociationType.ObjectType = this;

			StatefulCompanyManager.RoleType.ObjectType = PersonClass.Instance;;
			StatefulCompanyManager.RoleType.AssignedSingularName = "Manager";
			StatefulCompanyManager.RoleType.AssignedPluralName = "Managers";
			this.Roles.Manager = StatefulCompanyManager.RoleType;


		}

		internal override void SetRelationTypes()
		{
			StatefulCompanyClass.Instance.ConcreteRoles.Employee = StatefulCompanyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Employee]; 

			StatefulCompanyClass.Instance.ConcreteRoles.Name = StatefulCompanyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			StatefulCompanyClass.Instance.ConcreteRoles.Manager = StatefulCompanyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Manager]; 


		}

		internal class RolesType
		{
			internal RoleType Employee;
			internal RoleType Name;
			internal RoleType Manager;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Employee;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType Manager;

		}
	}public partial class C1Class : Class
	{
		public static C1Class Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal C1Class() : base(TestsDomain.Instance, new Guid("7041c691-d896-4628-8f50-1c24f5d03414"))
        {
			this.SingularName = "C1";
			this.PluralName = "C1s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("115b7b76-8802-4b17-9426-987bb46bb89a"))
			{
				Subtype = Instance,
				Supertype = I1Interface.Instance
			};
			new Inheritance(TestsDomain.Instance, new Guid("a71cee9c-3f73-4be5-941c-3f86d9fb0e07"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var C1I1One2One = new RelationType(TestsDomain.Instance, new Guid("0e7f529b-bc91-4a40-a7e7-a17341c6bf5b"), new Guid("1d1374c3-a28d-4904-b98a-3a14ceb2f7ea"), new Guid("da5ccb42-7878-45a9-9350-17f0f0a52fd4"));
			C1I1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C1I1One2One.IsIndexed = true;

			C1I1One2One.AssociationType.ObjectType = this;

			C1I1One2One.RoleType.ObjectType = I1Interface.Instance;;
			C1I1One2One.RoleType.AssignedSingularName = "I1One2One";
			C1I1One2One.RoleType.AssignedPluralName = "I1One2Ones";
			this.Roles.C1I1One2One = C1I1One2One.RoleType;

			var C1AllorsString = new RelationType(TestsDomain.Instance, new Guid("20713860-8abd-4d71-8ccc-2b4d1b88bce3"), new Guid("974aa133-255b-431f-a15d-b6a126d362b5"), new Guid("6dc98925-87a7-4959-8095-90eedef0e9a0"));
			C1AllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsString.AssociationType.ObjectType = this;

			C1AllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			C1AllorsString.RoleType.Size = 256;
			this.Roles.C1AllorsString = C1AllorsString.RoleType;

			var C1C2Many2One = new RelationType(TestsDomain.Instance, new Guid("5490dc63-a8f6-4a86-91ef-fef97a86f119"), new Guid("3f307d57-1f39-4aba-822d-9881cef7223c"), new Guid("66a06e06-95e4-43ad-9b45-56687f8a2051"));
			C1C2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C1C2Many2One.IsIndexed = true;

			C1C2Many2One.AssociationType.ObjectType = this;

			C1C2Many2One.RoleType.ObjectType = C2Class.Instance;;
			C1C2Many2One.RoleType.AssignedSingularName = "C2Many2One";
			C1C2Many2One.RoleType.AssignedPluralName = "C2Many2Ones";
			this.Roles.C1C2Many2One = C1C2Many2One.RoleType;

			var C1I2One2One = new RelationType(TestsDomain.Instance, new Guid("6def7988-4bcf-4964-9de6-c6ede41d5e5a"), new Guid("75e47fbe-6ce1-4cc1-a20f-51a861df9cc3"), new Guid("e7d1e28d-69ad-4d3a-b35a-2d0aaacb67db"));
			C1I2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C1I2One2One.IsIndexed = true;

			C1I2One2One.AssociationType.ObjectType = this;

			C1I2One2One.RoleType.ObjectType = I2Interface.Instance;;
			C1I2One2One.RoleType.AssignedSingularName = "I2One2One";
			C1I2One2One.RoleType.AssignedPluralName = "I2One2Ones";
			this.Roles.C1I2One2One = C1I2One2One.RoleType;

			var C1C1One2One = new RelationType(TestsDomain.Instance, new Guid("79c00218-bb4f-40e9-af7d-61af444a4a54"), new Guid("2276c942-dd96-41a6-b52f-cd3862c4692f"), new Guid("40ee2908-2556-4bdf-a82f-2ea33e181b91"));
			C1C1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C1C1One2One.IsIndexed = true;

			C1C1One2One.AssociationType.ObjectType = this;

			C1C1One2One.RoleType.ObjectType = C1Class.Instance;;
			C1C1One2One.RoleType.AssignedSingularName = "C1One2One";
			C1C1One2One.RoleType.AssignedPluralName = "C1One2Ones";
			this.Roles.C1C1One2One = C1C1One2One.RoleType;

			var C1I1Many2One = new RelationType(TestsDomain.Instance, new Guid("7bb216f2-8e9c-4dcd-890b-579130ab0a8b"), new Guid("531e89ab-a295-4f72-8496-cdd0d8605d37"), new Guid("8af8fbc6-2f59-4026-9093-5b335dfb8b7f"));
			C1I1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C1I1Many2One.IsIndexed = true;

			C1I1Many2One.AssociationType.ObjectType = this;

			C1I1Many2One.RoleType.ObjectType = I1Interface.Instance;;
			C1I1Many2One.RoleType.AssignedSingularName = "I1Many2One";
			C1I1Many2One.RoleType.AssignedPluralName = "I1Many2Ones";
			this.Roles.C1I1Many2One = C1I1Many2One.RoleType;

			var C1I1Many2Many = new RelationType(TestsDomain.Instance, new Guid("815878f6-16f2-42f2-9b24-f394ddf789c2"), new Guid("eca51eab-3815-410f-b4c5-f7e2a1318791"), new Guid("39f62f9e-52d3-47c5-8fd4-44e91d9b78be"));
			C1I1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C1I1Many2Many.IsIndexed = true;

			C1I1Many2Many.AssociationType.ObjectType = this;

			C1I1Many2Many.RoleType.ObjectType = I1Interface.Instance;;
			C1I1Many2Many.RoleType.AssignedSingularName = "I1Many2Many";
			C1I1Many2Many.RoleType.AssignedPluralName = "I1Many2Manies";
			this.Roles.C1I1Many2Many = C1I1Many2Many.RoleType;

			var C1I2One2Many = new RelationType(TestsDomain.Instance, new Guid("82f5fb26-c260-41bc-a784-a2d5e35243bd"), new Guid("f5329d84-1301-44ea-85b4-dc7d98554694"), new Guid("ca30ba2a-627f-43d1-b467-fe0d7cd015cc"));
			C1I2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C1I2One2Many.IsIndexed = true;

			C1I2One2Many.AssociationType.ObjectType = this;

			C1I2One2Many.RoleType.ObjectType = I2Interface.Instance;;
			C1I2One2Many.RoleType.AssignedSingularName = "I2One2Many";
			C1I2One2Many.RoleType.AssignedPluralName = "I2One2Manies";
			this.Roles.C1I2One2Many = C1I2One2Many.RoleType;

			var C1AllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("87eb0d19-73a7-4aae-aeed-66dc9163233c"), new Guid("96e8dfaf-3e1e-4c59-88f3-d47be6c96b74"), new Guid("43ccd07d-b9c4-465c-b2f9-083a36315e85"));
			C1AllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsDecimal.AssociationType.ObjectType = this;

			C1AllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			C1AllorsDecimal.RoleType.Scale = 2;
			C1AllorsDecimal.RoleType.Precision = 10;
			this.Roles.C1AllorsDecimal = C1AllorsDecimal.RoleType;

			var C1C1Many2Many = new RelationType(TestsDomain.Instance, new Guid("8c198447-e943-4f5a-b749-9534b181c664"), new Guid("154222cb-0eb8-459d-839c-9c8857bd1c7e"), new Guid("c403f160-6486-4207-b32c-aa9ade27a28c"));
			C1C1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C1C1Many2Many.IsIndexed = true;

			C1C1Many2Many.AssociationType.ObjectType = this;

			C1C1Many2Many.RoleType.ObjectType = C1Class.Instance;;
			C1C1Many2Many.RoleType.AssignedSingularName = "C1Many2Many";
			C1C1Many2Many.RoleType.AssignedPluralName = "C1Many2Manies";
			this.Roles.C1C1Many2Many = C1C1Many2Many.RoleType;

			var C1I12Many2Many = new RelationType(TestsDomain.Instance, new Guid("94a2b37d-9431-4496-b992-630cda5b9851"), new Guid("a4a31323-7193-4709-828e-88b2c0f3f8aa"), new Guid("f225d708-c98f-44ff-9ed8-847cb1ddaacb"));
			C1I12Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C1I12Many2Many.IsIndexed = true;

			C1I12Many2Many.AssociationType.ObjectType = this;

			C1I12Many2Many.RoleType.ObjectType = I12Interface.Instance;;
			C1I12Many2Many.RoleType.AssignedSingularName = "I12Many2Many";
			C1I12Many2Many.RoleType.AssignedPluralName = "I12Many2Manies";
			this.Roles.C1I12Many2Many = C1I12Many2Many.RoleType;

			var C1AllorsBinary = new RelationType(TestsDomain.Instance, new Guid("97f31053-0e7b-42a0-90c2-ce6f09c56e86"), new Guid("70e42b8b-09e2-4cb1-a632-ff3785ee1c8d"), new Guid("e5cd692c-ab97-4cf8-9f8a-1de733526e74"));
			C1AllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsBinary.AssociationType.ObjectType = this;

			C1AllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			C1AllorsBinary.RoleType.Size = -1;
			this.Roles.C1AllorsBinary = C1AllorsBinary.RoleType;

			var C1I12One2Many = new RelationType(TestsDomain.Instance, new Guid("98c5f58b-1777-4d9a-8828-37dbf7051510"), new Guid("3218ac29-2eac-4dc9-acad-2c708c3df994"), new Guid("51b3b28e-9017-4a1e-b5ba-06a9b14d88d6"));
			C1I12One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C1I12One2Many.IsIndexed = true;

			C1I12One2Many.AssociationType.ObjectType = this;

			C1I12One2Many.RoleType.ObjectType = I12Interface.Instance;;
			C1I12One2Many.RoleType.AssignedSingularName = "I12One2Many";
			C1I12One2Many.RoleType.AssignedPluralName = "I12One2Manies";
			this.Roles.C1I12One2Many = C1I12One2Many.RoleType;

			var C1C2One2Many = new RelationType(TestsDomain.Instance, new Guid("9f6538c2-e6dd-4c27-80ed-2748f645cb95"), new Guid("3ddac067-46f1-4302-bb1b-aa0e05dd55ae"), new Guid("c749e58c-0f1d-4946-b35d-878221aac72f"));
			C1C2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C1C2One2Many.IsIndexed = true;

			C1C2One2Many.AssociationType.ObjectType = this;

			C1C2One2Many.RoleType.ObjectType = C2Class.Instance;;
			C1C2One2Many.RoleType.AssignedSingularName = "C2One2Many";
			C1C2One2Many.RoleType.AssignedPluralName = "C2One2Manies";
			this.Roles.C1C2One2Many = C1C2One2Many.RoleType;

			var C1C1One2Many = new RelationType(TestsDomain.Instance, new Guid("a0ac5a65-2cbd-4c51-9417-b10150bc5699"), new Guid("d595765b-5e67-46f2-b19c-c58563dd1ae0"), new Guid("3d121ffa-0ff5-4627-9ec3-879c2830ff04"));
			C1C1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C1C1One2Many.IsIndexed = true;

			C1C1One2Many.AssociationType.ObjectType = this;

			C1C1One2Many.RoleType.ObjectType = C1Class.Instance;;
			C1C1One2Many.RoleType.AssignedSingularName = "C1One2Many";
			C1C1One2Many.RoleType.AssignedPluralName = "C1One2Manies";
			this.Roles.C1C1One2Many = C1C1One2Many.RoleType;

			var C1AllorsStringMax = new RelationType(TestsDomain.Instance, new Guid("a64abd21-dadf-483d-9499-d19aa8e33791"), new Guid("099e3d39-16b5-431a-853b-942a354c3a52"), new Guid("c186bb2f-8e19-468d-8a01-561384e5187d"));
			C1AllorsStringMax.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsStringMax.AssociationType.ObjectType = this;

			C1AllorsStringMax.RoleType.ObjectType = AllorsStringUnit.Instance;;
			C1AllorsStringMax.RoleType.AssignedSingularName = "AllorsStringMax";
			C1AllorsStringMax.RoleType.AssignedPluralName = "AllorsStringsMax";
			C1AllorsStringMax.RoleType.Size = -1;
			this.Roles.AllorsStringMax = C1AllorsStringMax.RoleType;

			var C1C1Many2One = new RelationType(TestsDomain.Instance, new Guid("a8e18ea7-cbf2-4ea7-ae14-9f4bcfdb55de"), new Guid("8a546f48-fc09-48ae-997d-4a6de0cd458a"), new Guid("e6b21250-194b-4424-8b92-221c6d0e6228"));
			C1C1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C1C1Many2One.IsIndexed = true;

			C1C1Many2One.AssociationType.ObjectType = this;

			C1C1Many2One.RoleType.ObjectType = C1Class.Instance;;
			C1C1Many2One.RoleType.AssignedSingularName = "C1Many2One";
			C1C1Many2One.RoleType.AssignedPluralName = "C1Many2Ones";
			this.Roles.C1C1Many2One = C1C1Many2One.RoleType;

			var C1AllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("b4ee673f-bba0-4e24-9cda-3cf993c79a0a"), new Guid("948aa9e6-5cb3-48dc-a3b7-3f8770269dae"), new Guid("ad456144-a19e-4c89-845b-9391dbc8f372"));
			C1AllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsBoolean.AssociationType.ObjectType = this;

			C1AllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.C1AllorsBoolean = C1AllorsBoolean.RoleType;

			var C1I12One2One = new RelationType(TestsDomain.Instance, new Guid("b9f2c4c7-6979-40cf-82a2-fa99a5d9e9a4"), new Guid("911a9327-0237-4254-99a7-afff0d6a0369"), new Guid("50bf56c3-f05f-4172-86e1-aefead4a3a8c"));
			C1I12One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C1I12One2One.IsIndexed = true;

			C1I12One2One.AssociationType.ObjectType = this;

			C1I12One2One.RoleType.ObjectType = I12Interface.Instance;;
			C1I12One2One.RoleType.AssignedSingularName = "I12One2One";
			C1I12One2One.RoleType.AssignedPluralName = "I12One2Ones";
			this.Roles.C1I12One2One = C1I12One2One.RoleType;

			var C1I12Many2One = new RelationType(TestsDomain.Instance, new Guid("bcf4df45-6616-4cdf-8ada-f944f9c7ff1a"), new Guid("2128418c-6918-4be8-8a02-2bea142b7fc4"), new Guid("b5b4892d-e1d3-4a4b-a8a4-ac6ed0ff930e"));
			C1I12Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C1I12Many2One.IsIndexed = true;

			C1I12Many2One.AssociationType.ObjectType = this;

			C1I12Many2One.RoleType.ObjectType = I12Interface.Instance;;
			C1I12Many2One.RoleType.AssignedSingularName = "I12Many2One";
			C1I12Many2One.RoleType.AssignedPluralName = "I12Many2Ones";
			this.Roles.C1I12Many2One = C1I12Many2One.RoleType;

			var C1I2Many2Many = new RelationType(TestsDomain.Instance, new Guid("cda97972-84c8-48e3-99d8-fd7c99c5dbc9"), new Guid("8ef5784c-6f76-431e-b59d-075813ad7863"), new Guid("ce5170b0-347a-49b7-9925-a7a5c5eb2c75"));
			C1I2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C1I2Many2Many.IsIndexed = true;

			C1I2Many2Many.AssociationType.ObjectType = this;

			C1I2Many2Many.RoleType.ObjectType = I2Interface.Instance;;
			C1I2Many2Many.RoleType.AssignedSingularName = "I2Many2Many";
			C1I2Many2Many.RoleType.AssignedPluralName = "I2Many2Manies";
			this.Roles.C1I2Many2Many = C1I2Many2Many.RoleType;

			var C1AllorsUnique = new RelationType(TestsDomain.Instance, new Guid("cef13620-b7d7-4bfe-8d3b-c0f826da5989"), new Guid("6c18bd8f-9084-470b-9dfe-30263c98771b"), new Guid("2721249b-dadd-410d-b4e0-9d4a48e615d1"));
			C1AllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsUnique.AssociationType.ObjectType = this;

			C1AllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.C1AllorsUnique = C1AllorsUnique.RoleType;

			var C1I2Many2One = new RelationType(TestsDomain.Instance, new Guid("d0341bed-2732-4bcb-b1bb-9f9589de5d03"), new Guid("dacd7dfa-6650-438d-b564-49fbf89fea8d"), new Guid("2db69dd4-008b-4a17-aba5-6a050f35f7e3"));
			C1I2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C1I2Many2One.IsIndexed = true;

			C1I2Many2One.AssociationType.ObjectType = this;

			C1I2Many2One.RoleType.ObjectType = I2Interface.Instance;;
			C1I2Many2One.RoleType.AssignedSingularName = "I2Many2One";
			C1I2Many2One.RoleType.AssignedPluralName = "I2Many2Ones";
			this.Roles.C1I2Many2One = C1I2Many2One.RoleType;

			var C1I1One2Many = new RelationType(TestsDomain.Instance, new Guid("e0656d9a-75a6-4e59-aaa1-3ff03d440059"), new Guid("637c5967-fb6c-45d4-81c4-de5559df785f"), new Guid("89e4802f-7c61-4deb-a243-f78e79578082"));
			C1I1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C1I1One2Many.IsIndexed = true;

			C1I1One2Many.AssociationType.ObjectType = this;

			C1I1One2Many.RoleType.ObjectType = I1Interface.Instance;;
			C1I1One2Many.RoleType.AssignedSingularName = "I1One2Many";
			C1I1One2Many.RoleType.AssignedPluralName = "I1One2Manies";
			this.Roles.C1I1One2Many = C1I1One2Many.RoleType;

			var C1C2One2One = new RelationType(TestsDomain.Instance, new Guid("e97fc754-c736-4359-9662-19dce9429f89"), new Guid("5bd37271-01c0-4cd3-94d5-0284700b3567"), new Guid("392f5a47-f181-475c-b5c9-f0b729c8413f"));
			C1C2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C1C2One2One.IsIndexed = true;

			C1C2One2One.AssociationType.ObjectType = this;

			C1C2One2One.RoleType.ObjectType = C2Class.Instance;;
			C1C2One2One.RoleType.AssignedSingularName = "C2One2One";
			C1C2One2One.RoleType.AssignedPluralName = "C2One2Ones";
			this.Roles.C1C2One2One = C1C2One2One.RoleType;

			var C1AllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("ef75cc4e-8787-4f1c-ae5c-73577d721467"), new Guid("8c8baa81-0c59-485c-b416-c7e6ec972595"), new Guid("610129f7-0c35-4649-9f4b-14698d0d1c77"));
			C1AllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsDateTime.AssociationType.ObjectType = this;

			C1AllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.C1AllorsDateTime = C1AllorsDateTime.RoleType;

			var C1AllorsDouble = new RelationType(TestsDomain.Instance, new Guid("f268783d-42ed-41c1-b0b0-b8a60e30a601"), new Guid("6ed0694c-a74f-44c3-835b-897f56343576"), new Guid("459d20d8-dadd-44e1-aa8a-396e6eab7538"));
			C1AllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsDouble.AssociationType.ObjectType = this;

			C1AllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.C1AllorsDouble = C1AllorsDouble.RoleType;

			var C1C2Many2Many = new RelationType(TestsDomain.Instance, new Guid("f29d4a52-9ba5-40f6-ba99-050cbd03e554"), new Guid("122dc72f-cc92-440c-84e5-fe8340020c43"), new Guid("608db13d-1778-44a8-94f0-b86fc0f6992d"));
			C1C2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C1C2Many2Many.IsIndexed = true;

			C1C2Many2Many.AssociationType.ObjectType = this;

			C1C2Many2Many.RoleType.ObjectType = C2Class.Instance;;
			C1C2Many2Many.RoleType.AssignedSingularName = "C2Many2Many";
			C1C2Many2Many.RoleType.AssignedPluralName = "C2Many2Manies";
			this.Roles.C1C2Many2Many = C1C2Many2Many.RoleType;

			var C1AllorsInteger = new RelationType(TestsDomain.Instance, new Guid("f4920d94-8cd0-45b6-be00-f18d377368fd"), new Guid("c4202876-b670-4193-a459-3f0376e24c38"), new Guid("2687f5be-eebe-4ffb-a8b2-538134cb6f73"));
			C1AllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			C1AllorsInteger.IsIndexed = true;

			C1AllorsInteger.AssociationType.ObjectType = this;

			C1AllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.C1AllorsInteger = C1AllorsInteger.RoleType;


		}

		internal override void SetRelationTypes()
		{
			C1Class.Instance.ConcreteRoles.C1I1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I1One2One]; 

			C1Class.Instance.ConcreteRoles.C1AllorsString = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsString]; 

			C1Class.Instance.ConcreteRoles.C1C2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C2Many2One]; 

			C1Class.Instance.ConcreteRoles.C1I2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I2One2One]; 

			C1Class.Instance.ConcreteRoles.C1C1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C1One2One]; 

			C1Class.Instance.ConcreteRoles.C1I1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I1Many2One]; 

			C1Class.Instance.ConcreteRoles.C1I1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I1Many2Many]; 

			C1Class.Instance.ConcreteRoles.C1I2One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I2One2Many]; 

			C1Class.Instance.ConcreteRoles.C1AllorsDecimal = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsDecimal]; 

			C1Class.Instance.ConcreteRoles.C1C1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C1Many2Many]; 

			C1Class.Instance.ConcreteRoles.C1I12Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I12Many2Many]; 

			C1Class.Instance.ConcreteRoles.C1AllorsBinary = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsBinary]; 

			C1Class.Instance.ConcreteRoles.C1I12One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I12One2Many]; 

			C1Class.Instance.ConcreteRoles.C1C2One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C2One2Many]; 

			C1Class.Instance.ConcreteRoles.C1C1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C1One2Many]; 

			C1Class.Instance.ConcreteRoles.AllorsStringMax = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsStringMax]; 

			C1Class.Instance.ConcreteRoles.C1C1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C1Many2One]; 

			C1Class.Instance.ConcreteRoles.C1AllorsBoolean = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsBoolean]; 

			C1Class.Instance.ConcreteRoles.C1I12One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I12One2One]; 

			C1Class.Instance.ConcreteRoles.C1I12Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I12Many2One]; 

			C1Class.Instance.ConcreteRoles.C1I2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I2Many2Many]; 

			C1Class.Instance.ConcreteRoles.C1AllorsUnique = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsUnique]; 

			C1Class.Instance.ConcreteRoles.C1I2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I2Many2One]; 

			C1Class.Instance.ConcreteRoles.C1I1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1I1One2Many]; 

			C1Class.Instance.ConcreteRoles.C1C2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C2One2One]; 

			C1Class.Instance.ConcreteRoles.C1AllorsDateTime = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsDateTime]; 

			C1Class.Instance.ConcreteRoles.C1AllorsDouble = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsDouble]; 

			C1Class.Instance.ConcreteRoles.C1C2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1C2Many2Many]; 

			C1Class.Instance.ConcreteRoles.C1AllorsInteger = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C1AllorsInteger]; 


		}

		internal class RolesType
		{
			internal RoleType C1I1One2One;
			internal RoleType C1AllorsString;
			internal RoleType C1C2Many2One;
			internal RoleType C1I2One2One;
			internal RoleType C1C1One2One;
			internal RoleType C1I1Many2One;
			internal RoleType C1I1Many2Many;
			internal RoleType C1I2One2Many;
			internal RoleType C1AllorsDecimal;
			internal RoleType C1C1Many2Many;
			internal RoleType C1I12Many2Many;
			internal RoleType C1AllorsBinary;
			internal RoleType C1I12One2Many;
			internal RoleType C1C2One2Many;
			internal RoleType C1C1One2Many;
			internal RoleType AllorsStringMax;
			internal RoleType C1C1Many2One;
			internal RoleType C1AllorsBoolean;
			internal RoleType C1I12One2One;
			internal RoleType C1I12Many2One;
			internal RoleType C1I2Many2Many;
			internal RoleType C1AllorsUnique;
			internal RoleType C1I2Many2One;
			internal RoleType C1I1One2Many;
			internal RoleType C1C2One2One;
			internal RoleType C1AllorsDateTime;
			internal RoleType C1AllorsDouble;
			internal RoleType C1C2Many2Many;
			internal RoleType C1AllorsInteger;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType C1I1One2One;
			internal ConcreteRoleType C1AllorsString;
			internal ConcreteRoleType C1C2Many2One;
			internal ConcreteRoleType C1I2One2One;
			internal ConcreteRoleType C1C1One2One;
			internal ConcreteRoleType C1I1Many2One;
			internal ConcreteRoleType C1I1Many2Many;
			internal ConcreteRoleType C1I2One2Many;
			internal ConcreteRoleType C1AllorsDecimal;
			internal ConcreteRoleType C1C1Many2Many;
			internal ConcreteRoleType C1I12Many2Many;
			internal ConcreteRoleType C1AllorsBinary;
			internal ConcreteRoleType C1I12One2Many;
			internal ConcreteRoleType C1C2One2Many;
			internal ConcreteRoleType C1C1One2Many;
			internal ConcreteRoleType AllorsStringMax;
			internal ConcreteRoleType C1C1Many2One;
			internal ConcreteRoleType C1AllorsBoolean;
			internal ConcreteRoleType C1I12One2One;
			internal ConcreteRoleType C1I12Many2One;
			internal ConcreteRoleType C1I2Many2Many;
			internal ConcreteRoleType C1AllorsUnique;
			internal ConcreteRoleType C1I2Many2One;
			internal ConcreteRoleType C1I1One2Many;
			internal ConcreteRoleType C1C2One2One;
			internal ConcreteRoleType C1AllorsDateTime;
			internal ConcreteRoleType C1AllorsDouble;
			internal ConcreteRoleType C1C2Many2Many;
			internal ConcreteRoleType C1AllorsInteger;
			internal ConcreteRoleType I1I1Many2One;
			internal ConcreteRoleType I1I12Many2Many;
			internal ConcreteRoleType I1I2Many2Many;
			internal ConcreteRoleType I1I2Many2One;
			internal ConcreteRoleType I1AllorsString;
			internal ConcreteRoleType I1I12Many2One;
			internal ConcreteRoleType I1AllorsDateTime;
			internal ConcreteRoleType I1I2One2Many;
			internal ConcreteRoleType I1C2One2Many;
			internal ConcreteRoleType I1C1One2One;
			internal ConcreteRoleType I1AllorsInteger;
			internal ConcreteRoleType I1C2Many2Many;
			internal ConcreteRoleType I1I1One2Many;
			internal ConcreteRoleType I1I1Many2Many;
			internal ConcreteRoleType I1AllorsBoolean;
			internal ConcreteRoleType I1AllorsDecimal;
			internal ConcreteRoleType I1I12One2One;
			internal ConcreteRoleType I1I2One2One;
			internal ConcreteRoleType I1C2One2One;
			internal ConcreteRoleType I1C1One2Many;
			internal ConcreteRoleType I1AllorsBinary;
			internal ConcreteRoleType I1C1Many2Many;
			internal ConcreteRoleType I1AllorsDouble;
			internal ConcreteRoleType I1I1One2One;
			internal ConcreteRoleType I1C1Many2One;
			internal ConcreteRoleType I1I12One2Many;
			internal ConcreteRoleType I1C2Many2One;
			internal ConcreteRoleType I1AllorsUnique;
			internal ConcreteRoleType I12AllorsBinary;
			internal ConcreteRoleType I12C2One2One;
			internal ConcreteRoleType I12AllorsDouble;
			internal ConcreteRoleType I12I1Many2One;
			internal ConcreteRoleType I12AllorsString;
			internal ConcreteRoleType I12I12Many2Many;
			internal ConcreteRoleType I12AllorsDecimal;
			internal ConcreteRoleType I12I2Many2Many;
			internal ConcreteRoleType I12C2Many2Many;
			internal ConcreteRoleType I12I1Many2Many;
			internal ConcreteRoleType I12I12One2Many;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType I12C1Many2Many;
			internal ConcreteRoleType I12I2Many2One;
			internal ConcreteRoleType I12AllorsUnique;
			internal ConcreteRoleType I12AllorsInteger;
			internal ConcreteRoleType I12I1One2Many;
			internal ConcreteRoleType I12C1One2One;
			internal ConcreteRoleType I12I12One2One;
			internal ConcreteRoleType I12I2One2One;
			internal ConcreteRoleType Dependency;
			internal ConcreteRoleType I12I2One2Many;
			internal ConcreteRoleType I12C2Many2One;
			internal ConcreteRoleType I12I12Many2One;
			internal ConcreteRoleType I12AllorsBoolean;
			internal ConcreteRoleType I12I1One2One;
			internal ConcreteRoleType I12C1One2Many;
			internal ConcreteRoleType I12C1Many2One;
			internal ConcreteRoleType I12AllorsDateTime;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class C2Class : Class
	{
		public static C2Class Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal C2Class() : base(TestsDomain.Instance, new Guid("72c07e8a-03f5-4da8-ab37-236333d4f74e"))
        {
			this.SingularName = "C2";
			this.PluralName = "C2s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("5f8ac71f-e26c-4e6c-a3d9-55450ad44ff2"))
			{
				Subtype = Instance,
				Supertype = I2Interface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var C2AllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("07eaa992-322a-40e9-bf2c-aa33b69f54cd"), new Guid("172c107a-e140-4462-9a62-5ef91e6ead2a"), new Guid("152c92f0-485e-4a28-b321-d6ed3b730fc0"));
			C2AllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsDecimal.AssociationType.ObjectType = this;

			C2AllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			C2AllorsDecimal.RoleType.Scale = 2;
			C2AllorsDecimal.RoleType.Precision = 19;
			this.Roles.C2AllorsDecimal = C2AllorsDecimal.RoleType;

			var C2C1One2One = new RelationType(TestsDomain.Instance, new Guid("0c8209e3-b2fc-4c7a-acd2-6b5b8ac89bf4"), new Guid("56bb9554-819f-418a-9ce1-a91fa704b371"), new Guid("9292cb86-3e04-4cd4-b3fd-a5af7a5ce538"));
			C2C1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C2C1One2One.IsIndexed = true;

			C2C1One2One.AssociationType.ObjectType = this;

			C2C1One2One.RoleType.ObjectType = C1Class.Instance;;
			C2C1One2One.RoleType.AssignedSingularName = "C1One2One";
			C2C1One2One.RoleType.AssignedPluralName = "C1One2Ones";
			this.Roles.C2C1One2One = C2C1One2One.RoleType;

			var C2C2Many2One = new RelationType(TestsDomain.Instance, new Guid("12896fc2-c9e9-4a89-b875-0aeb92e298e5"), new Guid("781b282e-b86f-4747-9d5e-d0f7c08b0899"), new Guid("f41ddb05-4a96-40fa-859b-8b3d6dfcd86b"));
			C2C2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C2C2Many2One.IsIndexed = true;

			C2C2Many2One.AssociationType.ObjectType = this;

			C2C2Many2One.RoleType.ObjectType = C2Class.Instance;;
			C2C2Many2One.RoleType.AssignedSingularName = "C2Many2One";
			C2C2Many2One.RoleType.AssignedPluralName = "C2Many2Ones";
			this.Roles.C2C2Many2One = C2C2Many2One.RoleType;

			var C2AllorsUnique = new RelationType(TestsDomain.Instance, new Guid("1444d919-6ca1-4642-8d18-9d955c817581"), new Guid("9263c1e7-0cda-4129-a16d-da5351adafcb"), new Guid("cc1f2cae-2a5d-4584-aa08-4b03fc2176d2"));
			C2AllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsUnique.AssociationType.ObjectType = this;

			C2AllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.C2AllorsUnique = C2AllorsUnique.RoleType;

			var C2I12Many2One = new RelationType(TestsDomain.Instance, new Guid("165cc83e-935d-4d0d-aec7-5da155300086"), new Guid("bc437b29-f883-41c1-b36f-20be37bc9b30"), new Guid("b2f83414-aa5c-44fd-a382-56119727785a"));
			C2I12Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C2I12Many2One.IsIndexed = true;

			C2I12Many2One.AssociationType.ObjectType = this;

			C2I12Many2One.RoleType.ObjectType = I12Interface.Instance;;
			C2I12Many2One.RoleType.AssignedSingularName = "I12Many2One";
			C2I12Many2One.RoleType.AssignedPluralName = "I12Many2Ones";
			this.Roles.C2I12Many2One = C2I12Many2One.RoleType;

			var C2I12One2One = new RelationType(TestsDomain.Instance, new Guid("1d0c57c9-a3d1-4134-bc7d-7bb587d8250f"), new Guid("07c026ad-3515-4df0-bee7-ab61d5a9217d"), new Guid("c0562ba5-0402-44ea-acd0-9e78d20e7576"));
			C2I12One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C2I12One2One.IsIndexed = true;

			C2I12One2One.AssociationType.ObjectType = this;

			C2I12One2One.RoleType.ObjectType = I12Interface.Instance;;
			C2I12One2One.RoleType.AssignedSingularName = "I12One2One";
			C2I12One2One.RoleType.AssignedPluralName = "I12One2Ones";
			this.Roles.C2I12One2One = C2I12One2One.RoleType;

			var C2I1Many2Many = new RelationType(TestsDomain.Instance, new Guid("1d98eda7-6dba-43f1-a5ce-44f7ed104cf9"), new Guid("cae17f3c-a605-4dce-b38d-01c23eea29df"), new Guid("d3e84546-02fc-40be-b550-dbd54cd8a139"));
			C2I1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C2I1Many2Many.IsIndexed = true;

			C2I1Many2Many.AssociationType.ObjectType = this;

			C2I1Many2Many.RoleType.ObjectType = I1Interface.Instance;;
			C2I1Many2Many.RoleType.AssignedSingularName = "I1Many2Many";
			C2I1Many2Many.RoleType.AssignedPluralName = "I1Many2Manies";
			this.Roles.C2I1Many2Many = C2I1Many2Many.RoleType;

			var C2AllorsDouble = new RelationType(TestsDomain.Instance, new Guid("262ad367-a52c-4d8b-94e2-b477bb098423"), new Guid("31be0ad7-0886-406a-a69f-7e38b4526199"), new Guid("c52984df-80f8-4622-84e0-0e9f97cfaff3"));
			C2AllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsDouble.AssociationType.ObjectType = this;

			C2AllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.C2AllorsDouble = C2AllorsDouble.RoleType;

			var C2I1One2Many = new RelationType(TestsDomain.Instance, new Guid("2ac55066-c748-4f90-9d0f-1090fe02cc76"), new Guid("02a5ac2c-d0ac-482d-abee-117ed7eaa5ba"), new Guid("28f373c6-62b6-4f5c-b794-c10138043a63"));
			C2I1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C2I1One2Many.IsIndexed = true;

			C2I1One2Many.AssociationType.ObjectType = this;

			C2I1One2Many.RoleType.ObjectType = I1Interface.Instance;;
			C2I1One2Many.RoleType.AssignedSingularName = "I1One2Many";
			C2I1One2Many.RoleType.AssignedPluralName = "I1One2Manies";
			this.Roles.C2I1One2Many = C2I1One2Many.RoleType;

			var C2I2One2One = new RelationType(TestsDomain.Instance, new Guid("38063edc-271a-410d-b857-807a9100c7b5"), new Guid("6bedcc6b-af15-4f27-93e8-4404d23dfd99"), new Guid("642f5531-896d-482f-b746-4ecf08f27027"));
			C2I2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C2I2One2One.IsIndexed = true;

			C2I2One2One.AssociationType.ObjectType = this;

			C2I2One2One.RoleType.ObjectType = I2Interface.Instance;;
			C2I2One2One.RoleType.AssignedSingularName = "I2One2One";
			C2I2One2One.RoleType.AssignedPluralName = "I2One2Ones";
			this.Roles.C2I2One2One = C2I2One2One.RoleType;

			var C2AllorsInteger = new RelationType(TestsDomain.Instance, new Guid("42f9f4b6-3b35-4168-93cb-35171dbf83f4"), new Guid("622f9b4f-efc8-454f-9dd6-884bed5b5f4b"), new Guid("f5545dfc-e19a-456a-8469-46708ea5bb68"));
			C2AllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsInteger.AssociationType.ObjectType = this;

			C2AllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.C2AllorsInteger = C2AllorsInteger.RoleType;

			var C2I2Many2Many = new RelationType(TestsDomain.Instance, new Guid("4a963639-72c3-4e9f-9058-bcfc8fe0bc9e"), new Guid("e8c9548b-3d75-4f2b-af4f-f953572c587c"), new Guid("a1a975a4-7d1e-4734-962e-2f717386783a"));
			C2I2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C2I2Many2Many.IsIndexed = true;

			C2I2Many2Many.AssociationType.ObjectType = this;

			C2I2Many2Many.RoleType.ObjectType = I2Interface.Instance;;
			C2I2Many2Many.RoleType.AssignedSingularName = "I2Many2Many";
			C2I2Many2Many.RoleType.AssignedPluralName = "I2Many2Manies";
			this.Roles.C2I2Many2Many = C2I2Many2Many.RoleType;

			var C2I12Many2Many = new RelationType(TestsDomain.Instance, new Guid("50300577-b5fb-4c16-9ac5-41151543f958"), new Guid("1f16f92e-ba99-4553-bd1d-b95ba360468a"), new Guid("6210478c-86e3-4d8c-8e3c-3b29da3175ca"));
			C2I12Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C2I12Many2Many.IsIndexed = true;

			C2I12Many2Many.AssociationType.ObjectType = this;

			C2I12Many2Many.RoleType.ObjectType = I12Interface.Instance;;
			C2I12Many2Many.RoleType.AssignedSingularName = "I12Many2Many";
			C2I12Many2Many.RoleType.AssignedPluralName = "I12Many2Manies";
			this.Roles.C2I12Many2Many = C2I12Many2Many.RoleType;

			var C2C2One2Many = new RelationType(TestsDomain.Instance, new Guid("60680366-4790-4443-a941-b30cd4bd3848"), new Guid("8fa68cfd-8e0c-40c6-881b-4ebe88487ae7"), new Guid("bfa632a3-f334-4c92-a1b1-21cfa726ab90"));
			C2C2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C2C2One2Many.IsIndexed = true;

			C2C2One2Many.AssociationType.ObjectType = this;

			C2C2One2Many.RoleType.ObjectType = C2Class.Instance;;
			C2C2One2Many.RoleType.AssignedSingularName = "C2One2Many";
			C2C2One2Many.RoleType.AssignedPluralName = "C2One2Manies";
			this.Roles.C2C2One2Many = C2C2One2Many.RoleType;

			var C2AllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("61daaaae-dd22-405e-aa98-6321d2f8af04"), new Guid("a0291a20-3519-44e6-bb8d-b53682c02c52"), new Guid("bff48eef-9e8f-45b7-83ff-7b63dac407f1"));
			C2AllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsBoolean.AssociationType.ObjectType = this;

			C2AllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.C2AllorsBoolean = C2AllorsBoolean.RoleType;

			var C2I1Many2One = new RelationType(TestsDomain.Instance, new Guid("65a246a7-cd78-45eb-90db-39f542e7c6cf"), new Guid("eb4f1289-1c6c-4964-a9ba-50f991a96564"), new Guid("6ff71b5b-723d-424f-9e2f-fb37bb8118fe"));
			C2I1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C2I1Many2One.IsIndexed = true;

			C2I1Many2One.AssociationType.ObjectType = this;

			C2I1Many2One.RoleType.ObjectType = I1Interface.Instance;;
			C2I1Many2One.RoleType.AssignedSingularName = "I1Many2One";
			C2I1Many2One.RoleType.AssignedPluralName = "I1Many2Ones";
			this.Roles.C2I1Many2One = C2I1Many2One.RoleType;

			var C2I1One2One = new RelationType(TestsDomain.Instance, new Guid("67780894-fa62-48ba-8f47-7f54106090cd"), new Guid("38cd28ba-c584-4d06-b521-dcc8094c5ed3"), new Guid("128eb00f-03fc-432e-bec6-8fcfb265a3a9"));
			C2I1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C2I1One2One.IsIndexed = true;

			C2I1One2One.AssociationType.ObjectType = this;

			C2I1One2One.RoleType.ObjectType = I1Interface.Instance;;
			C2I1One2One.RoleType.AssignedSingularName = "I1One2One";
			C2I1One2One.RoleType.AssignedPluralName = "I1One2Ones";
			this.Roles.C2I1One2One = C2I1One2One.RoleType;

			var C2C1Many2Many = new RelationType(TestsDomain.Instance, new Guid("70600f67-7b18-4b5c-b11e-2ed180c5b2d6"), new Guid("a373cb01-731b-48be-a387-d057fdb70684"), new Guid("572738e4-956b-404d-97e8-4bb431ce7692"));
			C2C1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C2C1Many2Many.IsIndexed = true;

			C2C1Many2Many.AssociationType.ObjectType = this;

			C2C1Many2Many.RoleType.ObjectType = C1Class.Instance;;
			C2C1Many2Many.RoleType.AssignedSingularName = "C1Many2Many";
			C2C1Many2Many.RoleType.AssignedPluralName = "C1Many2Manies";
			this.Roles.C2C1Many2Many = C2C1Many2Many.RoleType;

			var C2I12One2Many = new RelationType(TestsDomain.Instance, new Guid("770eb33c-c8ef-4629-a3a0-20decd92ff62"), new Guid("de757393-f81a-413c-897b-a47efd48cc79"), new Guid("8ac9a5cd-35a4-4ca3-a1af-ab3f489c7a52"));
			C2I12One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C2I12One2Many.IsIndexed = true;

			C2I12One2Many.AssociationType.ObjectType = this;

			C2I12One2Many.RoleType.ObjectType = I12Interface.Instance;;
			C2I12One2Many.RoleType.AssignedSingularName = "I12One2Many";
			C2I12One2Many.RoleType.AssignedPluralName = "I12One2Manies";
			this.Roles.C2I12One2Many = C2I12One2Many.RoleType;

			var C2I2One2Many = new RelationType(TestsDomain.Instance, new Guid("7a9129c9-7b6d-4bdd-a630-cfd1392549b7"), new Guid("87f7a34c-476f-4670-a670-30451c05842d"), new Guid("19f3caa1-c8d1-4257-b4ad-2f8ccb809524"));
			C2I2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C2I2One2Many.IsIndexed = true;

			C2I2One2Many.AssociationType.ObjectType = this;

			C2I2One2Many.RoleType.ObjectType = I2Interface.Instance;;
			C2I2One2Many.RoleType.AssignedSingularName = "I2One2Many";
			C2I2One2Many.RoleType.AssignedPluralName = "I2One2Manies";
			this.Roles.C2I2One2Many = C2I2One2Many.RoleType;

			var C2C2One2One = new RelationType(TestsDomain.Instance, new Guid("86ad371b-0afd-420b-a855-38ebb3f39f38"), new Guid("23f5e29b-c36b-416f-93da-9ef2d79fc0f1"), new Guid("cdf7b6ee-fa50-44a1-9433-d04d61ef3aeb"));
			C2C2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			C2C2One2One.IsIndexed = true;

			C2C2One2One.AssociationType.ObjectType = this;

			C2C2One2One.RoleType.ObjectType = C2Class.Instance;;
			C2C2One2One.RoleType.AssignedSingularName = "C2One2One";
			C2C2One2One.RoleType.AssignedPluralName = "C2One2Ones";
			this.Roles.C2C2One2One = C2C2One2One.RoleType;

			var C2AllorsString = new RelationType(TestsDomain.Instance, new Guid("9c7cde3f-9b61-4c79-a5d7-afe1067262ce"), new Guid("71d6109e-1c04-4598-88fa-f06308beb45b"), new Guid("8a96d544-e96f-45b5-aeee-d9381946ff31"));
			C2AllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsString.AssociationType.ObjectType = this;

			C2AllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			C2AllorsString.RoleType.Size = 256;
			this.Roles.C2AllorsString = C2AllorsString.RoleType;

			var C2C1Many2One = new RelationType(TestsDomain.Instance, new Guid("a5315151-aa0f-42a3-9d5b-2c7f2cb92560"), new Guid("f2bf51b6-0375-4d77-8881-d4d313d682ef"), new Guid("54dce296-9454-440e-9cf3-1327ea439f0e"));
			C2C1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C2C1Many2One.IsIndexed = true;

			C2C1Many2One.AssociationType.ObjectType = this;

			C2C1Many2One.RoleType.ObjectType = C1Class.Instance;;
			C2C1Many2One.RoleType.AssignedSingularName = "C1Many2One";
			C2C1Many2One.RoleType.AssignedPluralName = "C1Many2Ones";
			this.Roles.C2C1Many2One = C2C1Many2One.RoleType;

			var C2C2Many2Many = new RelationType(TestsDomain.Instance, new Guid("bc6c7fe0-6501-428c-a929-da87a9f4b885"), new Guid("794d2637-293c-49cc-a052-246a779825e9"), new Guid("73d243be-d8d0-42c7-b354-fd9786b4eaaa"));
			C2C2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			C2C2Many2Many.IsIndexed = true;

			C2C2Many2Many.AssociationType.ObjectType = this;

			C2C2Many2Many.RoleType.ObjectType = C2Class.Instance;;
			C2C2Many2Many.RoleType.AssignedSingularName = "C2Many2Many";
			C2C2Many2Many.RoleType.AssignedPluralName = "C2Many2Manies";
			this.Roles.C2C2Many2Many = C2C2Many2Many.RoleType;

			var C2AllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("ce23482d-3a22-4202-98e7-5934fd9abd2d"), new Guid("6d752249-af37-4f22-9e59-bfae9e6537ee"), new Guid("6e9490f2-740f-498c-9c0f-d601c76f28ad"));
			C2AllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsDateTime.AssociationType.ObjectType = this;

			C2AllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.C2AllorsDateTime = C2AllorsDateTime.RoleType;

			var C2I2Many2One = new RelationType(TestsDomain.Instance, new Guid("e08d75a9-9b67-4d20-a476-757f8fb17376"), new Guid("7d45be10-724e-46c4-8dac-4acdf7f515ad"), new Guid("888cd015-7323-45da-83fe-eb297e8ede51"));
			C2I2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			C2I2Many2One.IsIndexed = true;

			C2I2Many2One.AssociationType.ObjectType = this;

			C2I2Many2One.RoleType.ObjectType = I2Interface.Instance;;
			C2I2Many2One.RoleType.AssignedSingularName = "I2Many2One";
			C2I2Many2One.RoleType.AssignedPluralName = "I2Many2Ones";
			this.Roles.C2I2Many2One = C2I2Many2One.RoleType;

			var C2C1One2Many = new RelationType(TestsDomain.Instance, new Guid("f748949e-de5a-4f2e-85e2-e15516d9bf24"), new Guid("92c02837-9e6c-45ad-8772-b97a17afad8c"), new Guid("2c172bc6-a87b-4945-b02f-e00a38eb866d"));
			C2C1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			C2C1One2Many.IsIndexed = true;

			C2C1One2Many.AssociationType.ObjectType = this;

			C2C1One2Many.RoleType.ObjectType = C1Class.Instance;;
			C2C1One2Many.RoleType.AssignedSingularName = "C1One2Many";
			C2C1One2Many.RoleType.AssignedPluralName = "C1One2Manies";
			this.Roles.C2C1One2Many = C2C1One2Many.RoleType;

			var C2AllorsBinary = new RelationType(TestsDomain.Instance, new Guid("fa8ad982-9953-47dd-9905-81cc4572300e"), new Guid("604eec66-6a75-465b-93d8-349dcbccb2bd"), new Guid("e701ac90-488a-476f-9b13-ea361e8ff450"));
			C2AllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			C2AllorsBinary.AssociationType.ObjectType = this;

			C2AllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			C2AllorsBinary.RoleType.Size = -1;
			this.Roles.C2AllorsBinary = C2AllorsBinary.RoleType;


		}

		internal override void SetRelationTypes()
		{
			C2Class.Instance.ConcreteRoles.C2AllorsDecimal = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsDecimal]; 

			C2Class.Instance.ConcreteRoles.C2C1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C1One2One]; 

			C2Class.Instance.ConcreteRoles.C2C2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C2Many2One]; 

			C2Class.Instance.ConcreteRoles.C2AllorsUnique = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsUnique]; 

			C2Class.Instance.ConcreteRoles.C2I12Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I12Many2One]; 

			C2Class.Instance.ConcreteRoles.C2I12One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I12One2One]; 

			C2Class.Instance.ConcreteRoles.C2I1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I1Many2Many]; 

			C2Class.Instance.ConcreteRoles.C2AllorsDouble = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsDouble]; 

			C2Class.Instance.ConcreteRoles.C2I1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I1One2Many]; 

			C2Class.Instance.ConcreteRoles.C2I2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I2One2One]; 

			C2Class.Instance.ConcreteRoles.C2AllorsInteger = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsInteger]; 

			C2Class.Instance.ConcreteRoles.C2I2Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I2Many2Many]; 

			C2Class.Instance.ConcreteRoles.C2I12Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I12Many2Many]; 

			C2Class.Instance.ConcreteRoles.C2C2One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C2One2Many]; 

			C2Class.Instance.ConcreteRoles.C2AllorsBoolean = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsBoolean]; 

			C2Class.Instance.ConcreteRoles.C2I1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I1Many2One]; 

			C2Class.Instance.ConcreteRoles.C2I1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I1One2One]; 

			C2Class.Instance.ConcreteRoles.C2C1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C1Many2Many]; 

			C2Class.Instance.ConcreteRoles.C2I12One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I12One2Many]; 

			C2Class.Instance.ConcreteRoles.C2I2One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I2One2Many]; 

			C2Class.Instance.ConcreteRoles.C2C2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C2One2One]; 

			C2Class.Instance.ConcreteRoles.C2AllorsString = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsString]; 

			C2Class.Instance.ConcreteRoles.C2C1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C1Many2One]; 

			C2Class.Instance.ConcreteRoles.C2C2Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C2Many2Many]; 

			C2Class.Instance.ConcreteRoles.C2AllorsDateTime = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsDateTime]; 

			C2Class.Instance.ConcreteRoles.C2I2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2I2Many2One]; 

			C2Class.Instance.ConcreteRoles.C2C1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2C1One2Many]; 

			C2Class.Instance.ConcreteRoles.C2AllorsBinary = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.C2AllorsBinary]; 


		}

		internal class RolesType
		{
			internal RoleType C2AllorsDecimal;
			internal RoleType C2C1One2One;
			internal RoleType C2C2Many2One;
			internal RoleType C2AllorsUnique;
			internal RoleType C2I12Many2One;
			internal RoleType C2I12One2One;
			internal RoleType C2I1Many2Many;
			internal RoleType C2AllorsDouble;
			internal RoleType C2I1One2Many;
			internal RoleType C2I2One2One;
			internal RoleType C2AllorsInteger;
			internal RoleType C2I2Many2Many;
			internal RoleType C2I12Many2Many;
			internal RoleType C2C2One2Many;
			internal RoleType C2AllorsBoolean;
			internal RoleType C2I1Many2One;
			internal RoleType C2I1One2One;
			internal RoleType C2C1Many2Many;
			internal RoleType C2I12One2Many;
			internal RoleType C2I2One2Many;
			internal RoleType C2C2One2One;
			internal RoleType C2AllorsString;
			internal RoleType C2C1Many2One;
			internal RoleType C2C2Many2Many;
			internal RoleType C2AllorsDateTime;
			internal RoleType C2I2Many2One;
			internal RoleType C2C1One2Many;
			internal RoleType C2AllorsBinary;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType C2AllorsDecimal;
			internal ConcreteRoleType C2C1One2One;
			internal ConcreteRoleType C2C2Many2One;
			internal ConcreteRoleType C2AllorsUnique;
			internal ConcreteRoleType C2I12Many2One;
			internal ConcreteRoleType C2I12One2One;
			internal ConcreteRoleType C2I1Many2Many;
			internal ConcreteRoleType C2AllorsDouble;
			internal ConcreteRoleType C2I1One2Many;
			internal ConcreteRoleType C2I2One2One;
			internal ConcreteRoleType C2AllorsInteger;
			internal ConcreteRoleType C2I2Many2Many;
			internal ConcreteRoleType C2I12Many2Many;
			internal ConcreteRoleType C2C2One2Many;
			internal ConcreteRoleType C2AllorsBoolean;
			internal ConcreteRoleType C2I1Many2One;
			internal ConcreteRoleType C2I1One2One;
			internal ConcreteRoleType C2C1Many2Many;
			internal ConcreteRoleType C2I12One2Many;
			internal ConcreteRoleType C2I2One2Many;
			internal ConcreteRoleType C2C2One2One;
			internal ConcreteRoleType C2AllorsString;
			internal ConcreteRoleType C2C1Many2One;
			internal ConcreteRoleType C2C2Many2Many;
			internal ConcreteRoleType C2AllorsDateTime;
			internal ConcreteRoleType C2I2Many2One;
			internal ConcreteRoleType C2C1One2Many;
			internal ConcreteRoleType C2AllorsBinary;
			internal ConcreteRoleType I2I2Many2One;
			internal ConcreteRoleType I2C1Many2One;
			internal ConcreteRoleType I2I12Many2One;
			internal ConcreteRoleType I2AllorsBoolean;
			internal ConcreteRoleType I2C1One2Many;
			internal ConcreteRoleType I2C1One2One;
			internal ConcreteRoleType I2AllorsDecimal;
			internal ConcreteRoleType I2Many2any;
			internal ConcreteRoleType I2AllorsBinary;
			internal ConcreteRoleType I2AllorsUnique;
			internal ConcreteRoleType I2I1Many2One;
			internal ConcreteRoleType I2AllorsDateTime;
			internal ConcreteRoleType I2I12One2Many;
			internal ConcreteRoleType I2I12One2One;
			internal ConcreteRoleType I2C2Many2Many;
			internal ConcreteRoleType I2I1Many2Many;
			internal ConcreteRoleType I2C2Many2One;
			internal ConcreteRoleType I2AllorsString;
			internal ConcreteRoleType I2C2One2Many;
			internal ConcreteRoleType I2I1One2One;
			internal ConcreteRoleType I2I1One2Many;
			internal ConcreteRoleType I2I12Many2Many;
			internal ConcreteRoleType I2I2One2One;
			internal ConcreteRoleType I2AllorsInteger;
			internal ConcreteRoleType I2I2One2Many;
			internal ConcreteRoleType I2C1Many2Many;
			internal ConcreteRoleType I2C2One2One;
			internal ConcreteRoleType I2AllorsDouble;
			internal ConcreteRoleType I12AllorsBinary;
			internal ConcreteRoleType I12C2One2One;
			internal ConcreteRoleType I12AllorsDouble;
			internal ConcreteRoleType I12I1Many2One;
			internal ConcreteRoleType I12AllorsString;
			internal ConcreteRoleType I12I12Many2Many;
			internal ConcreteRoleType I12AllorsDecimal;
			internal ConcreteRoleType I12I2Many2Many;
			internal ConcreteRoleType I12C2Many2Many;
			internal ConcreteRoleType I12I1Many2Many;
			internal ConcreteRoleType I12I12One2Many;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType I12C1Many2Many;
			internal ConcreteRoleType I12I2Many2One;
			internal ConcreteRoleType I12AllorsUnique;
			internal ConcreteRoleType I12AllorsInteger;
			internal ConcreteRoleType I12I1One2Many;
			internal ConcreteRoleType I12C1One2One;
			internal ConcreteRoleType I12I12One2One;
			internal ConcreteRoleType I12I2One2One;
			internal ConcreteRoleType Dependency;
			internal ConcreteRoleType I12I2One2Many;
			internal ConcreteRoleType I12C2Many2One;
			internal ConcreteRoleType I12I12Many2One;
			internal ConcreteRoleType I12AllorsBoolean;
			internal ConcreteRoleType I12I1One2One;
			internal ConcreteRoleType I12C1One2Many;
			internal ConcreteRoleType I12C1Many2One;
			internal ConcreteRoleType I12AllorsDateTime;

		}
	}public partial class ToClass : Class
	{
		public static ToClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ToClass() : base(TestsDomain.Instance, new Guid("7eb25112-4b81-4e8d-9f75-90950c40c65f"))
        {
			this.SingularName = "To";
			this.PluralName = "Tos";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var ToName = new RelationType(TestsDomain.Instance, new Guid("4be564ac-77bc-48b8-b945-7d39f2ea9903"), new Guid("7a6714c1-e58a-45ac-8ee5-ca5f22b6d528"), new Guid("53e0761a-a9f1-4516-a086-b766650ac28b"));
			ToName.AssignedMultiplicity = Multiplicity.OneToOne;
			ToName.AssociationType.ObjectType = this;

			ToName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			ToName.RoleType.AssignedSingularName = "Name";
			ToName.RoleType.AssignedPluralName = "Names";
			ToName.RoleType.Size = 256;
			this.Roles.Name = ToName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			ToClass.Instance.ConcreteRoles.Name = ToClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Name;

		}
	}public partial class MailboxAddressClass : Class
	{
		public static MailboxAddressClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal MailboxAddressClass() : base(TestsDomain.Instance, new Guid("7ee3b00b-4e63-4774-b744-3add2c6035ab"))
        {
			this.SingularName = "MailboxAddress";
			this.PluralName = "MailboxAddresses";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("c849f4f5-f4ec-45d0-a384-d94a997c854d"))
			{
				Subtype = Instance,
				Supertype = AddressInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var MailboxAddressPoBox = new RelationType(TestsDomain.Instance, new Guid("03c9970e-d9d6-427d-83d0-00e0888f5588"), new Guid("8d565792-4315-44eb-9930-55aa30e8f23a"), new Guid("10b46f89-7f3a-4571-8621-259a2a501dc7"));
			MailboxAddressPoBox.AssignedMultiplicity = Multiplicity.OneToOne;
			MailboxAddressPoBox.AssociationType.ObjectType = this;

			MailboxAddressPoBox.RoleType.ObjectType = AllorsStringUnit.Instance;;
			MailboxAddressPoBox.RoleType.AssignedSingularName = "PoBox";
			MailboxAddressPoBox.RoleType.AssignedPluralName = "PoBoxes";
			MailboxAddressPoBox.RoleType.Size = 256;
			this.Roles.PoBox = MailboxAddressPoBox.RoleType;


		}

		internal override void SetRelationTypes()
		{
			MailboxAddressClass.Instance.ConcreteRoles.PoBox = MailboxAddressClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PoBox]; 


		}

		internal class RolesType
		{
			internal RoleType PoBox;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType PoBox;
			internal ConcreteRoleType Place;

		}
	}public partial class ExtenderClass : Class
	{
		public static ExtenderClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ExtenderClass() : base(TestsDomain.Instance, new Guid("830cdcb1-31f1-4481-8399-00c034661450"))
        {
			this.SingularName = "Extender";
			this.PluralName = "Extenders";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var ExtenderAllorsString = new RelationType(TestsDomain.Instance, new Guid("525bbc9e-d488-419f-ac02-0ab6ac409bac"), new Guid("7dcdf3d7-25ad-4e8f-9634-63b771990681"), new Guid("bf9f7482-5277-40db-a6ac-5d4731cb5537"));
			ExtenderAllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			ExtenderAllorsString.AssociationType.ObjectType = this;

			ExtenderAllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			ExtenderAllorsString.RoleType.Size = 256;
			this.Roles.AllorsString = ExtenderAllorsString.RoleType;


		}

		internal override void SetRelationTypes()
		{
			ExtenderClass.Instance.ConcreteRoles.AllorsString = ExtenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsString]; 


		}

		internal class RolesType
		{
			internal RoleType AllorsString;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType AllorsString;

		}
	}public partial class TwoClass : Class
	{
		public static TwoClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal TwoClass() : base(TestsDomain.Instance, new Guid("9ec7e136-815c-4726-9991-e95a3ec9e092"))
        {
			this.SingularName = "Two";
			this.PluralName = "Twos";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("a28aef2a-8dff-4427-b132-2161894e1886"))
			{
				Subtype = Instance,
				Supertype = SharedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var TwoShared = new RelationType(TestsDomain.Instance, new Guid("8930c13c-ad5a-4b0e-b3bf-d7cdf6f5b867"), new Guid("fd97db6d-d946-47ba-a2a0-88b732457b96"), new Guid("39eda296-4e8d-492b-b0c1-756ffcf9a493"));
			TwoShared.AssignedMultiplicity = Multiplicity.ManyToOne;
			TwoShared.IsIndexed = true;

			TwoShared.AssociationType.ObjectType = this;

			TwoShared.RoleType.ObjectType = SharedInterface.Instance;;
			this.Roles.Shared = TwoShared.RoleType;


		}

		internal override void SetRelationTypes()
		{
			TwoClass.Instance.ConcreteRoles.Shared = TwoClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Shared]; 


		}

		internal class RolesType
		{
			internal RoleType Shared;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Shared;

		}
	}public partial class I12Interface: Interface
	{
		public static I12Interface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal I12Interface() : base(TestsDomain.Instance, new Guid("b45ec13c-704f-413d-a662-bdc59a17bfe3"))
        {
			this.SingularName = "I12";
			this.PluralName = "I12s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var I12AllorsBinary = new RelationType(TestsDomain.Instance, new Guid("042d1311-1c06-4d7c-b68e-eb734f9c7327"), new Guid("0d3f0f95-aaa2-47bb-9e2b-654d2747b2b1"), new Guid("f7809a25-1b10-4eb0-9309-aeea6efcd7cb"));
			I12AllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsBinary.AssociationType.ObjectType = this;

			I12AllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			I12AllorsBinary.RoleType.Size = -1;
			this.Roles.I12AllorsBinary = I12AllorsBinary.RoleType;

			var I12C2One2One = new RelationType(TestsDomain.Instance, new Guid("107c212d-cc1c-41b2-9c1d-b40c0102072c"), new Guid("0a1b3b66-6bb2-4062-b3bb-991987dd5194"), new Guid("4c448b25-b56c-4486-b0c8-ac04a3249677"));
			I12C2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I12C2One2One.IsIndexed = true;

			I12C2One2One.AssociationType.ObjectType = this;

			I12C2One2One.RoleType.ObjectType = C2Class.Instance;;
			I12C2One2One.RoleType.AssignedSingularName = "C2One2One";
			I12C2One2One.RoleType.AssignedPluralName = "C2One2Ones";
			this.Roles.I12C2One2One = I12C2One2One.RoleType;

			var I12AllorsDouble = new RelationType(TestsDomain.Instance, new Guid("1611cb5d-4676-4e85-bfc5-5572e8ff1138"), new Guid("4af20cc8-a610-4493-9420-7cd110cc6755"), new Guid("5f2eff86-71bf-480d-a6ad-1c93fc68b08d"));
			I12AllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsDouble.AssociationType.ObjectType = this;

			I12AllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.I12AllorsDouble = I12AllorsDouble.RoleType;

			var I12I1Many2One = new RelationType(TestsDomain.Instance, new Guid("167b53c0-644c-467e-9f7c-fcb9415d02c6"), new Guid("d039c8f9-217a-46cc-b428-7480d4991e1e"), new Guid("2e3dc9b9-3700-4090-bafa-2c60050d52d5"));
			I12I1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I12I1Many2One.IsIndexed = true;

			I12I1Many2One.AssociationType.ObjectType = this;

			I12I1Many2One.RoleType.ObjectType = I1Interface.Instance;;
			I12I1Many2One.RoleType.AssignedSingularName = "I1Many2One";
			I12I1Many2One.RoleType.AssignedPluralName = "I1Many2Ones";
			this.Roles.I12I1Many2One = I12I1Many2One.RoleType;

			var I12AllorsString = new RelationType(TestsDomain.Instance, new Guid("199a84c4-c7cb-4f23-8b6c-078b14525e18"), new Guid("65ed1ff6-eb81-410d-8817-62d61765408a"), new Guid("c778c7a7-9cf7-4a7e-8408-e4eb1ca94ce8"));
			I12AllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsString.AssociationType.ObjectType = this;

			I12AllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			I12AllorsString.RoleType.Size = 256;
			this.Roles.I12AllorsString = I12AllorsString.RoleType;

			var I12I12Many2Many = new RelationType(TestsDomain.Instance, new Guid("1bf2abe0-9273-4fb9-b491-020320f1f8db"), new Guid("732fc964-194e-4ece-bd39-bb3c47b83ff9"), new Guid("b311c57d-9565-48c1-80d8-1d3cf5a498ea"));
			I12I12Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I12I12Many2Many.IsIndexed = true;

			I12I12Many2Many.AssociationType.ObjectType = this;

			I12I12Many2Many.RoleType.ObjectType = I12Interface.Instance;;
			I12I12Many2Many.RoleType.AssignedSingularName = "I12Many2Many";
			I12I12Many2Many.RoleType.AssignedPluralName = "I12Many2Manies";
			this.Roles.I12I12Many2Many = I12I12Many2Many.RoleType;

			var I12AllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("41a74fec-cfbc-43ca-a6e7-890f0dd1eddb"), new Guid("7293e939-ad0b-4b62-935d-44a5309f2515"), new Guid("295a4e46-3133-4aff-a1dc-5101e584fb8a"));
			I12AllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsDecimal.AssociationType.ObjectType = this;

			I12AllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			I12AllorsDecimal.RoleType.Scale = 2;
			I12AllorsDecimal.RoleType.Precision = 19;
			this.Roles.I12AllorsDecimal = I12AllorsDecimal.RoleType;

			var I12I2Many2Many = new RelationType(TestsDomain.Instance, new Guid("4a2b2f43-037d-4149-8a1e-401e5df963ba"), new Guid("cd90d290-95da-4137-aaf1-bcb59f10e9cb"), new Guid("f266759c-34c5-49a8-8d92-e2bbcb41c86a"));
			I12I2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I12I2Many2Many.IsIndexed = true;

			I12I2Many2Many.AssociationType.ObjectType = this;

			I12I2Many2Many.RoleType.ObjectType = I2Interface.Instance;;
			I12I2Many2Many.RoleType.AssignedSingularName = "I2Many2Many";
			I12I2Many2Many.RoleType.AssignedPluralName = "I2Many2Manies";
			this.Roles.I12I2Many2Many = I12I2Many2Many.RoleType;

			var I12C2Many2Many = new RelationType(TestsDomain.Instance, new Guid("51ebb024-c847-4165-b216-b3b6e8883961"), new Guid("04bca123-7c45-43f4-a5ed-8691b0cbb0e3"), new Guid("f5928b47-5a57-4be8-a0a9-a729e8e467bb"));
			I12C2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I12C2Many2Many.IsIndexed = true;

			I12C2Many2Many.AssociationType.ObjectType = this;

			I12C2Many2Many.RoleType.ObjectType = C2Class.Instance;;
			I12C2Many2Many.RoleType.AssignedSingularName = "C2Many2Many";
			I12C2Many2Many.RoleType.AssignedPluralName = "C2Many2Manies";
			this.Roles.I12C2Many2Many = I12C2Many2Many.RoleType;

			var I12I1Many2Many = new RelationType(TestsDomain.Instance, new Guid("59ae05e3-573c-4ea4-9181-2c545236ed1e"), new Guid("064f5e1b-b5c8-45ee-baf1-094f6a723ede"), new Guid("397b339e-0277-4700-a5d1-d9d0ac23c362"));
			I12I1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I12I1Many2Many.IsIndexed = true;

			I12I1Many2Many.AssociationType.ObjectType = this;

			I12I1Many2Many.RoleType.ObjectType = I1Interface.Instance;;
			I12I1Many2Many.RoleType.AssignedSingularName = "I1Many2Many";
			I12I1Many2Many.RoleType.AssignedPluralName = "I1Many2Manies";
			this.Roles.I12I1Many2Many = I12I1Many2Many.RoleType;

			var I12I12One2Many = new RelationType(TestsDomain.Instance, new Guid("5e473f63-b1d7-4530-b64f-26435fb5063c"), new Guid("83e23750-52eb-4b3f-a675-bfe32570357b"), new Guid("d786aeb4-03bb-419a-90c9-e6ddaa940e93"));
			I12I12One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I12I12One2Many.IsIndexed = true;

			I12I12One2Many.AssociationType.ObjectType = this;

			I12I12One2Many.RoleType.ObjectType = I12Interface.Instance;;
			I12I12One2Many.RoleType.AssignedSingularName = "I12One2Many";
			I12I12One2Many.RoleType.AssignedPluralName = "I12One2Manies";
			this.Roles.I12I12One2Many = I12I12One2Many.RoleType;

			var I12Name = new RelationType(TestsDomain.Instance, new Guid("6daafb16-1bc3-4f15-8e25-1a982c5bb3c5"), new Guid("d39d3782-71a6-4b63-aaeb-0a6da0db153d"), new Guid("a89707e2-e3e1-4d24-9c56-180671e3409c"));
			I12Name.AssignedMultiplicity = Multiplicity.OneToOne;
			I12Name.AssociationType.ObjectType = this;

			I12Name.RoleType.ObjectType = AllorsStringUnit.Instance;;
			I12Name.RoleType.AssignedSingularName = "Name";
			I12Name.RoleType.AssignedPluralName = "Names";
			I12Name.RoleType.Size = 256;
			this.Roles.Name = I12Name.RoleType;

			var I12C1Many2Many = new RelationType(TestsDomain.Instance, new Guid("7827af95-147f-4803-865a-b418d567da68"), new Guid("7e707f89-6dd2-44a4-8f85-e00666af4d00"), new Guid("a4c1f678-a3ae-4707-81e9-b3f3411a5d93"));
			I12C1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I12C1Many2Many.IsIndexed = true;

			I12C1Many2Many.AssociationType.ObjectType = this;

			I12C1Many2Many.RoleType.ObjectType = C1Class.Instance;;
			I12C1Many2Many.RoleType.AssignedSingularName = "C1Many2Many";
			I12C1Many2Many.RoleType.AssignedPluralName = "C1Many2Manies";
			this.Roles.I12C1Many2Many = I12C1Many2Many.RoleType;

			var I12I2Many2One = new RelationType(TestsDomain.Instance, new Guid("7f6fdb73-3e19-40e7-8feb-6ddbdf2e745a"), new Guid("644f55c6-8d39-4602-89bb-5797c9c8e1fd"), new Guid("2073096f-8918-4432-8fa2-42f4fd1a53a1"));
			I12I2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I12I2Many2One.IsIndexed = true;

			I12I2Many2One.AssociationType.ObjectType = this;

			I12I2Many2One.RoleType.ObjectType = I2Interface.Instance;;
			I12I2Many2One.RoleType.AssignedSingularName = "I2Many2One";
			I12I2Many2One.RoleType.AssignedPluralName = "I2Many2Ones";
			this.Roles.I12I2Many2One = I12I2Many2One.RoleType;

			var I12AllorsUnique = new RelationType(TestsDomain.Instance, new Guid("93a59d0a-278d-435b-967e-551523f0cb85"), new Guid("9c700ad0-e33e-4731-ac3a-4063c2da655b"), new Guid("839c7aaa-f044-4b93-97aa-00beeed8f3eb"));
			I12AllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsUnique.AssociationType.ObjectType = this;

			I12AllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.I12AllorsUnique = I12AllorsUnique.RoleType;

			var I12AllorsInteger = new RelationType(TestsDomain.Instance, new Guid("95551e3a-bad2-4136-923f-c8e5f0f2aec7"), new Guid("f57afc9e-3832-4ae1-b3a0-659d7f00604c"), new Guid("cbd73ad2-a4cd-4b65-a3cd-55bb7c6f52bc"));
			I12AllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsInteger.AssociationType.ObjectType = this;

			I12AllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.I12AllorsInteger = I12AllorsInteger.RoleType;

			var I12I1One2Many = new RelationType(TestsDomain.Instance, new Guid("95c77a0f-7f4c-4142-a93f-f688cfd554af"), new Guid("870af1ab-075f-4e19-a283-6e6875d362bb"), new Guid("29f38fb4-8e6a-4f70-9ee9-f6819b9d759e"));
			I12I1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I12I1One2Many.IsIndexed = true;

			I12I1One2Many.AssociationType.ObjectType = this;

			I12I1One2Many.RoleType.ObjectType = I1Interface.Instance;;
			I12I1One2Many.RoleType.AssignedSingularName = "I1One2Many";
			I12I1One2Many.RoleType.AssignedPluralName = "I1One2Manies";
			this.Roles.I12I1One2Many = I12I1One2Many.RoleType;

			var I12C1One2One = new RelationType(TestsDomain.Instance, new Guid("9aefdda0-e547-4c9b-bf28-431669f8ea2e"), new Guid("f4399c8b-3394-4c2a-9ff0-16b2ece87fdf"), new Guid("ee9379c4-ef6a-4c6e-8190-bc71c36ac009"));
			I12C1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I12C1One2One.IsIndexed = true;

			I12C1One2One.AssociationType.ObjectType = this;

			I12C1One2One.RoleType.ObjectType = C1Class.Instance;;
			I12C1One2One.RoleType.AssignedSingularName = "C1One2One";
			I12C1One2One.RoleType.AssignedPluralName = "C1One2Ones";
			this.Roles.I12C1One2One = I12C1One2One.RoleType;

			var I12I12One2One = new RelationType(TestsDomain.Instance, new Guid("a89b4c06-bba5-4b05-bd6f-c32bc195c32f"), new Guid("8dd3e2b6-805f-4c93-98d8-4864e6807760"), new Guid("e68fba09-6113-4b49-a6fa-a09e46a004f1"));
			I12I12One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I12I12One2One.IsIndexed = true;

			I12I12One2One.AssociationType.ObjectType = this;

			I12I12One2One.RoleType.ObjectType = I12Interface.Instance;;
			I12I12One2One.RoleType.AssignedSingularName = "I12One2One";
			I12I12One2One.RoleType.AssignedPluralName = "I12One2Ones";
			this.Roles.I12I12One2One = I12I12One2One.RoleType;

			var I12I2One2One = new RelationType(TestsDomain.Instance, new Guid("ac920d1d-290b-484b-9283-3829337182bc"), new Guid("991e5b73-a9b0-40a4-8230-b3fb7cc46761"), new Guid("07702752-2c97-4b44-8c43-7c1f2a5e3d0d"));
			I12I2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I12I2One2One.IsIndexed = true;

			I12I2One2One.AssociationType.ObjectType = this;

			I12I2One2One.RoleType.ObjectType = I2Interface.Instance;;
			I12I2One2One.RoleType.AssignedSingularName = "I2One2One";
			I12I2One2One.RoleType.AssignedPluralName = "I2One2Ones";
			this.Roles.I12I2One2One = I12I2One2One.RoleType;

			var DerivableDependency = new RelationType(TestsDomain.Instance, new Guid("b2e3ddda-0cc3-4cfd-a114-9040882ec58a"), new Guid("014cf60e-595f-42d5-9146-e7d860396f4d"), new Guid("d5c22b99-6984-4042-98fd-93fe60dfe5d7"));
			DerivableDependency.AssignedMultiplicity = Multiplicity.ManyToMany;
			DerivableDependency.IsIndexed = true;

			DerivableDependency.AssociationType.ObjectType = this;

			DerivableDependency.RoleType.ObjectType = I12Interface.Instance;;
			DerivableDependency.RoleType.AssignedSingularName = "Dependency";
			DerivableDependency.RoleType.AssignedPluralName = "Dependencies";
			this.Roles.Dependency = DerivableDependency.RoleType;

			var I12I2One2Many = new RelationType(TestsDomain.Instance, new Guid("b2f568a1-51ba-4b6b-a1f1-b82bdec382b5"), new Guid("6f37656a-21d0-4574-8eac-5342f7c6850d"), new Guid("09a2a7a1-4713-4c5c-828d-8be40f33d1ae"));
			I12I2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I12I2One2Many.IsIndexed = true;

			I12I2One2Many.AssociationType.ObjectType = this;

			I12I2One2Many.RoleType.ObjectType = I2Interface.Instance;;
			I12I2One2Many.RoleType.AssignedSingularName = "I2One2Many";
			I12I2One2Many.RoleType.AssignedPluralName = "I2One2Manies";
			this.Roles.I12I2One2Many = I12I2One2Many.RoleType;

			var I12C2Many2One = new RelationType(TestsDomain.Instance, new Guid("c018face-b292-455c-a2c0-8f71377fb6cb"), new Guid("3239eb17-dc55-465f-854c-1d2d024bca94"), new Guid("2ff52878-3ade-4afe-9961-8f79336bb0a2"));
			I12C2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I12C2Many2One.IsIndexed = true;

			I12C2Many2One.AssociationType.ObjectType = this;

			I12C2Many2One.RoleType.ObjectType = C2Class.Instance;;
			I12C2Many2One.RoleType.AssignedSingularName = "C2Many2One";
			I12C2Many2One.RoleType.AssignedPluralName = "C2Many2Ones";
			this.Roles.I12C2Many2One = I12C2Many2One.RoleType;

			var I12I12Many2One = new RelationType(TestsDomain.Instance, new Guid("c6ecc142-0fbd-48b7-98ae-994fa9b5b814"), new Guid("c7469ffd-ffd7-4913-962c-8a7a0b4df3dd"), new Guid("1d091625-ec4a-486d-a9be-8f87fe300967"));
			I12I12Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I12I12Many2One.IsIndexed = true;

			I12I12Many2One.AssociationType.ObjectType = this;

			I12I12Many2One.RoleType.ObjectType = I12Interface.Instance;;
			I12I12Many2One.RoleType.AssignedSingularName = "I12Many2One";
			I12I12Many2One.RoleType.AssignedPluralName = "I12Many2Ones";
			this.Roles.I12I12Many2One = I12I12Many2One.RoleType;

			var I12AllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("ccdd1ae2-263e-4221-9841-4cff1907ee8d"), new Guid("55be99e6-71fd-4483-b211-c3080e6ffa05"), new Guid("79723949-b9ad-40bf-baee-96d001942855"));
			I12AllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsBoolean.AssociationType.ObjectType = this;

			I12AllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.I12AllorsBoolean = I12AllorsBoolean.RoleType;

			var I12I1One2One = new RelationType(TestsDomain.Instance, new Guid("ce0f7d58-b415-43f3-989b-9d8b34754e4b"), new Guid("33bd508e-d754-4533-9ecd-9c8ce8d10c88"), new Guid("72545574-d138-467c-8f21-0c5d15b1d793"));
			I12I1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I12I1One2One.IsIndexed = true;

			I12I1One2One.AssociationType.ObjectType = this;

			I12I1One2One.RoleType.ObjectType = I1Interface.Instance;;
			I12I1One2One.RoleType.AssignedSingularName = "I1One2One";
			I12I1One2One.RoleType.AssignedPluralName = "I1One2Ones";
			this.Roles.I12I1One2One = I12I1One2One.RoleType;

			var I12C1One2Many = new RelationType(TestsDomain.Instance, new Guid("f302dd07-1abc-409e-aa71-ec9f7ac439aa"), new Guid("99b3bf26-3437-4b5b-a786-28c095975a48"), new Guid("ee291df6-6a3e-4d92-a779-879679e1b688"));
			I12C1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I12C1One2Many.IsIndexed = true;

			I12C1One2Many.AssociationType.ObjectType = this;

			I12C1One2Many.RoleType.ObjectType = C1Class.Instance;;
			I12C1One2Many.RoleType.AssignedSingularName = "C1One2Many";
			I12C1One2Many.RoleType.AssignedPluralName = "C1One2Manies";
			this.Roles.I12C1One2Many = I12C1One2Many.RoleType;

			var I12C1Many2One = new RelationType(TestsDomain.Instance, new Guid("f6436bc9-e307-4001-8f1f-5b37553ab3c6"), new Guid("63297178-60c1-4cbc-a68d-2842385ba266"), new Guid("6e5b98b0-1af3-4e99-8781-37ea97792a24"));
			I12C1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I12C1Many2One.IsIndexed = true;

			I12C1Many2One.AssociationType.ObjectType = this;

			I12C1Many2One.RoleType.ObjectType = C1Class.Instance;;
			I12C1Many2One.RoleType.AssignedSingularName = "C1Many2One";
			I12C1Many2One.RoleType.AssignedPluralName = "C1Many2Ones";
			this.Roles.I12C1Many2One = I12C1Many2One.RoleType;

			var I12AllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("fa6656dc-3a7a-4701-bc6b-3cd06aaa4483"), new Guid("6e4d05f3-52e3-4937-b8d2-8d9d52e7c8bf"), new Guid("823e8329-0a90-49ed-9b2c-4bfb9db2ee02"));
			I12AllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			I12AllorsDateTime.AssociationType.ObjectType = this;

			I12AllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.I12AllorsDateTime = I12AllorsDateTime.RoleType;


		}

		internal override void SetRelationTypes()
		{
			C1Class.Instance.ConcreteRoles.I12AllorsBinary = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsBinary]; 
			C2Class.Instance.ConcreteRoles.I12AllorsBinary = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsBinary]; 

			C1Class.Instance.ConcreteRoles.I12C2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2One2One]; 
			C2Class.Instance.ConcreteRoles.I12C2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2One2One]; 

			C1Class.Instance.ConcreteRoles.I12AllorsDouble = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDouble]; 
			C2Class.Instance.ConcreteRoles.I12AllorsDouble = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDouble]; 

			C1Class.Instance.ConcreteRoles.I12I1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1Many2One]; 
			C2Class.Instance.ConcreteRoles.I12I1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1Many2One]; 

			C1Class.Instance.ConcreteRoles.I12AllorsString = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsString]; 
			C2Class.Instance.ConcreteRoles.I12AllorsString = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsString]; 

			C1Class.Instance.ConcreteRoles.I12I12Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12Many2Many]; 
			C2Class.Instance.ConcreteRoles.I12I12Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12Many2Many]; 

			C1Class.Instance.ConcreteRoles.I12AllorsDecimal = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDecimal]; 
			C2Class.Instance.ConcreteRoles.I12AllorsDecimal = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDecimal]; 

			C1Class.Instance.ConcreteRoles.I12I2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2Many2Many]; 
			C2Class.Instance.ConcreteRoles.I12I2Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2Many2Many]; 

			C1Class.Instance.ConcreteRoles.I12C2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2Many2Many]; 
			C2Class.Instance.ConcreteRoles.I12C2Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2Many2Many]; 

			C1Class.Instance.ConcreteRoles.I12I1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1Many2Many]; 
			C2Class.Instance.ConcreteRoles.I12I1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1Many2Many]; 

			C1Class.Instance.ConcreteRoles.I12I12One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12One2Many]; 
			C2Class.Instance.ConcreteRoles.I12I12One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12One2Many]; 

			C1Class.Instance.ConcreteRoles.Name = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 
			C2Class.Instance.ConcreteRoles.Name = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			C1Class.Instance.ConcreteRoles.I12C1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1Many2Many]; 
			C2Class.Instance.ConcreteRoles.I12C1Many2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1Many2Many]; 

			C1Class.Instance.ConcreteRoles.I12I2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2Many2One]; 
			C2Class.Instance.ConcreteRoles.I12I2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2Many2One]; 

			C1Class.Instance.ConcreteRoles.I12AllorsUnique = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsUnique]; 
			C2Class.Instance.ConcreteRoles.I12AllorsUnique = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsUnique]; 

			C1Class.Instance.ConcreteRoles.I12AllorsInteger = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsInteger]; 
			C2Class.Instance.ConcreteRoles.I12AllorsInteger = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsInteger]; 

			C1Class.Instance.ConcreteRoles.I12I1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1One2Many]; 
			C2Class.Instance.ConcreteRoles.I12I1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1One2Many]; 

			C1Class.Instance.ConcreteRoles.I12C1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1One2One]; 
			C2Class.Instance.ConcreteRoles.I12C1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1One2One]; 

			C1Class.Instance.ConcreteRoles.I12I12One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12One2One]; 
			C2Class.Instance.ConcreteRoles.I12I12One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12One2One]; 

			C1Class.Instance.ConcreteRoles.I12I2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2One2One]; 
			C2Class.Instance.ConcreteRoles.I12I2One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2One2One]; 

			C1Class.Instance.ConcreteRoles.Dependency = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.Dependency]; 
			C2Class.Instance.ConcreteRoles.Dependency = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.Dependency]; 

			C1Class.Instance.ConcreteRoles.I12I2One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2One2Many]; 
			C2Class.Instance.ConcreteRoles.I12I2One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I2One2Many]; 

			C1Class.Instance.ConcreteRoles.I12C2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2Many2One]; 
			C2Class.Instance.ConcreteRoles.I12C2Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C2Many2One]; 

			C1Class.Instance.ConcreteRoles.I12I12Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12Many2One]; 
			C2Class.Instance.ConcreteRoles.I12I12Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I12Many2One]; 

			C1Class.Instance.ConcreteRoles.I12AllorsBoolean = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsBoolean]; 
			C2Class.Instance.ConcreteRoles.I12AllorsBoolean = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsBoolean]; 

			C1Class.Instance.ConcreteRoles.I12I1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1One2One]; 
			C2Class.Instance.ConcreteRoles.I12I1One2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12I1One2One]; 

			C1Class.Instance.ConcreteRoles.I12C1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1One2Many]; 
			C2Class.Instance.ConcreteRoles.I12C1One2Many = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1One2Many]; 

			C1Class.Instance.ConcreteRoles.I12C1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1Many2One]; 
			C2Class.Instance.ConcreteRoles.I12C1Many2One = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12C1Many2One]; 

			C1Class.Instance.ConcreteRoles.I12AllorsDateTime = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDateTime]; 
			C2Class.Instance.ConcreteRoles.I12AllorsDateTime = C2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I12AllorsDateTime]; 


		}

		internal class RolesType
		{
			internal RoleType I12AllorsBinary;
			internal RoleType I12C2One2One;
			internal RoleType I12AllorsDouble;
			internal RoleType I12I1Many2One;
			internal RoleType I12AllorsString;
			internal RoleType I12I12Many2Many;
			internal RoleType I12AllorsDecimal;
			internal RoleType I12I2Many2Many;
			internal RoleType I12C2Many2Many;
			internal RoleType I12I1Many2Many;
			internal RoleType I12I12One2Many;
			internal RoleType Name;
			internal RoleType I12C1Many2Many;
			internal RoleType I12I2Many2One;
			internal RoleType I12AllorsUnique;
			internal RoleType I12AllorsInteger;
			internal RoleType I12I1One2Many;
			internal RoleType I12C1One2One;
			internal RoleType I12I12One2One;
			internal RoleType I12I2One2One;
			internal RoleType Dependency;
			internal RoleType I12I2One2Many;
			internal RoleType I12C2Many2One;
			internal RoleType I12I12Many2One;
			internal RoleType I12AllorsBoolean;
			internal RoleType I12I1One2One;
			internal RoleType I12C1One2Many;
			internal RoleType I12C1Many2One;
			internal RoleType I12AllorsDateTime;

		}
	}public partial class BadUIClass : Class
	{
		public static BadUIClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal BadUIClass() : base(TestsDomain.Instance, new Guid("bb1b0a2e-66d1-4e09-860f-52dc7145029e"))
        {
			this.SingularName = "BadUI";
			this.PluralName = "BadUIs";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var BadUIPersonMany = new RelationType(TestsDomain.Instance, new Guid("8a999086-ca90-40a1-90ae-475d231bb1eb"), new Guid("0ce20e7c-7be0-4c07-a179-e8d0e77f3de1"), new Guid("4ab20876-f8fc-4d39-87d7-8758f044587b"));
			BadUIPersonMany.AssignedMultiplicity = Multiplicity.OneToMany;
			BadUIPersonMany.IsIndexed = true;

			BadUIPersonMany.AssociationType.ObjectType = this;

			BadUIPersonMany.RoleType.ObjectType = PersonClass.Instance;;
			BadUIPersonMany.RoleType.AssignedSingularName = "PersonMany";
			BadUIPersonMany.RoleType.AssignedPluralName = "PersonsMany";
			this.Roles.PersonMany = BadUIPersonMany.RoleType;

			var BadUICompanyOne = new RelationType(TestsDomain.Instance, new Guid("9ebbb9d1-2ca7-4a7f-9f18-f25c05fd28c1"), new Guid("37c64e26-a391-4c7b-98fb-53ccb5fbc795"), new Guid("4d2c7c20-b9c7-451b-b6b1-8552322ceddd"));
			BadUICompanyOne.AssignedMultiplicity = Multiplicity.ManyToOne;
			BadUICompanyOne.IsIndexed = true;

			BadUICompanyOne.AssociationType.ObjectType = this;

			BadUICompanyOne.RoleType.ObjectType = OrganisationClass.Instance;;
			BadUICompanyOne.RoleType.AssignedSingularName = "CompanyOne";
			BadUICompanyOne.RoleType.AssignedPluralName = "CompanyOnes";
			this.Roles.CompanyOne = BadUICompanyOne.RoleType;

			var BadUIPersonOne = new RelationType(TestsDomain.Instance, new Guid("a4db0d75-3dff-45ac-9c1d-623bca046b4a"), new Guid("5ed577d8-f048-42b8-9fb4-38b88ebf35f1"), new Guid("c1b45f09-59fe-4484-8999-e2a3d9147919"));
			BadUIPersonOne.AssignedMultiplicity = Multiplicity.ManyToOne;
			BadUIPersonOne.IsIndexed = true;

			BadUIPersonOne.AssociationType.ObjectType = this;

			BadUIPersonOne.RoleType.ObjectType = PersonClass.Instance;;
			BadUIPersonOne.RoleType.AssignedSingularName = "PersonOne";
			BadUIPersonOne.RoleType.AssignedPluralName = "PersonOnes";
			this.Roles.PersonOne = BadUIPersonOne.RoleType;

			var BadUICompanyMany = new RelationType(TestsDomain.Instance, new Guid("a8621048-48b5-43c4-b10b-17225958d177"), new Guid("718eaf0b-1b62-43b2-b336-c9820d806b28"), new Guid("1663525b-5add-4a96-a596-5d736d466985"));
			BadUICompanyMany.AssignedMultiplicity = Multiplicity.ManyToOne;
			BadUICompanyMany.IsIndexed = true;

			BadUICompanyMany.AssociationType.ObjectType = this;

			BadUICompanyMany.RoleType.ObjectType = OrganisationClass.Instance;;
			BadUICompanyMany.RoleType.AssignedSingularName = "CompanyMany";
			BadUICompanyMany.RoleType.AssignedPluralName = "CompanyManies";
			this.Roles.CompanyMany = BadUICompanyMany.RoleType;

			var BadUIAllorsString = new RelationType(TestsDomain.Instance, new Guid("c93a102e-ecdb-4189-a0fc-eeea8b4b85d4"), new Guid("2225f3e0-1304-4a55-9b89-29563fe52e3c"), new Guid("7f2dc0db-4628-45a8-8cc5-2cc1b87e0eb3"));
			BadUIAllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			BadUIAllorsString.AssociationType.ObjectType = this;

			BadUIAllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			BadUIAllorsString.RoleType.Size = 256;
			this.Roles.AllorsString = BadUIAllorsString.RoleType;


		}

		internal override void SetRelationTypes()
		{
			BadUIClass.Instance.ConcreteRoles.PersonMany = BadUIClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PersonMany]; 

			BadUIClass.Instance.ConcreteRoles.CompanyOne = BadUIClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.CompanyOne]; 

			BadUIClass.Instance.ConcreteRoles.PersonOne = BadUIClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PersonOne]; 

			BadUIClass.Instance.ConcreteRoles.CompanyMany = BadUIClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.CompanyMany]; 

			BadUIClass.Instance.ConcreteRoles.AllorsString = BadUIClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsString]; 


		}

		internal class RolesType
		{
			internal RoleType PersonMany;
			internal RoleType CompanyOne;
			internal RoleType PersonOne;
			internal RoleType CompanyMany;
			internal RoleType AllorsString;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType PersonMany;
			internal ConcreteRoleType CompanyOne;
			internal ConcreteRoleType PersonOne;
			internal ConcreteRoleType CompanyMany;
			internal ConcreteRoleType AllorsString;

		}
	}public partial class ThreeClass : Class
	{
		public static ThreeClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ThreeClass() : base(TestsDomain.Instance, new Guid("bdaed62e-6369-46c0-a379-a1eef81b1c3d"))
        {
			this.SingularName = "Three";
			this.PluralName = "Threes";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("0e2ff702-d2fe-4298-b97d-ab9d7bec94b3"))
			{
				Subtype = Instance,
				Supertype = SharedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var ThreeFour = new RelationType(TestsDomain.Instance, new Guid("1697f09c-0d3d-4e5e-9f3f-9d3ae0718fd3"), new Guid("dc813d9a-84e9-4995-8d2c-0ef449b12024"), new Guid("25737278-d039-47c5-8749-19f22ad7a4c3"));
			ThreeFour.AssignedMultiplicity = Multiplicity.ManyToOne;
			ThreeFour.IsIndexed = true;

			ThreeFour.AssociationType.ObjectType = this;

			ThreeFour.RoleType.ObjectType = FourClass.Instance;;
			this.Roles.Four = ThreeFour.RoleType;

			var ThreeAllorsString = new RelationType(TestsDomain.Instance, new Guid("4ace9948-4a22-465c-aa40-61c8fd65784d"), new Guid("6e20b25f-3ecd-447e-8a93-3977a53452b6"), new Guid("f8f85b3d-371c-42df-8414-cf034c339917"));
			ThreeAllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			ThreeAllorsString.AssociationType.ObjectType = this;

			ThreeAllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			ThreeAllorsString.RoleType.Size = -1;
			this.Roles.AllorsString = ThreeAllorsString.RoleType;


		}

		internal override void SetRelationTypes()
		{
			ThreeClass.Instance.ConcreteRoles.Four = ThreeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Four]; 

			ThreeClass.Instance.ConcreteRoles.AllorsString = ThreeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AllorsString]; 


		}

		internal class RolesType
		{
			internal RoleType Four;
			internal RoleType AllorsString;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Four;
			internal ConcreteRoleType AllorsString;

		}
	}public partial class SecondClass : Class
	{
		public static SecondClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal SecondClass() : base(TestsDomain.Instance, new Guid("c1f169a1-553b-4a24-aba7-01e0b7102fe5"))
        {
			this.SingularName = "Second";
			this.PluralName = "Seconds";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var SecondThird = new RelationType(TestsDomain.Instance, new Guid("4f0eba0d-09b4-4bbc-8e42-15de94921ab5"), new Guid("08d8689d-88ce-496d-95e4-f20af0677cac"), new Guid("ec263924-1234-4b53-9d33-91e167d6862f"));
			SecondThird.AssignedMultiplicity = Multiplicity.OneToOne;
			SecondThird.IsIndexed = true;

			SecondThird.AssociationType.ObjectType = this;

			SecondThird.RoleType.ObjectType = ThirdClass.Instance;;
			this.Roles.Third = SecondThird.RoleType;

			var SecondIsDerived = new RelationType(TestsDomain.Instance, new Guid("8a7b7af9-f421-4e96-a1a7-04d4c4bdd1d7"), new Guid("e986349f-fc8c-4627-9bf7-966ad6299cff"), new Guid("3f37f82c-3f7a-4d4c-b775-4ff09c105f92"));
			SecondIsDerived.AssignedMultiplicity = Multiplicity.OneToOne;
			SecondIsDerived.AssociationType.ObjectType = this;

			SecondIsDerived.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			SecondIsDerived.RoleType.AssignedSingularName = "IsDerived";
			SecondIsDerived.RoleType.AssignedPluralName = "IsDeriveds";
			this.Roles.IsDerived = SecondIsDerived.RoleType;


		}

		internal override void SetRelationTypes()
		{
			SecondClass.Instance.ConcreteRoles.Third = SecondClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Third]; 

			SecondClass.Instance.ConcreteRoles.IsDerived = SecondClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsDerived]; 


		}

		internal class RolesType
		{
			internal RoleType Third;
			internal RoleType IsDerived;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Third;
			internal ConcreteRoleType IsDerived;

		}
	}public partial class DerivationLogC2Class : Class
	{
		public static DerivationLogC2Class Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal DerivationLogC2Class() : base(TestsDomain.Instance, new Guid("c7563dd3-77b2-43ff-92f9-a4f98db36acf"))
        {
			this.SingularName = "DerivationLogC2";
			this.PluralName = "DerivationLogC2s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("f3e0444d-8b32-4396-80c4-b07905563c17"))
			{
				Subtype = Instance,
				Supertype = DerivationLogI12Interface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType UniqueId;

		}
	}public partial class DerivationLogI12Interface: Interface
	{
		public static DerivationLogI12Interface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal DerivationLogI12Interface() : base(TestsDomain.Instance, new Guid("d61872ee-3778-47e8-8931-003f3f48cbc5"))
        {
			this.SingularName = "DerivationLogI12";
			this.PluralName = "DerivationLogI12s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var DerivationLogI12UniqueId = new RelationType(TestsDomain.Instance, new Guid("0b89b096-a69a-495c-acfe-b24a9b27e320"), new Guid("e178ed0f-7764-4836-bd6f-fcb7f5d62346"), new Guid("007a6d25-8506-483d-9140-414c0056d812"));
			DerivationLogI12UniqueId.AssignedMultiplicity = Multiplicity.OneToOne;
			DerivationLogI12UniqueId.AssociationType.ObjectType = this;

			DerivationLogI12UniqueId.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			DerivationLogI12UniqueId.RoleType.AssignedSingularName = "UniqueId";
			DerivationLogI12UniqueId.RoleType.AssignedPluralName = "UniqueIds";
			this.Roles.UniqueId = DerivationLogI12UniqueId.RoleType;


		}

		internal override void SetRelationTypes()
		{
			DerivationLogC1Class.Instance.ConcreteRoles.UniqueId = DerivationLogC1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			DerivationLogC2Class.Instance.ConcreteRoles.UniqueId = DerivationLogC2Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 


		}

		internal class RolesType
		{
			internal RoleType UniqueId;

		}
	}public partial class ClassWithoutRolesClass : Class
	{
		public static ClassWithoutRolesClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ClassWithoutRolesClass() : base(TestsDomain.Instance, new Guid("e1008840-6d7c-4d44-b2ad-1545d23f90d8"))
        {
			this.SingularName = "ClassWithoutRoles";
			this.PluralName = "ClassesWithoutRoles";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
		}
	}public partial class I1Interface: Interface
	{
		public static I1Interface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal I1Interface() : base(TestsDomain.Instance, new Guid("fefcf1b6-ac8f-47b0-bed5-939207a2833e"))
        {
			this.SingularName = "I1";
			this.PluralName = "I1s";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(TestsDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(TestsDomain.Instance, new Guid("3c0b0ee1-3777-4ba2-8477-00d94e3f67a3"))
			{
				Subtype = Instance,
				Supertype = I12Interface.Instance
			};
			new Inheritance(TestsDomain.Instance, new Guid("638974c2-868d-49ce-a261-e1b9808d761b"))
			{
				Subtype = Instance,
				Supertype = S1Interface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var I1I1Many2One = new RelationType(TestsDomain.Instance, new Guid("06b72534-49a8-4f6d-a991-bc4aaf6f939f"), new Guid("854c6ec4-51d4-4d68-bd26-4168c26707de"), new Guid("9fd09ce4-3f52-4889-b018-fd9374656e8c"));
			I1I1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I1I1Many2One.IsIndexed = true;

			I1I1Many2One.AssociationType.ObjectType = this;

			I1I1Many2One.RoleType.ObjectType = I1Interface.Instance;;
			I1I1Many2One.RoleType.AssignedSingularName = "I1Many2One";
			I1I1Many2One.RoleType.AssignedPluralName = "I1Many2Ones";
			this.Roles.I1I1Many2One = I1I1Many2One.RoleType;

			var I1I12Many2Many = new RelationType(TestsDomain.Instance, new Guid("0a2895ec-0102-493d-9b94-e12e94b4a403"), new Guid("295bfc0e-e123-4ac8-84da-45e8d77b1865"), new Guid("94c8ca3f-45d6-4f70-8b4a-5d469b0ee897"));
			I1I12Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I1I12Many2Many.IsIndexed = true;

			I1I12Many2Many.AssociationType.ObjectType = this;

			I1I12Many2Many.RoleType.ObjectType = I12Interface.Instance;;
			I1I12Many2Many.RoleType.AssignedSingularName = "I12Many2Many";
			I1I12Many2Many.RoleType.AssignedPluralName = "I12Many2Manies";
			this.Roles.I1I12Many2Many = I1I12Many2Many.RoleType;

			var I1I2Many2Many = new RelationType(TestsDomain.Instance, new Guid("0acbea28-f8aa-477c-b296-b8976d9b10a5"), new Guid("5b4da68a-6aeb-4d5c-8e09-5bef3b1358a9"), new Guid("5e8608ed-7987-40d0-a877-a244d6520554"));
			I1I2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I1I2Many2Many.IsIndexed = true;

			I1I2Many2Many.AssociationType.ObjectType = this;

			I1I2Many2Many.RoleType.ObjectType = I2Interface.Instance;;
			I1I2Many2Many.RoleType.AssignedSingularName = "I2Many2Many";
			I1I2Many2Many.RoleType.AssignedPluralName = "I2Many2Manies";
			this.Roles.I1I2Many2Many = I1I2Many2Many.RoleType;

			var I1I2Many2One = new RelationType(TestsDomain.Instance, new Guid("194580f4-e0e3-4b52-b9ba-6020171be4e9"), new Guid("39a81eb4-e1bb-45ef-8126-21cf233ba684"), new Guid("98017570-bc3b-442b-9e51-b16565fa443c"));
			I1I2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I1I2Many2One.IsIndexed = true;

			I1I2Many2One.AssociationType.ObjectType = this;

			I1I2Many2One.RoleType.ObjectType = I2Interface.Instance;;
			I1I2Many2One.RoleType.AssignedSingularName = "I2Many2One";
			I1I2Many2One.RoleType.AssignedPluralName = "I2Many2Ones";
			this.Roles.I1I2Many2One = I1I2Many2One.RoleType;

			var I1AllorsString = new RelationType(TestsDomain.Instance, new Guid("28ceffc2-c776-4a0a-9825-a6d1bcb265dc"), new Guid("0287a603-59e5-4241-8b2e-a21698476e67"), new Guid("fec573a7-5ab3-4f30-9b50-7d720b4af4b4"));
			I1AllorsString.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsString.AssociationType.ObjectType = this;

			I1AllorsString.RoleType.ObjectType = AllorsStringUnit.Instance;;
			I1AllorsString.RoleType.Size = 256;
			this.Roles.I1AllorsString = I1AllorsString.RoleType;

			var I1I12Many2One = new RelationType(TestsDomain.Instance, new Guid("2e85d74a-8d13-4bc0-ae4f-42b305e79373"), new Guid("d6ccfcb8-623e-4852-a878-d7cb377af853"), new Guid("ec030f88-1060-4c2b-bda1-d9c5dc4fc9d3"));
			I1I12Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I1I12Many2One.IsIndexed = true;

			I1I12Many2One.AssociationType.ObjectType = this;

			I1I12Many2One.RoleType.ObjectType = I12Interface.Instance;;
			I1I12Many2One.RoleType.AssignedSingularName = "I12Many2One";
			I1I12Many2One.RoleType.AssignedPluralName = "I12Many2Ones";
			this.Roles.I1I12Many2One = I1I12Many2One.RoleType;

			var I1AllorsDateTime = new RelationType(TestsDomain.Instance, new Guid("32fc21cc-4be7-4a0e-ac71-df135be95e68"), new Guid("e0006bdc-74e2-4067-871c-6f0b53eba5de"), new Guid("12824c37-d0d2-4cb9-9481-cad7f5f54976"));
			I1AllorsDateTime.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsDateTime.AssociationType.ObjectType = this;

			I1AllorsDateTime.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			this.Roles.I1AllorsDateTime = I1AllorsDateTime.RoleType;

			var I1I2One2Many = new RelationType(TestsDomain.Instance, new Guid("39e28141-fd6b-4f49-8884-d5400f6c57ff"), new Guid("9118c09c-e8c2-4685-a464-9be9ece2e746"), new Guid("a4b456e2-b45f-4398-875b-4ba99ead49fe"));
			I1I2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I1I2One2Many.IsIndexed = true;

			I1I2One2Many.AssociationType.ObjectType = this;

			I1I2One2Many.RoleType.ObjectType = I2Interface.Instance;;
			I1I2One2Many.RoleType.AssignedSingularName = "I2One2Many";
			I1I2One2Many.RoleType.AssignedPluralName = "I2One2Manies";
			this.Roles.I1I2One2Many = I1I2One2Many.RoleType;

			var I1C2One2Many = new RelationType(TestsDomain.Instance, new Guid("4506a14b-22f1-41fe-972b-40fab7c6dd31"), new Guid("54c659d3-98ff-45e6-b734-bc45f13428d8"), new Guid("d75a5613-4ed9-494f-accf-352d9e115ba9"));
			I1C2One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I1C2One2Many.IsIndexed = true;

			I1C2One2Many.AssociationType.ObjectType = this;

			I1C2One2Many.RoleType.ObjectType = C2Class.Instance;;
			I1C2One2Many.RoleType.AssignedSingularName = "C2One2Many";
			I1C2One2Many.RoleType.AssignedPluralName = "C2One2Manies";
			this.Roles.I1C2One2Many = I1C2One2Many.RoleType;

			var I1C1One2One = new RelationType(TestsDomain.Instance, new Guid("593914b1-af95-4992-9703-2b60f4ea0926"), new Guid("ee0f3844-928b-4968-9077-afd255554c8b"), new Guid("bca02f1e-a026-4c0b-9762-1bd52d49b953"));
			I1C1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I1C1One2One.IsIndexed = true;

			I1C1One2One.AssociationType.ObjectType = this;

			I1C1One2One.RoleType.ObjectType = C1Class.Instance;;
			I1C1One2One.RoleType.AssignedSingularName = "C1One2One";
			I1C1One2One.RoleType.AssignedPluralName = "C1One2Ones";
			this.Roles.I1C1One2One = I1C1One2One.RoleType;

			var I1AllorsInteger = new RelationType(TestsDomain.Instance, new Guid("5cb44331-fd8c-4f73-8994-161f702849b6"), new Guid("2484aae6-db3b-4795-be76-016b33cbb679"), new Guid("c9f9dd15-54b4-4847-8b7e-ac88063804a3"));
			I1AllorsInteger.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsInteger.AssociationType.ObjectType = this;

			I1AllorsInteger.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			this.Roles.I1AllorsInteger = I1AllorsInteger.RoleType;

			var I1C2Many2Many = new RelationType(TestsDomain.Instance, new Guid("6199e5b4-133d-4d0e-9941-207316164ec8"), new Guid("75342efb-659c-43a9-8340-1e110087141c"), new Guid("920f26a7-971a-4771-81b1-af3972c997ff"));
			I1C2Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I1C2Many2Many.IsIndexed = true;

			I1C2Many2Many.AssociationType.ObjectType = this;

			I1C2Many2Many.RoleType.ObjectType = C2Class.Instance;;
			I1C2Many2Many.RoleType.AssignedSingularName = "C2Many2Many";
			I1C2Many2Many.RoleType.AssignedPluralName = "C2Many2Manies";
			this.Roles.I1C2Many2Many = I1C2Many2Many.RoleType;

			var I1I1One2Many = new RelationType(TestsDomain.Instance, new Guid("670c753e-8ea0-40b1-bfc9-7388074191d3"), new Guid("b1c6c329-09e3-4b07-8ddf-e6a4fd8d0285"), new Guid("6d36c9f9-1426-46a5-8d4f-7275a51c9c17"));
			I1I1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I1I1One2Many.IsIndexed = true;

			I1I1One2Many.AssociationType.ObjectType = this;

			I1I1One2Many.RoleType.ObjectType = I1Interface.Instance;;
			I1I1One2Many.RoleType.AssignedSingularName = "I1One2Many";
			I1I1One2Many.RoleType.AssignedPluralName = "I1One2Manies";
			this.Roles.I1I1One2Many = I1I1One2Many.RoleType;

			var I1I1Many2Many = new RelationType(TestsDomain.Instance, new Guid("6bb3ba6d-ffc7-4700-9723-c323b9b2d233"), new Guid("86623fe9-c7cc-4328-85d9-b0dfce2b9a59"), new Guid("9c64a761-136a-43aa-bef9-6bcd1259d591"));
			I1I1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I1I1Many2Many.IsIndexed = true;

			I1I1Many2Many.AssociationType.ObjectType = this;

			I1I1Many2Many.RoleType.ObjectType = I1Interface.Instance;;
			I1I1Many2Many.RoleType.AssignedSingularName = "I1Many2Many";
			I1I1Many2Many.RoleType.AssignedPluralName = "I1Many2Manies";
			this.Roles.I1I1Many2Many = I1I1Many2Many.RoleType;

			var I1AllorsBoolean = new RelationType(TestsDomain.Instance, new Guid("6c3d04be-6f95-44b8-863a-245e150e3110"), new Guid("e6c314af-d366-4169-b28d-9dc83d694079"), new Guid("631a2bdb-ceca-43b2-abb8-9c9ea743c9de"));
			I1AllorsBoolean.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsBoolean.AssociationType.ObjectType = this;

			I1AllorsBoolean.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			this.Roles.I1AllorsBoolean = I1AllorsBoolean.RoleType;

			var I1AllorsDecimal = new RelationType(TestsDomain.Instance, new Guid("818b4013-5ef1-4455-9f0d-9a39fa3425bb"), new Guid("335902bc-6bfa-4c7b-b52f-9a617c746afd"), new Guid("56e68d93-a62f-4090-a93a-8f0f364b08bd"));
			I1AllorsDecimal.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsDecimal.AssociationType.ObjectType = this;

			I1AllorsDecimal.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			I1AllorsDecimal.RoleType.Scale = 2;
			I1AllorsDecimal.RoleType.Precision = 10;
			this.Roles.I1AllorsDecimal = I1AllorsDecimal.RoleType;

			var I1I12One2One = new RelationType(TestsDomain.Instance, new Guid("a51d9d21-40ec-44b9-853d-8c18f54d659d"), new Guid("1d785350-3f68-4f8d-86d4-74a0cd8adac7"), new Guid("222d2644-197d-4420-a01a-276b35ad61d1"));
			I1I12One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I1I12One2One.IsIndexed = true;

			I1I12One2One.AssociationType.ObjectType = this;

			I1I12One2One.RoleType.ObjectType = I12Interface.Instance;;
			I1I12One2One.RoleType.AssignedSingularName = "I12One2One";
			I1I12One2One.RoleType.AssignedPluralName = "I12One2Ones";
			this.Roles.I1I12One2One = I1I12One2One.RoleType;

			var I1I2One2One = new RelationType(TestsDomain.Instance, new Guid("a5761a0e-5c10-407a-bd68-0c4f69d78968"), new Guid("b6cf882a-e27a-40e3-9a0d-43ade4d236b6"), new Guid("3950129b-6ac5-4eae-b5c2-de12500b0561"));
			I1I2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I1I2One2One.IsIndexed = true;

			I1I2One2One.AssociationType.ObjectType = this;

			I1I2One2One.RoleType.ObjectType = I2Interface.Instance;;
			I1I2One2One.RoleType.AssignedSingularName = "I2One2One";
			I1I2One2One.RoleType.AssignedPluralName = "I2One2Ones";
			this.Roles.I1I2One2One = I1I2One2One.RoleType;

			var I1C2One2One = new RelationType(TestsDomain.Instance, new Guid("b6e0fce0-14fc-46e3-995d-1b6e3699ed96"), new Guid("ddc18ebf-0b61-441f-854a-0f964859035e"), new Guid("3899bad1-d563-4f65-85b1-2b274b6a278f"));
			I1C2One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I1C2One2One.IsIndexed = true;

			I1C2One2One.AssociationType.ObjectType = this;

			I1C2One2One.RoleType.ObjectType = C2Class.Instance;;
			I1C2One2One.RoleType.AssignedSingularName = "C2One2One";
			I1C2One2One.RoleType.AssignedPluralName = "C2One2Ones";
			this.Roles.I1C2One2One = I1C2One2One.RoleType;

			var I1C1One2Many = new RelationType(TestsDomain.Instance, new Guid("b89092f1-8775-4b6a-99ef-f8626bc770bd"), new Guid("d0b99a68-2104-4c4d-ba4c-73d725e406e9"), new Guid("6303d423-6cc4-4933-9546-4b6b39fa0ae4"));
			I1C1One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I1C1One2Many.IsIndexed = true;

			I1C1One2Many.AssociationType.ObjectType = this;

			I1C1One2Many.RoleType.ObjectType = C1Class.Instance;;
			I1C1One2Many.RoleType.AssignedSingularName = "C1One2Many";
			I1C1One2Many.RoleType.AssignedPluralName = "C1One2Manies";
			this.Roles.I1C1One2Many = I1C1One2Many.RoleType;

			var I1AllorsBinary = new RelationType(TestsDomain.Instance, new Guid("b9c67658-4abc-41f3-9434-c8512a482179"), new Guid("ba4fa583-b169-4327-a60a-fc0d2c142b3f"), new Guid("bbd469af-25f5-47aa-86f6-80d3bba53ce5"));
			I1AllorsBinary.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsBinary.AssociationType.ObjectType = this;

			I1AllorsBinary.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			I1AllorsBinary.RoleType.Size = -1;
			this.Roles.I1AllorsBinary = I1AllorsBinary.RoleType;

			var I1C1Many2Many = new RelationType(TestsDomain.Instance, new Guid("bcc9eee6-fa07-4d37-be84-b691bfce24be"), new Guid("b6c7354a-4997-4764-826a-0c9989431d3b"), new Guid("7da3b7ea-2e1a-400c-adbf-436d35720ae9"));
			I1C1Many2Many.AssignedMultiplicity = Multiplicity.ManyToMany;
			I1C1Many2Many.IsIndexed = true;

			I1C1Many2Many.AssociationType.ObjectType = this;

			I1C1Many2Many.RoleType.ObjectType = C1Class.Instance;;
			I1C1Many2Many.RoleType.AssignedSingularName = "C1Many2Many";
			I1C1Many2Many.RoleType.AssignedPluralName = "C1Many2Manies";
			this.Roles.I1C1Many2Many = I1C1Many2Many.RoleType;

			var I1AllorsDouble = new RelationType(TestsDomain.Instance, new Guid("cdb758bf-ecaf-4d99-88fb-58df9258c13c"), new Guid("62961c44-f0ab-4edf-9aa7-63312643e6b4"), new Guid("e33e809e-bbd3-4ecc-b46e-e233c5c93ce6"));
			I1AllorsDouble.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsDouble.AssociationType.ObjectType = this;

			I1AllorsDouble.RoleType.ObjectType = AllorsFloatUnit.Instance;
			this.Roles.I1AllorsDouble = I1AllorsDouble.RoleType;

			var I1I1One2One = new RelationType(TestsDomain.Instance, new Guid("e1b13216-7210-4c24-a668-83b40162a21b"), new Guid("f14f50da-635f-47d0-9f3d-28364b767637"), new Guid("911abf5b-ea84-4ffe-b6fb-558b4af50503"));
			I1I1One2One.AssignedMultiplicity = Multiplicity.OneToOne;
			I1I1One2One.IsIndexed = true;

			I1I1One2One.AssociationType.ObjectType = this;

			I1I1One2One.RoleType.ObjectType = I1Interface.Instance;;
			I1I1One2One.RoleType.AssignedSingularName = "I1One2One";
			I1I1One2One.RoleType.AssignedPluralName = "I1One2Ones";
			this.Roles.I1I1One2One = I1I1One2One.RoleType;

			var I1C1Many2One = new RelationType(TestsDomain.Instance, new Guid("e3126228-342a-4415-a2e8-d52eceaeaf89"), new Guid("202575b6-aaff-46ce-9e8a-e976a8a9d263"), new Guid("2598d7df-a764-4b6e-bf91-5234404b97c2"));
			I1C1Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I1C1Many2One.IsIndexed = true;

			I1C1Many2One.AssociationType.ObjectType = this;

			I1C1Many2One.RoleType.ObjectType = C1Class.Instance;;
			I1C1Many2One.RoleType.AssignedSingularName = "C1Many2One";
			I1C1Many2One.RoleType.AssignedPluralName = "C1Many2Ones";
			this.Roles.I1C1Many2One = I1C1Many2One.RoleType;

			var I1I12One2Many = new RelationType(TestsDomain.Instance, new Guid("e386cca6-e738-4c37-8bfc-b23057d7a0be"), new Guid("a3af5653-20c0-410c-9a6f-160e10e2fe69"), new Guid("6c708f4b-9fb1-412b-84c8-02f03efede5e"));
			I1I12One2Many.AssignedMultiplicity = Multiplicity.OneToMany;
			I1I12One2Many.IsIndexed = true;

			I1I12One2Many.AssociationType.ObjectType = this;

			I1I12One2Many.RoleType.ObjectType = I12Interface.Instance;;
			I1I12One2Many.RoleType.AssignedSingularName = "I12One2Many";
			I1I12One2Many.RoleType.AssignedPluralName = "I12One2Manies";
			this.Roles.I1I12One2Many = I1I12One2Many.RoleType;

			var I1C2Many2One = new RelationType(TestsDomain.Instance, new Guid("ef1a0a5e-1794-4478-9d0a-517182355206"), new Guid("7b80b14e-dd35-4e7c-ba85-ac7860a5dc28"), new Guid("1d51d303-f68b-4dca-9299-a6376e13c90e"));
			I1C2Many2One.AssignedMultiplicity = Multiplicity.ManyToOne;
			I1C2Many2One.IsIndexed = true;

			I1C2Many2One.AssociationType.ObjectType = this;

			I1C2Many2One.RoleType.ObjectType = C2Class.Instance;;
			I1C2Many2One.RoleType.AssignedSingularName = "C2Many2One";
			I1C2Many2One.RoleType.AssignedPluralName = "C2Many2Ones";
			this.Roles.I1C2Many2One = I1C2Many2One.RoleType;

			var I1AllorsUnique = new RelationType(TestsDomain.Instance, new Guid("f9d7411e-7993-4e43-a7e2-726f1e44e29c"), new Guid("84ae4441-5f83-4196-8439-483311b05055"), new Guid("5ebf419f-1c7f-46f2-844c-0f54321888ee"));
			I1AllorsUnique.AssignedMultiplicity = Multiplicity.OneToOne;
			I1AllorsUnique.AssociationType.ObjectType = this;

			I1AllorsUnique.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			this.Roles.I1AllorsUnique = I1AllorsUnique.RoleType;


		}

		internal override void SetRelationTypes()
		{
			C1Class.Instance.ConcreteRoles.I1I1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I1Many2One]; 

			C1Class.Instance.ConcreteRoles.I1I12Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I12Many2Many]; 

			C1Class.Instance.ConcreteRoles.I1I2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I2Many2Many]; 

			C1Class.Instance.ConcreteRoles.I1I2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I2Many2One]; 

			C1Class.Instance.ConcreteRoles.I1AllorsString = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsString]; 

			C1Class.Instance.ConcreteRoles.I1I12Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I12Many2One]; 

			C1Class.Instance.ConcreteRoles.I1AllorsDateTime = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsDateTime]; 

			C1Class.Instance.ConcreteRoles.I1I2One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I2One2Many]; 

			C1Class.Instance.ConcreteRoles.I1C2One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C2One2Many]; 

			C1Class.Instance.ConcreteRoles.I1C1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C1One2One]; 

			C1Class.Instance.ConcreteRoles.I1AllorsInteger = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsInteger]; 

			C1Class.Instance.ConcreteRoles.I1C2Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C2Many2Many]; 

			C1Class.Instance.ConcreteRoles.I1I1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I1One2Many]; 

			C1Class.Instance.ConcreteRoles.I1I1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I1Many2Many]; 

			C1Class.Instance.ConcreteRoles.I1AllorsBoolean = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsBoolean]; 

			C1Class.Instance.ConcreteRoles.I1AllorsDecimal = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsDecimal]; 

			C1Class.Instance.ConcreteRoles.I1I12One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I12One2One]; 

			C1Class.Instance.ConcreteRoles.I1I2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I2One2One]; 

			C1Class.Instance.ConcreteRoles.I1C2One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C2One2One]; 

			C1Class.Instance.ConcreteRoles.I1C1One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C1One2Many]; 

			C1Class.Instance.ConcreteRoles.I1AllorsBinary = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsBinary]; 

			C1Class.Instance.ConcreteRoles.I1C1Many2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C1Many2Many]; 

			C1Class.Instance.ConcreteRoles.I1AllorsDouble = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsDouble]; 

			C1Class.Instance.ConcreteRoles.I1I1One2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I1One2One]; 

			C1Class.Instance.ConcreteRoles.I1C1Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C1Many2One]; 

			C1Class.Instance.ConcreteRoles.I1I12One2Many = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1I12One2Many]; 

			C1Class.Instance.ConcreteRoles.I1C2Many2One = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1C2Many2One]; 

			C1Class.Instance.ConcreteRoles.I1AllorsUnique = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.I1AllorsUnique]; 


		}

		internal class RolesType
		{
			internal RoleType I1I1Many2One;
			internal RoleType I1I12Many2Many;
			internal RoleType I1I2Many2Many;
			internal RoleType I1I2Many2One;
			internal RoleType I1AllorsString;
			internal RoleType I1I12Many2One;
			internal RoleType I1AllorsDateTime;
			internal RoleType I1I2One2Many;
			internal RoleType I1C2One2Many;
			internal RoleType I1C1One2One;
			internal RoleType I1AllorsInteger;
			internal RoleType I1C2Many2Many;
			internal RoleType I1I1One2Many;
			internal RoleType I1I1Many2Many;
			internal RoleType I1AllorsBoolean;
			internal RoleType I1AllorsDecimal;
			internal RoleType I1I12One2One;
			internal RoleType I1I2One2One;
			internal RoleType I1C2One2One;
			internal RoleType I1C1One2Many;
			internal RoleType I1AllorsBinary;
			internal RoleType I1C1Many2Many;
			internal RoleType I1AllorsDouble;
			internal RoleType I1I1One2One;
			internal RoleType I1C1Many2One;
			internal RoleType I1I12One2Many;
			internal RoleType I1C2Many2One;
			internal RoleType I1AllorsUnique;

		}
	}public partial class LocalisedTextClass : Class
	{
		public static LocalisedTextClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal LocalisedTextClass() : base(BaseDomain.Instance, new Guid("020f5d4d-4a59-4d7b-865a-d72fc70e4d97"))
        {
			this.SingularName = "LocalisedText";
			this.PluralName = "LocalisedTexts";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("c2b526fd-920d-470a-8a40-405b7e4d8335"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("ebe85e2a-084a-452c-896f-aaf390c5bf1a"))
			{
				Subtype = Instance,
				Supertype = LocalisedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var LocalisedTextText = new RelationType(BaseDomain.Instance, new Guid("50dc85f0-3d22-4bc1-95d9-153674b89f7a"), new Guid("accd061b-20b9-4a24-bb2c-c2f7276f43ab"), new Guid("8d3f68e1-fa6e-414f-aa4d-25fcc2c975d6"));
			LocalisedTextText.AssignedMultiplicity = Multiplicity.OneToOne;
			LocalisedTextText.AssociationType.ObjectType = this;

			LocalisedTextText.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LocalisedTextText.RoleType.AssignedSingularName = "Text";
			LocalisedTextText.RoleType.AssignedPluralName = "Texts";
			LocalisedTextText.RoleType.Size = -1;
			this.Roles.Text = LocalisedTextText.RoleType;


		}

		internal override void SetRelationTypes()
		{
			LocalisedTextClass.Instance.ConcreteRoles.Text = LocalisedTextClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Text]; 


		}

		internal class RolesType
		{
			internal RoleType Text;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Text;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;
			internal ConcreteRoleType Locale;

		}
	}public partial class CounterClass : Class
	{
		public static CounterClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal CounterClass() : base(BaseDomain.Instance, new Guid("0568354f-e3d9-439e-baac-b7dce31b956a"))
        {
			this.SingularName = "Counter";
			this.PluralName = "Counters";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("f2a0c00d-ba20-44bd-94ec-1173370d77c9"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var CounterValue = new RelationType(BaseDomain.Instance, new Guid("309d07d9-8dea-4e99-a3b8-53c0d360bc54"), new Guid("0c807020-5397-4cdb-8380-52899b7af6b7"), new Guid("ab60f6a7-d913-4377-ab47-97f0fb9d8f3b"));
			CounterValue.AssignedMultiplicity = Multiplicity.OneToOne;
			CounterValue.AssociationType.ObjectType = this;

			CounterValue.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			CounterValue.RoleType.AssignedSingularName = "Value";
			CounterValue.RoleType.AssignedPluralName = "Values";
			this.Roles.Value = CounterValue.RoleType;


		}

		internal override void SetRelationTypes()
		{
			CounterClass.Instance.ConcreteRoles.Value = CounterClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Value]; 


		}

		internal class RolesType
		{
			internal RoleType Value;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Value;
			internal ConcreteRoleType UniqueId;

		}
	}public partial class StringTemplateClass : Class
	{
		public static StringTemplateClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal StringTemplateClass() : base(BaseDomain.Instance, new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969"))
        {
			this.SingularName = "StringTemplate";
			this.PluralName = "StringTemplates";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("714a9702-01f8-48fc-8add-2f50a8b0bd91"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("84fc6495-3e2c-4a99-b0bc-7d818c24eb0b"))
			{
				Subtype = Instance,
				Supertype = LocalisedInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var StringTemplateBody = new RelationType(BaseDomain.Instance, new Guid("2f88f9f8-3c22-40d3-885c-2abd43af96cc"), new Guid("9ad9b285-2a91-4bd9-90dd-8f963ef0a465"), new Guid("3fcb83d0-11c5-48ba-ba9c-5126f0b4e9f4"));
			StringTemplateBody.AssignedMultiplicity = Multiplicity.OneToOne;
			StringTemplateBody.AssociationType.ObjectType = this;

			StringTemplateBody.RoleType.ObjectType = AllorsStringUnit.Instance;;
			StringTemplateBody.RoleType.AssignedSingularName = "Body";
			StringTemplateBody.RoleType.AssignedPluralName = "Bodies";
			StringTemplateBody.RoleType.Size = -1;
			this.Roles.Body = StringTemplateBody.RoleType;

			var StringTemplateName = new RelationType(BaseDomain.Instance, new Guid("c501103b-037a-4961-93df-2dbb74b88a76"), new Guid("1bcdddcc-e462-4d59-af2d-7346245cb271"), new Guid("37bd5d22-89f1-47a4-b6bd-8841e194b213"));
			StringTemplateName.AssignedMultiplicity = Multiplicity.OneToOne;
			StringTemplateName.AssociationType.ObjectType = this;

			StringTemplateName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			StringTemplateName.RoleType.AssignedSingularName = "Name";
			StringTemplateName.RoleType.AssignedPluralName = "Names";
			StringTemplateName.RoleType.Size = 256;
			this.Roles.Name = StringTemplateName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			StringTemplateClass.Instance.ConcreteRoles.Body = StringTemplateClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Body]; 

			StringTemplateClass.Instance.ConcreteRoles.Name = StringTemplateClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType Body;
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Body;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType UniqueId;
			internal ConcreteRoleType Locale;

		}
	}public partial class UniquelyIdentifiableInterface: Interface
	{
		public static UniquelyIdentifiableInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal UniquelyIdentifiableInterface() : base(BaseDomain.Instance, new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469"))
        {
			this.SingularName = "UniquelyIdentifiable";
			this.PluralName = "UniquelyIdentifiables";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var UniquelyIdentifiableUniqueId = new RelationType(BaseDomain.Instance, new Guid("e1842d87-8157-40e7-b06e-4375f311f2c3"), new Guid("fe413e96-cfcf-4e8d-9f23-0fa4f457fdf1"), new Guid("d73fd9a4-13ee-4fa9-8925-d93eca328bf6"));
			UniquelyIdentifiableUniqueId.AssignedMultiplicity = Multiplicity.OneToOne;
			UniquelyIdentifiableUniqueId.IsIndexed = true;

			UniquelyIdentifiableUniqueId.AssociationType.ObjectType = this;

			UniquelyIdentifiableUniqueId.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			UniquelyIdentifiableUniqueId.RoleType.AssignedSingularName = "UniqueId";
			UniquelyIdentifiableUniqueId.RoleType.AssignedPluralName = "UniqueIds";
			this.Roles.UniqueId = UniquelyIdentifiableUniqueId.RoleType;


		}

		internal override void SetRelationTypes()
		{
			GenderClass.Instance.ConcreteRoles.UniqueId = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			OrganisationClass.Instance.ConcreteRoles.UniqueId = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			CounterClass.Instance.ConcreteRoles.UniqueId = CounterClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			StringTemplateClass.Instance.ConcreteRoles.UniqueId = StringTemplateClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			UserGroupClass.Instance.ConcreteRoles.UniqueId = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			RoleClass.Instance.ConcreteRoles.UniqueId = RoleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			PrintQueueClass.Instance.ConcreteRoles.UniqueId = PrintQueueClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			PersonClass.Instance.ConcreteRoles.UniqueId = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 
			MediaClass.Instance.ConcreteRoles.UniqueId = MediaClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UniqueId]; 


		}

		internal class RolesType
		{
			internal RoleType UniqueId;

		}
	}public partial class SingletonClass : Class
	{
		public static SingletonClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal SingletonClass() : base(BaseDomain.Instance, new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b"))
        {
			this.SingularName = "Singleton";
			this.PluralName = "Singletons";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("dc655fb2-bb19-4338-a641-e95689c58409"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var SingletonDefaultPrintQueue = new RelationType(BaseDomain.Instance, new Guid("64aed238-7009-4157-8395-7eb58ebf7889"), new Guid("2f79ecfe-5fd4-44d1-9c39-457bb3dc6815"), new Guid("d861c8f8-7362-4805-9941-661a99ab11ac"));
			SingletonDefaultPrintQueue.AssignedMultiplicity = Multiplicity.OneToOne;
			SingletonDefaultPrintQueue.IsIndexed = true;

			SingletonDefaultPrintQueue.AssociationType.ObjectType = this;

			SingletonDefaultPrintQueue.RoleType.ObjectType = PrintQueueClass.Instance;;
			SingletonDefaultPrintQueue.RoleType.AssignedSingularName = "DefaultPrintQueue";
			SingletonDefaultPrintQueue.RoleType.AssignedPluralName = "DefaultPrintQueues";
			this.Roles.DefaultPrintQueue = SingletonDefaultPrintQueue.RoleType;

			var SingletonDefaultLocale = new RelationType(BaseDomain.Instance, new Guid("9c1634ab-be99-4504-8690-ed4b39fec5bc"), new Guid("45a4205d-7c02-40d4-8d97-6d7d59e05def"), new Guid("1e051b37-cf30-43ed-a623-dd2928d6d0a3"));
			SingletonDefaultLocale.AssignedMultiplicity = Multiplicity.ManyToOne;
			SingletonDefaultLocale.IsIndexed = true;

			SingletonDefaultLocale.AssociationType.ObjectType = this;

			SingletonDefaultLocale.RoleType.ObjectType = LocaleClass.Instance;;
			SingletonDefaultLocale.RoleType.AssignedSingularName = "DefaultLocale";
			SingletonDefaultLocale.RoleType.AssignedPluralName = "DefaultLocales";
			this.Roles.DefaultLocale = SingletonDefaultLocale.RoleType;

			var SingletonLocale = new RelationType(BaseDomain.Instance, new Guid("9e5a3413-ed33-474f-adf2-149ad5a80719"), new Guid("33d5d8b9-3472-48d8-ab1a-83d00d9cb691"), new Guid("e75a8956-4d02-49ba-b0cf-747b7a9f350d"));
			SingletonLocale.AssignedMultiplicity = Multiplicity.OneToMany;
			SingletonLocale.IsIndexed = true;

			SingletonLocale.AssociationType.ObjectType = this;

			SingletonLocale.RoleType.ObjectType = LocaleClass.Instance;;
			SingletonLocale.RoleType.AssignedSingularName = "Locale";
			SingletonLocale.RoleType.AssignedPluralName = "Locales";
			this.Roles.Locale = SingletonLocale.RoleType;

			var SingletonAdministratorSecurityToken = new RelationType(BaseDomain.Instance, new Guid("d9ea02e5-9aa1-4cbe-9318-06324529a923"), new Guid("6247e69d-4789-4ee0-a75b-c2de44a5fcce"), new Guid("c11f31e1-75a7-4b23-9d58-7dfec256b658"));
			SingletonAdministratorSecurityToken.AssignedMultiplicity = Multiplicity.ManyToOne;
			SingletonAdministratorSecurityToken.IsIndexed = true;

			SingletonAdministratorSecurityToken.AssociationType.ObjectType = this;

			SingletonAdministratorSecurityToken.RoleType.ObjectType = SecurityTokenClass.Instance;;
			SingletonAdministratorSecurityToken.RoleType.AssignedSingularName = "AdministratorSecurityToken";
			SingletonAdministratorSecurityToken.RoleType.AssignedPluralName = "AdministratorSecurityTokens";
			this.Roles.AdministratorSecurityToken = SingletonAdministratorSecurityToken.RoleType;

			var SingletonGuest = new RelationType(BaseDomain.Instance, new Guid("f16652b0-b712-43d7-8d4e-34a22487514d"), new Guid("c92466b5-55ba-496a-8880-2821f32f8f8e"), new Guid("3a12d798-40c3-40e0-ba9f-9d01b1e39e89"));
			SingletonGuest.AssignedMultiplicity = Multiplicity.OneToOne;
			SingletonGuest.AssociationType.ObjectType = this;

			SingletonGuest.RoleType.ObjectType = UserInterface.Instance;;
			SingletonGuest.RoleType.AssignedSingularName = "Guest";
			SingletonGuest.RoleType.AssignedPluralName = "Guests";
			this.Roles.Guest = SingletonGuest.RoleType;

			var SingletonDefaultSecurityToken = new RelationType(BaseDomain.Instance, new Guid("f579494b-e550-4be6-9d93-84618ac78704"), new Guid("33f17e75-99cc-417e-99f3-c29080f08f0a"), new Guid("ca9e3469-583c-4950-ba2c-1bc3a0fc3e96"));
			SingletonDefaultSecurityToken.AssignedMultiplicity = Multiplicity.ManyToOne;
			SingletonDefaultSecurityToken.IsIndexed = true;

			SingletonDefaultSecurityToken.AssociationType.ObjectType = this;

			SingletonDefaultSecurityToken.RoleType.ObjectType = SecurityTokenClass.Instance;;
			SingletonDefaultSecurityToken.RoleType.AssignedSingularName = "DefaultSecurityToken";
			SingletonDefaultSecurityToken.RoleType.AssignedPluralName = "DefaultSecurityTokens";
			this.Roles.DefaultSecurityToken = SingletonDefaultSecurityToken.RoleType;

			var SingletonPersonTemplate = new RelationType(TestsDomain.Instance, new Guid("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236"), new Guid("04bc6904-bd6e-4401-9720-088ebf1fb392"), new Guid("7ab62a77-c098-4ad6-836d-53ae820df951"));
			SingletonPersonTemplate.AssignedMultiplicity = Multiplicity.ManyToOne;
			SingletonPersonTemplate.IsIndexed = true;

			SingletonPersonTemplate.AssociationType.ObjectType = this;

			SingletonPersonTemplate.RoleType.ObjectType = StringTemplateClass.Instance;;
			SingletonPersonTemplate.RoleType.AssignedSingularName = "PersonTemplate";
			SingletonPersonTemplate.RoleType.AssignedPluralName = "PersonTemplates";
			this.Roles.PersonTemplate = SingletonPersonTemplate.RoleType;


		}

		internal override void SetRelationTypes()
		{
			SingletonClass.Instance.ConcreteRoles.DefaultPrintQueue = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DefaultPrintQueue]; 

			SingletonClass.Instance.ConcreteRoles.DefaultLocale = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DefaultLocale]; 

			SingletonClass.Instance.ConcreteRoles.Locale = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Locale]; 

			SingletonClass.Instance.ConcreteRoles.AdministratorSecurityToken = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.AdministratorSecurityToken]; 

			SingletonClass.Instance.ConcreteRoles.Guest = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Guest]; 

			SingletonClass.Instance.ConcreteRoles.DefaultSecurityToken = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DefaultSecurityToken]; 

			SingletonClass.Instance.ConcreteRoles.PersonTemplate = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PersonTemplate]; 


		}

		internal class RolesType
		{
			internal RoleType DefaultPrintQueue;
			internal RoleType DefaultLocale;
			internal RoleType Locale;
			internal RoleType AdministratorSecurityToken;
			internal RoleType Guest;
			internal RoleType DefaultSecurityToken;
			internal RoleType PersonTemplate;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType DefaultPrintQueue;
			internal ConcreteRoleType DefaultLocale;
			internal ConcreteRoleType Locale;
			internal ConcreteRoleType AdministratorSecurityToken;
			internal ConcreteRoleType Guest;
			internal ConcreteRoleType DefaultSecurityToken;
			internal ConcreteRoleType PersonTemplate;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class LocaleClass : Class
	{
		public static LocaleClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal LocaleClass() : base(BaseDomain.Instance, new Guid("45033ae6-85b5-4ced-87ce-02518e6c27fd"))
        {
			this.SingularName = "Locale";
			this.PluralName = "Locales";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("317927bf-e978-4239-b257-a443a22e4665"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var LocaleName = new RelationType(BaseDomain.Instance, new Guid("2a2c6f77-e6a2-4eab-bfe3-5d35a8abd7f7"), new Guid("09422255-fa17-41d8-991b-d21d7b37c6c5"), new Guid("647db2b3-997b-4c3a-9ae2-d49b410933c1"));
			LocaleName.AssignedMultiplicity = Multiplicity.OneToOne;
			LocaleName.AssociationType.ObjectType = this;

			LocaleName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LocaleName.RoleType.AssignedSingularName = "Name";
			LocaleName.RoleType.AssignedPluralName = "Names";
			LocaleName.RoleType.Size = 256;
			this.Roles.Name = LocaleName.RoleType;

			var LocaleLanguage = new RelationType(BaseDomain.Instance, new Guid("d8cac34a-9bb2-4190-bd2a-ec0b87e04cf5"), new Guid("af501892-3c83-41d1-826b-f5c4cb1de7fe"), new Guid("ed32b12a-00ad-420b-9dfa-f1c6ce773fcd"));
			LocaleLanguage.AssignedMultiplicity = Multiplicity.ManyToOne;
			LocaleLanguage.IsIndexed = true;

			LocaleLanguage.AssociationType.ObjectType = this;

			LocaleLanguage.RoleType.ObjectType = LanguageClass.Instance;;
			LocaleLanguage.RoleType.AssignedSingularName = "Language";
			LocaleLanguage.RoleType.AssignedPluralName = "Languages";
			this.Roles.Language = LocaleLanguage.RoleType;

			var LocaleCountry = new RelationType(BaseDomain.Instance, new Guid("ea778b77-2929-4ab4-ad99-bf2f970401a9"), new Guid("bb5904f5-feb0-47eb-903a-0351d55f0d8c"), new Guid("b2fc6e06-3881-427e-b4cc-8457a65f8076"));
			LocaleCountry.AssignedMultiplicity = Multiplicity.ManyToOne;
			LocaleCountry.IsIndexed = true;

			LocaleCountry.AssociationType.ObjectType = this;

			LocaleCountry.RoleType.ObjectType = CountryClass.Instance;;
			LocaleCountry.RoleType.AssignedSingularName = "Country";
			LocaleCountry.RoleType.AssignedPluralName = "Countries";
			this.Roles.Country = LocaleCountry.RoleType;


		}

		internal override void SetRelationTypes()
		{
			LocaleClass.Instance.ConcreteRoles.Name = LocaleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			LocaleClass.Instance.ConcreteRoles.Language = LocaleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Language]; 

			LocaleClass.Instance.ConcreteRoles.Country = LocaleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Country]; 


		}

		internal class RolesType
		{
			internal RoleType Name;
			internal RoleType Language;
			internal RoleType Country;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Name;
			internal ConcreteRoleType Language;
			internal ConcreteRoleType Country;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class LanguageClass : Class
	{
		public static LanguageClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal LanguageClass() : base(BaseDomain.Instance, new Guid("4a0eca4b-281f-488d-9c7e-497de882c044"))
        {
			this.SingularName = "Language";
			this.PluralName = "Languages";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("a4ebd1f9-84db-4888-ba53-414b67b03c73"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var LanguageName = new RelationType(BaseDomain.Instance, new Guid("be482902-beb5-4a76-8ad0-c1b1c1c0e5c4"), new Guid("d3369fa9-afb7-4d5a-b476-3e4d43cce0fd"), new Guid("308d73b0-1b65-40a9-88f1-288848849c51"));
			LanguageName.AssignedMultiplicity = Multiplicity.OneToOne;
			LanguageName.IsIndexed = true;

			LanguageName.AssociationType.ObjectType = this;

			LanguageName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LanguageName.RoleType.AssignedSingularName = "Name";
			LanguageName.RoleType.AssignedPluralName = "Names";
			LanguageName.RoleType.Size = 256;
			this.Roles.Name = LanguageName.RoleType;

			var LanguageIsoCode = new RelationType(BaseDomain.Instance, new Guid("d2a32d9f-21cc-4f9d-b0d3-a9b75da66907"), new Guid("6c860e73-d12e-4e35-897e-ed9f8fd8eba0"), new Guid("84f904a6-8dcc-4089-bda6-34325ade6367"));
			LanguageIsoCode.AssignedMultiplicity = Multiplicity.OneToOne;
			LanguageIsoCode.AssociationType.ObjectType = this;

			LanguageIsoCode.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LanguageIsoCode.RoleType.AssignedSingularName = "IsoCode";
			LanguageIsoCode.RoleType.AssignedPluralName = "IsoCodes";
			LanguageIsoCode.RoleType.Size = 256;
			this.Roles.IsoCode = LanguageIsoCode.RoleType;

			var LanguageLocalisedName = new RelationType(BaseDomain.Instance, new Guid("f091b264-e6b1-4a57-bbfb-8225cbe8190c"), new Guid("6650af3b-f537-4c2f-afff-6773552315cd"), new Guid("5e9fcced-727d-42a2-95e6-a0f9d8be4ec7"));
			LanguageLocalisedName.AssignedMultiplicity = Multiplicity.OneToMany;
			LanguageLocalisedName.IsIndexed = true;

			LanguageLocalisedName.AssociationType.ObjectType = this;

			LanguageLocalisedName.RoleType.ObjectType = LocalisedTextClass.Instance;;
			LanguageLocalisedName.RoleType.AssignedSingularName = "LocalisedName";
			LanguageLocalisedName.RoleType.AssignedPluralName = "LocalisedNames";
			this.Roles.LocalisedName = LanguageLocalisedName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			LanguageClass.Instance.ConcreteRoles.Name = LanguageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			LanguageClass.Instance.ConcreteRoles.IsoCode = LanguageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsoCode]; 

			LanguageClass.Instance.ConcreteRoles.LocalisedName = LanguageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.LocalisedName]; 


		}

		internal class RolesType
		{
			internal RoleType Name;
			internal RoleType IsoCode;
			internal RoleType LocalisedName;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Name;
			internal ConcreteRoleType IsoCode;
			internal ConcreteRoleType LocalisedName;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class UserGroupClass : Class
	{
		public static UserGroupClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal UserGroupClass() : base(BaseDomain.Instance, new Guid("60065f5d-a3c2-4418-880d-1026ab607319"))
        {
			this.SingularName = "UserGroup";
			this.PluralName = "UserGroups";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("6147b424-b6a9-44b9-b173-30d259165a51"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("ff0d36e7-49d3-4bea-88d0-f40e8ddb714e"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var UserGroupRole = new RelationType(BaseDomain.Instance, new Guid("2f8cf270-a153-4e0d-b844-991d577222d4"), new Guid("46f531f2-b211-4f2a-902d-7198cda9c50d"), new Guid("a1b92c88-79d9-4a4f-bb99-0fde4e251a28"));
			UserGroupRole.AssignedMultiplicity = Multiplicity.OneToOne;
			UserGroupRole.IsIndexed = true;

			UserGroupRole.AssociationType.ObjectType = this;

			UserGroupRole.RoleType.ObjectType = RoleClass.Instance;;
			UserGroupRole.RoleType.AssignedSingularName = "Role";
			UserGroupRole.RoleType.AssignedPluralName = "Roles";
			this.Roles.Role = UserGroupRole.RoleType;

			var UserGroupMember = new RelationType(BaseDomain.Instance, new Guid("585bb5cf-9ba4-4865-9027-3667185abc4f"), new Guid("1e2d1e31-ed80-4435-8850-7663d9c5f41d"), new Guid("c552f0b7-95ce-4d45-aaea-56bc8365eee4"));
			UserGroupMember.AssignedMultiplicity = Multiplicity.ManyToMany;
			UserGroupMember.IsIndexed = true;

			UserGroupMember.AssociationType.ObjectType = this;

			UserGroupMember.RoleType.ObjectType = UserInterface.Instance;;
			UserGroupMember.RoleType.AssignedSingularName = "Member";
			UserGroupMember.RoleType.AssignedPluralName = "Members";
			this.Roles.Member = UserGroupMember.RoleType;

			var UserGroupParent = new RelationType(BaseDomain.Instance, new Guid("be9dc116-a7ea-4a4b-aaca-eb0f91fc3741"), new Guid("d8d8fdf7-f261-449b-b611-7c58dc43f6d3"), new Guid("6ec327af-86bc-4c79-8f00-bcb399686bf3"));
			UserGroupParent.AssignedMultiplicity = Multiplicity.ManyToOne;
			UserGroupParent.IsIndexed = true;

			UserGroupParent.AssociationType.ObjectType = this;

			UserGroupParent.RoleType.ObjectType = UserGroupClass.Instance;;
			UserGroupParent.RoleType.AssignedSingularName = "Parent";
			UserGroupParent.RoleType.AssignedPluralName = "Parents";
			this.Roles.Parent = UserGroupParent.RoleType;

			var UserGroupName = new RelationType(BaseDomain.Instance, new Guid("e94e7f05-78bd-4291-923f-38f82d00e3f4"), new Guid("75859e2c-c1a3-4f4c-bb37-4064d0aa81d0"), new Guid("9d3c1eec-bf10-4a79-a37f-bc6a20ff2a79"));
			UserGroupName.AssignedMultiplicity = Multiplicity.OneToOne;
			UserGroupName.IsIndexed = true;

			UserGroupName.AssociationType.ObjectType = this;

			UserGroupName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			UserGroupName.RoleType.AssignedSingularName = "Name";
			UserGroupName.RoleType.AssignedPluralName = "Names";
			UserGroupName.RoleType.Size = 256;
			this.Roles.Name = UserGroupName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			UserGroupClass.Instance.ConcreteRoles.Role = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Role]; 

			UserGroupClass.Instance.ConcreteRoles.Member = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Member]; 

			UserGroupClass.Instance.ConcreteRoles.Parent = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Parent]; 

			UserGroupClass.Instance.ConcreteRoles.Name = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType Role;
			internal RoleType Member;
			internal RoleType Parent;
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Role;
			internal ConcreteRoleType Member;
			internal ConcreteRoleType Parent;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType UniqueId;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class PrintableInterface: Interface
	{
		public static PrintableInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal PrintableInterface() : base(BaseDomain.Instance, new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1"))
        {
			this.SingularName = "Printable";
			this.PluralName = "Printables";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("82285900-358c-426f-a592-c7ae138287ed"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("9bc4b1c9-7e87-4b5b-bcf8-c02b462f0d53"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var PrintablePrintContent = new RelationType(BaseDomain.Instance, new Guid("c75d4e4c-d47c-4757-bcb0-25b6daedec9e"), new Guid("480b7df7-b463-4038-a48d-35b8a8af899e"), new Guid("8d530dcd-2c3b-4d1d-8acc-9963338968ed"));
			PrintablePrintContent.AssignedMultiplicity = Multiplicity.OneToOne;
			PrintablePrintContent.IsDerived = true;
			PrintablePrintContent.AssociationType.ObjectType = this;

			PrintablePrintContent.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PrintablePrintContent.RoleType.AssignedSingularName = "PrintContent";
			PrintablePrintContent.RoleType.AssignedPluralName = "PrintContents";
			PrintablePrintContent.RoleType.Size = -1;
			this.Roles.PrintContent = PrintablePrintContent.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PersonClass.Instance.ConcreteRoles.PrintContent = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.PrintContent]; 


		}

		internal class RolesType
		{
			internal RoleType PrintContent;

		}
	}public partial class MediaContentClass : Class
	{
		public static MediaContentClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal MediaContentClass() : base(BaseDomain.Instance, new Guid("6c20422e-cb3e-4402-bb40-dacaf584405e"))
        {
			this.SingularName = "MediaContent";
			this.PluralName = "MediaContents";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var MediaContentValue = new RelationType(BaseDomain.Instance, new Guid("0756d508-44b7-405e-bf92-bc09e5702e63"), new Guid("76e6547b-8dcf-4e69-ae2d-c8f8c33989e9"), new Guid("85170945-b020-485b-bb6f-c4122992ebfd"));
			MediaContentValue.AssignedMultiplicity = Multiplicity.OneToOne;
			MediaContentValue.AssociationType.ObjectType = this;

			MediaContentValue.RoleType.ObjectType = AllorsBinaryUnit.Instance;;
			MediaContentValue.RoleType.AssignedSingularName = "Value";
			MediaContentValue.RoleType.AssignedPluralName = "Values";
			MediaContentValue.RoleType.Size = -1;
			this.Roles.Value = MediaContentValue.RoleType;

			var MediaContentHash = new RelationType(BaseDomain.Instance, new Guid("890598a9-0be4-49ee-8dd8-3581ee9355e6"), new Guid("3cf7f10e-dc56-4a50-95a5-fe7fae0be291"), new Guid("70823e7d-5829-4db7-99e0-f6c5f2b0e87b"));
			MediaContentHash.AssignedMultiplicity = Multiplicity.OneToOne;
			MediaContentHash.IsDerived = true;
			MediaContentHash.IsIndexed = true;

			MediaContentHash.AssociationType.ObjectType = this;

			MediaContentHash.RoleType.ObjectType = AllorsStringUnit.Instance;;
			MediaContentHash.RoleType.AssignedSingularName = "Hash";
			MediaContentHash.RoleType.AssignedPluralName = "Hashes";
			MediaContentHash.RoleType.Size = 1024;
			this.Roles.Hash = MediaContentHash.RoleType;


		}

		internal override void SetRelationTypes()
		{
			MediaContentClass.Instance.ConcreteRoles.Value = MediaContentClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Value]; 

			MediaContentClass.Instance.ConcreteRoles.Hash = MediaContentClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Hash]; 


		}

		internal class RolesType
		{
			internal RoleType Value;
			internal RoleType Hash;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Value;
			internal ConcreteRoleType Hash;

		}
	}public partial class LocalisedInterface: Interface
	{
		public static LocalisedInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal LocalisedInterface() : base(BaseDomain.Instance, new Guid("7979a17c-0829-46df-a0d4-1b01775cfaac"))
        {
			this.SingularName = "Localised";
			this.PluralName = "Localiseds";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var LocalisedLocale = new RelationType(BaseDomain.Instance, new Guid("8c005a4e-5ffe-45fd-b279-778e274f4d83"), new Guid("6684d98b-cd43-4612-bf9d-afefe02a0d43"), new Guid("d43b92ac-9e6f-4238-9625-1e889be054cf"));
			LocalisedLocale.AssignedMultiplicity = Multiplicity.ManyToOne;
			LocalisedLocale.IsIndexed = true;

			LocalisedLocale.AssociationType.ObjectType = this;

			LocalisedLocale.RoleType.ObjectType = LocaleClass.Instance;;
			LocalisedLocale.RoleType.AssignedSingularName = "Locale";
			LocalisedLocale.RoleType.AssignedPluralName = "Locales";
			this.Roles.Locale = LocalisedLocale.RoleType;


		}

		internal override void SetRelationTypes()
		{
			LocalisedTextClass.Instance.ConcreteRoles.Locale = LocalisedTextClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Locale]; 
			StringTemplateClass.Instance.ConcreteRoles.Locale = StringTemplateClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Locale]; 
			PersonClass.Instance.ConcreteRoles.Locale = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Locale]; 


		}

		internal class RolesType
		{
			internal RoleType Locale;

		}
	}public partial class PermissionClass : Class
	{
		public static PermissionClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal PermissionClass() : base(BaseDomain.Instance, new Guid("7fded183-3337-4196-afb0-3266377944bc"))
        {
			this.SingularName = "Permission";
			this.PluralName = "Permissions";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("799f485c-6f77-4da1-8f63-a2b1469f8808"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("aec174d4-5633-462c-91a1-10d3e782fdb4"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var PermissionOperandTypePointer = new RelationType(BaseDomain.Instance, new Guid("097bb620-f068-440e-8d02-ef9d8be1d0f0"), new Guid("3442728c-164a-477c-87be-19a789229585"), new Guid("3fd81194-2f6f-43e7-9c6b-91f5e3e118ac"));
			PermissionOperandTypePointer.AssignedMultiplicity = Multiplicity.OneToOne;
			PermissionOperandTypePointer.IsIndexed = true;

			PermissionOperandTypePointer.AssociationType.ObjectType = this;

			PermissionOperandTypePointer.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			PermissionOperandTypePointer.RoleType.AssignedSingularName = "OperandTypePointer";
			PermissionOperandTypePointer.RoleType.AssignedPluralName = "OperandTypePointers";
			this.Roles.OperandTypePointer = PermissionOperandTypePointer.RoleType;

			var PermissionConcreteClassPointer = new RelationType(BaseDomain.Instance, new Guid("29b80857-e51b-4dec-b859-887ed74b1626"), new Guid("8ffed1eb-b64e-4341-bbb6-348ed7f06e83"), new Guid("cadaca05-55ba-4a13-8758-786ff29c8e46"));
			PermissionConcreteClassPointer.AssignedMultiplicity = Multiplicity.OneToOne;
			PermissionConcreteClassPointer.IsIndexed = true;

			PermissionConcreteClassPointer.AssociationType.ObjectType = this;

			PermissionConcreteClassPointer.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			PermissionConcreteClassPointer.RoleType.AssignedSingularName = "ConcreteClassPointer";
			PermissionConcreteClassPointer.RoleType.AssignedPluralName = "ConcreteClassePointers";
			this.Roles.ConcreteClassPointer = PermissionConcreteClassPointer.RoleType;

			var PermissionOperationEnum = new RelationType(BaseDomain.Instance, new Guid("9d73d437-4918-4f20-b9a7-3ce23a04bd7b"), new Guid("891734d6-4855-4b33-8b3b-f46fd6103149"), new Guid("d29ce0ed-fba8-409d-8675-dc95e1566cfb"));
			PermissionOperationEnum.AssignedMultiplicity = Multiplicity.OneToOne;
			PermissionOperationEnum.IsIndexed = true;

			PermissionOperationEnum.AssociationType.ObjectType = this;

			PermissionOperationEnum.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			PermissionOperationEnum.RoleType.AssignedSingularName = "OperationEnum";
			PermissionOperationEnum.RoleType.AssignedPluralName = "OperationEnums";
			this.Roles.OperationEnum = PermissionOperationEnum.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PermissionClass.Instance.ConcreteRoles.OperandTypePointer = PermissionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.OperandTypePointer]; 

			PermissionClass.Instance.ConcreteRoles.ConcreteClassPointer = PermissionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.ConcreteClassPointer]; 

			PermissionClass.Instance.ConcreteRoles.OperationEnum = PermissionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.OperationEnum]; 


		}

		internal class RolesType
		{
			internal RoleType OperandTypePointer;
			internal RoleType ConcreteClassPointer;
			internal RoleType OperationEnum;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType OperandTypePointer;
			internal ConcreteRoleType ConcreteClassPointer;
			internal ConcreteRoleType OperationEnum;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class PeriodInterface: Interface
	{
		public static PeriodInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal PeriodInterface() : base(BaseDomain.Instance, new Guid("80adbbfd-952e-46f3-a744-78e0ce42bc80"))
        {
			this.SingularName = "Period";
			this.PluralName = "Periods";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var PeriodFromDate = new RelationType(BaseDomain.Instance, new Guid("5aeb31c7-03d4-4314-bbb2-fca5704b1eab"), new Guid("8cf0bd14-753d-4f34-99b3-7a6b0d90c3d4"), new Guid("0da8ef4e-53b7-4152-b219-7e0cebbca268"));
			PeriodFromDate.AssignedMultiplicity = Multiplicity.OneToOne;
			PeriodFromDate.AssociationType.ObjectType = this;

			PeriodFromDate.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			PeriodFromDate.RoleType.AssignedSingularName = "FromDate";
			PeriodFromDate.RoleType.AssignedPluralName = "FromDates";
			this.Roles.FromDate = PeriodFromDate.RoleType;

			var PeriodThroughDate = new RelationType(BaseDomain.Instance, new Guid("d7576ce2-da27-487a-86aa-b0912f745bc0"), new Guid("cb2fa6c1-f826-45f0-a03f-00e6cb268ebb"), new Guid("4e021875-5bae-4f01-8deb-641016cd2f8d"));
			PeriodThroughDate.AssignedMultiplicity = Multiplicity.OneToOne;
			PeriodThroughDate.AssociationType.ObjectType = this;

			PeriodThroughDate.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			PeriodThroughDate.RoleType.AssignedSingularName = "ThroughDate";
			PeriodThroughDate.RoleType.AssignedPluralName = "ThroughDates";
			this.Roles.ThroughDate = PeriodThroughDate.RoleType;


		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
			internal RoleType FromDate;
			internal RoleType ThroughDate;

		}
	}public partial class DeletableInterface: Interface
	{
		public static DeletableInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal DeletableInterface() : base(BaseDomain.Instance, new Guid("9279e337-c658-4086-946d-03c75cdb1ad3"))
        {
			this.SingularName = "Deletable";
			this.PluralName = "Deletables";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}
	}public partial class UserInterface: Interface
	{
		public static UserInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal UserInterface() : base(BaseDomain.Instance, new Guid("a0309c3b-6f80-4777-983e-6e69800df5be"))
        {
			this.SingularName = "User";
			this.PluralName = "Users";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("17c51f3d-869f-4f1e-95e0-011021837b69"))
			{
				Subtype = Instance,
				Supertype = SecurityTokenOwnerInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("42aae0bd-7080-4c11-8cbd-1634aa046d32"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("765d49c1-f1ef-4af9-b295-08d0b010b7fe"))
			{
				Subtype = Instance,
				Supertype = LocalisedInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var UserUserEmailConfirmed = new RelationType(BaseDomain.Instance, new Guid("0b3b650b-fcd4-4475-b5c4-e2ee4f39b0be"), new Guid("c89a8e3f-6f76-41ac-b4dc-839f9080d917"), new Guid("1b1409b8-add7-494c-a895-002fc969ac7b"));
			UserUserEmailConfirmed.AssignedMultiplicity = Multiplicity.OneToOne;
			UserUserEmailConfirmed.AssociationType.ObjectType = this;

			UserUserEmailConfirmed.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			UserUserEmailConfirmed.RoleType.AssignedSingularName = "UserEmailConfirmed";
			UserUserEmailConfirmed.RoleType.AssignedPluralName = "UserEmailConfirmeds";
			this.Roles.UserEmailConfirmed = UserUserEmailConfirmed.RoleType;

			var UserUserName = new RelationType(BaseDomain.Instance, new Guid("5e8ab257-1a1c-4448-aacc-71dbaaba525b"), new Guid("eca7ef36-8928-4116-bfce-1896a685fe8c"), new Guid("3b7d40a0-18ea-4018-b797-6417723e1890"));
			UserUserName.AssignedMultiplicity = Multiplicity.OneToOne;
			UserUserName.AssociationType.ObjectType = this;

			UserUserName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			UserUserName.RoleType.AssignedSingularName = "UserName";
			UserUserName.RoleType.AssignedPluralName = "UserNames";
			UserUserName.RoleType.Size = 256;
			this.Roles.UserName = UserUserName.RoleType;

			var UserUserEmail = new RelationType(BaseDomain.Instance, new Guid("c1ae3652-5854-4b68-9890-a954067767fc"), new Guid("111104a2-1181-4958-92f6-6528cef79af7"), new Guid("58e35754-91a9-4956-aa66-ca48d05c7042"));
			UserUserEmail.AssignedMultiplicity = Multiplicity.OneToOne;
			UserUserEmail.AssociationType.ObjectType = this;

			UserUserEmail.RoleType.ObjectType = AllorsStringUnit.Instance;;
			UserUserEmail.RoleType.AssignedSingularName = "UserEmail";
			UserUserEmail.RoleType.AssignedPluralName = "UserEmails";
			UserUserEmail.RoleType.Size = 256;
			this.Roles.UserEmail = UserUserEmail.RoleType;

			var UserUserPasswordHash = new RelationType(BaseDomain.Instance, new Guid("ea0c7596-c0b8-4984-bc25-cb4b4857954e"), new Guid("8537ddb5-8ce2-4f35-a16f-207f2519ba9c"), new Guid("75ee3ec2-02bb-4666-a6f0-bac84c844dfa"));
			UserUserPasswordHash.AssignedMultiplicity = Multiplicity.OneToOne;
			UserUserPasswordHash.AssociationType.ObjectType = this;

			UserUserPasswordHash.RoleType.ObjectType = AllorsStringUnit.Instance;;
			UserUserPasswordHash.RoleType.AssignedSingularName = "UserPasswordHash";
			UserUserPasswordHash.RoleType.AssignedPluralName = "UserPasswordHashes";
			UserUserPasswordHash.RoleType.Size = 256;
			this.Roles.UserPasswordHash = UserUserPasswordHash.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PersonClass.Instance.ConcreteRoles.UserEmailConfirmed = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UserEmailConfirmed]; 

			PersonClass.Instance.ConcreteRoles.UserName = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UserName]; 

			PersonClass.Instance.ConcreteRoles.UserEmail = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UserEmail]; 

			PersonClass.Instance.ConcreteRoles.UserPasswordHash = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.UserPasswordHash]; 


		}

		internal class RolesType
		{
			internal RoleType UserEmailConfirmed;
			internal RoleType UserName;
			internal RoleType UserEmail;
			internal RoleType UserPasswordHash;

		}
	}public partial class SecurityTokenClass : Class
	{
		public static SecurityTokenClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal SecurityTokenClass() : base(BaseDomain.Instance, new Guid("a53f1aed-0e3f-4c3c-9600-dc579cccf893"))
        {
			this.SingularName = "SecurityToken";
			this.PluralName = "SecurityTokens";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("0a52522a-c2e2-4647-98d4-27fb2fa6a7ad"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}

		internal class ConcreteRolesType
		{
		}
	}public partial class SecurityTokenOwnerInterface: Interface
	{
		public static SecurityTokenOwnerInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal SecurityTokenOwnerInterface() : base(BaseDomain.Instance, new Guid("a69cad9c-c2f1-463f-9af1-873ce65aeea6"))
        {
			this.SingularName = "SecurityTokenOwner";
			this.PluralName = "SecurityTokenOwners";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var SecurityTokenOwnerOwnerSecurityToken = new RelationType(BaseDomain.Instance, new Guid("5fb15e8b-011c-46f7-83dd-485d4cc4f9f2"), new Guid("cdc21c1c-918e-4622-a01f-a3de06a8c802"), new Guid("2acda9b3-89e8-475f-9d70-b9cde334409c"));
			SecurityTokenOwnerOwnerSecurityToken.AssignedMultiplicity = Multiplicity.OneToOne;
			SecurityTokenOwnerOwnerSecurityToken.IsDerived = true;
			SecurityTokenOwnerOwnerSecurityToken.IsIndexed = true;

			SecurityTokenOwnerOwnerSecurityToken.AssociationType.ObjectType = this;

			SecurityTokenOwnerOwnerSecurityToken.RoleType.ObjectType = SecurityTokenClass.Instance;;
			SecurityTokenOwnerOwnerSecurityToken.RoleType.AssignedSingularName = "OwnerSecurityToken";
			SecurityTokenOwnerOwnerSecurityToken.RoleType.AssignedPluralName = "OwnerSecurityTokens";
			this.Roles.OwnerSecurityToken = SecurityTokenOwnerOwnerSecurityToken.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PersonClass.Instance.ConcreteRoles.OwnerSecurityToken = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.OwnerSecurityToken]; 


		}

		internal class RolesType
		{
			internal RoleType OwnerSecurityToken;

		}
	}public partial class TransitionClass : Class
	{
		public static TransitionClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal TransitionClass() : base(BaseDomain.Instance, new Guid("a7e490c0-ce29-4298-97c4-519904bb755a"))
        {
			this.SingularName = "Transition";
			this.PluralName = "Transitions";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var TransitionFromState = new RelationType(BaseDomain.Instance, new Guid("c6ee1a42-05fa-462b-b04f-811f01c6b646"), new Guid("ae7fa215-20bb-4472-9d25-ee3174f40fdb"), new Guid("e79fa7b8-870a-4a6e-8522-bb39437e0650"));
			TransitionFromState.AssignedMultiplicity = Multiplicity.ManyToMany;
			TransitionFromState.IsIndexed = true;

			TransitionFromState.AssociationType.ObjectType = this;

			TransitionFromState.RoleType.ObjectType = ObjectStateInterface.Instance;;
			TransitionFromState.RoleType.AssignedSingularName = "FromState";
			TransitionFromState.RoleType.AssignedPluralName = "FromStates";
			this.Roles.FromState = TransitionFromState.RoleType;

			var TransitionToState = new RelationType(BaseDomain.Instance, new Guid("dd19e7f8-83b7-4ff1-b475-02c4296b47e4"), new Guid("c88c9ab2-af38-45ca-9caa-fcb5715da129"), new Guid("c68eb959-1b2c-48a7-b15a-944a576944ef"));
			TransitionToState.AssignedMultiplicity = Multiplicity.ManyToOne;
			TransitionToState.IsIndexed = true;

			TransitionToState.AssociationType.ObjectType = this;

			TransitionToState.RoleType.ObjectType = ObjectStateInterface.Instance;;
			TransitionToState.RoleType.AssignedSingularName = "ToState";
			TransitionToState.RoleType.AssignedPluralName = "ToStates";
			this.Roles.ToState = TransitionToState.RoleType;


		}

		internal override void SetRelationTypes()
		{
			TransitionClass.Instance.ConcreteRoles.FromState = TransitionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.FromState]; 

			TransitionClass.Instance.ConcreteRoles.ToState = TransitionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.ToState]; 


		}

		internal class RolesType
		{
			internal RoleType FromState;
			internal RoleType ToState;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType FromState;
			internal ConcreteRoleType ToState;

		}
	}public partial class MediaTypeClass : Class
	{
		public static MediaTypeClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal MediaTypeClass() : base(BaseDomain.Instance, new Guid("aa7d61f8-6618-47a0-9cf2-e75dd81dbd5b"))
        {
			this.SingularName = "MediaType";
			this.PluralName = "MediaTypes";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("8d9af4d9-0b71-46de-a0ca-1bbfef4cdd8f"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var MediaTypeDefaultFileExtension = new RelationType(BaseDomain.Instance, new Guid("19e52bd9-26cb-4e74-9c28-9f01e684f3da"), new Guid("b1928c18-ef98-4cee-b03c-660221046486"), new Guid("7223c1e2-d722-440b-8345-ab4cfe56d0e9"));
			MediaTypeDefaultFileExtension.AssignedMultiplicity = Multiplicity.OneToOne;
			MediaTypeDefaultFileExtension.AssociationType.ObjectType = this;

			MediaTypeDefaultFileExtension.RoleType.ObjectType = AllorsStringUnit.Instance;;
			MediaTypeDefaultFileExtension.RoleType.AssignedSingularName = "DefaultFileExtension";
			MediaTypeDefaultFileExtension.RoleType.AssignedPluralName = "DefaultFileExtensions";
			MediaTypeDefaultFileExtension.RoleType.Size = 256;
			this.Roles.DefaultFileExtension = MediaTypeDefaultFileExtension.RoleType;

			var MediaTypeName = new RelationType(BaseDomain.Instance, new Guid("5fcee025-29fd-42d8-ad5a-75cb88d8aef0"), new Guid("0353bfc3-552c-43c7-bfe2-666d2a8199dc"), new Guid("437caa53-1838-4cc4-a403-d65cf3b64358"));
			MediaTypeName.AssignedMultiplicity = Multiplicity.OneToOne;
			MediaTypeName.AssociationType.ObjectType = this;

			MediaTypeName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			MediaTypeName.RoleType.AssignedSingularName = "Name";
			MediaTypeName.RoleType.AssignedPluralName = "Names";
			MediaTypeName.RoleType.Size = 256;
			this.Roles.Name = MediaTypeName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			MediaTypeClass.Instance.ConcreteRoles.DefaultFileExtension = MediaTypeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DefaultFileExtension]; 

			MediaTypeClass.Instance.ConcreteRoles.Name = MediaTypeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType DefaultFileExtension;
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType DefaultFileExtension;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class TransitionalInterface: Interface
	{
		public static TransitionalInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal TransitionalInterface() : base(BaseDomain.Instance, new Guid("ab2179ad-9eac-4b61-8d84-81cd777c4926"))
        {
			this.SingularName = "Transitional";
			this.PluralName = "Transitionals";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("c9e990a7-7853-4b34-860d-672224d36162"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
		}
	}public partial class LoginClass : Class
	{
		public static LoginClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal LoginClass() : base(BaseDomain.Instance, new Guid("ad7277a8-eda4-4128-a990-b47fe43d120a"))
        {
			this.SingularName = "Login";
			this.PluralName = "Logins";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("9690ff31-1364-4e0e-9279-3e290477a37b"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var LoginKey = new RelationType(BaseDomain.Instance, new Guid("18262218-a14f-48c3-87a5-87196d3b5974"), new Guid("3f067cf5-4fcb-4be4-9afb-15ba37700658"), new Guid("e5393717-f46c-4a4c-a87f-3e4684428860"));
			LoginKey.AssignedMultiplicity = Multiplicity.OneToOne;
			LoginKey.IsIndexed = true;

			LoginKey.AssociationType.ObjectType = this;

			LoginKey.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LoginKey.RoleType.AssignedSingularName = "Key";
			LoginKey.RoleType.AssignedPluralName = "Keys";
			LoginKey.RoleType.Size = 256;
			this.Roles.Key = LoginKey.RoleType;

			var LoginProvider = new RelationType(BaseDomain.Instance, new Guid("7a82e721-d0b7-4567-aaef-bd3987ae6d01"), new Guid("2f2ef41d-8310-45fd-8ab5-e5d067862e3d"), new Guid("c8e3851a-bc57-4acb-934a-4adfc37b1da7"));
			LoginProvider.AssignedMultiplicity = Multiplicity.OneToOne;
			LoginProvider.IsIndexed = true;

			LoginProvider.AssociationType.ObjectType = this;

			LoginProvider.RoleType.ObjectType = AllorsStringUnit.Instance;;
			LoginProvider.RoleType.AssignedSingularName = "Provider";
			LoginProvider.RoleType.AssignedPluralName = "Providers";
			LoginProvider.RoleType.Size = 256;
			this.Roles.Provider = LoginProvider.RoleType;

			var LoginUser = new RelationType(BaseDomain.Instance, new Guid("c2d950ad-39d3-40f1-8817-11a026e9890b"), new Guid("e8091111-9f92-41a9-b4b1-4e8f277ea575"), new Guid("150daf84-13ce-4b5f-83e6-64c7ef4f81c6"));
			LoginUser.AssignedMultiplicity = Multiplicity.ManyToOne;
			LoginUser.IsIndexed = true;

			LoginUser.AssociationType.ObjectType = this;

			LoginUser.RoleType.ObjectType = UserInterface.Instance;;
			LoginUser.RoleType.AssignedSingularName = "User";
			LoginUser.RoleType.AssignedPluralName = "Users";
			this.Roles.User = LoginUser.RoleType;


		}

		internal override void SetRelationTypes()
		{
			LoginClass.Instance.ConcreteRoles.Key = LoginClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Key]; 

			LoginClass.Instance.ConcreteRoles.Provider = LoginClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Provider]; 

			LoginClass.Instance.ConcreteRoles.User = LoginClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.User]; 


		}

		internal class RolesType
		{
			internal RoleType Key;
			internal RoleType Provider;
			internal RoleType User;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Key;
			internal ConcreteRoleType Provider;
			internal ConcreteRoleType User;

		}
	}public partial class RoleClass : Class
	{
		public static RoleClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal RoleClass() : base(BaseDomain.Instance, new Guid("af6fe5f4-e5bc-4099-bcd1-97528af6505d"))
        {
			this.SingularName = "Role";
			this.PluralName = "Roles";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("4e737d59-0330-4f4e-a3f3-7ec617d63748"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("816ab651-b27d-4f4f-83d7-39e7b501b2c0"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var RolePermission = new RelationType(BaseDomain.Instance, new Guid("51e56ae1-72dc-443f-a2a3-f5aa3650f8d2"), new Guid("47af1a0f-497d-4a19-887b-79e5fb77c8bd"), new Guid("7e6a71b0-2194-47f8-b562-cb4a15e335b6"));
			RolePermission.AssignedMultiplicity = Multiplicity.ManyToMany;
			RolePermission.IsIndexed = true;

			RolePermission.AssociationType.ObjectType = this;

			RolePermission.RoleType.ObjectType = PermissionClass.Instance;;
			RolePermission.RoleType.AssignedSingularName = "Permission";
			RolePermission.RoleType.AssignedPluralName = "Permissions";
			this.Roles.Permission = RolePermission.RoleType;

			var RoleName = new RelationType(BaseDomain.Instance, new Guid("934bcbbe-5286-445c-a1bd-e2fcc786c448"), new Guid("05785884-ca83-43de-a6f3-86d3fa7ec82a"), new Guid("8d87c74f-53ed-4e1d-a2ea-12190ac233d2"));
			RoleName.AssignedMultiplicity = Multiplicity.OneToOne;
			RoleName.AssociationType.ObjectType = this;

			RoleName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			RoleName.RoleType.AssignedSingularName = "Name";
			RoleName.RoleType.AssignedPluralName = "Names";
			RoleName.RoleType.Size = 256;
			this.Roles.Name = RoleName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			RoleClass.Instance.ConcreteRoles.Permission = RoleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Permission]; 

			RoleClass.Instance.ConcreteRoles.Name = RoleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType Permission;
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Permission;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;
			internal ConcreteRoleType UniqueId;

		}
	}public partial class PrintQueueClass : Class
	{
		public static PrintQueueClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal PrintQueueClass() : base(BaseDomain.Instance, new Guid("b45705e3-0dc6-4296-824a-76bb6af223d3"))
        {
			this.SingularName = "PrintQueue";
			this.PluralName = "PrintQueues";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("8f8c54bf-2aa2-4dd0-86ec-d4451b50aeb1"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("e4518c12-8dd9-4762-93b8-ba3e42b6bf0d"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var PrintQueuePrintable = new RelationType(BaseDomain.Instance, new Guid("679156a1-f683-4772-b724-54b318eb3cb3"), new Guid("9124aa32-3ed5-4a1a-8988-961eea280b86"), new Guid("432f8b01-0bb8-4bd2-8a41-107b6d043a40"));
			PrintQueuePrintable.AssignedMultiplicity = Multiplicity.ManyToMany;
			PrintQueuePrintable.IsIndexed = true;

			PrintQueuePrintable.AssociationType.ObjectType = this;

			PrintQueuePrintable.RoleType.ObjectType = PrintableInterface.Instance;;
			PrintQueuePrintable.RoleType.AssignedSingularName = "Printable";
			PrintQueuePrintable.RoleType.AssignedPluralName = "Printables";
			this.Roles.Printable = PrintQueuePrintable.RoleType;

			var PrintQueueName = new RelationType(BaseDomain.Instance, new Guid("7a85e090-55cf-47f5-912e-4bd87c66a060"), new Guid("01fa325c-4b41-4cbf-9ffe-65d25e0ae694"), new Guid("285adf08-7f1b-4dfe-8db5-cbf4a9d0cb59"));
			PrintQueueName.AssignedMultiplicity = Multiplicity.OneToOne;
			PrintQueueName.IsIndexed = true;

			PrintQueueName.AssociationType.ObjectType = this;

			PrintQueueName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PrintQueueName.RoleType.AssignedSingularName = "Name";
			PrintQueueName.RoleType.AssignedPluralName = "Names";
			PrintQueueName.RoleType.Size = 256;
			this.Roles.Name = PrintQueueName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PrintQueueClass.Instance.ConcreteRoles.Printable = PrintQueueClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Printable]; 

			PrintQueueClass.Instance.ConcreteRoles.Name = PrintQueueClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 


		}

		internal class RolesType
		{
			internal RoleType Printable;
			internal RoleType Name;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Printable;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;
			internal ConcreteRoleType UniqueId;

		}
	}public partial class EnumerationInterface: Interface
	{
		public static EnumerationInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal EnumerationInterface() : base(BaseDomain.Instance, new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445"))
        {
			this.SingularName = "Enumeration";
			this.PluralName = "Enumerations";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("9b0a0816-0ec4-4e6d-9617-ba819f7081a5"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("b5e48ee4-971a-4487-b09c-d0cb397aa0e9"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var EnumerationLocalisedName = new RelationType(BaseDomain.Instance, new Guid("07e034f1-246a-4115-9662-4c798f31343f"), new Guid("bcf428fd-0263-488c-b9ac-963ceca1c972"), new Guid("919fdad7-830e-4b12-b23c-f433951236af"));
			EnumerationLocalisedName.AssignedMultiplicity = Multiplicity.OneToMany;
			EnumerationLocalisedName.IsIndexed = true;

			EnumerationLocalisedName.AssociationType.ObjectType = this;

			EnumerationLocalisedName.RoleType.ObjectType = LocalisedTextClass.Instance;;
			EnumerationLocalisedName.RoleType.AssignedSingularName = "LocalisedName";
			EnumerationLocalisedName.RoleType.AssignedPluralName = "LocalisedNames";
			this.Roles.LocalisedName = EnumerationLocalisedName.RoleType;

			var EnumerationName = new RelationType(BaseDomain.Instance, new Guid("3d3ae4d0-bac6-4645-8a53-3e9f7f9af086"), new Guid("004cc333-b8ae-4952-ae13-f2ab80eb018c"), new Guid("5850860d-c772-402f-815b-7634c9a1e697"));
			EnumerationName.AssignedMultiplicity = Multiplicity.OneToOne;
			EnumerationName.IsIndexed = true;

			EnumerationName.AssociationType.ObjectType = this;

			EnumerationName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			EnumerationName.RoleType.AssignedSingularName = "Name";
			EnumerationName.RoleType.AssignedPluralName = "Names";
			EnumerationName.RoleType.Size = 256;
			this.Roles.Name = EnumerationName.RoleType;

			var EnumerationIsActive = new RelationType(BaseDomain.Instance, new Guid("f57bb62e-77a8-4519-81e6-539d54b71cb7"), new Guid("a8993304-52c0-4b53-9982-6caa5675467a"), new Guid("0c6faf5a-eac9-454c-bd53-3b8409e56d34"));
			EnumerationIsActive.AssignedMultiplicity = Multiplicity.OneToOne;
			EnumerationIsActive.IsIndexed = true;

			EnumerationIsActive.AssociationType.ObjectType = this;

			EnumerationIsActive.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			EnumerationIsActive.RoleType.AssignedSingularName = "IsActive";
			EnumerationIsActive.RoleType.AssignedPluralName = "AreActive";
			this.Roles.IsActive = EnumerationIsActive.RoleType;


		}

		internal override void SetRelationTypes()
		{
			GenderClass.Instance.ConcreteRoles.LocalisedName = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.LocalisedName]; 

			GenderClass.Instance.ConcreteRoles.Name = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			GenderClass.Instance.ConcreteRoles.IsActive = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsActive]; 


		}

		internal class RolesType
		{
			internal RoleType LocalisedName;
			internal RoleType Name;
			internal RoleType IsActive;

		}
	}public partial class CountryClass : Class
	{
		public static CountryClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal CountryClass() : base(BaseDomain.Instance, new Guid("c22bf60e-6428-4d10-8194-94f7be396f28"))
        {
			this.SingularName = "Country";
			this.PluralName = "Countries";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("1771a830-0d21-4c5e-8d1c-56db36de55b2"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var CountryCurrency = new RelationType(BaseDomain.Instance, new Guid("62009cef-7424-4ec0-8953-e92b3cd6639d"), new Guid("323173ee-385c-4f74-8b78-ff05735460f8"), new Guid("4ca5a640-5d9e-4910-95ed-6872c7ea13d2"));
			CountryCurrency.AssignedMultiplicity = Multiplicity.ManyToOne;
			CountryCurrency.IsIndexed = true;

			CountryCurrency.AssociationType.ObjectType = this;

			CountryCurrency.RoleType.ObjectType = CurrencyClass.Instance;;
			CountryCurrency.RoleType.AssignedSingularName = "Currency";
			CountryCurrency.RoleType.AssignedPluralName = "Currencies";
			this.Roles.Currency = CountryCurrency.RoleType;

			var CountryName = new RelationType(BaseDomain.Instance, new Guid("6b9c977f-b394-440e-9781-5d56733b60da"), new Guid("6e3532ae-3528-4114-9274-54fc08effd0d"), new Guid("60f1f9a3-13d1-485f-bc77-fda1f9ef1815"));
			CountryName.AssignedMultiplicity = Multiplicity.OneToOne;
			CountryName.IsIndexed = true;

			CountryName.AssociationType.ObjectType = this;

			CountryName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CountryName.RoleType.AssignedSingularName = "Name";
			CountryName.RoleType.AssignedPluralName = "Names";
			CountryName.RoleType.Size = 256;
			this.Roles.Name = CountryName.RoleType;

			var CountryLocalisedName = new RelationType(BaseDomain.Instance, new Guid("8236a702-a76d-4bb5-9afd-acacb1508261"), new Guid("9b682612-50f9-43f3-abde-4d0cb5156f0d"), new Guid("99c52c13-ef50-4f68-a32f-fef660aa3044"));
			CountryLocalisedName.AssignedMultiplicity = Multiplicity.OneToMany;
			CountryLocalisedName.IsIndexed = true;

			CountryLocalisedName.AssociationType.ObjectType = this;

			CountryLocalisedName.RoleType.ObjectType = LocalisedTextClass.Instance;;
			CountryLocalisedName.RoleType.AssignedSingularName = "LocalisedName";
			CountryLocalisedName.RoleType.AssignedPluralName = "LocalisedNames";
			this.Roles.LocalisedName = CountryLocalisedName.RoleType;

			var CountryIsoCode = new RelationType(BaseDomain.Instance, new Guid("f93acc4e-f89e-4610-ada9-e58f21c165bc"), new Guid("ea0efe67-89f2-4317-97e7-f0e14358e718"), new Guid("4fe997d6-d221-432b-9f09-4f77735c109b"));
			CountryIsoCode.AssignedMultiplicity = Multiplicity.OneToOne;
			CountryIsoCode.AssociationType.ObjectType = this;

			CountryIsoCode.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CountryIsoCode.RoleType.AssignedSingularName = "IsoCode";
			CountryIsoCode.RoleType.AssignedPluralName = "IsoCodes";
			CountryIsoCode.RoleType.Size = 2;
			this.Roles.IsoCode = CountryIsoCode.RoleType;


		}

		internal override void SetRelationTypes()
		{
			CountryClass.Instance.ConcreteRoles.Currency = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Currency]; 

			CountryClass.Instance.ConcreteRoles.Name = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			CountryClass.Instance.ConcreteRoles.LocalisedName = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.LocalisedName]; 

			CountryClass.Instance.ConcreteRoles.IsoCode = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsoCode]; 


		}

		internal class RolesType
		{
			internal RoleType Currency;
			internal RoleType Name;
			internal RoleType LocalisedName;
			internal RoleType IsoCode;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Currency;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType LocalisedName;
			internal ConcreteRoleType IsoCode;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class AccessControlClass : Class
	{
		public static AccessControlClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal AccessControlClass() : base(BaseDomain.Instance, new Guid("c4d93d5e-34c3-4731-9d37-47a8e801d9a8"))
        {
			this.SingularName = "AccessControl";
			this.PluralName = "AccessControls";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("421b85e6-f8c3-469f-bbd7-ce425355fb04"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("b4c7e051-3605-41e6-a78b-edb1c70bde9d"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var AccessControlSubjectGroup = new RelationType(BaseDomain.Instance, new Guid("0dbbff5c-3dca-4257-b2da-442d263dcd86"), new Guid("92e8d639-9205-422b-b4ff-c7e8c2980abf"), new Guid("2d9b7157-5409-40d3-bd3e-6911df9aface"));
			AccessControlSubjectGroup.AssignedMultiplicity = Multiplicity.ManyToMany;
			AccessControlSubjectGroup.IsIndexed = true;

			AccessControlSubjectGroup.AssociationType.ObjectType = this;

			AccessControlSubjectGroup.RoleType.ObjectType = UserGroupClass.Instance;;
			AccessControlSubjectGroup.RoleType.AssignedSingularName = "SubjectGroup";
			AccessControlSubjectGroup.RoleType.AssignedPluralName = "SubjectGroups";
			this.Roles.SubjectGroup = AccessControlSubjectGroup.RoleType;

			var AccessControlSubject = new RelationType(BaseDomain.Instance, new Guid("37dd1e27-ba75-404c-9410-c6399d28317c"), new Guid("3d74101d-97bc-46fb-9548-96cb7aa01b39"), new Guid("e0303a17-bf7a-4a7f-bb4b-0a447c56aaaf"));
			AccessControlSubject.AssignedMultiplicity = Multiplicity.ManyToMany;
			AccessControlSubject.IsIndexed = true;

			AccessControlSubject.AssociationType.ObjectType = this;

			AccessControlSubject.RoleType.ObjectType = UserInterface.Instance;;
			AccessControlSubject.RoleType.AssignedSingularName = "Subject";
			AccessControlSubject.RoleType.AssignedPluralName = "Subjects";
			this.Roles.Subject = AccessControlSubject.RoleType;

			var AccessControlObject = new RelationType(BaseDomain.Instance, new Guid("6503574b-8bab-4da8-a19d-23a9bcffe01e"), new Guid("cae9e5c2-afa1-46f4-b930-69d4e810038f"), new Guid("ab2b4b9c-87dd-4712-b123-f5f9271c6193"));
			AccessControlObject.AssignedMultiplicity = Multiplicity.ManyToMany;
			AccessControlObject.IsIndexed = true;

			AccessControlObject.AssociationType.ObjectType = this;

			AccessControlObject.RoleType.ObjectType = SecurityTokenClass.Instance;;
			AccessControlObject.RoleType.AssignedSingularName = "Object";
			AccessControlObject.RoleType.AssignedPluralName = "Objects";
			this.Roles.Object = AccessControlObject.RoleType;

			var AccessControlRole = new RelationType(BaseDomain.Instance, new Guid("69a9dae8-678d-4c1c-a464-2e5aa5caf39e"), new Guid("ec79e57d-be81-430a-b12f-08ffd1e94af3"), new Guid("370d3d12-3164-4753-8a72-1c604bda1b64"));
			AccessControlRole.AssignedMultiplicity = Multiplicity.ManyToOne;
			AccessControlRole.AssociationType.ObjectType = this;

			AccessControlRole.RoleType.ObjectType = RoleClass.Instance;;
			AccessControlRole.RoleType.AssignedSingularName = "Role";
			AccessControlRole.RoleType.AssignedPluralName = "Roles";
			this.Roles.Role = AccessControlRole.RoleType;

			var AccessControlCacheId = new RelationType(BaseDomain.Instance, new Guid("f4763e29-6a7b-4c66-b59b-c80149df5861"), new Guid("d3b3ee52-c9ea-4d8a-84a7-5915f7e4fba7"), new Guid("a4197a6a-9070-4e5e-a8f9-7077574da8db"));
			AccessControlCacheId.AssignedMultiplicity = Multiplicity.OneToOne;
			AccessControlCacheId.AssociationType.ObjectType = this;

			AccessControlCacheId.RoleType.ObjectType = AllorsUniqueUnit.Instance;;
			AccessControlCacheId.RoleType.AssignedSingularName = "CacheId";
			AccessControlCacheId.RoleType.AssignedPluralName = "CacheIds";
			this.Roles.CacheId = AccessControlCacheId.RoleType;


		}

		internal override void SetRelationTypes()
		{
			AccessControlClass.Instance.ConcreteRoles.SubjectGroup = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SubjectGroup]; 

			AccessControlClass.Instance.ConcreteRoles.Subject = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Subject]; 

			AccessControlClass.Instance.ConcreteRoles.Object = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Object]; 

			AccessControlClass.Instance.ConcreteRoles.Role = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Role]; 

			AccessControlClass.Instance.ConcreteRoles.CacheId = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.CacheId]; 


		}

		internal class RolesType
		{
			internal RoleType SubjectGroup;
			internal RoleType Subject;
			internal RoleType Object;
			internal RoleType Role;
			internal RoleType CacheId;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType SubjectGroup;
			internal ConcreteRoleType Subject;
			internal ConcreteRoleType Object;
			internal ConcreteRoleType Role;
			internal ConcreteRoleType CacheId;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class PersonClass : Class
	{
		public static PersonClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal PersonClass() : base(BaseDomain.Instance, new Guid("c799ca62-a554-467d-9aa2-1663293bb37f"))
        {
			this.SingularName = "Person";
			this.PluralName = "Persons";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("306eb440-10ac-40e3-969d-14e3fdab134a"))
			{
				Subtype = Instance,
				Supertype = UserInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("7aa25de8-6602-434a-ace6-02694efa61a4"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("9cff65a7-70df-4614-868e-b007cf8d88b8"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};
			new Inheritance(TestsDomain.Instance, new Guid("19848b71-0042-40c2-8e88-d44db074bf5a"))
			{
				Subtype = Instance,
				Supertype = PrintableInterface.Instance
			};
			new Inheritance(TestsDomain.Instance, new Guid("be5f7b11-2103-4fd7-be58-07c1f15aa127"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var PersonLastName = new RelationType(BaseDomain.Instance, new Guid("8a3e4664-bb40-4208-8e90-a1b5be323f27"), new Guid("9b48ff56-afef-4501-ac97-6173731ff2c9"), new Guid("ace04ad8-bf64-4fc3-8216-14a720d3105d"));
			PersonLastName.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonLastName.AssociationType.ObjectType = this;

			PersonLastName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonLastName.RoleType.AssignedSingularName = "LastName";
			PersonLastName.RoleType.AssignedPluralName = "LastNames";
			PersonLastName.RoleType.Size = 256;
			this.Roles.LastName = PersonLastName.RoleType;

			var PersonMiddleName = new RelationType(BaseDomain.Instance, new Guid("eb18bb28-da9c-47b4-a091-2f8f2303dcb6"), new Guid("e3a4d7b2-c5f1-4101-9aab-a0135d37a5a5"), new Guid("a86fc7a6-dedd-4da9-a250-75c9ec730d22"));
			PersonMiddleName.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonMiddleName.AssociationType.ObjectType = this;

			PersonMiddleName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonMiddleName.RoleType.AssignedSingularName = "MiddleName";
			PersonMiddleName.RoleType.AssignedPluralName = "MiddleNames";
			PersonMiddleName.RoleType.Size = 256;
			this.Roles.MiddleName = PersonMiddleName.RoleType;

			var PersonFirstName = new RelationType(BaseDomain.Instance, new Guid("ed4b710a-fe24-4143-bb96-ed1bd9beae1a"), new Guid("1ea9eca4-eed0-4f61-8903-c99feae961ad"), new Guid("f10ea049-6d24-4ca2-8efa-032fcf3000b3"));
			PersonFirstName.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonFirstName.AssociationType.ObjectType = this;

			PersonFirstName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonFirstName.RoleType.AssignedSingularName = "FirstName";
			PersonFirstName.RoleType.AssignedPluralName = "FirstNames";
			PersonFirstName.RoleType.Size = 256;
			this.Roles.FirstName = PersonFirstName.RoleType;

			var PersonMainAddress = new RelationType(TestsDomain.Instance, new Guid("0375a3d3-1a1b-4cbb-b735-1fe508bcc672"), new Guid("ebaedf39-1af9-42b7-83dc-8945450cebf2"), new Guid("86685c44-5196-46dd-9260-e40a434e9a52"));
			PersonMainAddress.AssignedMultiplicity = Multiplicity.ManyToOne;
			PersonMainAddress.IsIndexed = true;

			PersonMainAddress.AssociationType.ObjectType = this;

			PersonMainAddress.RoleType.ObjectType = AddressInterface.Instance;;
			PersonMainAddress.RoleType.AssignedSingularName = "MainAddress";
			PersonMainAddress.RoleType.AssignedPluralName = "MainAddresses";
			this.Roles.MainAddress = PersonMainAddress.RoleType;

			var PersonTinyMCEText = new RelationType(TestsDomain.Instance, new Guid("15de4e58-c5ef-4ebb-9bf6-5ab06a02c5a4"), new Guid("be22968c-a450-418f-8f2e-f6140a56589c"), new Guid("ad249eb0-6cf2-4bcb-b3d1-3ff1282cd2f9"));
			PersonTinyMCEText.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonTinyMCEText.AssociationType.ObjectType = this;

			PersonTinyMCEText.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonTinyMCEText.RoleType.AssignedSingularName = "TinyMCEText";
			PersonTinyMCEText.RoleType.AssignedPluralName = "TinyMCETexts";
			PersonTinyMCEText.RoleType.Size = -1;
			this.Roles.TinyMCEText = PersonTinyMCEText.RoleType;

			var PersonText = new RelationType(TestsDomain.Instance, new Guid("1b057406-3343-426b-ab5b-ceb93ba02446"), new Guid("91d44bdd-7b17-4fa7-aeb7-625571b252b9"), new Guid("93d01c4a-0aa3-4d7c-a6d8-139b8ed1ffcc"));
			PersonText.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonText.AssociationType.ObjectType = this;

			PersonText.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonText.RoleType.AssignedSingularName = "Text";
			PersonText.RoleType.AssignedPluralName = "Texts";
			PersonText.RoleType.Size = -1;
			this.Roles.Text = PersonText.RoleType;

			var PersonAge = new RelationType(TestsDomain.Instance, new Guid("2a25125f-3545-4209-afc6-523eb0d8851e"), new Guid("94b038b3-2dd6-42a8-9cd6-800ddbef104c"), new Guid("fb6dcca2-14a6-4b00-bd3e-81acf59fbbe2"));
			PersonAge.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonAge.AssociationType.ObjectType = this;

			PersonAge.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			PersonAge.RoleType.AssignedSingularName = "Age";
			PersonAge.RoleType.AssignedPluralName = "Ages";
			this.Roles.Age = PersonAge.RoleType;

			var PersonIsStudent = new RelationType(TestsDomain.Instance, new Guid("54f11f06-8d3f-4d58-bcdc-d40e6820fdad"), new Guid("03a7ffcc-4291-4ae1-a2ab-69f7257fbf04"), new Guid("abd2a4b3-4b17-48d4-b465-0ffcb5a2664d"));
			PersonIsStudent.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonIsStudent.AssociationType.ObjectType = this;

			PersonIsStudent.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			PersonIsStudent.RoleType.AssignedSingularName = "IsStudent";
			PersonIsStudent.RoleType.AssignedPluralName = "AreStudent";
			this.Roles.IsStudent = PersonIsStudent.RoleType;

			var PersonMailboxAddress = new RelationType(TestsDomain.Instance, new Guid("6340de2a-c3b1-4893-a7f3-cb924b82fa0e"), new Guid("b6ea4ac5-088a-4773-8410-6813d0185d7c"), new Guid("5a472c98-481f-407c-b53e-eaaa7e7a5340"));
			PersonMailboxAddress.AssignedMultiplicity = Multiplicity.ManyToOne;
			PersonMailboxAddress.IsIndexed = true;

			PersonMailboxAddress.AssociationType.ObjectType = this;

			PersonMailboxAddress.RoleType.ObjectType = MailboxAddressClass.Instance;;
			PersonMailboxAddress.RoleType.AssignedSingularName = "MailboxAddress";
			PersonMailboxAddress.RoleType.AssignedPluralName = "MailboxAddresses";
			this.Roles.MailboxAddress = PersonMailboxAddress.RoleType;

			var PersonGender = new RelationType(TestsDomain.Instance, new Guid("654f6c84-62f2-4c0a-9d68-532ed3f39447"), new Guid("5ec6caf4-4752-4a89-92ec-13fd69b444f2"), new Guid("34704a90-d513-4fe2-a1ed-ad6d89399c73"));
			PersonGender.AssignedMultiplicity = Multiplicity.ManyToOne;
			PersonGender.IsIndexed = true;

			PersonGender.AssociationType.ObjectType = this;

			PersonGender.RoleType.ObjectType = GenderClass.Instance;;
			PersonGender.RoleType.AssignedSingularName = "Gender";
			PersonGender.RoleType.AssignedPluralName = "Genders";
			this.Roles.Gender = PersonGender.RoleType;

			var PersonFullName = new RelationType(TestsDomain.Instance, new Guid("688ebeb9-8a53-4e8d-b284-3faa0a01ef7c"), new Guid("8a181cec-7bae-4248-8e24-8abc7e01eea2"), new Guid("e431d53c-37ed-4fde-86a9-755f354c1d75"));
			PersonFullName.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonFullName.IsDerived = true;
			PersonFullName.AssociationType.ObjectType = this;

			PersonFullName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonFullName.RoleType.AssignedSingularName = "FullName";
			PersonFullName.RoleType.AssignedPluralName = "FullNames";
			PersonFullName.RoleType.Size = 256;
			this.Roles.FullName = PersonFullName.RoleType;

			var PersonShirtSize = new RelationType(TestsDomain.Instance, new Guid("6b626ba5-0c45-48c7-8b6b-5ea85e002d90"), new Guid("520bb966-6e8a-46a4-a3c0-18422af13cba"), new Guid("66e20063-ab51-417a-8ce4-135bb6e115c1"));
			PersonShirtSize.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonShirtSize.AssociationType.ObjectType = this;

			PersonShirtSize.RoleType.ObjectType = AllorsIntegerUnit.Instance;;
			PersonShirtSize.RoleType.AssignedSingularName = "ShirtSize";
			PersonShirtSize.RoleType.AssignedPluralName = "ShirtSizes";
			this.Roles.ShirtSize = PersonShirtSize.RoleType;

			var PersonCKEditorText = new RelationType(TestsDomain.Instance, new Guid("6cc34453-ac7a-4004-8380-033f92324e99"), new Guid("5a99b822-8c51-4cf6-82e9-ee4ca311216a"), new Guid("cc14daec-604d-4ca6-9908-a57c10ba1403"));
			PersonCKEditorText.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonCKEditorText.AssociationType.ObjectType = this;

			PersonCKEditorText.RoleType.ObjectType = AllorsStringUnit.Instance;;
			PersonCKEditorText.RoleType.AssignedSingularName = "CKEditorText";
			PersonCKEditorText.RoleType.AssignedPluralName = "CKEditorTexts";
			PersonCKEditorText.RoleType.Size = -1;
			this.Roles.CKEditorText = PersonCKEditorText.RoleType;

			var PersonIsMarried = new RelationType(TestsDomain.Instance, new Guid("a8a3b4b8-c4f2-4054-ab2a-2eac6fd058e4"), new Guid("0fdeacf1-35bd-473d-88a9-acd65803f731"), new Guid("656f11e4-7652-4b4d-9dda-28cfe16333ec"));
			PersonIsMarried.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonIsMarried.AssociationType.ObjectType = this;

			PersonIsMarried.RoleType.ObjectType = AllorsBooleanUnit.Instance;;
			PersonIsMarried.RoleType.AssignedSingularName = "IsMarried";
			PersonIsMarried.RoleType.AssignedPluralName = "AreMarried";
			this.Roles.IsMarried = PersonIsMarried.RoleType;

			var PersonBirthDate = new RelationType(TestsDomain.Instance, new Guid("adf83a86-878d-4148-a9fc-152f56697136"), new Guid("b9da077d-bfc7-4b4e-be62-03aded6da22e"), new Guid("0ffd9c62-efc6-4438-aaa3-755e4c637c21"));
			PersonBirthDate.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonBirthDate.AssociationType.ObjectType = this;

			PersonBirthDate.RoleType.ObjectType = AllorsDateTimeUnit.Instance;;
			PersonBirthDate.RoleType.AssignedSingularName = "BirthDate";
			PersonBirthDate.RoleType.AssignedPluralName = "BirthDates";
			this.Roles.BirthDate = PersonBirthDate.RoleType;

			var PersonWeight = new RelationType(TestsDomain.Instance, new Guid("afc32e62-c310-421b-8c1d-6f2b0bb88b54"), new Guid("c21ebc52-6b32-4af7-847e-d3d7e1c4defe"), new Guid("0aab73c3-f997-4dd9-885a-2c1c892adb0e"));
			PersonWeight.AssignedMultiplicity = Multiplicity.OneToOne;
			PersonWeight.AssociationType.ObjectType = this;

			PersonWeight.RoleType.ObjectType = AllorsDecimalUnit.Instance;;
			PersonWeight.RoleType.AssignedSingularName = "Weight";
			PersonWeight.RoleType.AssignedPluralName = "Weights";
			PersonWeight.RoleType.Scale = 2;
			PersonWeight.RoleType.Precision = 19;
			this.Roles.Weight = PersonWeight.RoleType;

			var PersonPhoto = new RelationType(TestsDomain.Instance, new Guid("b3ddd2df-8a5a-4747-bd4f-1f1eb37386b3"), new Guid("912b48f5-215e-4cc0-a83b-56b74d986608"), new Guid("f6624fac-db8e-4fb2-9e86-18021b59d31d"));
			PersonPhoto.AssignedMultiplicity = Multiplicity.ManyToOne;
			PersonPhoto.IsIndexed = true;

			PersonPhoto.AssociationType.ObjectType = this;

			PersonPhoto.RoleType.ObjectType = MediaClass.Instance;;
			PersonPhoto.RoleType.AssignedSingularName = "Photo";
			PersonPhoto.RoleType.AssignedPluralName = "Photos";
			this.Roles.Photo = PersonPhoto.RoleType;

			var PersonAddress = new RelationType(TestsDomain.Instance, new Guid("e9e7c874-4d94-42ff-a4c9-414d05ff9533"), new Guid("da5e0427-79f7-4259-8a68-0071aa4c6273"), new Guid("c922b44f-6c6f-4e8b-901d-6558e79bb558"));
			PersonAddress.AssignedMultiplicity = Multiplicity.ManyToMany;
			PersonAddress.IsIndexed = true;

			PersonAddress.AssociationType.ObjectType = this;

			PersonAddress.RoleType.ObjectType = AddressInterface.Instance;;
			PersonAddress.RoleType.AssignedSingularName = "Address";
			PersonAddress.RoleType.AssignedPluralName = "Addresses";
			this.Roles.Address = PersonAddress.RoleType;


		}

		internal override void SetRelationTypes()
		{
			PersonClass.Instance.ConcreteRoles.LastName = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.LastName]; 

			PersonClass.Instance.ConcreteRoles.MiddleName = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MiddleName]; 

			PersonClass.Instance.ConcreteRoles.FirstName = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.FirstName]; 

			PersonClass.Instance.ConcreteRoles.MainAddress = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MainAddress]; 

			PersonClass.Instance.ConcreteRoles.TinyMCEText = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.TinyMCEText]; 

			PersonClass.Instance.ConcreteRoles.Text = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Text]; 

			PersonClass.Instance.ConcreteRoles.Age = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Age]; 

			PersonClass.Instance.ConcreteRoles.IsStudent = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsStudent]; 

			PersonClass.Instance.ConcreteRoles.MailboxAddress = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MailboxAddress]; 

			PersonClass.Instance.ConcreteRoles.Gender = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Gender]; 

			PersonClass.Instance.ConcreteRoles.FullName = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.FullName]; 

			PersonClass.Instance.ConcreteRoles.ShirtSize = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.ShirtSize]; 

			PersonClass.Instance.ConcreteRoles.CKEditorText = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.CKEditorText]; 

			PersonClass.Instance.ConcreteRoles.IsMarried = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsMarried]; 

			PersonClass.Instance.ConcreteRoles.BirthDate = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.BirthDate]; 

			PersonClass.Instance.ConcreteRoles.Weight = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Weight]; 

			PersonClass.Instance.ConcreteRoles.Photo = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Photo]; 

			PersonClass.Instance.ConcreteRoles.Address = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Address]; 


		}

		internal class RolesType
		{
			internal RoleType LastName;
			internal RoleType MiddleName;
			internal RoleType FirstName;
			internal RoleType MainAddress;
			internal RoleType TinyMCEText;
			internal RoleType Text;
			internal RoleType Age;
			internal RoleType IsStudent;
			internal RoleType MailboxAddress;
			internal RoleType Gender;
			internal RoleType FullName;
			internal RoleType ShirtSize;
			internal RoleType CKEditorText;
			internal RoleType IsMarried;
			internal RoleType BirthDate;
			internal RoleType Weight;
			internal RoleType Photo;
			internal RoleType Address;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType LastName;
			internal ConcreteRoleType MiddleName;
			internal ConcreteRoleType FirstName;
			internal ConcreteRoleType MainAddress;
			internal ConcreteRoleType TinyMCEText;
			internal ConcreteRoleType Text;
			internal ConcreteRoleType Age;
			internal ConcreteRoleType IsStudent;
			internal ConcreteRoleType MailboxAddress;
			internal ConcreteRoleType Gender;
			internal ConcreteRoleType FullName;
			internal ConcreteRoleType ShirtSize;
			internal ConcreteRoleType CKEditorText;
			internal ConcreteRoleType IsMarried;
			internal ConcreteRoleType BirthDate;
			internal ConcreteRoleType Weight;
			internal ConcreteRoleType Photo;
			internal ConcreteRoleType Address;
			internal ConcreteRoleType UserEmailConfirmed;
			internal ConcreteRoleType UserName;
			internal ConcreteRoleType UserEmail;
			internal ConcreteRoleType UserPasswordHash;
			internal ConcreteRoleType OwnerSecurityToken;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;
			internal ConcreteRoleType Locale;
			internal ConcreteRoleType UniqueId;
			internal ConcreteRoleType PrintContent;

		}
	}public partial class ImageClass : Class
	{
		public static ImageClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal ImageClass() : base(BaseDomain.Instance, new Guid("caa2a2de-9454-4812-a69f-9d3728706345"))
        {
			this.SingularName = "Image";
			this.PluralName = "Images";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("ae0a1a2a-6413-470a-9c0d-e15ed36d4948"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var ImageOriginal = new RelationType(BaseDomain.Instance, new Guid("366410a7-7d51-4d7c-82fd-3444bdc0b3f7"), new Guid("9d45e17e-962b-4f9b-a029-c1c1562e5260"), new Guid("9ed94fa8-e01e-4f63-9932-d56134616474"));
			ImageOriginal.AssignedMultiplicity = Multiplicity.ManyToOne;
			ImageOriginal.IsIndexed = true;

			ImageOriginal.AssociationType.ObjectType = this;

			ImageOriginal.RoleType.ObjectType = MediaClass.Instance;;
			ImageOriginal.RoleType.AssignedSingularName = "Original";
			ImageOriginal.RoleType.AssignedPluralName = "Originals";
			this.Roles.Original = ImageOriginal.RoleType;

			var ImageResponsive = new RelationType(BaseDomain.Instance, new Guid("59689164-7a45-45d4-98fa-f8cf50c62899"), new Guid("386c7cfc-4bec-4564-a7c4-b2c1bccf6ebe"), new Guid("ce4c0fbb-5bdb-4c7f-a70a-b930c1020624"));
			ImageResponsive.AssignedMultiplicity = Multiplicity.OneToOne;
			ImageResponsive.IsIndexed = true;

			ImageResponsive.AssociationType.ObjectType = this;

			ImageResponsive.RoleType.ObjectType = MediaClass.Instance;;
			ImageResponsive.RoleType.AssignedSingularName = "Responsive";
			ImageResponsive.RoleType.AssignedPluralName = "Responsives";
			this.Roles.Responsive = ImageResponsive.RoleType;

			var ImageOriginalFilename = new RelationType(BaseDomain.Instance, new Guid("d149b012-1dc2-4bd1-a650-26b7c6f9024b"), new Guid("75fccc6e-1c89-4e0f-88c2-527eb3b0d71d"), new Guid("2f1c8149-f94a-448b-a832-4994f635c48f"));
			ImageOriginalFilename.AssignedMultiplicity = Multiplicity.OneToOne;
			ImageOriginalFilename.AssociationType.ObjectType = this;

			ImageOriginalFilename.RoleType.ObjectType = AllorsStringUnit.Instance;;
			ImageOriginalFilename.RoleType.AssignedSingularName = "OriginalFilename";
			ImageOriginalFilename.RoleType.AssignedPluralName = "OriginalFilenames";
			ImageOriginalFilename.RoleType.Size = 256;
			this.Roles.OriginalFilename = ImageOriginalFilename.RoleType;


		}

		internal override void SetRelationTypes()
		{
			ImageClass.Instance.ConcreteRoles.Original = ImageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Original]; 

			ImageClass.Instance.ConcreteRoles.Responsive = ImageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Responsive]; 

			ImageClass.Instance.ConcreteRoles.OriginalFilename = ImageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.OriginalFilename]; 


		}

		internal class RolesType
		{
			internal RoleType Original;
			internal RoleType Responsive;
			internal RoleType OriginalFilename;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType Original;
			internal ConcreteRoleType Responsive;
			internal ConcreteRoleType OriginalFilename;

		}
	}public partial class MediaClass : Class
	{
		public static MediaClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal MediaClass() : base(BaseDomain.Instance, new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374"))
        {
			this.SingularName = "Media";
			this.PluralName = "Medias";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("c8cd0830-d1a7-4343-8049-dc18c34c213e"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("dae544a9-9dea-4b84-99c7-2b701868333d"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};
			new Inheritance(BaseDomain.Instance, new Guid("f1586900-8030-46c1-a49b-7f2b5d6b6e64"))
			{
				Subtype = Instance,
				Supertype = DeletableInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var MediaMediaType = new RelationType(BaseDomain.Instance, new Guid("49481792-06f0-49a1-b32f-28d265815a24"), new Guid("7ca17a9e-0b68-445f-8080-84b08ca0eb2d"), new Guid("f1008c56-b375-4aa8-ac7e-c1f7ef9b2080"));
			MediaMediaType.AssignedMultiplicity = Multiplicity.ManyToOne;
			MediaMediaType.IsIndexed = true;

			MediaMediaType.AssociationType.ObjectType = this;

			MediaMediaType.RoleType.ObjectType = MediaTypeClass.Instance;;
			MediaMediaType.RoleType.AssignedSingularName = "MediaType";
			MediaMediaType.RoleType.AssignedPluralName = "MediaTypes";
			this.Roles.MediaType = MediaMediaType.RoleType;

			var MediaMediaContent = new RelationType(BaseDomain.Instance, new Guid("67082a51-1502-490b-b8db-537799e550bd"), new Guid("e8537dcf-1bd7-46c4-a37c-077bee6a78a1"), new Guid("02fe1ce8-c019-4a40-bd6f-b38d2f47a288"));
			MediaMediaContent.AssignedMultiplicity = Multiplicity.ManyToOne;
			MediaMediaContent.IsDerived = true;
			MediaMediaContent.IsIndexed = true;

			MediaMediaContent.AssociationType.ObjectType = this;

			MediaMediaContent.RoleType.ObjectType = MediaContentClass.Instance;;
			MediaMediaContent.RoleType.AssignedSingularName = "MediaContent";
			MediaMediaContent.RoleType.AssignedPluralName = "MediaContents";
			this.Roles.MediaContent = MediaMediaContent.RoleType;


		}

		internal override void SetRelationTypes()
		{
			MediaClass.Instance.ConcreteRoles.MediaType = MediaClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MediaType]; 

			MediaClass.Instance.ConcreteRoles.MediaContent = MediaClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.MediaContent]; 


		}

		internal class RolesType
		{
			internal RoleType MediaType;
			internal RoleType MediaContent;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType MediaType;
			internal ConcreteRoleType MediaContent;
			internal ConcreteRoleType UniqueId;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class AccessControlledObjectInterface: Interface
	{
		public static AccessControlledObjectInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal AccessControlledObjectInterface() : base(BaseDomain.Instance, new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2"))
        {
			this.SingularName = "AccessControlledObject";
			this.PluralName = "AccessControlledObjects";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var AccessControlledObjectDeniedPermission = new RelationType(BaseDomain.Instance, new Guid("5c70ca14-4601-4c65-9b0d-cb189f90be27"), new Guid("267053f0-43b4-4cc7-a0e2-103992b2d0c5"), new Guid("867765fa-49dc-462f-b430-3c0e264c5283"));
			AccessControlledObjectDeniedPermission.AssignedMultiplicity = Multiplicity.ManyToMany;
			AccessControlledObjectDeniedPermission.IsIndexed = true;

			AccessControlledObjectDeniedPermission.AssociationType.ObjectType = this;

			AccessControlledObjectDeniedPermission.RoleType.ObjectType = PermissionClass.Instance;;
			AccessControlledObjectDeniedPermission.RoleType.AssignedSingularName = "DeniedPermission";
			AccessControlledObjectDeniedPermission.RoleType.AssignedPluralName = "DeniedPermissions";
			this.Roles.DeniedPermission = AccessControlledObjectDeniedPermission.RoleType;

			var AccessControlledObjectSecurityToken = new RelationType(BaseDomain.Instance, new Guid("b816fccd-08e0-46e0-a49c-7213c3604416"), new Guid("1739db0d-fe6b-42e1-a6a5-286536ff4f56"), new Guid("9f722315-385a-42ab-b84e-83063b0e5b0d"));
			AccessControlledObjectSecurityToken.AssignedMultiplicity = Multiplicity.ManyToMany;
			AccessControlledObjectSecurityToken.IsIndexed = true;

			AccessControlledObjectSecurityToken.AssociationType.ObjectType = this;

			AccessControlledObjectSecurityToken.RoleType.ObjectType = SecurityTokenClass.Instance;;
			AccessControlledObjectSecurityToken.RoleType.AssignedSingularName = "SecurityToken";
			AccessControlledObjectSecurityToken.RoleType.AssignedPluralName = "SecurityTokens";
			this.Roles.SecurityToken = AccessControlledObjectSecurityToken.RoleType;


		}

		internal override void SetRelationTypes()
		{
			GenderClass.Instance.ConcreteRoles.DeniedPermission = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			OrganisationClass.Instance.ConcreteRoles.DeniedPermission = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			UnitClass.Instance.ConcreteRoles.DeniedPermission = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			C1Class.Instance.ConcreteRoles.DeniedPermission = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			LocalisedTextClass.Instance.ConcreteRoles.DeniedPermission = LocalisedTextClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			SingletonClass.Instance.ConcreteRoles.DeniedPermission = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			LocaleClass.Instance.ConcreteRoles.DeniedPermission = LocaleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			LanguageClass.Instance.ConcreteRoles.DeniedPermission = LanguageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			UserGroupClass.Instance.ConcreteRoles.DeniedPermission = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			PermissionClass.Instance.ConcreteRoles.DeniedPermission = PermissionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			MediaTypeClass.Instance.ConcreteRoles.DeniedPermission = MediaTypeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			RoleClass.Instance.ConcreteRoles.DeniedPermission = RoleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			PrintQueueClass.Instance.ConcreteRoles.DeniedPermission = PrintQueueClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			CountryClass.Instance.ConcreteRoles.DeniedPermission = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			AccessControlClass.Instance.ConcreteRoles.DeniedPermission = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			PersonClass.Instance.ConcreteRoles.DeniedPermission = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			MediaClass.Instance.ConcreteRoles.DeniedPermission = MediaClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 
			CurrencyClass.Instance.ConcreteRoles.DeniedPermission = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.DeniedPermission]; 

			GenderClass.Instance.ConcreteRoles.SecurityToken = GenderClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			OrganisationClass.Instance.ConcreteRoles.SecurityToken = OrganisationClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			UnitClass.Instance.ConcreteRoles.SecurityToken = UnitClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			C1Class.Instance.ConcreteRoles.SecurityToken = C1Class.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			LocalisedTextClass.Instance.ConcreteRoles.SecurityToken = LocalisedTextClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			SingletonClass.Instance.ConcreteRoles.SecurityToken = SingletonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			LocaleClass.Instance.ConcreteRoles.SecurityToken = LocaleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			LanguageClass.Instance.ConcreteRoles.SecurityToken = LanguageClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			UserGroupClass.Instance.ConcreteRoles.SecurityToken = UserGroupClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			PermissionClass.Instance.ConcreteRoles.SecurityToken = PermissionClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			MediaTypeClass.Instance.ConcreteRoles.SecurityToken = MediaTypeClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			RoleClass.Instance.ConcreteRoles.SecurityToken = RoleClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			PrintQueueClass.Instance.ConcreteRoles.SecurityToken = PrintQueueClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			CountryClass.Instance.ConcreteRoles.SecurityToken = CountryClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			AccessControlClass.Instance.ConcreteRoles.SecurityToken = AccessControlClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			PersonClass.Instance.ConcreteRoles.SecurityToken = PersonClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			MediaClass.Instance.ConcreteRoles.SecurityToken = MediaClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 
			CurrencyClass.Instance.ConcreteRoles.SecurityToken = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.SecurityToken]; 


		}

		internal class RolesType
		{
			internal RoleType DeniedPermission;
			internal RoleType SecurityToken;

		}
	}public partial class ObjectStateInterface: Interface
	{
		public static ObjectStateInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal ObjectStateInterface() : base(BaseDomain.Instance, new Guid("f991813f-3146-4431-96d0-554aa2186887"))
        {
			this.SingularName = "ObjectState";
			this.PluralName = "ObjectStates";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("dd9d3cf5-5c9b-444a-a4b1-a4d807597cf6"))
			{
				Subtype = Instance,
				Supertype = UniquelyIdentifiableInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var ObjectStateDeniedPermission = new RelationType(BaseDomain.Instance, new Guid("59338f0b-40e7-49e8-ba1a-3ecebf96aebe"), new Guid("fca0f3f6-bdd6-4405-93b3-35dd769bff0e"), new Guid("c338f087-559c-4239-9c6a-1f691e58ed16"));
			ObjectStateDeniedPermission.AssignedMultiplicity = Multiplicity.ManyToMany;
			ObjectStateDeniedPermission.IsIndexed = true;

			ObjectStateDeniedPermission.AssociationType.ObjectType = this;

			ObjectStateDeniedPermission.RoleType.ObjectType = PermissionClass.Instance;;
			ObjectStateDeniedPermission.RoleType.AssignedSingularName = "DeniedPermission";
			ObjectStateDeniedPermission.RoleType.AssignedPluralName = "DeniedPermissions";
			this.Roles.DeniedPermission = ObjectStateDeniedPermission.RoleType;

			var ObjectStateName = new RelationType(BaseDomain.Instance, new Guid("b86f9e42-fe10-4302-ab7c-6c6c7d357c39"), new Guid("052ec640-3150-458a-99d5-0edce6eb6149"), new Guid("945cbba6-4b09-4b87-931e-861b147c3823"));
			ObjectStateName.AssignedMultiplicity = Multiplicity.OneToOne;
			ObjectStateName.IsIndexed = true;

			ObjectStateName.AssociationType.ObjectType = this;

			ObjectStateName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			ObjectStateName.RoleType.AssignedSingularName = "Name";
			ObjectStateName.RoleType.AssignedPluralName = "Names";
			ObjectStateName.RoleType.Size = 256;
			this.Roles.Name = ObjectStateName.RoleType;


		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
			internal RoleType DeniedPermission;
			internal RoleType Name;

		}
	}public partial class CurrencyClass : Class
	{
		public static CurrencyClass Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();
		internal readonly ConcreteRolesType ConcreteRoles = new ConcreteRolesType();

		internal CurrencyClass() : base(BaseDomain.Instance, new Guid("fd397adf-40b4-4ef8-b449-dd5a24273df3"))
        {
			this.SingularName = "Currency";
			this.PluralName = "Currencies";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};

			new Inheritance(BaseDomain.Instance, new Guid("09b3ac36-f944-4316-9749-99d4f12ffe74"))
			{
				Subtype = Instance,
				Supertype = AccessControlledObjectInterface.Instance
			};

		}

		internal override void BuildRelationTypes()
		{
			var CurrencyIsoCode = new RelationType(BaseDomain.Instance, new Guid("294a4bdc-f03a-47a2-a649-419e6b9021a3"), new Guid("f9eec7c6-c4cd-4d8c-a5f7-44855328cd7e"), new Guid("09d74027-4457-4788-803c-24b857245565"));
			CurrencyIsoCode.AssignedMultiplicity = Multiplicity.OneToOne;
			CurrencyIsoCode.AssociationType.ObjectType = this;

			CurrencyIsoCode.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CurrencyIsoCode.RoleType.AssignedSingularName = "IsoCode";
			CurrencyIsoCode.RoleType.AssignedPluralName = "IsoCodes";
			CurrencyIsoCode.RoleType.Size = 256;
			this.Roles.IsoCode = CurrencyIsoCode.RoleType;

			var CurrencyName = new RelationType(BaseDomain.Instance, new Guid("74c8308b-1b76-4218-9532-f01c9d1e146b"), new Guid("2cb43671-c648-4bd4-ac08-7302c29246e7"), new Guid("e7c93764-d634-4187-97ed-9248ea56bab2"));
			CurrencyName.AssignedMultiplicity = Multiplicity.OneToOne;
			CurrencyName.IsIndexed = true;

			CurrencyName.AssociationType.ObjectType = this;

			CurrencyName.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CurrencyName.RoleType.AssignedSingularName = "Name";
			CurrencyName.RoleType.AssignedPluralName = "Names";
			CurrencyName.RoleType.Size = 256;
			this.Roles.Name = CurrencyName.RoleType;

			var CurrencySymbol = new RelationType(BaseDomain.Instance, new Guid("82797074-8d6c-4d61-a885-34ae7133a503"), new Guid("0d4524d0-503f-494d-87a4-cbc239b278e1"), new Guid("43e13383-ea7f-4aa1-872c-eec0b53a998e"));
			CurrencySymbol.AssignedMultiplicity = Multiplicity.OneToOne;
			CurrencySymbol.AssociationType.ObjectType = this;

			CurrencySymbol.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CurrencySymbol.RoleType.AssignedSingularName = "Symbol";
			CurrencySymbol.RoleType.AssignedPluralName = "Symbols";
			CurrencySymbol.RoleType.Size = 256;
			this.Roles.Symbol = CurrencySymbol.RoleType;

			var CurrencyLocalisedName = new RelationType(BaseDomain.Instance, new Guid("e9fc0472-cf7a-4e02-b061-cb42b6f5c273"), new Guid("06b8f2b2-91f0-4b89-ae19-b47de4524556"), new Guid("e1301b8f-25cc-4ace-884e-79af1d303f53"));
			CurrencyLocalisedName.AssignedMultiplicity = Multiplicity.OneToMany;
			CurrencyLocalisedName.IsIndexed = true;

			CurrencyLocalisedName.AssociationType.ObjectType = this;

			CurrencyLocalisedName.RoleType.ObjectType = LocalisedTextClass.Instance;;
			CurrencyLocalisedName.RoleType.AssignedSingularName = "LocalisedName";
			CurrencyLocalisedName.RoleType.AssignedPluralName = "LocalisedNames";
			this.Roles.LocalisedName = CurrencyLocalisedName.RoleType;


		}

		internal override void SetRelationTypes()
		{
			CurrencyClass.Instance.ConcreteRoles.IsoCode = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.IsoCode]; 

			CurrencyClass.Instance.ConcreteRoles.Name = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Name]; 

			CurrencyClass.Instance.ConcreteRoles.Symbol = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.Symbol]; 

			CurrencyClass.Instance.ConcreteRoles.LocalisedName = CurrencyClass.Instance.ConcreteRoleTypeByRoleType[this.Roles.LocalisedName]; 


		}

		internal class RolesType
		{
			internal RoleType IsoCode;
			internal RoleType Name;
			internal RoleType Symbol;
			internal RoleType LocalisedName;

		}

		internal class ConcreteRolesType
		{
			internal ConcreteRoleType IsoCode;
			internal ConcreteRoleType Name;
			internal ConcreteRoleType Symbol;
			internal ConcreteRoleType LocalisedName;
			internal ConcreteRoleType DeniedPermission;
			internal ConcreteRoleType SecurityToken;

		}
	}public partial class CommentableInterface: Interface
	{
		public static CommentableInterface Instance {get; internal set;}

		internal readonly RolesType Roles = new RolesType();

		internal CommentableInterface() : base(BaseDomain.Instance, new Guid("fdd52472-e863-4e91-bb01-1dada2acc8f6"))
        {
			this.SingularName = "Commentable";
			this.PluralName = "Commentables";
        }

		internal override void BuildInheritances()
		{
			new Inheritance(BaseDomain.Instance, Guid.NewGuid())
			{
				Subtype = Instance,
				Supertype = ObjectInterface.Instance
			};


		}

		internal override void BuildRelationTypes()
		{
			var CommentableComment = new RelationType(BaseDomain.Instance, new Guid("d800f9a2-fadd-45f1-8731-4dac177c6b1b"), new Guid("d3aadbc5-e488-4346-ac34-9e14cb8d2350"), new Guid("8b41d441-cd12-49d0-823c-b8a3163baadc"));
			CommentableComment.AssignedMultiplicity = Multiplicity.OneToOne;
			CommentableComment.AssociationType.ObjectType = this;

			CommentableComment.RoleType.ObjectType = AllorsStringUnit.Instance;;
			CommentableComment.RoleType.AssignedSingularName = "Comment";
			CommentableComment.RoleType.AssignedPluralName = "Comments";
			CommentableComment.RoleType.Size = -1;
			this.Roles.Comment = CommentableComment.RoleType;


		}

		internal override void SetRelationTypes()
		{
		}

		internal class RolesType
		{
			internal RoleType Comment;

		}
	}
}