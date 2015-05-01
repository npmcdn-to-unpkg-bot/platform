namespace Website.OrganisationMvc
{
    using Allors.Web.Mvc.Models;

    public interface IFilter : IModel
    {
        string FilterName { get; set; }
    }
}