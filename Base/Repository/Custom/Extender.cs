namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("830cdcb1-31f1-4481-8399-00c034661450")]
    #endregion
    public partial class Extender : Object 
    {

        #region Allors
        [Id("525bbc9e-d488-419f-ac02-0ab6ac409bac")]
        [Size(256)]
        #endregion
        public string AllorsString { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}