namespace Allors.Meta
{
    public partial class ObjectInterface
    {
        #region Allors
        [Id("76DE1BA6-735E-494D-BE8A-344E14580F57")]
        #endregion
        public MethodType OnBuild;

        #region Allors
        [Id("1989B10E-0293-4FDA-A32F-E4B7783CF430")]
        #endregion
        public MethodType OnPostBuild;

        #region Allors
        [Id("BF273E72-BBDD-47E8-8066-F66ABD2EF648")]
        #endregion
        public MethodType OnPreDerive;

        #region Allors
        [Id("5B41C8C1-A3EA-4E47-A432-8CC0B881A5C3")]
        #endregion
        public MethodType OnDerive;

        #region Allors
        [Id("EB0F0242-FDBF-423F-BB0C-FBE2F6E433EB")]
        #endregion
        public MethodType OnPostDerive;

        internal override void BaseExtend()
        {
        }
    }
}