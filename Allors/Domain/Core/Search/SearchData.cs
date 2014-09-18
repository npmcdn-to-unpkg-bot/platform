// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchData.cs" company="Allors bvba">
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
    using Allors.Domain;
    using System.Collections.Generic;

    public partial class SearchData
    {
        public void DeriveSearchFragments(SearchFragment.Cache searchFragmentCache)
        {
            if (!this.ExistWordBoundaryText && !this.ExistCharacterBoundaryText)
            {
                this.RemoveSearchFragments();

                this.RemovePreviousWordBoundaryText();
                this.RemovePreviousCharacterBoundaryText();
            }
            else
            {
                if (!Equals(this.WordBoundaryText, this.PreviousWordBoundaryText)
                    || !Equals(this.CharacterBoundaryText, this.PreviousCharacterBoundaryText))
                {
                    var currentStringFragments = new HashSet<string>();
                    foreach (SearchFragment searchFragment in this.SearchFragments)
                    {
                        currentStringFragments.Add(searchFragment.LowerCaseText);
                    }

                    var newStringFragments = this.CreateStringFragments();

                    var toAdd = new HashSet<string>(newStringFragments);
                    toAdd.ExceptWith(currentStringFragments);

                    var toRemove = new HashSet<string>(currentStringFragments);
                    toRemove.ExceptWith(newStringFragments);

                    foreach (SearchFragment searchFragment in this.SearchFragments)
                    {
                        if (toRemove.Contains(searchFragment.LowerCaseText))
                        {
                            this.RemoveSearchFragment(searchFragment);
                        }
                    }

                    foreach (var toAddString in toAdd)
                    {
                        var searchFragment = searchFragmentCache.CachedFindOrCreateByLowerCaseText(toAddString);
                        this.AddSearchFragment(searchFragment);
                    }

                    this.PreviousWordBoundaryText = this.WordBoundaryText;
                    this.PreviousCharacterBoundaryText = this.CharacterBoundaryText;
                }
            }
        }

        protected override void CoreDerive(IDerivation derivation)
        {
            base.CoreDerive(derivation);

            if (!SearchDatas.SkipDerivation)
            {
                this.DeriveSearchFragments(new SearchFragments(this.Session));
            }
        }

        private HashSet<string> CreateStringFragments()
        {
            var set = new HashSet<string>();

            if (this.ExistCharacterBoundaryText)
            {
                var normalized = SearchResults.WhiteSpaceRegex.Replace(this.CharacterBoundaryText.ToLower(), " ");
                var stringFragments = normalized.Split(' ');

                foreach (var stringFragment in stringFragments)
                {
                    var searchText = stringFragment;

                    for (var j = 2; j <= searchText.Length; j++)
                    {
                        for (var i = 0; i <= searchText.Length - j; i++)
                        {
                            var lowerCaseSubString = searchText.Substring(i, j);
                            set.Add(lowerCaseSubString);
                        }
                    }
                }
            }

            if (this.ExistWordBoundaryText)
            {
                var normalized = SearchResults.WhiteSpaceRegex.Replace(this.WordBoundaryText.ToLower(), " ");
                var stringFragments = normalized.Split(' ');

                foreach (var stringFragment in stringFragments)
                {
                    set.Add(stringFragment);
                }
            }

            return set;
        }
    }
}