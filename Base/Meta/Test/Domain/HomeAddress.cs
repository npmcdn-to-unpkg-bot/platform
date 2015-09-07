namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("2561e93c-5b85-44fb-a924-a1c0d1f78846")]
	#endregion
	[Inherit(typeof(AddressInterface))]

	[Plural("HomeAddresses")]
	public partial class HomeAddressClass : Class
	{
		#region Allors
		[Id("6f0f42c4-9b47-47c2-a632-da8e08116be4")]
		[AssociationId("652a00b8-f708-4804-80b6-c1fe3211acf2")]
		[RoleId("fc273b47-d98a-4afd-90ba-574fbdbfb395")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("Street")]
		public RelationType Street;

		#region Allors
		[Id("b181d077-e897-4add-9456-67b9760d32e8")]
		[AssociationId("5eca1733-0f01-4141-b0d0-d7a2bfd90388")]
		[RoleId("d29dbed0-a68a-4075-b893-55e16e6335fd")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("HouseNumbers")]
		public RelationType HouseNumber;



		public static HomeAddressClass Instance {get; internal set;}

		internal HomeAddressClass() : base(MetaPopulation.Instance)
        {
        }
	}
}