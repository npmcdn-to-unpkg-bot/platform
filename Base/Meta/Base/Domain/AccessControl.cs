namespace Allors.Meta
{
	#region Allors
	[Id("c4d93d5e-34c3-4731-9d37-47a8e801d9a8")]
	#endregion
	[Inherit(typeof(DeletableInterface))]
	[Inherit(typeof(AccessControlledObjectInterface))]
	public partial class AccessControlClass : Class
	{
		#region Allors
		[Id("0dbbff5c-3dca-4257-b2da-442d263dcd86")]
		[AssociationId("92e8d639-9205-422b-b4ff-c7e8c2980abf")]
		[RoleId("2d9b7157-5409-40d3-bd3e-6911df9aface")]
		#endregion
		[Indexed]
		[Type(typeof(UserGroupClass))]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType SubjectGroup;

		#region Allors
		[Id("37dd1e27-ba75-404c-9410-c6399d28317c")]
		[AssociationId("3d74101d-97bc-46fb-9548-96cb7aa01b39")]
		[RoleId("e0303a17-bf7a-4a7f-bb4b-0a447c56aaaf")]
		#endregion
		[Indexed]
		[Type(typeof(UserInterface))]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType Subject;

		#region Allors
		[Id("6503574b-8bab-4da8-a19d-23a9bcffe01e")]
		[AssociationId("cae9e5c2-afa1-46f4-b930-69d4e810038f")]
		[RoleId("ab2b4b9c-87dd-4712-b123-f5f9271c6193")]
		#endregion
		[Indexed]
		[Type(typeof(SecurityTokenClass))]
		[Multiplicity(Multiplicity.ManyToMany)]
		public RelationType Object;

		#region Allors
		[Id("69a9dae8-678d-4c1c-a464-2e5aa5caf39e")]
		[AssociationId("ec79e57d-be81-430a-b12f-08ffd1e94af3")]
		[RoleId("370d3d12-3164-4753-8a72-1c604bda1b64")]
		#endregion
		[Type(typeof(RoleClass))]
		[Multiplicity(Multiplicity.ManyToOne)]
		public RelationType Role;

		#region Allors
		[Id("f4763e29-6a7b-4c66-b59b-c80149df5861")]
		[AssociationId("d3b3ee52-c9ea-4d8a-84a7-5915f7e4fba7")]
		[RoleId("a4197a6a-9070-4e5e-a8f9-7077574da8db")]
		#endregion
		[Type(typeof(AllorsUniqueUnit))]
		public RelationType CacheId;
        
		public static AccessControlClass Instance {get; internal set;}

		internal AccessControlClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void BaseExtend()
        {
            this.CacheId.RoleType.IsRequired = true;
            this.Role.RoleType.IsRequired = true;
            this.Object.RoleType.IsRequired = true;
        }
    }
}