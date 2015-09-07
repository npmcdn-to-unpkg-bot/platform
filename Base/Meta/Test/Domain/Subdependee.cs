namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("46a437d1-455b-4ddd-b83c-068938c352bd")]
	#endregion
	[Plural("Subdependees")]
	public partial class SubdependeeClass : Class
	{
		#region Allors
		[Id("194930f9-9c3f-458d-93ec-3d7bea4cd538")]
		[AssociationId("63ed21ba-b310-43fc-afed-a3eeea918204")]
		[RoleId("6765f2b5-bf55-4713-a693-946fc0846b27")]
		#endregion
		[Type(typeof(AllorsIntegerUnit))]
		[Plural("Subcounters")]
		public RelationType Subcounter;



		public static SubdependeeClass Instance {get; internal set;}

		internal SubdependeeClass() : base(MetaPopulation.Instance)
        {
        }
	}
}