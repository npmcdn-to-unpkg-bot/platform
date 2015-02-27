// --------------------------------------------------------------------------------------------------------------------
// <copyright file="localeTests.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
    using NUnit.Framework;

    [TestFixture]
    public class LocaleTests : DomainTest
    {
        [Test]
        public void GivenLocale_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new LocaleBuilder(this.DatabaseSession);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLanguage(new Languages(this.DatabaseSession).FindBy(Languages.Meta.IsoCode, "en"));
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCountry(new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "BE"));
            builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenLocale_WhenDeriving_ThenNameIsSet()
        {
            var locale = new LocaleBuilder(this.DatabaseSession)
                .WithLanguage(new Languages(this.DatabaseSession).FindBy(Languages.Meta.IsoCode, "en"))
                .WithCountry(new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "BE"))
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("en-BE", locale.Name);
        }

        [Test]
        public void GivenLocaleWhenValidatingThenRequiredRelationsMustExist()
        {
            var dutch = new Languages(this.DatabaseSession).LanguageByCode["nl"];
            var netherlands = new Countries(this.DatabaseSession).CountryByIsoCode["NL"];

            var builder = new LocaleBuilder(this.DatabaseSession);
            builder.Build();
            
            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            builder.WithLanguage(dutch).Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            builder.WithCountry(netherlands).Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenLocaleWhenValidatingThenNameIsSet()
        {
            var locale = new Locales(this.DatabaseSession).FindBy(Locales.Meta.Name, Locales.DutchNetherlandsName);

            Assert.AreEqual("nl-NL", locale.Name);
        }
    }
}