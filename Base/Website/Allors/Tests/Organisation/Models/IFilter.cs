namespace Website.OrganisationMvc
{
    using Allors.Web.Mvc.Models;

    public interface IFilter : IMetadataModel
    {
        string FilterName { get; set; }
    }
}