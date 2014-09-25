//------------------------------------------------------------------------------------------------- 
// <copyright file="RevenueQuantityBreakTests.cs" company="Allors bvba">
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
    public class RevenueQuantityBreakTests : DomainTest
    {
        [Test]
        public void GivenRevenueQuantityBreak_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new RevenueQuantityBreakBuilder(this.DatabaseSession);
            var revenueQuantityBreak = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFrom(10);
            revenueQuantityBreak = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithThrough(20);
            revenueQuantityBreak = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            revenueQuantityBreak.RemoveFrom();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenRevenueQuantityBreak_WhenDeriving_ThenDisplayNameIsSet()
        {
            var fromAmount = 10M;
            var throughAmount = 20M;

            var quantityBreak = new RevenueQuantityBreakBuilder(this.DatabaseSession)
                .WithFrom(fromAmount)
                .Build();

            this.DatabaseSession.Derive(true); 

            var displayname = string.Format("Revenue quantity: from {0} through {1}", fromAmount, 0);
            Assert.AreEqual(displayname, quantityBreak.DisplayName);

            quantityBreak.Through = throughAmount;
            
            this.DatabaseSession.Derive(true);

            displayname = string.Format("Revenue quantity: from {0} through {1}", fromAmount, throughAmount);
            Assert.AreEqual(displayname, quantityBreak.DisplayName);
        }
    }
}
