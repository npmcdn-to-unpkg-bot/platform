namespace Website
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Allors.Web.Mvc;

    public class MvcApplication : HttpApplication
    {
        public void Application_BeginRequest()
        {
            AllorsContext.Instance = new AllorsContext();
        }

        public void Application_Error()
        {
            AllorsContext.Instance.Dispose();
        }

        public void Application_EndRequest()
        {
            AllorsContext.Instance.Dispose();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MetaConfig.Register();
            ModelMetadataConfig.Register();
            ViewEngineConfig.Register();
            MenuConfig.Register();
        }
    }
}
