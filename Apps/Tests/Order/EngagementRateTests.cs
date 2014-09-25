//------------------------------------------------------------------------------------------------- 
// <copyright file="EngagementRateTests.cs" company="Allors bvba">
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
    public class EngagementRateTests : DomainTest
    {
        [Test]
        public void GivenEngagementRate_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new EngagementRateBuilder(this.DatabaseSession);
            var engagementRate = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithRatingType(new RatingTypes(this.DatabaseSession).Poor);
            engagementRate = builder.Build();
            
            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithBillingRate(10M);
            engagementRate = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenEngagementRate_WhenDeriving_ThenDisplayNameIsSet()
        {
            var poor = new RatingTypes(this.DatabaseSession).Poor;

            var engagementRate = new EngagementRateBuilder(this.DatabaseSession)
                .WithBillingRate(10M)
                .WithRatingType(poor)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(string.Format("{0} for {1}", 10M, poor.Name), engagementRate.DisplayName);
        }
    }
}
