namespace Allors.Meta
{
    using System;

    public partial class S1Interface
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("5C7F1AB4-0B61-416D-97F4-660663F0E933"))
            {
                ObjectType = Instance,
                Name = "SuperinterfaceMethod"
            };
        }
    }
}