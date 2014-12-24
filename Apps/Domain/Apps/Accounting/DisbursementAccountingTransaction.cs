// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisbursementAccountingTransaction.cs" company="Allors bvba">
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

    using Allors.Domain;
    

    public partial class DisbursementAccountingTransaction
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

            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.EntryDate);
            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.TransactionDate);
            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.FromParty);
            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.ToParty);
            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.Description);
            derivation.Log.AssertExists(this, DisbursementAccountingTransactions.Meta.Disbursement);

            this.DisplayName = string.Format(
                "Transaction date {0}, {1}, total amount {2}, disbursement {3} from party {4} to party {5}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDescription ? this.Description : null,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0,
                this.ExistDisbursement ? this.Disbursement.ComposeDisplayName() : null,
                this.ExistFromParty ? this.FromParty.DeriveDisplayName() : null,
                this.ExistToParty ? this.ToParty.DeriveDisplayName() : null);

            var characterBoundaryText = string.Format(
                "{0} {1} {2} {3}",
                this.ExistDescription ? this.Description : null,
                this.ExistDisbursement ? this.Disbursement.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistFromParty ? this.FromParty.DeriveSearchDataCharacterBoundaryText() : null,
                this.ExistToParty ? this.ToParty.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1} {2} {3}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDisbursement ? this.Disbursement.ComposeSearchDataWordBoundaryText() : null,
                this.ExistFromParty ? this.FromParty.DeriveSearchDataWordBoundaryText() : null,
                this.ExistToParty ? this.ToParty.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}