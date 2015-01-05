namespace Website.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public abstract class BaseDataController : Allors.Web.Mvc.Controller
    {
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}