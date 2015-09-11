using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Allors;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var configuration = new Allors.Adapters.Object.SqlClient.Configuration
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
            };
            Config.Default = new Allors.Adapters.Object.SqlClient.Database(configuration);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MetaConfig.Register();
        }
    }
}
