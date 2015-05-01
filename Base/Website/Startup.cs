using Microsoft.Owin;
using Website;

[assembly: OwinStartup(typeof(Startup))]
namespace Website
{
    using System;
    using System.Configuration;

    using Allors;
    using Allors.Databases.Object.SqlClient;
    using Allors.Web.Identity;
    using Allors.Workspaces.Memory.IntegerId;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.Cookies;

    using Owin;

    using Configuration = Allors.Databases.Object.SqlClient.Configuration;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new Configuration
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString, 
                ObjectFactory = Config.ObjectFactory, 
                WorkspaceFactory = new WorkspaceFactory()
            };
            Config.Default = new Database(configuration);


            // Identity
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);
            app.CreatePerOwinContext<IdentitySignInManager>(IdentitySignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, 
                LoginPath = new PathString("/Account/Login"), 
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityUserManager, IdentityUser>(
                        validateInterval: TimeSpan.FromMinutes(30), 
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}
