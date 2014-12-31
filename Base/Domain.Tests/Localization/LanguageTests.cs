// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LanguageTests.cs" company="Allors bvba">
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
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class LanguageTests : DomainTest
    {
        [Test]
        public void GivenLanguageWhenValidatingThenRequiredRelationsMustExist()
        {
            var builder = new LanguageBuilder(this.DatabaseSession);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);
               
            builder.WithIsoCode("XX").Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession).WithLocale(new Locales(this.DatabaseSession).FindBy(Locales.Meta.Name, Locales.EnglishGreatBritainName)).WithText("XXX)").Build());
        
            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenLanguageWhenValidatingThenDisplayNameIsSet()
        {
            var language = new Languages(this.DatabaseSession).FindBy(Languages.Meta.IsoCode, "nl");

            Assert.AreEqual("Dutch", language.DisplayName);
        }
    }
}