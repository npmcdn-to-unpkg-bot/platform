namespace Allors.Meta
{
    using System;

    public partial class C1Class
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("A80E3732-DAF2-4AD4-9378-B4BC13E74DDE"))
            {
                ObjectType = Instance,
                Name = "ClassMethod"
            };

            new MethodType(BaseDomain.Instance, new Guid("30BF52C7-7DF6-4A34-8333-F114E2752905"))
            {
                ObjectType = Instance,
                Name = "Sum"
            };
        }
    }
}