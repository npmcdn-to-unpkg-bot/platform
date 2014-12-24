// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxDue.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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
    using System;

    public partial class TaxDue
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, TaxDues.Meta.EntryDate);
            derivation.Log.AssertExists(this, TaxDues.Meta.TransactionDate);
            derivation.Log.AssertExists(this, TaxDues.Meta.FromParty);
            derivation.Log.AssertExists(this, TaxDues.Meta.ToParty);
            derivation.Log.AssertExists(this, TaxDues.Meta.Description);

            this.DisplayName = string.Format(
                "Transaction date {0}, {1}, total amount {2} from party {3} to party {4}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDescription ? this.Description : null,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0,
                this.ExistFromParty ? this.FromParty.DeriveDisplayName() : null,
                this.ExistToParty ? this.ToParty.DeriveDisplayName() : null);

            var characterBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistDescription ? this.Description : null,
                this.ExistFromParty ? this.FromParty.DeriveSearchDataCharacterBoundaryText() : null,
                this.ExistToParty ? this.ToParty.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistFromParty ? this.FromParty.DeriveSearchDataWordBoundaryText() : null,
                this.ExistToParty ? this.ToParty.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}