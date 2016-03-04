namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Allors.Meta;
    using Allors.Web.Database;

    public class EditOrganisationController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull(string id)
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var organisation = this.AllorsSession.Instantiate(id);
            response.AddObject("organisation", organisation, M.Organisation.EditResponse);

            return this.JsonSuccess(response.Build());
        }
    }
}