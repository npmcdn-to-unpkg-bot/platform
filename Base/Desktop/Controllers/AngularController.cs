namespace Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web;
    using Allors.Web.Workspace;

    using Common.Logging;

    public class AngularController : CustomController
    {
        public const string Group = Groups.Workspace;

        private readonly ILog log;

        public AngularController()
        {
            // obtain logger instance 
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Allors
        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var loadResponseBuilder = new LoadResponseBuilder(this.AllorsSession, user, loadRequest, Group);
                var response = loadResponseBuilder.Build();
                return this.JsonSuccess(response);
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(SaveRequest saveRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var saveResponseBuilder = new SaveResponseBuilder(this.AllorsSession, user, saveRequest, Group);
                var saveResponse = saveResponseBuilder.Build();
                return this.JsonSuccess(saveResponse);
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Invoke(InvokeRequest invokeRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var invokeResponseBuilder = new InvokeResponseBuilder(this.AllorsSession, user, invokeRequest, Group);
                var invokeResponse = invokeResponseBuilder.Build();
                return this.JsonSuccess(invokeResponse);
            }
            catch (Exception e)
            {
                this.log.Error(e);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);

                var persons = new People(this.AllorsSession).Extent();
                var or = persons.Filter.AddOr();
                or.AddLike(M.Person.LastName, criteria + "%");
                or.AddLike(M.Person.FirstName, criteria + "%");

                responseBuilder.AddCollection("results", persons.Take(100));

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                responseBuilder.AddObject("person", this.AllorsUser);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Settings()
        {
            try
            {
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                responseBuilder.AddObject("person", this.AllorsUser);

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        #endregion

        #region Relation

        [Authorize]
        [HttpPost]
        public ActionResult Relation()
        {
            try
            {
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                responseBuilder.AddObject("person", this.AllorsUser);
                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult People()
        {
            try
            {
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                var people = new People(this.AllorsSession).Extent();
                responseBuilder.AddCollection("people", people);

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult PersonEdit()
        {
            try
            {
                var responseBuilder = new ResponseBuilder(this.AllorsUser);

                var filterPeople = new People(this.AllorsSession).Extent();
                responseBuilder.AddCollection("people", filterPeople);

                return JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult PersonAdd()
        {
            try
            {
                var responseBuilder = new ResponseBuilder(this.AllorsUser);

                var filterPeople = new People(this.AllorsSession).Extent();
                responseBuilder.AddCollection("filterPeople", filterPeople);

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                this.log.Error(e);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                responseBuilder.AddObject("object", this.AllorsUser);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                var organisation = new Organisations(this.AllorsSession).FindBy(M.Organisation.Owner, this.AllorsUser);
                responseBuilder.AddObject("root", organisation, M.Organisation.AngularEmployees);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                var organisation = new Organisations(this.AllorsSession).FindBy(M.Organisation.Owner, this.AllorsUser);
                responseBuilder.AddObject("root", organisation, M.Organisation.AngularShareholders);
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
                var responseBuilder = new ResponseBuilder(this.AllorsUser);
                responseBuilder.AddObject("root", this.AllorsUser, M.Person.AngularHome);
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