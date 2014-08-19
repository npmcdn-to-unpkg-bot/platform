// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="Allors bvba">
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

namespace Allors.R1.Special
{
    using System;
    using System.Diagnostics;

    using Allors.R1;
    using Allors.R1.Adapters.Special;

    using Domain;

    public abstract class Profile : IProfile
    {
        private readonly ObjectFactory objectFactory = new ObjectFactory(M.D, M.A, "Domain");

        private int eventCounter;
        private IPopulation population;
        private ISession session;

        public abstract Action[] Markers { get; }

        public abstract Action[] Inits { get; }

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public ISession Session
        {
            get
            {
                return this.session ?? (this.session = this.population.CreateSession());
            }
        }

        public IPopulation Population
        {
            get { return this.population; }
        }

        public IDatabase CreateDatabase()
        {
            throw new NotSupportedException();
        }

        public IWorkspace CreateWorkspace(IDatabase database)
        {
            throw new NotSupportedException();
        }

        public abstract IPopulation CreatePopulation();

        public virtual void Dispose()
        {
            if (this.session != null)
            {
                this.session.Commit();
                this.session = null;
            }

            this.population = null;
        }

        protected internal void Init()
        {
            try
            {
                this.population = this.CreatePopulation();
                this.AddEvents(this.session);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        
        #region Events
        [Conditional("WITH_EVENTS")]
        private void AddEvents(ISession sessionWithEvents)
        {
            sessionWithEvents.Committed += this.SessionCommitted;
            sessionWithEvents.Committing += this.SessionCommitting;
            sessionWithEvents.RolledBack += this.SessionRolledBack;
            sessionWithEvents.RollingBack += this.SessionRollingBack;
        }

        private void SessionCommitted(object sender, SessionCommittedEventArgs args)
        {
            ++this.eventCounter;
        }

        private void SessionCommitting(object sender, SessionCommittingEventArgs args)
        {
            ++this.eventCounter;
        }

        private void SessionRolledBack(object sender, SessionRolledBackEventArgs args)
        {
            ++this.eventCounter;
        }

        private void SessionRollingBack(object sender, SessionRollingBackEventArgs args)
        {
            ++this.eventCounter;
        }
        #endregion
    }
}