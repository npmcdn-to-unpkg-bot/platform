namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("0c50c02a-cc9c-4617-8530-15a24d4ac969")]
    #endregion
    public partial class StringTemplate :  Object, UniquelyIdentifiable, Localised 
    {
        #region inherited properties
        public Guid UniqueId { get; set; }

        public Locale Locale { get; set; }

        #endregion

        #region Allors
        [Id("2f88f9f8-3c22-40d3-885c-2abd43af96cc")]
        [AssociationId("9ad9b285-2a91-4bd9-90dd-8f963ef0a465")]
        [RoleId("3fcb83d0-11c5-48ba-ba9c-5126f0b4e9f4")]
        [Size(-1)]
        #endregion
        public string Body { get; set; }
        #region Allors
        [Id("c501103b-037a-4961-93df-2dbb74b88a76")]
        [AssociationId("1bcdddcc-e462-4d59-af2d-7346245cb271")]
        [RoleId("37bd5d22-89f1-47a4-b6bd-8841e194b213")]
        [Required]
        [Size(256)]
        #endregion
        public string Name { get; set; }


        #region inherited methods


        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}


        #endregion

    }
}