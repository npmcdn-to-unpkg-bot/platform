namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("7ee3b00b-4e63-4774-b744-3add2c6035ab")]
	#endregion
	[Inherit(typeof(AddressInterface))]

	[Plural("MailboxAddresses")]
	public partial class MailboxAddressClass : Class
	{
		#region Allors
		[Id("03c9970e-d9d6-427d-83d0-00e0888f5588")]
		[AssociationId("8d565792-4315-44eb-9930-55aa30e8f23a")]
		[RoleId("10b46f89-7f3a-4571-8621-259a2a501dc7")]
		#endregion
		[Type(typeof(AllorsStringUnit))]
		[Size(256)]
		[Plural("PoBoxes")]
		public RelationType PoBox;



		public static MailboxAddressClass Instance {get; internal set;}

		internal MailboxAddressClass() : base(MetaPopulation.Instance)
        {
        }
	}
}