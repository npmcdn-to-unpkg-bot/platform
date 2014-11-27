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

namespace Allors.Workspaces
{
    using System;
    using System.Diagnostics;

    using Allors.Databases;
    using Allors;
    using Allors.Meta;
    using Allors.Populations;

    using Domain;

    public abstract class Profile : IProfile
    {
        private readonly ObjectFactory objectFactory = new ObjectFactory(Repository.MetaPopulation, typeof(ObjectBase).Assembly, "Allors.Domain");

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}