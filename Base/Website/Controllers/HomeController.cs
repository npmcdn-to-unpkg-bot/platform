namespace Website.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Allors.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "About.";

            return this.View();
        }

        [Authorize(Roles="Administrators, users")]
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Contact.";

            return this.View();
        }
    }
}