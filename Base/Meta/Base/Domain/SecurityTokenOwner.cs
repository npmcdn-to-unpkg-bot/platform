namespace Allors.Meta
{
	#region Allors
	[Id("a69cad9c-c2f1-463f-9af1-873ce65aeea6")]
	#endregion
  	public partial class SecurityTokenOwnerInterface: Interface
	{
		#region Allors
		[Id("5fb15e8b-011c-46f7-83dd-485d4cc4f9f2")]
		[AssociationId("cdc21c1c-918e-4622-a01f-a3de06a8c802")]
		[RoleId("2acda9b3-89e8-475f-9d70-b9cde334409c")]
		#endregion
		[Derived]
		[Indexed]
		[Type(typeof(SecurityTokenClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType OwnerSecurityToken;

		public static SecurityTokenOwnerInterface Instance {get; internal set;}

		internal SecurityTokenOwnerInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}