namespace Browser
{
    using Allors;

    using SpecsFor;
    using SpecsFor.Mvc;

    using Website.Controllers;

    public class BrowserTest : SpecsFor<MvcWebApp>
    {
        protected BrowserTest() : this(true)
        {
        }

        protected BrowserTest(bool setup)
        {
            Config.Default.Init();
            this.Session = Config.Default.CreateSession();

            if (setup)
            {
                new Setup(this.Session).Apply();
                this.Session.Commit();
            }
        }

        ~BrowserTest()
        {
             this.ReleaseSession();   
        }

        protected IDatabaseSession Session { get; private set; }

        public override void TearDown()
        {
            try
            {
                this.ReleaseSession();
            }
            finally
            {
                base.TearDown();
            }
        }

        protected void Login(string user)
        {
            this.SUT.NavigateTo<LocalController>(c => c.Login(user));
        }

        private void ReleaseSession()
        {
            if (this.Session != null)
            {
                this.Session.Rollback();
                this.Session = null;
            }
        }
    }
}