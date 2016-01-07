//------------------------------------------------------------------------------------------------- 
// <copyright file="CurrencyTests.cs" company="Allors bvba">
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
// <summary>Defines the CurrencyTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Domain
{
    using Allors;
    using Allors.Domain;
    using Allors.Meta;

    using NUnit.Framework;

    [TestFixture]
    public class CurrencyTests : DomainTest
    {
        [Test]
        public void GivenCurrencyWhenValidatingThenRequiredRelationsMustExist()
        {
            var builder = new CurrencyBuilder(this.Session);
            builder.Build();

            Assert.IsTrue(this.Session.Derive().HasErrors);

            builder.WithIsoCode("BND").Build();

            Assert.IsTrue(this.Session.Derive().HasErrors);

            builder.WithSymbol("$").Build();

            Assert.IsTrue(this.Session.Derive().HasErrors);

            builder
                .WithLocalisedName(new LocalisedTextBuilder(this.Session)
                .WithText("Brunei Dollar")
                .WithLocale(new Locales(this.Session).FindBy(M.Locale.Name, Locales.EnglishGreatBritainName))
                .Build());

            Assert.IsFalse(this.Session.Derive().HasErrors);
        }
    }
}
