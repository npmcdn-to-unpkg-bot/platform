namespace Desktop.Tests.Client
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    using Allors;
    using Allors.Adapters.Object.SqlClient;
    using Allors.Domain;

    using NUnit.Framework;

    using Protractor;

    public class Test
    {
        public const string AppUrl = "http://localhost:64738";
        public const string TSUnitUrl = "http://localhost:64738/test";

        public NgWebDriver Driver { get; private set; }

        public ISession Session { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            this.Init(true);
            this.Driver = ClientFixture.driver;
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.Session.Rollback();
            this.Session = null;
        }

        protected void Init(bool setup)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var configuration = new Configuration { ObjectFactory = Config.ObjectFactory };
            var database = new Database(configuration);
            Config.Default = database;

            database.Init();

            if (setup)
            {
                using (var session = database.CreateSession())
                {
                    try
                    {
                        new Setup(session, null).Apply();
                        session.Commit();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.StackTrace);
                        throw;
                    }
                }
            }

            this.Session = Config.Default.CreateSession();
        }
    }
}