// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRevenueHistoryTests.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
    
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class PackageRevenueHistoryTests : DomainTest
    {
        [Test]
        public void GivenPackageRevenueHistory_WhenDeriving_ThenDisplayNameIsSet()
        {
            const decimal Revenue = 100.25M;
            var internalOrganisation = new InternalOrganisations(this.DatabaseSession).FindBy(InternalOrganisations.Meta.Name, "internalOrganisation");
            var package = new PackageBuilder(this.DatabaseSession).WithName("package").Build();

            var revenueHistory = new PackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(internalOrganisation)
                .WithPackage(package)
                .WithRevenue(Revenue)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(string.Format("{0}: {1} revenue trailing twelve months at {2}", package.DisplayName, Revenue.AsCurrencyString(internalOrganisation.CurrencyFormat), internalOrganisation.DisplayName), revenueHistory.DisplayName);
        }
    }
}
