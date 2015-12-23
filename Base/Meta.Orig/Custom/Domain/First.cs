namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("1937b42e-954b-4ef9-bc63-5b8ae7903e9d")]
	#endregion
	[Plural("Firsts")]
	public partial class FirstClass : Class
	{
		#region Allors
		[Id("24886999-11f0-408f-b094-14b36ac4129b")]
		[AssociationId("e48ab2ee-c7a5-4d9a-b3ab-263f6aa4cdd1")]
		[RoleId("cf5c725d-e567-44de-ab5b-b47bb0bf8647")]
		#endregion
		[Indexed]
		[Type(typeof(SecondClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType Second;

		#region Allors
		[Id("b0274351-3403-4384-afb6-2cb49cd03893")]
		[AssociationId("ec145229-e33a-4807-a0dd-48778cc88ac7")]
		[RoleId("12c46bf1-eed0-4e2a-b704-5d40032b4911")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("CreateCycles")]
		public RelationType CreateCycle;

		#region Allors
		[Id("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278")]
		[AssociationId("ae9f23b5-20a7-4ecc-b642-503d75c486f1")]
		[RoleId("eb6b0565-1440-4b9b-aa23-51cfae3f93dd")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("IsDeriveds")]
		public RelationType IsDerived;



		public static FirstClass Instance {get; internal set;}

		internal FirstClass() : base(MetaPopulation.Instance)
        {
        }
	}
}