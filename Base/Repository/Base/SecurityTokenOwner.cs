namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("a69cad9c-c2f1-463f-9af1-873ce65aeea6")]
    #endregion
	public partial interface SecurityTokenOwner :  Object 
    {


        #region Allors
        [Id("5fb15e8b-011c-46f7-83dd-485d4cc4f9f2")]
        [AssociationId("cdc21c1c-918e-4622-a01f-a3de06a8c802")]
        [RoleId("2acda9b3-89e8-475f-9d70-b9cde334409c")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Derived]
        [Indexed]
        [Required]
        #endregion
        SecurityToken OwnerSecurityToken { get; set; }


        #region Allors
        [Id("056914ed-a658-4ae5-b859-97300e1b8911")]
        [AssociationId("04b811b2-71b1-46a9-9ef5-1c061a035f07")]
        [RoleId("ea2ecc92-0657-4ae9-a21d-4487353e7d00")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Derived]
        [Indexed]
        #endregion
        AccessControl OwnerAccessControl { get; set; }

    }
}