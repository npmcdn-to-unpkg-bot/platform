// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationTest.cs" company="Allors bvba">
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
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    using Allors;
    using Allors.Common;
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class DerivationTest : DomainTest
    {
        [Test]
        public void Conflicts()
        {
            var workspaceSession = this.CreateWorkspaceSession();

            var organisation = new OrganisationBuilder(this.DatabaseSession).WithName("Acme Corp.").Build();

            var disconnectedOrganisation = (Domain.Organisation)workspaceSession.Instantiate(organisation.Strategy.ObjectId);
            disconnectedOrganisation.Name = "Acme Ltd.";

            organisation.Name = "Acme Inc.";
            workspaceSession.DatabaseSession.Commit();

            var conflicts = workspaceSession.Conflicts;

            Assert.AreEqual(1, conflicts.Length);

            var derivationLog = new Derivation(workspaceSession).Log;
            derivationLog.AddConflicts(conflicts);

            Assert.AreEqual(1, derivationLog.Errors.Length);
        }

        [Test]
        public void Next()
        {
            var first = new FirstBuilder(this.DatabaseSession).Build();

            this.DatabaseSession.Derive(true);

            Assert.IsTrue(first.IsDerived.HasValue);
            Assert.IsTrue(first.IsDerived.Value);

            Assert.IsTrue(first.Second.IsDerived.HasValue);
            Assert.IsTrue(first.Second.IsDerived.Value);

            Assert.IsTrue(first.Second.Third.IsDerived.HasValue);
            Assert.IsTrue(first.Second.Third.IsDerived.Value);
        }

        [Test]
        public void Dependency()
        {
            var dependent = new DependentBuilder(this.DatabaseSession).Build();
            var dependee = new DependeeBuilder(this.DatabaseSession).Build();

            dependent.Dependee = dependee;
            
            this.DatabaseSession.Commit();

            dependee.Counter = 10;

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(11, dependent.Counter);
            Assert.AreEqual(11, dependee.Counter);
        }

        [Test]
        public void Subdependency()
        {
            var dependent = new DependentBuilder(this.DatabaseSession).Build();
            var dependee = new DependeeBuilder(this.DatabaseSession).Build();
            var subdependee = new SubdependeeBuilder(this.DatabaseSession).Build();

            dependent.Dependee = dependee;
            dependee.Subdependee = subdependee;

            this.DatabaseSession.Commit();

            subdependee.Subcounter = 10;

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(1, dependent.Counter);
            Assert.AreEqual(1, dependee.Counter);

            Assert.AreEqual(11, dependent.Subcounter);
            Assert.AreEqual(11, dependee.Subcounter);
            Assert.AreEqual(11, subdependee.Subcounter);
        }



        [Test]
        public void Deleted()
        {
            var dependent = new DependentBuilder(this.DatabaseSession).Build();
            var dependee = new DependeeBuilder(this.DatabaseSession).Build();

            dependent.Dependee = dependee;

            this.DatabaseSession.Commit();

            dependee.DeleteDependent = true;

            this.DatabaseSession.Derive(true);

            Assert.IsTrue(dependent.Strategy.IsDeleted);
            Assert.AreEqual(1, dependee.Counter);
        }
    }
}