namespace Areas.Default.Tests.OrganisationMvc
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Allors.Meta;
    using Allors.Web.Mvc.Models;

    public class View : IMetadataModel<OrganisationClass>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Information { get; set; }

        public bool? Incorporated { get; set; }

        public DateTime? IncorporationDate { get; set; }

        public Select Owner { get; set; }

        [Path("b95c7b34-a295-4600-82c8-826cc2186a00")]
        public MultipleSelect Werknemers { get; set; }

        public int EmployeeCount { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }
    }
}