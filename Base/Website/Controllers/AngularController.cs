using Allors.Meta;
using Allors.Web;
using Allors.Web.Workspace;

namespace Website.Controllers
{
    using System.Web.Mvc;

    public class AngularController : Allors.Web.Mvc.Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            return Json(null);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            var responseBuilder = new ResponseBuilder();
            responseBuilder.Add("root", this.AuthenticatedUser, PersonClass.Instance.HomeTree);
            return Json(responseBuilder.Build());
        }

    }
}