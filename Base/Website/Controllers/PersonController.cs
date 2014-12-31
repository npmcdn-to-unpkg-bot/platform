namespace Website.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Allors.Domain;

    public class PersonController : Allors.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var persons = new Persons(this.AllorsSession).Extent();
            return this.View(persons);
        }
    }
}