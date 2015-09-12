using Allors.Web;

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
        public ActionResult Load()
        {
            return Json(null);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            var response = new Response();

            return Json(response);
        }

    }
}