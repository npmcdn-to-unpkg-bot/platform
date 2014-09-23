// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Depreciation.cs" company="Allors bvba">
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
    

    public partial class Depreciation
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, Depreciations.Meta.EntryDate);
            derivation.Log.AssertExists(this, Depreciations.Meta.TransactionDate);
            derivation.Log.AssertExists(this, Depreciations.Meta.Description);
            derivation.Log.AssertExists(this, Depreciations.Meta.InternalOrganisation);
            derivation.Log.AssertExists(this, Depreciations.Meta.FixedAsset);

            this.DisplayName = string.Format(
                "Transaction date {0}, {1} total amount {2} fixed asset {3} for {4}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDescription ? this.Description : null,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0,
                this.ExistFixedAsset ? this.FixedAsset.Name : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.Name : null);

            var characterBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistDescription ? this.Description : null,
                this.ExistFixedAsset ? this.FixedAsset.Name : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}