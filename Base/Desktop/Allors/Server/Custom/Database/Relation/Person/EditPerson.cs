namespace Web.Controllers
{
    using System.Web.Mvc;

    using Allors.Web.Database;

    public class EditPersonController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull(string id)
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var person = this.AllorsSession.Instantiate(id);
            response.AddObject("person", person);

            return this.JsonSuccess(response.Build());
        }
    }
}