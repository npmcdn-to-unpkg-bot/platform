namespace Website.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web;
    using Allors.Web.Workspace;

    public class AngularController : CustomController
    {
        public const string Group = Groups.Workspace;

        #region Allors
        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            try
            {
                var user = this.AuthenticatedUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var loadResponseBuilder = new LoadResponseBuilder(this.AllorsSession, user, loadRequest, Group);
                var response = loadResponseBuilder.Build();
                return this.JsonSuccess(response);
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
                var saveResponse = saveResponseBuilder.Build();
                return this.JsonSuccess(saveResponse);
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
        #endregion

        #region TypeAhead
        [Authorize]
        [HttpPost]
        public ActionResult PersonTypeAhead(string criteria)
        {
            try
            {
                var responseBuilder = new ResponseBuilder();

                var persons = new People(this.AllorsSession).Extent();
                var or = persons.Filter.AddOr();
                or.AddLike(People.Meta.LastName, criteria + "%");
                or.AddLike(People.Meta.FirstName, criteria + "%");

                responseBuilder.AddCollection("results", persons.Take(100));

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
        }
        #endregion

        #region General
        [Authorize]
        [HttpPost]
        public ActionResult Main()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("person", this.AuthenticatedUser, MetaPerson.Instance.MainResponse);
                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Settings()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("person", this.AuthenticatedUser, MetaPerson.Instance.SettingsResponse);

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
        }
        
        #endregion

        #region Relation
      
        [Authorize]
        [HttpPost]
        public ActionResult OrganisationEdit()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();

                var filterOrganisations = new Organisations(this.AllorsSession).Extent();
                responseBuilder.AddCollection("filterOrganisations", filterOrganisations, MetaOrganisation.Instance.FilterResponse);

                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult OrganisationAdd()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();

                var filterOrganisations = new Organisations(this.AllorsSession).Extent();
                responseBuilder.AddCollection("filterOrganisations", filterOrganisations, MetaOrganisation.Instance.FilterResponse);

                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
        }
        #endregion

        #region Admin
        [Authorize]
        [HttpPost]
        public ActionResult Export()
        {
            var responseBuilder = new ResponseBuilder();
            try
            {
                var response = responseBuilder.Build();
                return this.JsonSuccess(response);
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        #endregion

        #region Custom Invokes

        #endregion

        #region Test

        [Authorize]
        [HttpPost]
        public ActionResult Test()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();

                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
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

        [Authorize]
        [HttpPost]
        public ActionResult Employees()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                var organisation = new Organisations(this.AllorsSession).FindBy(Organisations.Meta.Owner, this.AuthenticatedUser);
                responseBuilder.AddObject("root", organisation, Organisations.Meta.AngularEmployees);
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
                responseBuilder.AddObject("root", organisation, Organisations.Meta.AngularShareholders);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Home()
        {
            try
            {
                var responseBuilder = new ResponseBuilder();
                responseBuilder.AddObject("root", this.AuthenticatedUser, People.Meta.AngularHome);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }

        #endregion
    }
}