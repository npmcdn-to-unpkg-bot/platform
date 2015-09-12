namespace Website.Controllers
{
    using System.Web.Mvc;

    public class MvcController : Allors.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "About.";

            // TODO: 
            return this.View("Index");
        }

        [Authorize(Roles="Administrators, users")]
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Contact.";

            // TODO: 
            return this.View("Index");
        }
    }
}