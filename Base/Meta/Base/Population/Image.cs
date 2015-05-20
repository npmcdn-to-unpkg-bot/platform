namespace Allors.Meta
{
    using System;

    public partial class ImageClass
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("28950711-D4A2-4EAA-A0EE-7F1312A67F2E"))
            {
                ObjectType = Instance,
                Name = "CreateResponsive"
            };

            new MethodType(BaseDomain.Instance, new Guid("2716D4E4-52B2-471B-8241-1D6F1E2DD0E9"))
            {
                ObjectType = Instance,
                Name = "CreateThumbnail"
            };
        }
    }
}