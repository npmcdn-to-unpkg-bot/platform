// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResults.cs" company="Allors bvba">
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
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Allors;

    public partial class SearchResults
    {
        public static readonly SearchResult[] EmptyArray = new SearchResult[0];

        public static readonly Regex WhiteSpaceRegex = new Regex(@"\s+");

        public static readonly IComparer<SearchResult> Comparer = new SearchResultsComparer();

        public static HashSet<SearchResult> XSearch(IDatabaseSession session, string displayName)
        {
            return XSearch(session, displayName, null);
        }

        public static HashSet<SearchResult> XSearch(IDatabaseSession session, string displayName, HashSet<Meta.Class> concreteClasses)
        {
            var results = new HashSet<SearchResult>();

            return results;
        }

        public class SearchResultsComparer : IComparer<SearchResult>
        {
            public int Compare(SearchResult x, SearchResult y)
            {
                return -1;
            }
        }
    }
}