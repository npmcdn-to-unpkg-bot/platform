namespace Web.Controllers
{
using System.Web.Mvc;

    using Allors.Web.Database;

    public class SettingsController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull()
        {
            var responseBuilder = new PullResponseBuilder(this.AllorsUser);
            responseBuilder.AddObject("person", this.AllorsUser);
            return this.JsonSuccess(responseBuilder.Build());
        }
    }
}