namespace Allors.Meta
{
    using System;

    public partial class I1Interface
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("336DC840-BDD8-45CC-8B95-DD0EA55F130D"))
            {
                ObjectType = Instance,
                Name = "InterfaceMethod"
            };
        }
    }
}