// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Journal.cs" company="Allors bvba">
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

    using Resources;

    public partial class Journal
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistBlockUnpaidTransactions)
            {
                this.BlockUnpaidTransactions = false;
            }

            if (!this.ExistCloseWhenInBalance)
            {
                this.CloseWhenInBalance = false;
            }

            if (!this.ExistUseAsDefault)
            {
                this.UseAsDefault = false;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, Journals.Meta.Description);
            derivation.Log.AssertExists(this, Journals.Meta.InternalOrganisation);
            derivation.Log.AssertExists(this, Journals.Meta.JournalType);
            derivation.Log.AssertExists(this, Journals.Meta.ContraAccount);

            this.DeriveContraAccount(derivation);
            this.DerivePreviousJournalType(derivation);

            this.DisplayName = this.ExistDescription ? this.Description : null;

            var characterBoundaryText = this.ExistDescription ? this.Description : null;

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = null;
        }

        private void AppsDeriveContraAccount(IDerivation derivation)
        {
            if (this.ExistPreviousContraAccount)
            {
                if (this.PreviousContraAccount.ExistJournalEntryDetailsWhereGeneralLedgerAccount)
                {
                    derivation.Log.AssertAreEqual(this, Journals.Meta.ContraAccount, Journals.Meta.PreviousContraAccount);
                }
                else
                {
                    this.PreviousContraAccount = this.ContraAccount;
                }
            }
            else
            {
                if (this.JournalType.Equals(new JournalTypes(this.Session).Bank))
                {
                    // initial derivation of ContraAccount, PreviousContraAccount does not exist yet.
                    if (this.ExistContraAccount)
                    {
                        var savedContraAccount = this.ContraAccount;
                        this.RemoveContraAccount();
                        if (!savedContraAccount.IsNeutralAccount())
                        {
                            derivation.Log.AddError(this, Journals.Meta.ContraAccount, ErrorMessages.GeneralLedgerAccountNotNeutral);
                        }

                        if (!savedContraAccount.GeneralLedgerAccount.BalanceSheetAccount)
                        {
                            derivation.Log.AddError(this, Journals.Meta.ContraAccount, ErrorMessages.GeneralLedgerAccountNotBalanceAccount);
                        }

                        this.ContraAccount = savedContraAccount;
                    }
                }

                if (!derivation.Log.HasErrors)
                {
                    this.PreviousContraAccount = this.ContraAccount;
                }
            }
        }

        private void AppsDerivePreviousJournalType(IDerivation derivation)
        {
            if (this.ExistPreviousJournalType)
            {
                if (this.ExistPreviousContraAccount && this.PreviousContraAccount.ExistJournalEntryDetailsWhereGeneralLedgerAccount)
                {
                    derivation.Log.AssertAreEqual(this, Journals.Meta.JournalType, Journals.Meta.PreviousJournalType);
                }
                else
                {
                    this.PreviousJournalType = this.JournalType;
                }
            }
            else
            {
                this.PreviousJournalType = this.JournalType;
            }
        }
    }
}