// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaceToFaceCommunication.cs" company="Allors bvba">
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

    public partial class FaceToFaceCommunication
    {
        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new CommunicationEventObjectStates(this.DatabaseSession).Opened;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            this.AppsCommunicationEventDerive(derivation);

            derivation.Log.AssertExists(this, FaxCommunications.Meta.Description);

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();

            this.PreviousObjectState = this.CurrentObjectState;

            this.RemoveInvolvedParties();
            this.InvolvedParties = (Extent)this.Participants;
            this.AddInvolvedParty(Owner);
        }

        protected override void AppsApplySecurityOnDerive()
        {
            this.RemoveSecurityTokens();
            this.AddSecurityToken(Domain.Singleton.Instance(this.Session).AdministratorSecurityToken);

            if (this.ExistOwner)
            {
                this.AddSecurityToken(Owner.OwnerSecurityToken);
            }

            foreach (Person participant in this.Participants)
            {
                if (participant.ExistCurrentEmployment)
                {
                    this.AddSecurityToken(participant.CurrentEmployment.Employer.OwnerSecurityToken);
                }

                if (participant.ExistOrganisationContactRelationshipsWhereContact)
                {
                    foreach (OrganisationContactRelationship organisationContactRelationship in participant.OrganisationContactRelationshipsWhereContact)
                    {
                        if (organisationContactRelationship.ExistOrganisation)
                        {
                            foreach (CustomerRelationship customerRelationship in organisationContactRelationship.Organisation.CustomerRelationshipsWhereCustomer)
                            {
                                this.AddSecurityToken(customerRelationship.InternalOrganisation.OwnerSecurityToken);
                            }
                        }
                    }
                }
            }
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
            var text = new StringBuilder();
            text.Append("Face to Face meeting between");

            foreach (Person person in this.Participants)
            {
                text.Append(" ");
                text.Append(person.DeriveDisplayName());
            }

            return text.ToString();
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            var text = this.Description;

            foreach (Person person in this.Participants)
            {
                text += " " + person.DeriveSearchDataCharacterBoundaryText();
            }

            return text;
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            var text = string.Empty;

            foreach (Person person in this.Participants)
            {
                text += " " + person.DeriveSearchDataWordBoundaryText();
            }

            return text;
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
