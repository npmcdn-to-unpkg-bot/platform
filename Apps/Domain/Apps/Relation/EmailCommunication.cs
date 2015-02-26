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

using System;
using Allors.Meta;

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

        public void AppsDerive(ObjectDerive method)
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
            this.AddFromParty(this.Originator.PartyWherePersonalEmailAddress);
        }

        private void AppsDeriveToParties()
        {
            this.RemoveToParties();

            foreach (EmailAddress addressee in this.Addressees)
            {
                if (addressee.ExistPartyWherePersonalEmailAddress && !ToParties.Contains(addressee.PartyWherePersonalEmailAddress))
                {
                    this.AddToParty(addressee.PartyWherePersonalEmailAddress);
                }
            }

            foreach (EmailAddress carbonCopy in this.CarbonCopies)
            {
                if (carbonCopy.ExistPartyWherePersonalEmailAddress && !ToParties.Contains(carbonCopy.PartyWherePersonalEmailAddress))
                {
                    this.AddToParty(carbonCopy.PartyWherePersonalEmailAddress);
                }
            }

            foreach (EmailAddress blindCopy in this.BlindCopies)
            {
                if (blindCopy.ExistPartyWherePersonalEmailAddress && !ToParties.Contains(blindCopy.PartyWherePersonalEmailAddress))
                {
                    this.AddToParty(blindCopy.PartyWherePersonalEmailAddress);
                }
            }

            var party = this.GetRelationshipWithParty();
            if (party != null)
            {
                this.AddToParty(party);
            }

        }

        private void AppsDeriveInvolvedParties()
        {
            this.RemoveInvolvedParties();
            this.AddInvolvedParty(this.Owner);

            foreach (Party party in this.FromParties)
            {
                this.AddInvolvedParty(party);
            }

            foreach (Party party in this.ToParties)
            {
                this.AddInvolvedParty(party);
            }
        }

        private Party GetRelationshipWithParty()
        {
            var partyRelationship = this.PartyRelationshipWhereCommunicationEvent;
            if (partyRelationship != null)
            {
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ClientRelationshipClass.Instance.Name))
                {
                    var relationship = (ClientRelationship)partyRelationship;
                    return relationship.Client;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    CustomerRelationshipClass.Instance.Name))
                {
                    var relationship = (CustomerRelationship)partyRelationship;
                    return relationship.Customer;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    DistributionChannelRelationshipClass.Instance.Name))
                {
                    var relationship = (DistributionChannelRelationship)partyRelationship;
                    return relationship.Distributor;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name, EmploymentClass.Instance.Name))
                {
                    var relationship = (Employment)partyRelationship;
                    return relationship.Employee;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    OrganisationContactRelationshipClass.Instance.Name))
                {
                    var relationship = (OrganisationContactRelationship)partyRelationship;
                    return relationship.Contact;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name, PartnershipClass.Instance.Name))
                {
                    var relationship = (Partnership)partyRelationship;
                    return relationship.Partner;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ProfessionalServicesRelationshipClass.Instance.Name))
                {
                    var relationship = (ProfessionalServicesRelationship)partyRelationship;
                    return relationship.Professional;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ProspectRelationshipClass.Instance.Name))
                {
                    var relationship = (ProspectRelationship)partyRelationship;
                    return relationship.Prospect;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SalesRepCommissionClass.Instance.Name))
                {
                    var relationship = (SalesRepCommission)partyRelationship;
                    return relationship.SalesRep;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SubContractorRelationshipClass.Instance.Name))
                {
                    var relationship = (SubContractorRelationship)partyRelationship;
                    return relationship.SubContractor;
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SupplierRelationshipClass.Instance.Name))
                {
                    var relationship = (SupplierRelationship)partyRelationship;
                    return relationship.Supplier;
                }
            }

            return null;
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
