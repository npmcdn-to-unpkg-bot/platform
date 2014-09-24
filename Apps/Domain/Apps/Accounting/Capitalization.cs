// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Capitalization.cs" company="Allors bvba">
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
    

    public partial class Capitalization
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
            

            derivation.Log.AssertExists(this, Capitalizations.Meta.EntryDate);
            derivation.Log.AssertExists(this, Capitalizations.Meta.TransactionDate);
            derivation.Log.AssertExists(this, Capitalizations.Meta.Description);
            derivation.Log.AssertExists(this, Capitalizations.Meta.InternalOrganisation);

            this.DisplayName = string.Format(
                "Transaction date {0}, {1} total amount {2} for {3}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDescription ? this.Description : null,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0,
                this.ExistInternalOrganisation ? this.InternalOrganisation.Name : null);

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistTransactionDate ? this.TransactionDate : DateTime.MinValue,
                this.ExistDerivedTotalAmount ? this.DerivedTotalAmount : 0,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}