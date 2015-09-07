namespace Allors.Meta
{
	#region Allors
	[Id("29cb9717-2452-4da0-9a29-8bd5d815307a")]
	#endregion
  	public partial class I23Interface: Interface
	{
		#region Allors
		[Id("0407c93e-f2ea-49e4-8779-44b42c554e60")]
		[AssociationId("9eda27ec-db3f-420a-b9ed-4742b7105bf5")]
		[RoleId("1c1d8356-9240-4582-a3ab-a8a1a2553869")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		public RelationType AllorsString;

        public static I23Interface Instance {get; internal set;}

		internal I23Interface() : base(MetaPopulation.Instance)
        {
        }
	}
}