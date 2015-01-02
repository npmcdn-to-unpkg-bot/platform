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
        }
    }
}