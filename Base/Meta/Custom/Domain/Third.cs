namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("39116edf-34cf-45a6-ac09-2e4f98f28e14")]
	#endregion
	[Plural("Thirds")]
	public partial class ThirdClass : Class
	{
		#region Allors
		[Id("6ab5a7af-a0f0-4940-9be3-6f6430a9e728")]
		[AssociationId("a18d4c53-ba36-4936-8650-0d90182e5948")]
		[RoleId("7866ac81-e84d-40c6-b9c0-5a038b1e838f")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("IsDeriveds")]
		public RelationType IsDerived;



		public static ThirdClass Instance {get; internal set;}

		internal ThirdClass() : base(MetaPopulation.Instance)
        {
        }
	}
}