// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Controller.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Allors.Domain;

    public abstract partial class Controller : System.Web.Mvc.Controller
    {
        private ISession allorsSession;
        private User authenticatedUser; 

        ~Controller()
        {
            this.DisposeAllorsSession();
        }

        public ISession AllorsSession
        {
            get
            {
                return this.allorsSession;
            }

            set
            {
                if (this.allorsSession != null)
                {
                    throw new ArgumentException("AllorsSession is already set");
                }

                this.allorsSession = value;
            }
        }

        public ViewContext RootViewContext
        {
            get
            {
                var viewContext = this.ControllerContext.ParentActionViewContext;
                if (viewContext.ParentActionViewContext != null)
                {
                    viewContext = viewContext.ParentActionViewContext;
                }

                return viewContext;
            }
        }

        public User AuthenticatedUser
        {
            get
            {
                if (this.authenticatedUser == null)
                {
                    var userName = this.User.Identity.Name;

                    if (userName != null)
                    {
                        this.authenticatedUser = new Users(this.allorsSession).FindBy(Users.Meta.UserName, userName);
                    }
                }

                return this.authenticatedUser;
            }

            set
            {
                this.authenticatedUser = value;
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (this.allorsSession == null)
            {
                this.allorsSession = Config.Default.CreateSession();
            }

            base.Initialize(requestContext);
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
            var session = this.allorsSession;
            if (session != null)
            {
                try
                {
                    session.Dispose();
                }
                finally
                {
                    this.allorsSession = null;
                }
            }
        }
    }
}