namespace Website.OrganisationMvc
{
    using System;

    using Allors.Meta;
    using Allors.Meta.Ids;
    using Allors.Web.Mvc.Models;

    public class Edit : IMetadataModel<OrganisationClass>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Incorporated { get; set; }

        public DateTime? IncorporationDate { get; set; }

        public Select Owner { get; set; }

        [Path(OrganisationIds.Employees)]
        public MultipleSelect Werknemers { get; set; }
    }
}