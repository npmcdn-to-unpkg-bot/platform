namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("4f53e1e7-e88a-4161-969c-1fed0b3a24a2")]
	#endregion
  	public partial class ILT32CompositeInterface: Interface
	{
		#region Allors
		[Id("be3fc71d-66d8-411f-ab5f-4ed91e437852")]
		[AssociationId("a0cba3a2-b964-46c0-9c84-0dcf4b7e91f7")]
		[RoleId("a418995c-57b6-4a7a-a619-bdf2a58a184f")]
		#endregion
		[Type(typeof(ILT32CompositeInterface))]
		[Plural("Selfs3")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Self3;

		#region Allors
		[Id("c03a8b50-7fd1-4304-9d45-2c699fcbee80")]
		[AssociationId("a0eb47f7-e308-4d59-b7ef-439def081e76")]
		[RoleId("17a1cd9e-2d03-476e-8e82-ce87230358aa")]
		#endregion
		[Type(typeof(ILT32CompositeInterface))]
		[Plural("Selfs2")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Self2;

		#region Allors
		[Id("d0eeeb45-97a6-465e-9a05-7e0fa970a969")]
		[AssociationId("31d93345-8969-448f-a5bb-c61df5f0ab34")]
		[RoleId("92ef1bdf-3c23-4f5a-a835-6f46f2ce49be")]
		#endregion
		[Type(typeof(ILT32CompositeInterface))]
		[Plural("Selfs1")]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Self1;



		public static ILT32CompositeInterface Instance {get; internal set;}

		internal ILT32CompositeInterface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "ILT32Composite";
			this.PluralName = "ILT32Composites";
        }
	}
}