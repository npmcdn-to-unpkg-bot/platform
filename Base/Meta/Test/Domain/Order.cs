namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("94be4938-77c1-488f-b116-6d4daeffcc8d")]
	#endregion
	[Inherit(typeof(TransitionalInterface))]

	[Plural("Orders")]
	public partial class OrderClass : Class
	{
		#region Allors
		[Id("26560f5b-9552-42ea-861f-8a653abeb16e")]
		[AssociationId("d0cdd4a7-6323-4571-85e0-875a5adc56f7")]
		[RoleId("f97ce5e4-88e2-4a4f-a26c-01a68db33efa")]
		#endregion
		[Indexed]
		[Type(typeof(OrderObjectStateClass))]
		[Plural("CurrentObjectStates")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType CurrentObjectState;

		#region Allors
		[Id("5aa7fa5c-c0a5-4384-9b24-9ecef17c4848")]
		[AssociationId("ffcb8a00-571f-4032-b038-82b438f96f74")]
		[RoleId("cf1629aa-2aa0-4dc3-9873-fbf3008352ac")]
		#endregion
		[Type(typeof(AllorsIntegerUnit))]
		[Plural("Amounts")]
		public RelationType Amount;



		public static OrderClass Instance {get; internal set;}

		internal OrderClass() : base(MetaPopulation.Instance)
        {
        }
	}
}