namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("7ee3b00b-4e63-4774-b744-3add2c6035ab")]
    #endregion
    public partial class MailboxAddress : Object, Address 
    {
        public Place Place { get; set; }


        #region Allors
        [Id("03c9970e-d9d6-427d-83d0-00e0888f5588")]
        [Size(256)]
        #endregion
        public string PoBox { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}


    }
}