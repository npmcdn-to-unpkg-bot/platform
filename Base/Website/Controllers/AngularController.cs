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
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var loadResponseBuilder = new LoadResponseBuilder(this.AllorsSession, user, loadRequest, Group);
                return this.JsonSuccess(loadResponseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(SaveRequest saveRequest)
        {
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var saveResponseBuilder = new SaveResponseBuilder(this.AllorsSession, user, saveRequest, Group);
                return this.JsonSuccess(saveResponseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Invoke(InvokeRequest invokeRequest)
        {
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var invokeResponseBuilder = new InvokeResponseBuilder(this.AllorsSession, user, invokeRequest, Group);
                var invokeResponse = invokeResponseBuilder.Build();
                return this.JsonSuccess(invokeResponse);
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("root", this.AuthenticatedUser, PersonClass.Instance.AngularHome);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Person()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("root", this.AuthenticatedUser);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
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
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
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
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult NoTree()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("object", this.AuthenticatedUser);
                responseBuilder.AddCollection("collection", new Organisations(this.AllorsSession).Extent());
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }
    }
}