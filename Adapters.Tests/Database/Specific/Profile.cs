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

namespace Allors.Adapters.Special
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    using Allors.Adapters.Database.Caching;
    using Allors;
    using Allors.Meta;

    using Domain;

    public abstract class Profile : IProfile
    {
        private readonly ObjectFactory objectFactory;

        private int eventCounter;
        private IDatabase database;
        private ISession session;

        protected Profile()
        {
            this.CacheFactory = new CacheFactory();
            this.objectFactory = this.CreateObjectFactory(M.D);
        }

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public ISession Session
        {
            get { return this.session; }
        }

        public IPopulation Population
        {
            get { return this.database; }
        }

        public abstract Action[] Markers { get; }

        public virtual Action[] Inits
        {
            get
            {
                var caches = new List<Action> { this.Init };

                if (Settings.ExtraInits)
                {
                    caches.Add(
                        () =>
                        {
                            this.CacheFactory = new CacheFactory
                                                    {
                                                        TransientObjectTypes = this.database.ObjectFactory.Domain.ObjectTypes,
                                                    };
                            this.Init();
                        });
                }

                return caches.ToArray();
            }
        }

        protected ICacheFactory CacheFactory { get; set; }

        public void SwitchDatabase()
        {
            this.session.Rollback();
            this.database = this.CreateDatabase();
            this.session = this.database.CreateSession();
            this.AddEvents(this.session);
            this.session.Commit();
        }

        public virtual void Dispose()
        {
            if (this.session != null)
            {
                this.session.Rollback();
            }

            this.session = null;
            this.database = null;
        }

        public abstract IDatabase CreateDatabase();

        public abstract IWorkspace CreateWorkspace(IDatabase database);

        public void DropProcedure(string procedure)
        {
            using (var connection = ((Database.Sql.Database)this.CreateDatabase()).CreateDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var sql = new StringBuilder();
                    sql.Append("DROP PROCEDURE " + procedure);

                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                }
            }
        }

        internal ISession CreateSession()
        {
            return this.database.CreateSession();
        }

        protected internal void Init()
        {
            try
            {
                if (this.session != null)
                {
                    this.session.Rollback();
                }

                this.database = this.CreateDatabase();
                this.database.Init();
                this.session = this.database.CreateSession();
                this.AddEvents(this.session);
                this.session.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        protected ObjectFactory CreateObjectFactory(Domain domain)
        {
            return new ObjectFactory(domain, typeof(IObject).Assembly, "Domain");
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