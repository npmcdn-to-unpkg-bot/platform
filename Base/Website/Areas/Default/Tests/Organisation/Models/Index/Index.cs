namespace Areas.Default.Tests.OrganisationMvc
{
    using Allors.Web.Mvc.Models;

    public class Index : IMetadataModel, IFilter
    {
        #region Filter
        public string FilterName { get; set; }

        public string FilterLastName { get; set; }
        #endregion

        public Index_Organisation[] Organisations { get; set; }
    }
}