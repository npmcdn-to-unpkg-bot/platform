// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchFragment.cs" company="Allors bvba">
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
    using System.Collections.Generic;

    using Allors.Meta;

    public partial class SearchFragment
    {
        public interface Cache
        {
            SearchFragment CachedFindOrCreateByLowerCaseText(string lowerCaseText);
        }

        public HashSet<SearchResult> GetSearchResults(HashSet<SearchResult> searchResults, HashSet<SearchResult> containedIn, HashSet<ObjectType> concreteClasses)
        {
            foreach (SearchData searchData in this.SearchDatasWhereSearchFragment)
            {
                var searchable = searchData.SearchableWhereSearchData;
                if (searchable != null)
                {
                    Searchables.AddSearchResult(searchable, searchResults, containedIn, concreteClasses);
                }
            }

            return searchResults;
        }

        public override string ToString()
        {
            return this.LowerCaseText;
        }
    }
}