// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchFragmentTests.cs" company="Allors bvba">
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
    using System.Linq;

    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class SearchFragmentTests : DomainTest
    {
        [Test]
        public void GivenSearchTextDividedByMultipleSpacesWhenSearchingThenExtraWhiteSpaceIsIgnored()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("van knippenberg").Build();

            this.DatabaseSession.Derive(true);

            var result = SearchFragments.Search(this.DatabaseSession, "van      knippenberg");

            Assert.AreEqual(1, result.Count);
            Assert.Contains(person, result.ToArray());
        }

        [Test]
        public void GivenSearchTextWithMixedCaseWhenSearchingThenCaseIsIgnored()
        {
            var martien = new PersonBuilder(this.DatabaseSession).WithLastName("VaN KniPPenberg").Build();

            this.DatabaseSession.Derive(true);

            var result = SearchFragments.Search(this.DatabaseSession, "vAn knippenBERG");

            Assert.AreEqual(1, result.Count);
            Assert.Contains(martien, result.ToArray());
        }

        [Test]
        public void GivenSearchTextWhenSearchingThenResultIsQueriedUsingAndFilter()
        {
            var martien = new PersonBuilder(this.DatabaseSession).WithLastName("van knippenberg").Build();
            var koen = new PersonBuilder(this.DatabaseSession).WithLastName("van exem").Build();

            this.DatabaseSession.Derive(true);

            var result = SearchFragments.Search(this.DatabaseSession, "van           knippenberg             ");
            Assert.AreEqual(1, result.Count);
            Assert.Contains(martien, result.ToArray());

            result = SearchFragments.Search(this.DatabaseSession, "van");
            Assert.AreEqual(2, result.Count);

            Assert.Contains(martien, result.ToArray());
            Assert.Contains(koen, result.ToArray());

            result = SearchFragments.Search(this.DatabaseSession, "knippenberg");
            Assert.AreEqual(1, result.Count);

            Assert.Contains(martien, result.ToArray());
        }
    }
}