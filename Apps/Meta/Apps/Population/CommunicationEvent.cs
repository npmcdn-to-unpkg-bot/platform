using System.ComponentModel.DataAnnotations;

namespace Allors.Meta
{
    using System;

    public partial class CommunicationEventInterface
    {
        internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("433211EF-4376-451E-863F-376F5EC66758")) { ObjectType = this, Name = "Cancel" };
            new MethodType(AppsDomain.Instance, new Guid("53138963-6B25-4A90-BFE3-89B77AF73329")) { ObjectType = this, Name = "Close" };
            new MethodType(AppsDomain.Instance, new Guid("0E18F37B-39AA-452A-8085-6BD8AA686D33")) { ObjectType = this, Name = "Reopen" };


            this.Roles.Description.DataTypeAttribute = new DataTypeAttribute(DataType.MultilineText);
            this.Roles.Note.DataTypeAttribute = new DataTypeAttribute(DataType.MultilineText);

            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.Subject.IsRequired = true;
        }
    }
}