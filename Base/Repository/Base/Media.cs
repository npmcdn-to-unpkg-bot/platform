namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("da5b86a3-4f33-4c0d-965d-f4fbc1179374")]
	#endregion
    public partial class Media :  Object, UniquelyIdentifiable, AccessControlledObject, Deletable 
    {
        #region inherited properties
        public Guid UniqueId { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("49481792-06f0-49a1-b32f-28d265815a24")]
        [AssociationId("7ca17a9e-0b68-445f-8080-84b08ca0eb2d")]
        [RoleId("f1008c56-b375-4aa8-ac7e-c1f7ef9b2080")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Indexed]
        [Required]
        #endregion
        public MediaType MediaType { get; set; }
        #region Allors
        [Id("67082a51-1502-490b-b8db-537799e550bd")]
        [AssociationId("e8537dcf-1bd7-46c4-a37c-077bee6a78a1")]
        [RoleId("02fe1ce8-c019-4a40-bd6f-b38d2f47a288")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Derived]
        [Indexed]
        [Required]
        #endregion
        public MediaContent MediaContent { get; set; }


        #region inherited methods


        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}




        public void Delete(){}
        #endregion

    }
}