namespace Allors.Meta
{
	#region Allors
	[Id("6a082a25-a8f2-4acd-a1a3-ba4461b729f1")]
	#endregion
	[Inherit(typeof(NamedInterface))]
	public partial class PersonClass : Class
	{
		#region Allors
		[Id("25ff791d-9547-41ba-ac34-f2fe501ef217")]
		[AssociationId("1a7f499a-86cc-4db1-89b7-decd4362c178")]
		[RoleId("36743f8f-afc2-4b8b-b9e2-eb0f0e725b72")]
		#endregion
		[Type(typeof(PersonClass))]
		[Plural("NextPersons")]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType NextPerson;

		#region Allors
		[Id("6cc83cb8-cb94-4716-bb7d-e25201f06b20")]
		[AssociationId("1074f507-e2d7-4b5f-8170-f7ca54a946c8")]
		[RoleId("9959f5b6-cf68-48f9-91ae-ed98f691f16c")]
		#endregion
		[Indexed]
		[Type(typeof(CompanyClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Company;
        
		public static PersonClass Instance {get; internal set;}

		internal PersonClass() : base(MetaPopulation.Instance)
        {
        }
	}
}