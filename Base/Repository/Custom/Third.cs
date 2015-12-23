namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("39116edf-34cf-45a6-ac09-2e4f98f28e14")]
    #endregion
    public partial class Third : Object 
    {

        #region Allors
        [Id("6ab5a7af-a0f0-4940-9be3-6f6430a9e728")]
        #endregion
        public bool IsDerived { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}