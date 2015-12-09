// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrefetchTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Linq;

    using Adapters;

    using Allors;
    using Allors.Adapters.Object.SqlClient.Logging;
    using Allors.Domain;
    using Allors.Meta;

    using Domain;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public abstract class PerformanceTests
    {
        #region Population
        protected C1 c1A;
        protected C1 c1B;
        protected C1 c1C;
        protected C1 c1D;
        protected C2 c2A;
        protected C2 c2B;
        protected C2 c2C;
        protected C2 c2D;
        protected C3 c3A;
        protected C3 c3B;
        protected C3 c3C;
        protected C3 c3D;
        protected C4 c4A;
        protected C4 c4B;
        protected C4 c4C;
        protected C4 c4D;
        #endregion

        protected abstract IProfile Profile { get; }

        protected ISession Session => this.Profile.Session;

        protected Action[] Markers => this.Profile.Markers;

        protected Action[] Inits => this.Profile.Inits;

        [Test]
        public void NoCommands()
        {
            foreach (var init in this.Inits)
            {
                init();

                var database = (Database)this.Session.Database;
                var connectionFactory = (LoggedConnectionFactory)database.ConnectionFactory;

                var connection = connectionFactory.Connections.Last();

                connection.Commands.Count.ShouldEqual(0);

                this.Populate();
                this.Session.Commit();
            }
        }

        protected void Populate()
        {
            var population = new TestPopulation(this.Session);

            this.c1A = population.C1A;
            this.c1B = population.C1B;
            this.c1C = population.C1C;
            this.c1D = population.C1D;

            this.c2A = population.C2A;
            this.c2B = population.C2B;
            this.c2C = population.C2C;
            this.c2D = population.C2D;

            this.c3A = population.C3A;
            this.c3B = population.C3B;
            this.c3C = population.C3C;
            this.c3D = population.C3D;

            this.c4A = population.C4A;
            this.c4B = population.C4B;
            this.c4C = population.C4C;
            this.c4D = population.C4D;
        }

        protected ISession CreateSession()
        {
            return this.Profile.Population.CreateSession();
        }
    }
}