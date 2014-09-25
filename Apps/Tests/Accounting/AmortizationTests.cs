//------------------------------------------------------------------------------------------------- 
// <copyright file="AmortizationTests.cs" company="Allors bvba">
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
    public class AmortizationTests : DomainTest
    {
        [Test]
        public void GivenAmortization_WhenDeriving_ThenDisplayNameIsSet()
        {
            var internalOrganisation =
                new InternalOrganisations(this.DatabaseSession).FindBy(UserInterfaceables.Meta.DisplayName, "internalOrganisation");

            var amortization =
                new AmortizationBuilder(this.DatabaseSession).WithTransactionDate(DateTime.Now).WithEntryDate(
                    DateTime.Now).WithDescription("amortization").WithInternalOrganisation(internalOrganisation).Build();

            var expectedDisplayName = string.Format(
                "Transaction date {0}, {1}, for {2}",
                amortization.TransactionDate,
                amortization.Description,
                internalOrganisation.DisplayName);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(expectedDisplayName, amortization.DisplayName);
        }
    }
}
