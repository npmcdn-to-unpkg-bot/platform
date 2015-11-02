namespace Website.Controllers
{
    using System.Web.Mvc;

    public class HomeController : CustomController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}