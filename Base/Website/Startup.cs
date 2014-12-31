using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Website.Startup))]
namespace Website
{
    using Allors;
    using Allors.Web.Identity;
    using Allors.Workspaces.Memory.IntegerId;

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

            // Identity
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);
            app.CreatePerOwinContext<IdentitySignInManager>(IdentitySignInManager.Create);
        }
    }
}
