//------------------------------------------------------------------------------------------------- 
// <copyright file="AccountingTransactionDetailTests.cs" company="Allors bvba">
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

    using NUnit.Framework;
    using Allors.Domain;

    [TestFixture]
    public class AccountingTransactionDetailTests : DomainTest
    {
        [Test]
        public void GivenAccountingTransaction_WhenDeriving_ThenDisplayNameIsSet()
        {
            var period = new AccountingPeriodBuilder(this.DatabaseSession)
                .WithFromDate(new DateTime(2010, 01, 01))
                .WithPeriodNumber(1)
                .WithTimeFrequency(new TimeFrequencies(this.DatabaseSession).Day)
                .WithThroughDate(new DateTime(2010, 02, 01))
                .Build();

            var account = new GeneralLedgerAccountBuilder(this.DatabaseSession)
                .WithName("GL account")
                .WithAccountNumber("acc001")
                .WithBalanceSheetAccount(true)
                .WithSide(new DebitCreditConstants(this.DatabaseSession).Debit)
                .WithGeneralLedgerAccountType(new GeneralLedgerAccountTypeBuilder(this.DatabaseSession).WithDescription("accountType").Build())
                .WithGeneralLedgerAccountGroup(new GeneralLedgerAccountGroupBuilder(this.DatabaseSession).WithDescription("accountGroup").Build())
                .Build();

            var organisationAccount = new OrganisationGlAccountBuilder(this.DatabaseSession)
                .WithInternalOrganisation(new InternalOrganisations(this.DatabaseSession).FindBy(InternalOrganisations.Meta.Name, "internalOrganisation"))
                .WithGeneralLedgerAccount(account)
                .WithFromDate(new DateTime(2010, 01, 01))
                .Build();
     
            var balance = new OrganisationGlAccountBalanceBuilder(this.DatabaseSession)
                .WithOrganisationGlAccount(organisationAccount)
                .WithAmount(123)
                .WithAccountingPeriod(period).Build(); 

            var accountingTransactionDetail = new AccountingTransactionDetailBuilder(this.DatabaseSession)
                .WithAmount(111)
                .WithDebit(false)
                .WithOrganisationGlAccountBalance(balance)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("111 Credit period 01/01/2010 through 01/02/2010 balance amount 123 account acc001 GL account for internalOrganisation", accountingTransactionDetail.DisplayName);
        }
    }
}
