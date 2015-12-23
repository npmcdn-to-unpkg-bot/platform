namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("c1f169a1-553b-4a24-aba7-01e0b7102fe5")]
    #endregion
    public partial class Second : Object 
    {

        #region Allors
        [Id("4f0eba0d-09b4-4bbc-8e42-15de94921ab5")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public Third Third { get; set; }
        #region Allors
        [Id("8a7b7af9-f421-4e96-a1a7-04d4c4bdd1d7")]
        #endregion
        public bool IsDerived { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}