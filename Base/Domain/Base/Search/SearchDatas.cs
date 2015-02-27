// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchDatas.cs" company="Allors bvba">
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

    using Allors;
    using Allors.Meta;

    public partial class SearchDatas
    {
        public static bool SkipDerivation
        {
            get;
            set;
        }

        public static void Derive(IDatabase database)
        {
            using (var session = database.CreateSession())
            {
                var staleSearchDatas = new SearchDatas(session).Extent();
                var or = staleSearchDatas.Filter.AddOr();
                or.AddNot().AddEquals(Meta.CharacterBoundaryText, Meta.PreviousCharacterBoundaryText);
                or.AddNot().AddEquals(Meta.WordBoundaryText, Meta.PreviousWordBoundaryText);

                var andCharachterBoundary = or.AddAnd();
                andCharachterBoundary.AddExists(Meta.CharacterBoundaryText);
                andCharachterBoundary.AddNot().AddExists(Meta.PreviousCharacterBoundaryText);

                var andWordBoundary = or.AddAnd();
                andWordBoundary.AddExists(Meta.WordBoundaryText);
                andWordBoundary.AddNot().AddExists(Meta.PreviousWordBoundaryText);

                var searchFragmentCache = new SearchFragmentCache(session);

                var counter = 0;
                foreach (SearchData searchData in staleSearchDatas)
                {
                    searchData.DeriveSearchFragments(searchFragmentCache);

                    if (++counter % 100 == 0)
                    {
                        session.Commit();
                    }
                }

                session.Commit();
            }
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string displayName)
        {
            return Search(session, displayName, null);
        }

        public static HashSet<SearchResult> Search(IDatabaseSession session, string displayName, HashSet<Class> classes)
        {
            var results = new HashSet<SearchResult>();

            return results;
        }

        private class SearchFragmentCache : SearchFragment.Cache
        {
            private readonly Dictionary<string, ObjectId> objectIdByStringFragment;
            private readonly IDatabaseSession session;

            public SearchFragmentCache(IDatabaseSession session)
            {
                this.session = session;
                this.objectIdByStringFragment = new Dictionary<string, ObjectId>();
                foreach (SearchFragment searchFragment in new SearchFragments(this.session).Extent())
                {
                    this.objectIdByStringFragment[searchFragment.LowerCaseText] = searchFragment.Id;
                }
            }

            public SearchFragment CachedFindOrCreateByLowerCaseText(string lowerCaseText)
            {
                SearchFragment searchFragment;

                ObjectId objectId;
                if (this.objectIdByStringFragment.TryGetValue(lowerCaseText, out objectId))
                {
                    searchFragment = (SearchFragment)this.session.Instantiate(objectId);
                }
                else
                {
                    searchFragment = new SearchFragmentBuilder(this.session).WithLowerCaseText(lowerCaseText).Build();
                    this.objectIdByStringFragment[lowerCaseText] = searchFragment.Id;
                }

                return searchFragment;
            }
        }
    }
}