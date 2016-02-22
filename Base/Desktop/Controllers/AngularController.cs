namespace Web.Controllers
{
    using System;
    using System.Collections;
    using System.Globalization;
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
            this.log = LogManager.GetLogger<AngularController>();
        }

        #region Allors
        [Authorize]
        [HttpPost]
        public ActionResult Sync(SyncRequest syncRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var responseBuilder = new SyncResponseBuilder(this.AllorsSession, user, syncRequest, Group);
                var response = responseBuilder.Build();
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
        public ActionResult Push(PushRequest pushRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var responseBuilder = new PushResponseBuilder(this.AllorsSession, user, pushRequest, Group);
                var response = responseBuilder.Build();

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
        public ActionResult Invoke(InvokeRequest invokeRequest)
        {
            try
            {
                var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
                var responseBuilder = new InvokeResponseBuilder(this.AllorsSession, user, invokeRequest, Group);
                var response = responseBuilder.Build();
                return this.JsonSuccess(response);
            }
            catch (Exception e)
            {
                this.log.Error(e);
                return this.JsonError(e.Message);
            }
        }

        [HttpGet]
        public ActionResult Translate(string lang)
        {
            try
            {
                var cultureInfo = new CultureInfo(lang);

                var resources = Resources.WorkspaceMeta.ResourceManager.GetResourceSet(cultureInfo, true, true);
                var resourceDict = resources
                    .Cast<DictionaryEntry>()
                    .ToDictionary(entry => "meta_" + entry.Key.ToString().Replace(".", "_"), entry => entry.Value.ToString());

                return this.JsonSuccess(resourceDict);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);

                var people = new People(this.AllorsSession).Extent();
                var or = people.Filter.AddOr();
                or.AddLike(M.Person.LastName, criteria + "%");
                or.AddLike(M.Person.FirstName, criteria + "%");

                responseBuilder.AddCollection("results", people.Take(100));

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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
        public ActionResult EditPerson(string id)
        {
            try
            {
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);

                var person = this.AllorsSession.Instantiate(id);
                responseBuilder.AddObject("person", person);

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
        public ActionResult AddPerson()
        {
            try
            {
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
                
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
        public ActionResult Organisations()
        {
            try
            {
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
                var organisations = new Organisations(this.AllorsSession).Extent();
                responseBuilder.AddCollection("organisations", organisations);

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
        public ActionResult EditOrganisation(string id)
        {
            try
            {
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);

                var organisation = this.AllorsSession.Instantiate(id);
                responseBuilder.AddObject("organisation", organisation, M.Organisation.EditResponse);

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
        public ActionResult AddOrganisation()
        {
            try
            {
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);

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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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
                var responseBuilder = new PullResponseBuilder(this.AllorsUser);
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