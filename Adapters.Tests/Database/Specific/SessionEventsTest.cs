// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionEventsTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.R1.Adapters.Special
{
    using System;

    using Allors.R1;

    using NUnit.Framework;

    public abstract class SessionEventsTest
    {
        private SessionCommittingEventArgs sessionCommittingEventArgs;
        private SessionRollingBackEventArgs sessionRollingBackEventArgs;

        protected abstract IProfile Profile { get; }

        protected ISession Session
        {
            get
            {
                return this.Profile.Session;
            }
        }

        protected Action[] Markers
        {
            get
            {
                return this.Profile.Markers;
            }
        }

        protected Action[] Inits
        {
            get
            {
                return this.Profile.Inits;
            }
        }

        [Test]
        public void Committing()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.AddHandlers();

                this.Session.Commit();

                Assert.IsNotNull(this.sessionCommittingEventArgs);
                Assert.AreEqual(this.Session, this.sessionCommittingEventArgs.Session);
                this.sessionCommittingEventArgs = null;
            }
        }

        public void CommittingEventHandler(object sender, SessionCommittingEventArgs args)
        {
            this.sessionCommittingEventArgs = args;
        }

        [Test]
        public virtual void RollingBack()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.AddHandlers();

                this.Session.Rollback();

                Assert.IsNotNull(this.sessionRollingBackEventArgs);
                Assert.AreEqual(this.Session, this.sessionRollingBackEventArgs.Session);
                this.sessionRollingBackEventArgs = null;
            }
        }

        public void RollingBackEventHandler(object sender, SessionRollingBackEventArgs args)
        {
            this.sessionRollingBackEventArgs = args;
        }

        protected void AddHandlers()
        {
            this.Session.Committing += this.CommittingEventHandler;
            this.Session.RollingBack += this.RollingBackEventHandler;
        }
    }
}