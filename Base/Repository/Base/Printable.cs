namespace Allors.Repository.Domain
{
	using System;

	public partial interface Printable :  Object, AccessControlledObject, UniquelyIdentifiable 
	{


        #region Allors
        [Id("c75d4e4c-d47c-4757-bcb0-25b6daedec9e")]
        [AssociationId("480b7df7-b463-4038-a48d-35b8a8af899e")]
        [RoleId("8d530dcd-2c3b-4d1d-8acc-9963338968ed")]
        [Derived]
        [Required]
        [Size(-1)]
        #endregion
		string PrintContent { get; set; }

	}
}