namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("0d6bc154-112b-4a58-aa96-3b2a96f82523")]
	#endregion
	public partial class UserClass : Class
	{
		#region Allors
		[Id("1ffa3cb7-41f0-406a-a3a5-2f3a4c5ad59c")]
		[AssociationId("5b87b0d4-3bad-499d-96f1-9d39ab58d1e8")]
		[RoleId("939e2772-0bf6-4867-ae7d-3331ab395ba7")]
		#endregion
		[Indexed]
		[Type(typeof(UserClass))]
		[Plural("Selects")]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType Select;

		#region Allors
		[Id("bc6b71a8-2a66-4b57-9c86-ecf521b973ba")]
		[AssociationId("36058495-3b0d-416b-b2fb-cfe06e88035c")]
		[RoleId("4ed76e62-3de2-415f-896e-c90d1f32e129")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("Froms")]
		public RelationType From;



		public static UserClass Instance {get; internal set;}

		internal UserClass() : base(MetaPopulation.Instance)
        {
			this.SingularName = "User";
			this.PluralName = "Users";
        }
	}
}