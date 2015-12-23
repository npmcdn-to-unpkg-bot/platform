namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("46a437d1-455b-4ddd-b83c-068938c352bd")]
    #endregion
    public partial class Subdependee : Object 
    {

        #region Allors
        [Id("194930f9-9c3f-458d-93ec-3d7bea4cd538")]
        #endregion
        public int Subcounter { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}