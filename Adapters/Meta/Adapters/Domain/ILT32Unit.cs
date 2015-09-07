namespace Allors.Meta
{
	#region Allors
	[Id("228fa79f-afa7-418c-968e-8c0d38fb3ad2")]
	#endregion
  	public partial class ILT32UnitInterface: Interface
	{
		#region Allors
		[Id("6822f677-7249-4c28-9b9c-18b21ba6f597")]
		[AssociationId("def04d80-9003-4b5a-bd92-331f7781b2be")]
		[RoleId("c11b4173-37b3-4d0e-8f4b-254c22f95fb3")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("AllorsStrings1")]
		public RelationType AllorsString1;

		#region Allors
		[Id("b2734796-7140-4830-a0de-88df7d27b6a8")]
		[AssociationId("182f98a8-db0b-4809-bb02-d1f3dea4d55f")]
		[RoleId("a89b3426-3e2b-450e-8137-a96a2563200d")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("AllorsStrings3")]
		public RelationType AllorsString3;

		#region Allors
		[Id("ced16c48-6301-4652-8dcb-ed8a80ea7ce4")]
		[AssociationId("1ce2f53e-fa39-4f01-b808-685e1ad8d23a")]
		[RoleId("47c7b66c-e3c2-46f6-bcb7-ad73f3a1a1ce")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("AllorsStrings2")]
		public RelationType AllorsString2;

		public static ILT32UnitInterface Instance {get; internal set;}

		internal ILT32UnitInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}