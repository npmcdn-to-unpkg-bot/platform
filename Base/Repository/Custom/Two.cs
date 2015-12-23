namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("9ec7e136-815c-4726-9991-e95a3ec9e092")]
    #endregion
    public partial class Two : Object, Shared 
    {

        #region Allors
        [Id("8930c13c-ad5a-4b0e-b3bf-d7cdf6f5b867")]
        [Indexed]
        #endregion
        public Shared Shared { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}


    }
}