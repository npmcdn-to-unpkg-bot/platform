namespace Allors.Meta
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class OrganisationClass
    {
        internal override void BaseExtend()
        {
            new MethodType(BaseDomain.Instance, new Guid("55AAC529-BEAE-4D29-B069-DECDA86710A9"))
            {
                ObjectType = Instance,
                Name = "JustDoIt"
            };

            this.Roles.Description.DataTypeAttribute = new DataTypeAttribute(DataType.MultilineText);

            this.Roles.Information.DisplayAttribute = new DisplayAttribute{ Name = "Ik ben het label" };
            this.Roles.Information.DataTypeAttribute = new DataTypeAttribute(DataType.Html);
        }
    }
}