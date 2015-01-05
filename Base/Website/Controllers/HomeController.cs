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
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        [Authorize(Roles="Administrators, users")]
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}