namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("5d9b9cad-3720-47c3-9693-289698bf3dd0")]
    #endregion
    public partial class One : Object, Shared 
    {

        #region Allors
        [Id("448878af-c992-4256-baa7-239335a26bc6")]
        [Indexed]
        #endregion
        public Two Two { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}


    }
}