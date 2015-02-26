//------------------------------------------------------------------------------------------------- 
// <copyright file="ExternalAccountingTransactionTests.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the MediaTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class ExternalAccountingTransactionTests : DomainTest
    {
        [Test]
        public void GivenTaxDue_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var partyFrom = new OrganisationBuilder(this.DatabaseSession).WithName("party from").Build();
            var partyTo = new OrganisationBuilder(this.DatabaseSession).WithName("party to").Build();
            
            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var builder = new TaxDueBuilder(this.DatabaseSession);
            var taxDue = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("taxdue");
            taxDue = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithEntryDate(DateTime.Now);
            taxDue = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithTransactionDate(DateTime.Now.AddYears(1));
            taxDue = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromParty(partyFrom);
            taxDue = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithToParty(partyTo);
            taxDue = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenTaxDue_WhenDeriving_ThenDisplayNameIsSet()
        {
            throw new Exception("Review");

            //var fromParty = new OrganisationBuilder(this.DatabaseSession)
            //    .WithName("party paying")
            //    .WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain)
            //    .Build();

            //var toParty = new OrganisationBuilder(this.DatabaseSession)
            //    .WithName("party receiving")
            //    .WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain)
            //    .Build();

            //var taxDue = new TaxDueBuilder(this.DatabaseSession)
            //    .WithDescription("Three word description")
            //    .WithEntryDate(DateTime.Now)
            //    .WithTransactionDate(DateTime.Now)
            //    .WithFromParty(fromParty)
            //    .WithToParty(toParty)
            //    .WithDerivedTotalAmount(10M)
            //    .Build();

            //this.DatabaseSession.Derive(true);

            //var expectedDisplayName =
            //    string.Format(
            //        "Transaction date {0}, {1}, total amount {2} from party {3} to party {4}",
            //        taxDue.TransactionDate,
            //        taxDue.Description,
            //        taxDue.DerivedTotalAmount,
            //        fromParty.DisplayName,
            //        toParty.DisplayName);

            //Assert.AreEqual(expectedDisplayName, taxDue.DisplayName);
        }
    }
}