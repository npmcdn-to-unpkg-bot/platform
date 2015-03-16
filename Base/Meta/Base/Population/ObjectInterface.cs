namespace Allors.Meta
{
    using System;

    public partial class ObjectInterface
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("62D48A76-A500-4D16-9D20-6FEF43AC6DCB"))
            {
                ObjectType = Instance,
                Name = "OnPostBuild"
            };

            new MethodType(BaseDomain.Instance, new Guid("042375D8-BBDD-46E8-80B6-CC89D8782F1C"))
            {
                ObjectType = Instance,
                Name = "ApplySecurityOnPostBuild"
            };

            new MethodType(BaseDomain.Instance, new Guid("122D3D78-AB97-4A69-A725-F465C71757DA"))
            {
                ObjectType = Instance,
                Name = "PrepareDerivation"
            };

            new MethodType(BaseDomain.Instance, new Guid("527DA7F8-68B4-46AB-B0D8-6B9E82D2A5AC"))
            {
                ObjectType = Instance,
                Name = "Derive"
            };

            new MethodType(BaseDomain.Instance, new Guid("349CBCDE-B4E9-4965-B3FF-7C41B021825D"))
            {
                ObjectType = Instance,
                Name = "OnDerived"
            };
        }
    }
}