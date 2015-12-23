namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("320985b6-d571-4b6c-b940-e02c04ad37d3")]
    #endregion
    public partial class SimpleJob : Object 
    {

        #region Allors
        [Id("7cd27660-13c6-4a15-8fd8-5775920cfd28")]
        #endregion
        public int Index { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}