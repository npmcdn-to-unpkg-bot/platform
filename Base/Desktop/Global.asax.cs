using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Allors;
using Allors.Web.Mvc;
using Web;

namespace Desktop
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var configuration = new Allors.Adapters.Object.SqlClient.Configuration
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
            };
            Config.Default = new Allors.Adapters.Object.SqlClient.Database(configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AllorsConfig.Register();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewConfig.Register();
        }
    }
}
