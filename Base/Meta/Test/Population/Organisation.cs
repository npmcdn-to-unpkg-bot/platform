namespace Allors.Meta
{
    using System.ComponentModel.DataAnnotations;

    public partial class OrganisationClass
    {
        [Id("CBF9121E-A5E5-45C6-99FE-52FA80DC3220")]
        public MethodType JustDoIt;

        internal override void BaseExtend()
        {
            this.Roles.Description.DataTypeAttribute = new DataTypeAttribute(DataType.MultilineText);

            this.Roles.Information.DisplayAttribute = new DisplayAttribute { Name = "Ik ben het label" };
            this.Roles.Information.DataTypeAttribute = new DataTypeAttribute(DataType.Html);
        }
    }
}