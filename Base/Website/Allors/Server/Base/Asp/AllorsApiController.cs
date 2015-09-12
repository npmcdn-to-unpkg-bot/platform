namespace Allors.Api
{
    using System.Web.Http;

    using Allors;
    using Allors.Domain;

    public class AllorsApiController : ApiController
    {
        private User authenticatedUser;

        protected AllorsApiController()
        {
            this.AllorsSession = Config.Default.CreateSession();
        }

        ~AllorsApiController()
        {
            this.DisposeAllorsSession();
        }

        protected ISession AllorsSession { get; private set; }

        protected virtual User AuthenticatedUser
        {
            get
            {
                if (this.authenticatedUser == null)
                {
                    var userName = this.User.Identity.Name;

                    if (userName != null)
                    {
                        this.authenticatedUser = new Users(this.AllorsSession).FindBy(Users.Meta.UserName, userName);
                    }
                }

                return this.authenticatedUser;
            }

            set { this.authenticatedUser = value; }
            
        }

        protected void AddModelErrors(DerivationLog derivationLog)
        {
            foreach (var error in derivationLog.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeAllorsSession();
            }

            base.Dispose(disposing);
        }

        private void DisposeAllorsSession()
        {
            var session = this.AllorsSession;
            if (session != null)
            {
                try
                {
                    session.Dispose();
                }
                finally
                {
                    this.AllorsSession = null;
                }
            }
        }
    }
}
