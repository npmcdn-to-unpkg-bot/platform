namespace Website.OrganisationMvc
{
    using Allors.Meta;
    using Allors.Web.Mvc.Models;

    public class Edit : IModel<OrganisationClass>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Select Owner { get; set; }

        public MultipleSelect Employees { get; set; }
    }
}