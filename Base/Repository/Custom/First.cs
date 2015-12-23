namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("1937b42e-954b-4ef9-bc63-5b8ae7903e9d")]
    #endregion
    public partial class First : Object 
    {

        #region Allors
        [Id("24886999-11f0-408f-b094-14b36ac4129b")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public Second Second { get; set; }
        #region Allors
        [Id("b0274351-3403-4384-afb6-2cb49cd03893")]
        #endregion
        public bool CreateCycle { get; set; }
        #region Allors
        [Id("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278")]
        #endregion
        public bool IsDerived { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}