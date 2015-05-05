namespace Website.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;

    using Allors;
    using Allors.Web.Identity;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

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

        // Login with http://localhost/Local/Login?user=username
        [HttpGet]
        public ActionResult Login(string user)
        {
            if (this.IsProduction)
            {
                throw new Exception("Programmatic login is not supported in production");
            }
            
            // Forms authentication
            FormsAuthentication.SetAuthCookie(user, false);

            // Identity Framework
            var owinContext = this.HttpContext.GetOwinContext();
            if (owinContext != null)
            {
                var identityUserManager = owinContext.GetUserManager<IdentityUserManager>();
                if (identityUserManager != null)
                {
                    var identityUser = identityUserManager.FindByName(user);
                    if (identityUser != null)
                    {
                        var identity = identityUserManager.CreateIdentity(identityUser, DefaultAuthenticationTypes.ApplicationCookie);

                        var authenticationManager = owinContext.Authentication;
                        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                    }
                }
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

            var dataPath = ConfigurationManager.AppSettings["dataPath"];
            if (!Path.IsPathRooted(dataPath))
            {
                dataPath = HttpRuntime.AppDomainAppPath + dataPath;
            }

            using (var session = database.CreateSession())
            {
                new Setup(session, new DirectoryInfo(dataPath)).Apply();

                session.Derive();
                session.Commit();
            }


            return this.View("Index");
        }
       
    }
}