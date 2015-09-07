namespace Allors.Meta
{
	#region Allors
	[Id("313b97a5-328c-4600-9dd2-b5bc146fb13b")]
	#endregion
	[Inherit(typeof(AccessControlledObjectInterface))]
	public partial class SingletonClass : Class
	{
		#region Allors
		[Id("64aed238-7009-4157-8395-7eb58ebf7889")]
		[AssociationId("2f79ecfe-5fd4-44d1-9c39-457bb3dc6815")]
		[RoleId("d861c8f8-7362-4805-9941-661a99ab11ac")]
		#endregion
		[Indexed]
		[Type(typeof(PrintQueueClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType DefaultPrintQueue;

		#region Allors
		[Id("9c1634ab-be99-4504-8690-ed4b39fec5bc")]
		[AssociationId("45a4205d-7c02-40d4-8d97-6d7d59e05def")]
		[RoleId("1e051b37-cf30-43ed-a623-dd2928d6d0a3")]
		#endregion
		[Indexed]
		[Type(typeof(LocaleClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType DefaultLocale;

		#region Allors
		[Id("9e5a3413-ed33-474f-adf2-149ad5a80719")]
		[AssociationId("33d5d8b9-3472-48d8-ab1a-83d00d9cb691")]
		[RoleId("e75a8956-4d02-49ba-b0cf-747b7a9f350d")]
		#endregion
		[Indexed]
		[Type(typeof(LocaleClass))]
		[Multiplicity(Multiplicity.OneToMany)]
		public RelationType Locale;

		#region Allors
		[Id("d9ea02e5-9aa1-4cbe-9318-06324529a923")]
		[AssociationId("6247e69d-4789-4ee0-a75b-c2de44a5fcce")]
		[RoleId("c11f31e1-75a7-4b23-9d58-7dfec256b658")]
		#endregion
		[Indexed]
		[Type(typeof(SecurityTokenClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType AdministratorSecurityToken;

		#region Allors
		[Id("f16652b0-b712-43d7-8d4e-34a22487514d")]
		[AssociationId("c92466b5-55ba-496a-8880-2821f32f8f8e")]
		[RoleId("3a12d798-40c3-40e0-ba9f-9d01b1e39e89")]
		#endregion
		[Type(typeof(UserInterface))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType Guest;

		#region Allors
		[Id("f579494b-e550-4be6-9d93-84618ac78704")]
		[AssociationId("33f17e75-99cc-417e-99f3-c29080f08f0a")]
		[RoleId("ca9e3469-583c-4950-ba2c-1bc3a0fc3e96")]
		#endregion
		[Indexed]
		[Type(typeof(SecurityTokenClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType DefaultSecurityToken;

		public static SingletonClass Instance {get; internal set;}

		internal SingletonClass() : base(MetaPopulation.Instance)
        {
        }
	}
}