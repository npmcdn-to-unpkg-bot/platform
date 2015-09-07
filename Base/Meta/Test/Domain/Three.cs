namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("bdaed62e-6369-46c0-a379-a1eef81b1c3d")]
	#endregion
	[Inherit(typeof(SharedInterface))]

	[Plural("Threes")]
	public partial class ThreeClass : Class
	{
		#region Allors
		[Id("1697f09c-0d3d-4e5e-9f3f-9d3ae0718fd3")]
		[AssociationId("dc813d9a-84e9-4995-8d2c-0ef449b12024")]
		[RoleId("25737278-d039-47c5-8749-19f22ad7a4c3")]
		#endregion
		[Indexed]
		[Type(typeof(FourClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Four;

		#region Allors
		[Id("4ace9948-4a22-465c-aa40-61c8fd65784d")]
		[AssociationId("6e20b25f-3ecd-447e-8a93-3977a53452b6")]
		[RoleId("f8f85b3d-371c-42df-8414-cf034c339917")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(-1)]
		public RelationType AllorsString;



		public static ThreeClass Instance {get; internal set;}

		internal ThreeClass() : base(MetaPopulation.Instance)
        {
        }
	}
}