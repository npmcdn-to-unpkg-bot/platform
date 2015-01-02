namespace Allors.Meta
{
    using System;

    public partial class DeletableInterface
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("F4CC201F-D6CB-4D82-8AEB-E9C4C79C33F7"))
            {
                ObjectType = Instance,
                Name = "Delete"
            };
        }
    }
}