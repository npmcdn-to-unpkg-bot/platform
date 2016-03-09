namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Allors.Meta;
    using Allors.Web.Database;

    public class EditOrganisationController : PullController
    {
        private static Tree tree;

        private static Tree Tree => tree ?? (tree = new Tree(M.Organisation)
            .Add(M.Organisation.Owner)
            .Add(M.Organisation.Employees));

        [Authorize]
        [HttpPost]
        public ActionResult Pull(string id)
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var organisation = this.AllorsSession.Instantiate(id);
            response.AddObject("organisation", organisation, Tree);

            return this.JsonSuccess(response.Build());
        }
    }
}