// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LetterCorrespondence.cs" company="Allors bvba">
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
    
    
    public partial class LetterCorrespondence
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

            derivation.Log.AssertExists(this, LetterCorrespondences.Meta.Description);

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
            
            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveInvolvedPersons();
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
            searchText.Append("Letter ");

            if (this.ExistOriginator)
            {
                searchText.Append("from: ");
                searchText.Append(this.Originator.DeriveDisplayName());
            }

            searchText.Append(" to:");
            foreach (Person person in this.Receivers)
            {
                searchText.Append(" ");
                searchText.Append(person.DeriveDisplayName());
            }

            return searchText.ToString();
        }

        protected override string AppsComposeSearchDataCharacterBoundaryText()
        {
            var text = string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistOriginator ? this.Originator.DeriveSearchDataCharacterBoundaryText() : null);

            foreach (Person person in this.Receivers)
            {
                text += " " + person.DeriveSearchDataCharacterBoundaryText();
            }

            return text;
        }

        protected override string AppsComposeSearchDataWordBoundaryText()
        {
            var text = this.ExistOriginator ? this.Originator.DeriveSearchDataWordBoundaryText() : null;

            foreach (Person person in this.Receivers)
            {
                text += " " + person.DeriveSearchDataWordBoundaryText();
            }

            return text;
        }

        private void AppsDeriveInvolvedPersons()
        {
            this.RemoveInvolvedParties();
            this.InvolvedParties = this.Receivers;
            this.AddInvolvedParty(this.Owner);
            this.AddInvolvedParty(this.Originator);
        }
    }
}
