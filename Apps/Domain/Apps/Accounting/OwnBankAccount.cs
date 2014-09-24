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
    using Allors.Domain;
    

    public partial class OwnBankAccount
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

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            if (this.ExistBankAccount && derivation.ChangeSet.GetRoleTypes(this.Id).Contains(OwnBankAccounts.Meta.BankAccount))
            {
                derivation.AddDerivable(this.BankAccount);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            if (!this.ExistDescription && this.ExistBankAccount)
            {
                this.Description = this.BankAccount.ComposeDisplayName();
            }

            if (this.ExistInternalOrganisationWherePaymentMethod && this.InternalOrganisationWherePaymentMethod.DoAccounting)
            { 
                derivation.Log.AssertExists(this, OwnBankAccounts.Meta.Creditor);
                derivation.Log.AssertAtLeastOne(this, Cashes.Meta.GeneralLedgerAccount, Cashes.Meta.Journal);
            }
            
            derivation.Log.AssertExists(this, OwnBankAccounts.Meta.Description);
            derivation.Log.AssertExists(this, OwnBankAccounts.Meta.BankAccount);
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