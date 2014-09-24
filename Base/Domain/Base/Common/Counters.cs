// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Counters.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System;
    using System.Threading;

    using Allors;
    using Allors;

    using Allors.Domain;

    public partial class Counters
    {
        private static readonly Random Random = new Random();

        private Cache<Guid, Counter> counterById;

        public Cache<Guid, Counter> CounterById
        {
            get
            {
                return this.counterById ?? (this.counterById = new Cache<Guid, Counter>(this.Session, Meta.UniqueId));
            }
        }

        public static long NextValue(ISession session, Guid counterId, Retry snapshotRetryCount = null)
        {
            if (snapshotRetryCount == null)
            {
                snapshotRetryCount = Retry.Default;
            }

            var database = session.Population as IDatabase;
            if (database == null)
            {
                var workspace = (IWorkspace)session.Population;
                database = workspace.Database;
            }

            if (!database.IsShared)
            {
                return NextValueNonShared(session, counterId);
            }

            database = Databases.Serializable;
           
            if (database != null)
            {
                return NextValueSerializable(database, counterId);
            }

            database = Databases.Default;

            return NextValueSnapshot(database, counterId, snapshotRetryCount);
        }

        private static long NextValueNonShared(ISession session, Guid counterId)
        {
            var counter = new Counters(session).CounterById[counterId];
            counter.Value = counter.Value + 1;

            return counter.Value;
        }

        private static long NextValueSerializable(IDatabase database, Guid counterId)
        {
            using (var counterSession = database.CreateSession())
            {
                var counter = new Counters(counterSession).CounterById[counterId];
                var newValue = counter.Value + 1;
                counter.Value = newValue;

                counterSession.Commit();

                return newValue;
            }
        }

        private static long NextValueSnapshot(IDatabase database, Guid counterId, Retry snapshotRetryCount)
        {
            Exception lastException = null;
            for (var i = 0; i < snapshotRetryCount.Count; i++)
            {
                try
                {
                    using (var counterSession = database.CreateSession())
                    {
                        var counter = new Counters(counterSession).CounterById[counterId];
                        var newValue = counter.Value + 1;
                        counter.Value = newValue;

                        counterSession.Commit();

                        return newValue;
                    }
                }
                catch (Exception e)
                {
                    lastException = e;
                }

                var sleepTime = Random.Next(snapshotRetryCount.MinSleep, snapshotRetryCount.MaxSleep);
                Thread.Sleep(sleepTime);
            }

            if (lastException != null)
            {
                throw lastException;
            }

            throw new Exception("Could not get next value from counter with id " + counterId);
        }
    }
}