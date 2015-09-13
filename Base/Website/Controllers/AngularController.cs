using Allors.Domain;
using Allors.Meta;
using Allors.Web;
using Allors.Web.Workspace;

namespace Website.Controllers
{
    using System.Web.Mvc;

    public class AngularController : Allors.Web.Mvc.Controller
    {
        public const string Group = Groups.Workspace;

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            var loadResponseBuilder = new LoadResponseBuilder(this.AllorsSession, loadRequest, Group);
            return Json(loadResponseBuilder.Build());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            var responseBuilder = new ResponseBuilder();
            responseBuilder.Add("root", this.AuthenticatedUser, PersonClass.Instance.AngularHome);
            return Json(responseBuilder.Build());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Employees()
        {
            var responseBuilder = new ResponseBuilder();
            var organisation = new Organisations(this.AllorsSession).FindBy(Organisations.Meta.Owner, this.AuthenticatedUser);
            responseBuilder.Add("root", organisation, OrganisationClass.Instance.AngularEmployees);
            return Json(responseBuilder.Build());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Shareholders()
        {
            var responseBuilder = new ResponseBuilder();
            var organisation = new Organisations(this.AllorsSession).FindBy(Organisations.Meta.Owner, this.AuthenticatedUser);
            responseBuilder.Add("root", organisation, OrganisationClass.Instance.AngularShareholders);
            return Json(responseBuilder.Build());
        }
    }
}