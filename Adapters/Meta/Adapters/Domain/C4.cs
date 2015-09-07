namespace Allors.Meta
{
	#region Allors
	[Id("20049a79-20c7-478b-a5ba-c54b1e615168")]
	#endregion
	[Inherit(typeof(I4Interface))]
	[Inherit(typeof(I34Interface))]
	public partial class C4Class : Class
	{
		#region Allors
		[Id("9f24fc51-8568-4ffc-b47a-c5c317d00954")]
		[AssociationId("77d762d7-4676-4b02-8319-11600c4314f3")]
		[RoleId("6e74ef8d-d748-4142-8073-afbf5534c43f")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;

		public static C4Class Instance {get; internal set;}

		internal C4Class() : base(MetaPopulation.Instance)
        {
        }
	}
}