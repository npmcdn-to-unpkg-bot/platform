namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("c1f169a1-553b-4a24-aba7-01e0b7102fe5")]
	#endregion
	[Plural("Seconds")]
	public partial class SecondClass : Class
	{
		#region Allors
		[Id("4f0eba0d-09b4-4bbc-8e42-15de94921ab5")]
		[AssociationId("08d8689d-88ce-496d-95e4-f20af0677cac")]
		[RoleId("ec263924-1234-4b53-9d33-91e167d6862f")]
		#endregion
		[Indexed]
		[Type(typeof(ThirdClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType Third;

		#region Allors
		[Id("8a7b7af9-f421-4e96-a1a7-04d4c4bdd1d7")]
		[AssociationId("e986349f-fc8c-4627-9bf7-966ad6299cff")]
		[RoleId("3f37f82c-3f7a-4d4c-b775-4ff09c105f92")]
		#endregion
		[Type(typeof(AllorsBooleanUnit))]
		[Plural("IsDeriveds")]
		public RelationType IsDerived;



		public static SecondClass Instance {get; internal set;}

		internal SecondClass() : base(MetaPopulation.Instance)
        {
        }
	}
}