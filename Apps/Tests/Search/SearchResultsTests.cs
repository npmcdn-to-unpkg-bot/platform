// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResultsTests.cs" company="Allors bvba">
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
    using global::System.Collections.Generic;

    using NUnit.Framework;

    using Allors.Domain;

    [TestFixture]
    public class SearchResultsTests : DomainTest
    {
        [Test]
        public void Default()
        {
            var mechelen = new PlaceBuilder(this.DatabaseSession).WithPostalCode("2800").WithCity("Mechelen").Build();

            this.DatabaseSession.Derive(true);

            var fragment = new SearchFragments(this.DatabaseSession).FindBy(SearchFragments.Meta.LowerCaseText, "2800");

            var searchables = new List<Searchable>();
            foreach (SearchData searchData in fragment.SearchDatasWhereSearchFragment)
            {
                searchables.Add(searchData.SearchableWhereSearchData);
            }

            Assert.AreEqual(1, searchables.Count);
            Assert.AreEqual(mechelen, searchables[0]);

            var searchResults = fragment.GetSearchResults(new HashSet<SearchResult>(), null, null);
            Assert.AreEqual(0, searchResults.Count);
        }
    }
}