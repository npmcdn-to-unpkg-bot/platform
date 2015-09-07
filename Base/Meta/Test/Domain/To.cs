namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("7eb25112-4b81-4e8d-9f75-90950c40c65f")]
	#endregion
	[Plural("Tos")]
	public partial class ToClass : Class
	{
		#region Allors
		[Id("4be564ac-77bc-48b8-b945-7d39f2ea9903")]
		[AssociationId("7a6714c1-e58a-45ac-8ee5-ca5f22b6d528")]
		[RoleId("53e0761a-a9f1-4516-a086-b766650ac28b")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("Names")]
		public RelationType Name;



		public static ToClass Instance {get; internal set;}

		internal ToClass() : base(MetaPopulation.Instance)
        {
        }
	}
}