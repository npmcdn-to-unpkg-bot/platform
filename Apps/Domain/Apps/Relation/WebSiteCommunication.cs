// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebSiteCommunication.cs" company="Allors bvba">
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

    using Allors.Domain;
    

    public partial class WebSiteCommunication
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
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, WebSiteCommunications.Meta.Description);

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveInvolvedPersons(derivation);
        }

        protected override void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        protected override void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        protected override void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        protected override string AppsComposeDisplayName()
        {
            var searchText = new StringBuilder();
            searchText.Append("Website communication ");

            if (this.ExistOriginator)
            {
                searchText.Append("from: ");
                searchText.Append(this.Originator.DeriveDisplayName());
            }

            if (this.ExistReceiver)
            {
                searchText.Append(" to: ");
                searchText.Append(this.Receiver.DeriveDisplayName());
            }

            return searchText.ToString();
        }

        protected override string AppsComposeSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0} {1} {2}",
                this.ExistDescription ? this.Description : null,
                this.ExistOriginator ? this.Originator.DeriveSearchDataCharacterBoundaryText() : null,
                this.ExistReceiver ? this.Receiver.DeriveSearchDataCharacterBoundaryText() : null);
        }

        protected override string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0} {1}",
                this.ExistOriginator ? this.Originator.DeriveSearchDataWordBoundaryText() : null,
                this.ExistReceiver ? this.Receiver.DeriveSearchDataWordBoundaryText() : null);
        }

        private void AppsDeriveInvolvedPersons(IDerivation derivation)
        {
            this.RemoveInvolvedParties();
            this.AddInvolvedParty(this.Owner);
            this.AddInvolvedParty(this.Originator);
            this.AddInvolvedParty(this.Receiver);
        }
    }
}
