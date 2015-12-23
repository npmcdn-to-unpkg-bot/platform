namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("849393ed-cff6-40da-9b4d-483f045f2e99")]
    #endregion
    public partial class OrderObjectState : Object, ObjectState 
    {
        public Permission[] DeniedPermissions { get; set; }


        public string Name { get; set; }


        public Guid UniqueId { get; set; }



        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}



    }
}