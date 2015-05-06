namespace Website.OrganisationMvc
{
    using Allors.Meta;
    using Allors.Web.Mvc.Models;

    public class Add : IMetadataModel<OrganisationClass>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}