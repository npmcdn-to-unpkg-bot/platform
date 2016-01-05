// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationExtentTest.cs" company="Allors bvba">
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

namespace Allors.Adapters
{
    using System;

    using Allors.Meta;

    using Allors;

    using Allors.Domain;
    using Adapters;

    using NUnit.Framework;

    public abstract class RelationExtentTest
    {
        #region population
        private C1 c1_0;
        private C1 c1_1;
        private C1 c1_2;
        private C1 c1_3;
        private C2 c2_0;
        private C2 c2_1;
        private C2 c2_2;
        private C2 c2_3;
        private C3 c3_0;
        private C3 c3_1;
        private C3 c3_2;
        private C3 c3_3;
        private C4 c4_0;
        private C4 c4_1;
        private C4 c4_2;
        private C4 c4_3;
        #endregion

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
        public void UpgradeAssociation()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                Company acme = Company.Create(this.Session, "Acme", 2);
                Company acne = Company.Create(this.Session, "Acne", 1);

                Person john = Person.Create(this.Session, "John", 2);
                Person jane = Person.Create(this.Session, "Jane", 1);

                Person johny = Person.Create(this.Session, "johny", 4);
                Person janet = Person.Create(this.Session, "Janet", 3);

                // Many 2 one
                acme.Manager = john;
                acne.Manager = john;

                Extent managedCompanies = john.CompaniesWhereManager;

                Assert.AreEqual(2, managedCompanies.Count);

                managedCompanies.Filter.AddLike(MetaNamed.Instance.Name, "%ne");

                Assert.AreEqual(1, managedCompanies.Count);

                managedCompanies = john.CompaniesWhereManager;

                Assert.AreEqual(2, managedCompanies.Count);

                managedCompanies.AddSort(MetaNamed.Instance.Index, SortDirection.Descending);

                Assert.AreEqual(2, managedCompanies.Count);
                Assert.AreEqual(acme, managedCompanies[0]);
                Assert.AreEqual(acne, managedCompanies[1]);

                // Many to Many
                acme.AddOwner(john);
                acme.AddOwner(jane);

                acne.AddOwner(jane);
                acne.AddOwner(johny);

                Extent janesCompanies = jane.CompaniesWhereOwner;

                janesCompanies.Filter.AddLike(MetaNamed.Instance.Name, "%ne");

                Assert.AreEqual(1, janesCompanies.Count);

                janesCompanies = jane.CompaniesWhereOwner;

                Assert.AreEqual(2, janesCompanies.Count);

                janesCompanies.AddSort(MetaNamed.Instance.Index);

                Assert.AreEqual(2, janesCompanies.Count);
                Assert.AreEqual(acne, janesCompanies[0]);
                Assert.AreEqual(acme, janesCompanies[1]);
            }
        }

        [Test]
        public void UpgradeRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();
                
                Company acme = Company.Create(this.Session, "Acme");
                Company acne = Company.Create(this.Session, "Acne");

                Person john = Person.Create(this.Session, "John", 2);
                Person jane = Person.Create(this.Session, "Jane", 1);

                Person johny = Person.Create(this.Session, "Johny", 4);
                Person janet = Person.Create(this.Session, "Janet", 3);

                // One 2 Many
                acme.AddEmployee(john);
                acme.AddEmployee(jane);

                acne.AddEmployee(johny);

                Extent employees = acme.Employees;

                Assert.AreEqual(2, employees.Count);

                employees.Filter.AddLike(MetaNamed.Instance.Name, "Ja%");

                Assert.AreEqual(1, employees.Count);

                employees = acme.Employees;

                Assert.AreEqual(2, employees.Count);

                employees.AddSort(MetaNamed.Instance.Index, SortDirection.Descending);

                Assert.AreEqual(2, employees.Count);
                Assert.AreEqual(john, employees[0]);
                Assert.AreEqual(jane, employees[1]);

                // Many to Many
                acme.AddOwner(john);
                acme.AddOwner(jane);

                acne.AddOwner(jane);
                acne.AddOwner(johny);

                Extent acmeOwners = acme.Owners;
                Extent acneOwners = acme.Owners;

                acmeOwners.Filter.AddLike(MetaNamed.Instance.Name, "Ja%");

                Assert.AreEqual(1, acmeOwners.Count);

                acmeOwners = acme.Owners;

                Assert.AreEqual(2, acmeOwners.Count);

                acmeOwners.AddSort(MetaNamed.Instance.Index);

                Assert.AreEqual(2, acmeOwners.Count);
                Assert.AreEqual(jane, acmeOwners[0]);
                Assert.AreEqual(john, acmeOwners[1]);
            }
        }

        protected void Populate()
        {
            var population = new TestPopulation(this.Session);
            this.c1_0 = population.C1A;
            this.c1_1 = population.C1B;
            this.c1_2 = population.C1C;
            this.c1_3 = population.C1D;
            this.c2_0 = population.C2A;
            this.c2_1 = population.C2B;
            this.c2_2 = population.C2C;
            this.c2_3 = population.C2D;
            this.c3_0 = population.C3A;
            this.c3_1 = population.C3B;
            this.c3_2 = population.C3C;
            this.c3_3 = population.C3D;
            this.c4_0 = population.C4A;
            this.c4_1 = population.C4B;
            this.c4_2 = population.C4C;
            this.c4_3 = population.C4D;
        }
    }
}