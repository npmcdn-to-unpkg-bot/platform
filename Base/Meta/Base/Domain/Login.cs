namespace Allors.Meta
{
	#region Allors
	[Id("ad7277a8-eda4-4128-a990-b47fe43d120a")]
	#endregion
	[Inherit(typeof(DeletableInterface))]
	public partial class LoginClass : Class
	{
		#region Allors
		[Id("18262218-a14f-48c3-87a5-87196d3b5974")]
		[AssociationId("3f067cf5-4fcb-4be4-9afb-15ba37700658")]
		[RoleId("e5393717-f46c-4a4c-a87f-3e4684428860")]
		#endregion
		[Indexed]
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType Key;

		#region Allors
		[Id("7a82e721-d0b7-4567-aaef-bd3987ae6d01")]
		[AssociationId("2f2ef41d-8310-45fd-8ab5-e5d067862e3d")]
		[RoleId("c8e3851a-bc57-4acb-934a-4adfc37b1da7")]
		#endregion
		[Indexed]
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType Provider;

		#region Allors
		[Id("c2d950ad-39d3-40f1-8817-11a026e9890b")]
		[AssociationId("e8091111-9f92-41a9-b4b1-4e8f277ea575")]
		[RoleId("150daf84-13ce-4b5f-83e6-64c7ef4f81c6")]
		#endregion
		[Indexed]
		[Type(typeof(UserInterface))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType User;

		public static LoginClass Instance {get; internal set;}

		internal LoginClass() : base(MetaPopulation.Instance)
        {
        }
	}
}