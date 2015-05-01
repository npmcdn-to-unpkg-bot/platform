namespace Website.OrganisationMvc
{
    using Allors.Web.Mvc.Models;

    public class Index : IModel, IFilter
    {
        #region Filter
        public string FilterName { get; set; }

        public string FilterLastName { get; set; }
        #endregion

        public Index_Organisation[] Organisations { get; set; }
    }
}