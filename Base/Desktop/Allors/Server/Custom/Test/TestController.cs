namespace Website.Controllers
{
    using System;
    using System.Web.Configuration;
    using System.Web.Mvc;

    public class TestController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (!this.Request.IsLocal)
            {
                throw new Exception();
            }

            if (this.IsProduction)
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

    }
}