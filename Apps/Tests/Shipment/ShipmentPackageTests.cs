//------------------------------------------------------------------------------------------------- 
// <copyright file="ShipmentPackageTests.cs" company="Allors bvba">
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
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class ShipmentPackageTests : DomainTest
    {
        [Test]
        public void GivenShipmentPackageBuilder_WhenBuild_ThenPostBuildRelationsMustExist()
        {
            var package = new ShipmentPackageBuilder(this.DatabaseSession).Build();

            Assert.IsNotNull(package.CreationDate);
        }

        [Test]
        public void GivenShipmentPackage_WhenDeriving_ThenDisplayNameIsSet()
        {
            var customer = new PersonBuilder(this.DatabaseSession).WithLastName("customer").Build();
            var mechelen = new CityBuilder(this.DatabaseSession).WithName("Mechelen").Build();
            var shipToAddress = new PostalAddressBuilder(this.DatabaseSession).WithGeographicBoundary(mechelen).WithAddress1("Haverwerf 15").Build();

            var shipment = new CustomerShipmentBuilder(this.DatabaseSession)
                .WithShipToParty(customer)
                .WithShipToAddress(shipToAddress)
                .WithShipmentMethod(new ShipmentMethods(this.DatabaseSession).Ground)
                .Build();

            shipment.AddShipmentPackage(new ShipmentPackageBuilder(this.DatabaseSession).Build());

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(string.Format("Package 1"), shipment.ShipmentPackages[0].DisplayName);

            shipment.AddShipmentPackage(new ShipmentPackageBuilder(this.DatabaseSession).Build());

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(string.Format("Package 2"), shipment.ShipmentPackages[1].DisplayName);
        }
    }
}