namespace Website
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        private const string NamespacePrefix = "Areas.Default";

        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces =
              Assembly.GetExecutingAssembly()
                  .GetTypes()
                  .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.StartsWith(NamespacePrefix))
                  .Select(type => type.Namespace)
                  .ToArray();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: namespaces);
        }
    }
}
