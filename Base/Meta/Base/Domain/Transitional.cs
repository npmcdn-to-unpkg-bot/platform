namespace Allors.Meta
{
	#region Allors
	[Id("ab2179ad-9eac-4b61-8d84-81cd777c4926")]
	#endregion
	[Inherit(typeof(AccessControlledObjectInterface))]
  	public partial class TransitionalInterface: Interface
	{
		#region Allors
		[Id("6e27b0e8-2ac1-4ec8-90fe-6a38b7a2f690")]
		[AssociationId("52f880b3-09af-4842-bb44-e86cfd937e14")]
		[RoleId("2ff12d5c-2042-4b55-ba4b-1b6389a066c6")]
		#endregion
		[Derived]
		[Indexed]
		[Type(typeof(ObjectStateInterface))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType PreviousObjectState;

		#region Allors
		[Id("f0d9a21f-0570-4dca-9555-ccd8aabbb8d8")]
		[AssociationId("1e2268c1-badf-49bd-81fe-c6122cbd1f81")]
		[RoleId("d9c8465b-3f59-4985-9ff3-c04be6a242de")]
		#endregion
		[Derived]
		[Indexed]
		[Type(typeof(ObjectStateInterface))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType LastObjectState;

		public static TransitionalInterface Instance {get; internal set;}

		internal TransitionalInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}