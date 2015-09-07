namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("6217b428-4ad0-4f7f-ad4b-e334cf0b3ab1")]
	#endregion
	[Plural("Froms")]
	public partial class FromClass : Class
	{
		#region Allors
		[Id("d9a9896d-e175-410a-9916-9261d83aa229")]
		[AssociationId("a963f593-cad0-4fa9-96a3-3853f0f7d7c6")]
		[RoleId("775a29b8-6e21-4545-9881-d52f6eb7db8b")]
		#endregion
		[Indexed]
		[Type(typeof(ToClass))]
		[Plural("Tos")]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType To;



		public static FromClass Instance {get; internal set;}

		internal FromClass() : base(MetaPopulation.Instance)
        {
        }
	}
}