namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("7ba2ab26-491b-49eb-944c-26f6bb66e50f")]
	#endregion
  	public partial class ISandboxInterface: Interface
	{
		#region Allors
		[Id("38361bff-62b3-4607-8291-cfdaeedbd36d")]
		[AssociationId("f5403207-14c6-422e-9139-92e1c46ea15b")]
		[RoleId("675e80d6-5718-4a84-aef0-92ccf07dcdc7")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("InvisibleValues")]
		public RelationType InvisibleValue;

		#region Allors
		[Id("796ab057-88a0-4d71-bc4a-2673a209161b")]
		[AssociationId("34a3ba9b-6ba6-4cbd-977b-bb22b0ea7c10")]
		[RoleId("26fa08b3-598d-4985-9021-02c422fa4494")]
		#endregion
		[Type(typeof(ISandboxInterface))]
		[Plural("InvisibleManies")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType InvisibleMany;

		#region Allors
		[Id("dba5deb2-880d-47f4-adae-0b3125ff1379")]
		[AssociationId("8ad9a7aa-095e-43d9-aa4e-f21f7c70fdbb")]
		[RoleId("3e8d7881-8112-4001-a518-3fcef1a24615")]
		#endregion
		[Type(typeof(ISandboxInterface))]
		[Plural("InvisibleOnes")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType InvisibleOne;



		public static ISandboxInterface Instance {get; internal set;}

		internal ISandboxInterface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "ISandbox";
			this.PluralName = "ISandboxes";
        }
	}
}