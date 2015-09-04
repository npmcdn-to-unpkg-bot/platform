namespace Allors.Meta
{
    public partial class ObjectInterface
    {
        [Id("76DE1BA6-735E-494D-BE8A-344E14580F57")]
        public MethodType OnBuild;

        [Id("1989B10E-0293-4FDA-A32F-E4B7783CF430")]
        public MethodType OnPostBuild;

        [Id("BF273E72-BBDD-47E8-8066-F66ABD2EF648")]
        public MethodType OnPreDerive;

        [Id("5B41C8C1-A3EA-4E47-A432-8CC0B881A5C3")]
        public MethodType OnDerive;

        [Id("EB0F0242-FDBF-423F-BB0C-FBE2F6E433EB")]
        public MethodType OnPostDerive;

        internal override void BaseExtend()
        {
        }
    }
}