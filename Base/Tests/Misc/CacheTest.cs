// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheTest.cs" company="Allors bvba">
//   Copyright 2002-2011 Allors bvba.
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
    

    using NUnit.Framework;

    using global::Allors.Domain;

    [TestFixture]
    public class CacheTest : DomainTest
    {
        [Test]
        public void Default()
        {
            var existingOrganisation = new OrganisationBuilder(this.DatabaseSession).WithName("existing organisation").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var sessions = new ISession[] { this.DatabaseSession, this.CreateWorkspaceSession() };
            foreach (var session in sessions)
            {
                session.Commit();

                var cachedOrganisation = new Organisations(session).Cache.Get(existingOrganisation.UniqueId);
                Assert.AreEqual(existingOrganisation.UniqueId, cachedOrganisation.UniqueId);
                Assert.AreSame(session, cachedOrganisation.Session);

                var newOrganisation = new OrganisationBuilder(session).WithName("new organisation").Build();
                cachedOrganisation = new Organisations(session).Cache.Get(newOrganisation.UniqueId);
                Assert.AreEqual(newOrganisation.UniqueId, cachedOrganisation.UniqueId);
                Assert.AreSame(session, cachedOrganisation.Session);

                session.Rollback();
            }
        }
    }
}
