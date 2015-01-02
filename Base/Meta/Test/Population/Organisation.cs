namespace Allors.Meta
{
    using System;

    public partial class OrganisationClass
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("55AAC529-BEAE-4D29-B069-DECDA86710A9"))
            {
                ObjectType = Instance,
                Name = "JustDoIt"
            };
        }
    }
}