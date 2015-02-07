// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhoneCommunication.cs" company="Allors bvba">
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
    using System.Text;

    public partial class PhoneCommunication
    {
        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new CommunicationEventObjectStates(this.Strategy.DatabaseSession).Scheduled;
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
            
            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveFromParties();
            this.AppsDeriveToParties();
            this.AppsDeriveInvolvedParties(derivation);
        }

        protected void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        protected void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        protected void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        protected string AppsComposeDisplayName()
        {
            var searchText = new StringBuilder();
            searchText.Append("Phone conversation ");

            if (this.ExistCaller)
            {
                searchText.Append("from: ");
                searchText.Append(this.Caller.DeriveDisplayName());
            }

            searchText.Append(" with:");
            foreach (Party receiver in this.Receivers)
            {
                searchText.Append(" ");
                searchText.Append(receiver.DeriveDisplayName());
            }

            return searchText.ToString();
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistCaller ? this.Caller.DeriveSearchDataCharacterBoundaryText() : null);
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            var text = this.ExistCaller ? this.Caller.DeriveSearchDataWordBoundaryText() : null;

            foreach (Party party in this.Receivers)
            {
                text += " " + party.DeriveSearchDataWordBoundaryText();
            }

            return text;
        }

        private void AppsDeriveFromParties()
        {
            this.RemoveFromParties();
            this.AddFromParty(this.Caller);
        }

        private void AppsDeriveToParties()
        {
            this.RemoveToParties();
            this.ToParties = (Extent)this.Receivers;
        }

        private void AppsDeriveInvolvedParties(IDerivation derivation)
        {
            this.RemoveInvolvedParties();
            this.InvolvedParties = (Extent)this.Receivers;
            this.AddInvolvedParty(this.Owner);
            this.AddInvolvedParty(this.Caller);
        }

        ObjectState Transitional.PreviousObjectState
        {
            get
            {
                return this.PreviousObjectState;
            }
        }

        ObjectState Transitional.CurrentObjectState
        {
            get
            {
                return this.CurrentObjectState;
            }
        }
    }
}
