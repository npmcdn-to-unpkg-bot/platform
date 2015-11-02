namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("830cdcb1-31f1-4481-8399-00c034661450")]
	#endregion
	[Plural("Extenders")]
	public partial class ExtenderClass : Class
	{
		#region Allors
		[Id("525bbc9e-d488-419f-ac02-0ab6ac409bac")]
		[AssociationId("7dcdf3d7-25ad-4e8f-9634-63b771990681")]
		[RoleId("bf9f7482-5277-40db-a6ac-5d4731cb5537")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;



		public static ExtenderClass Instance {get; internal set;}

		internal ExtenderClass() : base(MetaPopulation.Instance)
        {
        }
	}
}