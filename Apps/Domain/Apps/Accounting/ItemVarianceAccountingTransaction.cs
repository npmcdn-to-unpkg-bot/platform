// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemVarianceAccountingTransaction.cs" company="Allors bvba">
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

    public partial class ItemVarianceAccountingTransaction
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

            derivation.Log.AssertExists(this, ItemVarianceAccountingTransactions.Meta.EntryDate);
            derivation.Log.AssertExists(this, ItemVarianceAccountingTransactions.Meta.TransactionDate);
            derivation.Log.AssertExists(this, ItemVarianceAccountingTransactions.Meta.Description);

            this.DisplayName = string.Format(
                "Transaction date {0}, {1} total amount {2}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDescription ? this.Description : null,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0);

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0);

            this.SearchData.CharacterBoundaryText = this.Description;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}