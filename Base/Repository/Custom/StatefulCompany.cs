namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("62859bfb-7949-4f7f-a428-658447576d0a")]
    #endregion
    public partial class StatefulCompany : Object 
    {

        #region Allors
        [Id("6c848eeb-7b42-45ea-81ac-fa983e1e0fa9")]
        [Indexed]
        #endregion
        public Person Employee { get; set; }
        #region Allors
        [Id("6e429d87-ea80-465e-9aa6-0f7d546b6bb3")]
        [Size(256)]
        #endregion
        public string Name { get; set; }
        #region Allors
        [Id("9940e8ed-189e-42c6-b0d1-7c01920b9fac")]
        [Indexed]
        #endregion
        public Person Manager { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}