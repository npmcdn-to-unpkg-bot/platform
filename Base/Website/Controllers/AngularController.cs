using System;
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
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var loadResponseBuilder = new LoadResponseBuilder(this.AllorsSession, user, loadRequest, Group);
                return JsonSuccess(loadResponseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(SaveRequest saveRequest)
        {
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var saveResponseBuilder = new SaveResponseBuilder(this.AllorsSession, user, saveRequest, Group);
                return JsonSuccess(saveResponseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("root", this.AuthenticatedUser, PersonClass.Instance.AngularHome);
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Person()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("root", this.AuthenticatedUser);
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Employees()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                var organisation = new Organisations(this.AllorsSession).FindBy(Organisations.Meta.Owner, this.AuthenticatedUser);
                responseBuilder.AddObject("root", organisation, OrganisationClass.Instance.AngularEmployees);
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Shareholders()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                var organisation = new Organisations(this.AllorsSession).FindBy(Organisations.Meta.Owner, this.AuthenticatedUser);
                responseBuilder.AddObject("root", organisation, OrganisationClass.Instance.AngularShareholders);
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }

        [Authorize]
        [HttpPost]
        public ActionResult NoTree()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("object", this.AuthenticatedUser);
                responseBuilder.AddCollection("collection", new Organisations(this.AllorsSession).Extent() );
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e) { return JsonError(e.Message); }
        }

    }
}