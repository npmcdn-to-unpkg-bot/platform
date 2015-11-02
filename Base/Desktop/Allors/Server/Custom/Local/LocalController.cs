namespace Website.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    using Allors;

    using Allors.Domain;

    using System.Web.Mvc;
    using System.Web.Security;

    public class LocalController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (!this.Request.IsLocal)
            {
                throw new Exception();
            }

            return base.BeginExecuteCore(callback, state);
        }

        public bool IsProduction 
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Login(string user, string returnUrl)
        {
            if (this.IsProduction)
            {
                throw new Exception("Programmatic login is not supported in production");
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                user = @"administrator";
            }

            FormsAuthentication.SetAuthCookie(user, false);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Setup()
        {
            if (this.IsProduction)
            {
                throw new Exception("Setup is not supported in production, use Console.");
            }

            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session, null).Apply();

                //var koen = new Persons(session).Extent().First(x => @"koen@inxin.com".Equals(x.UserName));
                //var patrick = new Persons(session).Extent().First(x => @"patrick.deboeck@inxin.com".Equals(x.UserName));
                    
                //new UserGroups(session).Administrators.AddMember(koen);
                //new UserGroups(session).Administrators.AddMember(patrick);

                session.Derive();
                session.Commit();
            }

            return this.View("Index");
        }
    }
}