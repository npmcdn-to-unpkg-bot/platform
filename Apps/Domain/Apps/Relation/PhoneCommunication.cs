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
            

            derivation.Log.AssertExists(this, PhoneCommunications.Meta.Description);

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
            
            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveInvolvedPersons(derivation);
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

            foreach (Person person in this.Receivers)
            {
                text += " " + person.DeriveSearchDataWordBoundaryText();
            }

            return text;
        }

        private void AppsDeriveInvolvedPersons(IDerivation derivation)
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
