using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Website.Startup))]
namespace Website
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Web.Identity;
    using Allors.Web.Mvc;
    using Allors.Workspaces.Memory.IntegerId;

    using Website.Controllers;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new Allors.Databases.Object.SqlClient.IntegerId.Configuration
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
                WorkspaceFactory = new WorkspaceFactory()
            };
            Config.Default = new Allors.Databases.Object.SqlClient.IntegerId.Database(configuration);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AllorsRazorViewEngine()); 

            // Identity
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);
            app.CreatePerOwinContext<IdentitySignInManager>(IdentitySignInManager.Create);

            Menus.Set("main", new Menu()
                .Add(new MenuItem().Action<HomeController>(c => c.Index()))
                .Add(new MenuItem().Action<HomeController>(c => c.About()))
                .Add(new MenuItem().Action<HomeController>(c => c.Contact()))
                );
        }

        private class AllorsRazorViewEngine : RazorViewEngine
        {
            public AllorsRazorViewEngine()
            {
                this.ViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Base/{1}/{0}.cshtml",
"~/Allors/Base/Shared/{0}.cshtml",
};

                this.MasterLocationFormats = this.ViewLocationFormats;

                this.PartialViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Base/{1}/{0}.cshtml",
"~/Allors/Base/Shared/{0}.cshtml",
};
            }
        } 
    }
}
