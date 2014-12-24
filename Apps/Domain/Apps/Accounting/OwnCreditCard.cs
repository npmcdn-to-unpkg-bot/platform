// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwnCreditCard.cs" company="Allors bvba">
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

    public partial class OwnCreditCard
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistIsActive)
            {
                this.IsActive = true;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            if (!this.ExistDescription && this.ExistCreditCard)
            {
                this.Description = this.CreditCard.ComposeDisplayName();
            }

            if (this.ExistInternalOrganisationWherePaymentMethod && this.InternalOrganisationWherePaymentMethod.DoAccounting.HasValue && this.InternalOrganisationWherePaymentMethod.DoAccounting.Value)
            { 
                derivation.Log.AssertExists(this, OwnCreditCards.Meta.Creditor);
                derivation.Log.AssertAtLeastOne(this, Cashes.Meta.GeneralLedgerAccount, Cashes.Meta.Journal);
            }

            if (this.ExistCreditCard)
            {
                if (this.CreditCard.ExpirationYear <= DateTime.Now.Year && this.CreditCard.ExpirationMonth <= DateTime.Now.Month)
                {
                    this.IsActive = false;
                }
            }

            derivation.Log.AssertExistsAtMostOne(this, Cashes.Meta.GeneralLedgerAccount, Cashes.Meta.Journal);

            this.DisplayName = this.ExistCreditCard? this.CreditCard.ComposeDisplayName() : null;

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistCreditCard ? this.CreditCard.ComposeSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = this.ExistCreditCard ? this.CreditCard.ComposeSearchDataWordBoundaryText() : null;

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}