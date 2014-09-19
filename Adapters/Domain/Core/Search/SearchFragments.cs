// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchFragments.cs" company="Allors bvba">
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
    using Allors;
    using Allors.Meta;

    public partial class SearchFragments : SearchFragment.Cache
    {
        private static readonly string CacheId = "Allors.Cache." + typeof(SearchFragment);

        public Dictionary<string, ObjectId> ObjectIdByStringFragment
        {
            get
            {
                var population = this.Session.Population;
                var cache = (Dictionary<string, ObjectId>)population[CacheId];
                if (cache == null)
                {
                    cache = new Dictionary<string, ObjectId>();
                    population[CacheId] = cache;
                }

                return cache;
            }
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string criteria)
        {
            return Search(session, criteria, null, null);
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string criteria, HashSet<SearchResult> containedIn)
        {
            return Search(session, criteria, containedIn, null);
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string criteria, HashSet<ObjectType> concreteClasses)
        {
            return Search(session, criteria, null, concreteClasses);
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string criteria, HashSet<SearchResult> containedIn, HashSet<ObjectType> concreteClasses)
        {
            var results = new HashSet<SearchResult>();

            var searchFragments = new SearchFragments(session);

            var criteriaParts = SearchResults.WhiteSpaceRegex.Replace(criteria.Trim().ToLower(), " ").Split(' ');
            if (criteriaParts.Length == 1)
            {
                var searchFragment = searchFragments.CachedFindByLowerCaseText(criteriaParts[0].ToLower());
                if (searchFragment != null)
                {
                    searchFragment.GetSearchResults(results, containedIn, concreteClasses);
                }
            }

            if (criteriaParts.Length > 1)
            {
                for (var i = 0; i < criteriaParts.Length; i++)
                {
                    var searchFragment = searchFragments.CachedFindByLowerCaseText(criteriaParts[i].ToLower());
                    if (searchFragment != null)
                    {
                        if (i == 0)
                        {
                            searchFragment.GetSearchResults(results, containedIn, concreteClasses);
                        }
                        else
                        {
                            results.IntersectWith(searchFragment.GetSearchResults(new HashSet<SearchResult>(), containedIn, concreteClasses));
                            if (results.Count == 0)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        return new HashSet<SearchResult>();
                    }
                }
            }

            return results;
        }

        public SearchFragment CachedFindOrCreateByLowerCaseText(string lowerCaseText)
        {
            var searchFragment = this.CachedFindByLowerCaseText(lowerCaseText);

            if (searchFragment == null)
            {
                searchFragment = new SearchFragmentBuilder(this.Session).WithLowerCaseText(lowerCaseText).Build();
                this.ObjectIdByStringFragment[lowerCaseText] = searchFragment.Id;
            }

            return searchFragment;
        }

        public SearchFragment CachedFindByLowerCaseText(string lowerCaseText)
        {
            SearchFragment searchFragment;

            ObjectId objectId;
            if (this.ObjectIdByStringFragment.TryGetValue(lowerCaseText, out objectId))
            {
                searchFragment = (SearchFragment)this.Session.Instantiate(objectId);
            }
            else
            {
                searchFragment = this.FindBy(SearchFragments.Meta.LowerCaseText, lowerCaseText);

                if (searchFragment != null)
                {
                    this.ObjectIdByStringFragment[lowerCaseText] = searchFragment.Id;
                }
            }

            return searchFragment;
        }
    }
}