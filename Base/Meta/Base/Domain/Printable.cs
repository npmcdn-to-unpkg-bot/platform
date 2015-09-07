namespace Allors.Meta
{
	#region Allors
	[Id("61207a42-3199-4249-baa4-9dd11dc0f5b1")]
	#endregion
	[Inherit(typeof(AccessControlledObjectInterface))]
	[Inherit(typeof(UniquelyIdentifiableInterface))]
  	public partial class PrintableInterface: Interface
	{
		#region Allors
		[Id("c75d4e4c-d47c-4757-bcb0-25b6daedec9e")]
		[AssociationId("480b7df7-b463-4038-a48d-35b8a8af899e")]
		[RoleId("8d530dcd-2c3b-4d1d-8acc-9963338968ed")]
		#endregion
		[Derived]
		[Type(typeof(AllorsStringUnit))]
		[Size(-1)]
		public RelationType PrintContent;
        
		public static PrintableInterface Instance {get; internal set;}

		internal PrintableInterface() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.PrintContent.RoleType.IsRequired = true;
        }
    }
}