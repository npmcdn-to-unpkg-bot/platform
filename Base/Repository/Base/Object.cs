namespace Allors.Repository.Domain
{
    public partial interface Object
    {
        [Id("FDD32313-CF62-4166-9167-EF90BE3A3C75")]
        void OnBuild();

        [Id("2B827E22-155D-4AA8-BA9F-46A64D7C79C8")]
        void OnPostBuild();

        [Id("B33F8EAE-17DC-4BF9-AFBB-E7FC38F42695")]
        void OnPreDerive();

        [Id("C107F8B3-12DC-4FF9-8CBF-A7DEC046244F")]
        void OnDerive();

        [Id("07AFF35D-F4CB-48FE-A39A-176B1931FAB7")]
        void OnPostDerive();
    }
}