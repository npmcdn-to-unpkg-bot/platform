namespace Browser
{
    using System.Configuration;
    using Allors;
    using NUnit.Framework;
    using SpecsFor.Mvc;
    using Website;

    [SetUpFixture]
    public class BrowserFixture
    {
        private SpecsForIntegrationHost host;

        [SetUp]
        public void SetUp()
        {
            var configuration = new Allors.Databases.Object.SqlClient.Configuration
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
                WorkspaceFactory = new Allors.Workspaces.Memory.IntegerId.WorkspaceFactory(),
                CommandTimeout = 0
            };

            Config.Default = new Allors.Databases.Object.SqlClient.Database(configuration);


            var config = new SpecsForMvcConfig();
            config.UseIISExpress()
                .With(Project.Named("Website"))
                .CleanupPublishedFiles();

            config.BuildRoutesUsing(RouteConfig.RegisterRoutes);

            //config.UseBrowser(BrowserDriver.InternetExplorer);
            //config.UseBrowser(BrowserDriver.Chrome);
            config.UseBrowser(BrowserDriver.Firefox);

            config.InterceptEmailMessagesOnPort(13565);

            this.host = new SpecsForIntegrationHost(config);
            this.host.Start();
        }

        [TearDown]
        public void TearDown()
        {
            this.host.Shutdown();
        }
    }
}