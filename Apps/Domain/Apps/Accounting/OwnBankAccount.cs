// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwnBankAccount.cs" company="Allors bvba">
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
    public partial class OwnBankAccount
    {
        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistIsActive)
            {
                this.IsActive = true;
            }
        }

        public void AppsPrepareDerivation(ObjectPrepareDerivation method)
        {
            var derivation = method.Derivation;

            if (this.ExistBankAccount && derivation.ChangeSet.GetRoleTypes(this.Id).Contains(OwnBankAccounts.Meta.BankAccount))
            {
                derivation.AddDerivable(this.BankAccount);
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            if (!this.ExistDescription && this.ExistBankAccount)
            {
                this.Description = this.BankAccount.ComposeDisplayName();
            }

            if (this.ExistInternalOrganisationWherePaymentMethod && this.InternalOrganisationWherePaymentMethod.DoAccounting)
            { 
                derivation.Log.AssertExists(this, OwnBankAccounts.Meta.Creditor);
                derivation.Log.AssertAtLeastOne(this, Cashes.Meta.GeneralLedgerAccount, Cashes.Meta.Journal);
            }
            
            derivation.Log.AssertExistsAtMostOne(this, Cashes.Meta.GeneralLedgerAccount, Cashes.Meta.Journal);

            this.DisplayName = this.ExistBankAccount ? this.BankAccount.ComposeDisplayName() : null;

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistBankAccount ? this.BankAccount.ComposeSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = this.ExistBankAccount ? this.BankAccount.ComposeSearchDataWordBoundaryText() : null;

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}