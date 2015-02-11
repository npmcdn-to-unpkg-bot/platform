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

using Allors.Meta;

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

            var partyRelationship = this.PartyRelationshipWhereCommunicationEvent;
            if (partyRelationship != null)
            {
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ClientRelationshipClass.Instance.Name))
                {
                    var relationship = (ClientRelationship) partyRelationship;
                    this.AddToParty(relationship.Client);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    CustomerRelationshipClass.Instance.Name))
                {
                    var relationship = (CustomerRelationship) partyRelationship;
                    this.AddToParty(relationship.Customer);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    DistributionChannelRelationshipClass.Instance.Name))
                {
                    var relationship = (DistributionChannelRelationship) partyRelationship;
                    this.AddToParty(relationship.Distributor);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name, EmploymentClass.Instance.Name))
                {
                    var relationship = (Employment) partyRelationship;
                    this.AddToParty(relationship.Employee);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    OrganisationContactRelationshipClass.Instance.Name))
                {
                    var relationship = (OrganisationContactRelationship) partyRelationship;
                    this.AddToParty(relationship.Contact);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name, PartnershipClass.Instance.Name))
                {
                    var relationship = (Partnership) partyRelationship;
                    this.AddToParty(relationship.Partner);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ProfessionalServicesRelationshipClass.Instance.Name))
                {
                    var relationship = (ProfessionalServicesRelationship) partyRelationship;
                    this.AddToParty(relationship.Professional);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    ProspectRelationshipClass.Instance.Name))
                {
                    var relationship = (ProspectRelationship) partyRelationship;
                    this.AddToParty(relationship.Prospect);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SalesRepCommissionClass.Instance.Name))
                {
                    var relationship = (SalesRepCommission) partyRelationship;
                    this.AddToParty(relationship.SalesRep);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SubContractorRelationshipClass.Instance.Name))
                {
                    var relationship = (SubContractorRelationship) partyRelationship;
                    this.AddToParty(relationship.SubContractor);
                }
                if (Equals(this.PartyRelationshipWhereCommunicationEvent.GetType().Name,
                    SupplierRelationshipClass.Instance.Name))
                {
                    var relationship = (SupplierRelationship) partyRelationship;
                    this.AddToParty(relationship.Supplier);
                }
            }
        }

        private void AppsDeriveInvolvedParties(IDerivation derivation)
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
