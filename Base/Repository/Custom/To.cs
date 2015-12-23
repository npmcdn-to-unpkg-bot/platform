namespace Allors.Repository.Domain
{
    #region Allors
    [Id("7eb25112-4b81-4e8d-9f75-90950c40c65f")]
    #endregion
    public partial class To : Object 
    {
        #region Allors
        [Id("4be564ac-77bc-48b8-b945-7d39f2ea9903")]
        [Size(256)]
        #endregion
        public string Name { get; set; }

        public void OnBuild() { }

        public void OnPostBuild() { }

        public void OnPreDerive() { }

        public void OnDerive() { }

        public void OnPostDerive() { }
    }
}