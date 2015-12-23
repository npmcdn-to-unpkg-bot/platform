namespace Allors.Repository.Domain
{
    using System;

    public partial interface UniquelyIdentifiable :  Object 
    {


        #region Allors
        [Id("e1842d87-8157-40e7-b06e-4375f311f2c3")]
        [AssociationId("fe413e96-cfcf-4e8d-9f23-0fa4f457fdf1")]
        [RoleId("d73fd9a4-13ee-4fa9-8925-d93eca328bf6")]
        [Indexed]
        [Required]
        #endregion
        Guid UniqueId { get; set; }

    }
}