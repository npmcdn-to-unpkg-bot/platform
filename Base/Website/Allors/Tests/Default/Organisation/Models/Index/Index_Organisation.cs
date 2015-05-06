namespace Website.OrganisationMvc
{
    using Allors.Meta;
    using Allors.Web.Mvc.Models;

    public class Index_Organisation : IMetadataModel<OrganisationClass>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }
    }
}