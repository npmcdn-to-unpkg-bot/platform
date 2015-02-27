//------------------------------------------------------------------------------------------------- 
// <copyright file="AgreementTest.cs" company="Allors bvba">
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
    using NUnit.Framework;

    [TestFixture]
    public class AgreementTest : DomainTest
    {
        [Test]
        public void GivenClientAgreement_WhenDeriving_ThenDescriptionIsRequired()
        {
            var builder = new ClientAgreementBuilder(this.DatabaseSession);
            var clientAgreement = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("client agreement");
            clientAgreement = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenEmploymentAgreement_WhenDeriving_ThenDescriptionIsRequired()
        {
            var builder = new EmploymentAgreementBuilder(this.DatabaseSession);
            var employmentAgreement = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("employment agreement");
            employmentAgreement = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenPurchaseAgreement_WhenDeriving_ThenDescriptionIsRequired()
        {
            var builder = new PurchaseAgreementBuilder(this.DatabaseSession);
            var purchaseAgreement = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("purchase agreement");
            purchaseAgreement = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenSalesAgreement_WhenDeriving_ThenDescriptionIsRequired()
        {
            var builder = new SalesAgreementBuilder(this.DatabaseSession);
            var salesAgreement = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("sales agreement");
            salesAgreement = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenSubContractorAgreement_WhenDeriving_ThenDescriptionIsRequired()
        {
            var builder = new SubContractorAgreementBuilder(this.DatabaseSession);
            var subContractorAgreement = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithDescription("subContractor agreement");
            subContractorAgreement = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }
    }
}
