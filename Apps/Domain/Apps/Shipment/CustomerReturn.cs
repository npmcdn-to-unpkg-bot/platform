// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerReturn.cs" company="Allors bvba">
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
    public partial class CustomerReturn
    {
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

        public void DeriveTemplate(IDerivation derivation)
        {
            this.PrintContent = "not implemented";
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new CustomerReturnObjectStates(this.DatabaseSession).Received;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }

            this.PreviousObjectState = this.CurrentObjectState;
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            if (!this.ExistShipToAddress && this.ExistShipToParty)
            {
                this.ShipToAddress = this.ShipToParty.ShippingAddress;
            }


            if (!this.ExistShipFromAddress && this.ExistShipFromParty)
            {
                this.ShipFromAddress = this.ShipFromParty.ShippingAddress;
            }

            this.DisplayName = string.Format(
                "{0} to {1}",
                this.ExistShipmentNumber ? this.ShipmentNumber : null,
                this.ExistShipToParty ? this.ShipToParty.DeriveDisplayName() : null);

            var characterBoundaryText = this.ExistShipToParty ? this.ShipToParty.DeriveSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistShipmentNumber ? this.ShipmentNumber : null,
                this.ExistShipToParty ? this.ShipToParty.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
            

            this.DeriveCurrentObjectState(derivation);
            this.PreviousObjectState = this.CurrentObjectState;

            this.DeriveTemplate(derivation);
        }

        private void AppsDeriveCurrentObjectState(IDerivation derivation)
        {
            

            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new CustomerReturnStatusBuilder(this.Session).WithCustomerReturnObjectState(this.CurrentObjectState).Build();
                this.AddShipmentStatus(currentStatus);
                this.CurrentShipmentStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }
        }
    }
}
