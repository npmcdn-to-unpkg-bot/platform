namespace Browser
{
    using System;
    using System.Configuration;
    using System.IO;

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

            // If you encounter errors then please run the following command at the command prompt
            // (make sure msbuild is in your path, e.g. use the Visual Studio Command Prompt)
            //
            // msbuild [SolutionName].sln /p:DeployOnBuild=true;DeployTarget=Package;_PackageTempDir="C:\github\allors\Base\Tests\bin\x64\Debug\SpecsForMvc.TempIntermediateDir
            using (var stringWriter = new StringWriter())
            {
                TextWriter console = null;
                try
                {
                    console = Console.Out;
                    Console.SetOut(stringWriter);
                    this.host.Start();
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (this.host != null)
                        {
                            this.host.Shutdown();
                        }
                    }
                    finally
                    {
                        if (console != null)
                        {
                            Console.SetOut(console);
                        }

                        throw new Exception("Build failed. Output: " + stringWriter, ex);
                    }
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            this.host.Shutdown();
        }
    }
}