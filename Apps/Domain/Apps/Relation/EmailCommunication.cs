// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailCommunication.cs" company="Allors bvba">
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
    
    public partial class EmailCommunication
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
            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();

            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveFromParties();
            this.AppsDeriveToParties();
            this.AppsDeriveInvolvedParties();
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
            searchText.Append("Email ");

            if (this.ExistSubject)
            {
                searchText.Append("subject: ");
                searchText.Append(this.Subject);
            }

            if (this.ExistOriginator)
            {
                searchText.Append(" from: ");
                searchText.Append(this.Originator.ElectronicAddressString);
            }

            searchText.Append(" to:");
            foreach (EmailAddress address in this.Addressees)
            {
                searchText.Append(" ");
                searchText.Append(address.ElectronicAddressString);
            }

            if (this.ExistCarbonCopies)
            {
                searchText.Append(" CC:");
                foreach (EmailAddress carbonCopy in this.CarbonCopies)
                {
                    searchText.Append(" ");
                    searchText.Append(carbonCopy.ElectronicAddressString);
                }
            }

            if (this.ExistBlindCopies)
            {
                searchText.Append(" BC:");
                foreach (EmailAddress blindCopy in this.BlindCopies)
                {
                    searchText.Append(" ");
                    searchText.Append(blindCopy.ElectronicAddressString);
                }
            }

            return searchText.ToString();
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            var text = string.Format(
                "{0} {1}",
                this.ExistSubject ? this.Subject : null,
                this.ExistOriginator ? this.Originator.SearchData.CharacterBoundaryText : null);

            foreach (EmailAddress address in this.Addressees)
            {
                text += " " + address.ElectronicAddressString;
            }

            foreach (EmailAddress address in this.CarbonCopies)
            {
                text += " " + address.ElectronicAddressString;
            }

            foreach (EmailAddress address in this.BlindCopies)
            {
                text += " " + address.ElectronicAddressString;
            }

            return text;
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            return this.ExistOriginator ? this.Originator.SearchData.WordBoundaryText : null;
        }

        private void AppsDeriveFromParties()
        {
            this.RemoveFromParties();
            this.AddToParty(this.Originator.PartyWherePersonalEmailAddress);

        }

        private void AppsDeriveToParties()
        {
            this.RemoveToParties();

            foreach (EmailAddress addressee in this.Addressees)
            {
                this.AddFromParty(addressee.PartyWherePersonalEmailAddress);
            }

            foreach (EmailAddress carbonCopy in this.CarbonCopies)
            {
                this.AddFromParty(carbonCopy.PartyWherePersonalEmailAddress);
            }

            foreach (EmailAddress blindCopy in this.BlindCopies)
            {
                this.AddFromParty(blindCopy.PartyWherePersonalEmailAddress);
            }
        }

        private void AppsDeriveInvolvedParties()
        {
            this.RemoveInvolvedParties();
            this.AddInvolvedParty(this.Owner);

            if (this.ExistOriginator)
            {
                this.AddInvolvedParty(this.Originator.PartyWherePersonalEmailAddress);
            }

            foreach (EmailAddress addressee in this.Addressees)
            {
                this.AddInvolvedParty(addressee.PartyWherePersonalEmailAddress);
            }

            foreach (EmailAddress carbonCopy in this.CarbonCopies)
            {
                this.AddInvolvedParty(carbonCopy.PartyWherePersonalEmailAddress);
            }

            foreach (EmailAddress blindCopy in this.BlindCopies)
            {
                this.AddInvolvedParty(blindCopy.PartyWherePersonalEmailAddress);
            }
        }

        ObjectState Transitional.PreviousObjectState {
            get
            {
                return this.PreviousObjectState;
            }
        }

        ObjectState Transitional.CurrentObjectState {
            get
            {
                return this.CurrentObjectState;
            }
        }
    }
}
