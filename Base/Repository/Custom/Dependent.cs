namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("0cb8d2a7-4566-432f-9882-893b05a77f44")]
    #endregion
    public partial class Dependent : Object, Deletable 
    {

        #region Allors
        [Id("8859af04-ba38-42ce-8ac9-f428c3f92f31")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public Dependee Dependee { get; set; }
        #region Allors
        [Id("9884955e-74ed-4f9d-9362-8e0274c53bf9")]
        #endregion
        public int Counter { get; set; }
        #region Allors
        [Id("e971733a-c381-4b5e-8e62-6bbd6d285bd7")]
        #endregion
        public int Subcounter { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}
        public void Delete(){}

    }
}