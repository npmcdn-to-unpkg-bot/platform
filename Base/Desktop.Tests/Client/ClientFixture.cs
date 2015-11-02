namespace Desktop.Tests.Client
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;
    using Protractor;
    using Process = System.Diagnostics.Process;

    [SetUpFixture]
    public class ClientFixture
    {
        public static NgWebDriver driver;

        private Process iisProcess;

        [SetUp]
        public void SetUp()
        {
            this.StopIISExpress();
            this.SetupIISExpress();
            this.SetupWebdriver();
        }

        [TearDown]
        public void TearDown()
        {
            this.TearDownWebdriver();
            this.TearDownIISExpress();
        }

        private void StopIISExpress()
        {
            Process.Start("taskkill", "/F /IM iisexpress.exe");

            // Make sure process is killed
            Thread.Sleep(1000);
        }

        private void SetupIISExpress()
        {
            var appLocation = new FileInfo("../../../Desktop").FullName;
            var port = "64738";
            var site = "allors";

            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", arg0: appLocation, arg1: port)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            this.iisProcess = new Process { StartInfo = startInfo };

            this.iisProcess.Start();
        }

        private void TearDownIISExpress()
        {
            try
            {
                this.iisProcess.CloseMainWindow();
            }
            finally
            {
                try
                {
                    this.iisProcess.Dispose();
                }
                catch
                {
                }
            }

        }

        private void SetupWebdriver()
        {
            var seleniumDriver = new FirefoxDriver();

            //var seleniumDriver = new ChromeDriver();
            //var seleniumDriver = new InternetExplorerDriver(new InternetExplorerOptions
            //{
            //    EnableNativeEvents = false,
            //    UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Accept,
            //    IntroduceInstabilityByIgnoringProtectedModeSettings = true,
            //    EnablePersistentHover = true
            //});
            // var seleniumDriver = new PhantomJSDriver();

            seleniumDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));
            driver = new NgWebDriver(seleniumDriver);
            driver.Manage().Window.Maximize();
        }

        private void TearDownWebdriver()
        {
            try
            {
                driver.Close();
            }
            finally
            {
                try
                {
                    driver.Quit();
                }
                finally
                {
                    try
                    {
                        Process.Start("taskkill", "/F /IM chrome.exe");
                        Process.Start("taskkill", "/F /IM iexplore.exe");
                        Process.Start("taskkill", "/F /IM firefox.exe");
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}