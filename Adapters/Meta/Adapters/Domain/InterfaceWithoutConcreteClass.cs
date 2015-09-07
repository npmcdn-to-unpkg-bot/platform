namespace Allors.Meta
{
	#region Allors
	[Id("2f4bc713-47c9-4e07-9f2b-1d22a0cb4fad")]
	#endregion
  	public partial class InterfaceWithoutConcreteClassInterface: Interface
	{
		#region Allors
		[Id("b490715d-e318-471b-bd37-1c1e12c0314e")]
		[AssociationId("6730e78c-e678-4763-aa98-a5de1be1500c")]
		[RoleId("e7edc290-a280-40dc-acc6-a6b7ebbb09b0")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		public RelationType AllorsBoolean;

		public static InterfaceWithoutConcreteClassInterface Instance {get; internal set;}

		internal InterfaceWithoutConcreteClassInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}