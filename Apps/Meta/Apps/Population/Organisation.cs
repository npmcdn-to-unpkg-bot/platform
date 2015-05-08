namespace Allors.Meta
{
    using System.ComponentModel.DataAnnotations;

    public partial class OrganisationClass
    {
        internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;

            this.Roles.Description.DataTypeAttribute = new DataTypeAttribute(DataType.Html);
            
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
        }
    }
}