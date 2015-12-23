namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("bdaed62e-6369-46c0-a379-a1eef81b1c3d")]
    #endregion
    public partial class Three : Object, Shared 
    {

        #region Allors
        [Id("1697f09c-0d3d-4e5e-9f3f-9d3ae0718fd3")]
        [Indexed]
        #endregion
        public Four Four { get; set; }
        #region Allors
        [Id("4ace9948-4a22-465c-aa40-61c8fd65784d")]
        [Size(-1)]
        #endregion
        public string AllorsString { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}


    }
}