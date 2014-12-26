// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartyContactMechanism.cs" company="Allors bvba">
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

    public partial class PartyContactMechanism
    {
        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistUseAsDefault)
            {
                this.UseAsDefault = false;
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            if (this.ExistUseAsDefault && this.UseAsDefault)
            {
                derivation.Log.AssertExists(this, PartyContactMechanisms.Meta.ContactPurpose);
            }

            this.AppsDeriveDisplayName();

            if (this.UseAsDefault && this.ExistPartyWherePartyContactMechanism)
            {
                var partyContactMechanisms = this.PartyWherePartyContactMechanism.PartyContactMechanisms;
                partyContactMechanisms.Filter.AddEquals(PartyContactMechanisms.Meta.ContactPurpose, this.ContactPurpose);

                foreach (PartyContactMechanism partyContactMechanism in partyContactMechanisms)
                {
                    if (!partyContactMechanism.Equals(this))
                    {
                        partyContactMechanism.UseAsDefault = false;
                    }
                }   
            }
        }

        private void AppsDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            if (this.ExistContactPurpose)
            {
                uiText.Append(this.ContactPurpose.Name);
                uiText.Append(": ");
            }

            if (this.ExistContactMechanism)
            {
                uiText.Append(this.ContactMechanism.ComposeDisplayName());
            }

            this.DisplayName = uiText.ToString();
        }
    }
}