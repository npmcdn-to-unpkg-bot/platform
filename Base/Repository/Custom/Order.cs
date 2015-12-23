namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("94be4938-77c1-488f-b116-6d4daeffcc8d")]
    #endregion
    public partial class Order : Object, Transitional 
    {
        public ObjectState PreviousObjectState { get; set; }


        public ObjectState LastObjectState { get; set; }


        public Permission[] DeniedPermissions { get; set; }


        public SecurityToken[] SecurityTokens { get; set; }


        #region Allors
        [Id("26560f5b-9552-42ea-861f-8a653abeb16e")]
        [Indexed]
        #endregion
        public OrderObjectState CurrentObjectState { get; set; }
        #region Allors
        [Id("5aa7fa5c-c0a5-4384-9b24-9ecef17c4848")]
        #endregion
        public int Amount { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}



    }
}