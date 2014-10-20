// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Searchables.cs" company="Allors bvba">
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
    using Allors;
    using Allors.Meta;
    using System.Collections.Generic;

    public partial class Searchables
    {
        internal static void AddSearchResult(IObject searchable, HashSet<SearchResult> searchResults, HashSet<SearchResult> containedIn, HashSet<ObjectType> concreteClasses)
        {
            if (searchable is SearchResult)
            {
                var searchResult = (SearchResult)searchable;
                Add(searchResults, searchResult, containedIn, concreteClasses);
            }
            else
            {
                // TODO: Add search result pointers
                //var objectType = new ObjectTypes(searchable.Strategy.Session).FindByPlatformObjectType(searchable.Strategy.ObjectType);
                //if (objectType.ExistSearchResultPaths)
                //{
                //    foreach (Path path in objectType.SearchResultPaths)
                //    {
                //        var value = path.GetValue((ObjectBase)searchable) as IObject;
                //        if (value is SearchResult)
                //        {
                //            var searchResult = (SearchResult)value;
                //            if (containedIn.Contains(searchResult))
                //            {
                //                Add(searchResults, searchResult, containedIn, concreteClasses);
                //            }
                //        }
                //        else if (value != null)
                //        {
                //            AddSearchResult(value, searchResults, containedIn, concreteClasses);
                //        }
                //    }
                //}
            }
        }

        private static void Add(HashSet<SearchResult> searchResults, SearchResult searchResult, HashSet<SearchResult> containedIn, HashSet<Allors.Meta.ObjectType> concreteClasses)
        {
            if (containedIn != null && !containedIn.Contains(searchResult))
            {
                return;
            }

            if (concreteClasses != null && !concreteClasses.Contains((ObjectType)searchResult.Strategy.ObjectType))
            {
                return;
            }

            searchResults.Add(searchResult);
        }
    }
}