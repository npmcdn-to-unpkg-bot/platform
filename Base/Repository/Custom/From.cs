namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("6217b428-4ad0-4f7f-ad4b-e334cf0b3ab1")]
    #endregion
    public partial class From : Object 
    {

        #region Allors
        [Id("d9a9896d-e175-410a-9916-9261d83aa229")]
        [Indexed]
        #endregion
        public To[] Tos { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}