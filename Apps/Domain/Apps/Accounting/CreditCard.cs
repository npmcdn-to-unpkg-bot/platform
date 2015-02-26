// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreditCard.cs" company="Allors bvba">
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
    public partial class CreditCard
    {
        protected string AppsComposeDisplayName()
        {
            return string.Format(
                "{0} card {1}, account holder {2}",
                this.ExistCreditCardCompany ? this.CreditCardCompany.Name : null,
                this.ExistCardNumber ? this.CardNumber : null,
                this.ExistNameOnCard ? this.NameOnCard : null);
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            return this.ExistNameOnCard ? this.NameOnCard: null;
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0} {1}",
                this.ExistCreditCardCompany ? this.CreditCardCompany.Name : null,
                this.ExistCardNumber ? this.CardNumber : null);
        }
        
        public void AppsPrepareDerivation(ObjectPrepareDerivation method)
        {
            var derivation = method.Derivation;

            if (this.ExistOwnCreditCardsWhereCreditCard)
            {
                foreach (Object ownCreditCard in this.OwnCreditCardsWhereCreditCard)
                {
                    derivation.AddDerivable(ownCreditCard);                    
                }
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertIsUnique(this, CreditCards.Meta.CardNumber);

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