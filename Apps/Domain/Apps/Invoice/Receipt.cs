// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Receipt.cs" company="Allors bvba">
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

    using Resources;

    public partial class Receipt
    {       
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistEffectiveDate)
            {
                this.EffectiveDate = DateTime.Now;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected string AppsComposeDisplayName()
        {
            return string.Format("{0} receipt for amount {1}", this.ExistEffectiveDate ? this.EffectiveDate : DateTime.MinValue, this.ExistAmount ? this.Amount : 0);
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            return null;
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format("{0} {1}", this.ExistEffectiveDate ? this.EffectiveDate : DateTime.MinValue, this.ExistAmount ? this.Amount : 0);
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            foreach (PaymentApplication paymentApplication in this.PaymentApplications)
            {
                derivation.AddDependency(this, paymentApplication);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, Receipts.Meta.EffectiveDate);
            derivation.Log.AssertExists(this, Receipts.Meta.Amount);

            decimal totalAmountApplied = 0;
            foreach (PaymentApplication paymentApplication in this.PaymentApplications)
            {
                totalAmountApplied += paymentApplication.AmountApplied;
            }

            if (this.ExistAmount && totalAmountApplied > this.Amount)
            {
                derivation.Log.AddError(this, Receipts.Meta.Amount, ErrorMessages.ReceiptAmountIsToSmall);
            }

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private void AppsSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }
    }
}