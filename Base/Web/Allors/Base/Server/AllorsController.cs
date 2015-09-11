namespace Website.Controllers
{
    using System.Web.Mvc;

    public class AllorsController : Controller
    {
        [HttpPost]
        public ActionResult Load()
        {
            var result = "ok";

            return Json(result);
        }

        [HttpPost]
        public ActionResult Save()
        {
            var result = "ok";

            return Json(result);
        }
    }
}