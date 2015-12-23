namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("2cc9bde1-80da-4159-bb20-219074266101")]
    #endregion
    public partial class Dependee : Object 
    {

        #region Allors
        [Id("1b8e0350-c446-48dc-85c0-71130cc1490e")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public Subdependee Subdependee { get; set; }
        #region Allors
        [Id("c1e86449-e5a8-4911-97c7-b03de9142f98")]
        #endregion
        public int Subcounter { get; set; }
        #region Allors
        [Id("d58d1f28-3abd-4294-abde-885bdd16f466")]
        #endregion
        public int Counter { get; set; }
        #region Allors
        [Id("e73b8fc5-0148-486a-9379-cfb051b303d2")]
        #endregion
        public bool DeleteDependent { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}

    }
}