namespace Allors.Meta
{
	public partial class SingletonClass : Class
	{
		#region Allors
		[Id("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236")]
		[AssociationId("04bc6904-bd6e-4401-9720-088ebf1fb392")]
		[RoleId("7ab62a77-c098-4ad6-836d-53ae820df951")]
		#endregion
		[Indexed]
		[Type(typeof(StringTemplateClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType PersonTemplate;
	}
}