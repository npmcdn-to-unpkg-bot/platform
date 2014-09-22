// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalisedTextTests.cs" company="Allors bvba">
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
    public class LocalisedTextTests : DomainTest
    {
        [Test]
        public void GivenLocalisedTextWhenValidatingThenRequiredRelationsMustExist()
        {
            var builder = new LocalisedTextBuilder(this.DatabaseSession);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            builder.WithText("description");

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenLocalisedTextWhenValidatingThenDisplayNameIsSet()
        {
            var localeDescription = new LocalisedTextBuilder(this.DatabaseSession).WithText("description").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("description", localeDescription.DisplayName);
        }
    }
}