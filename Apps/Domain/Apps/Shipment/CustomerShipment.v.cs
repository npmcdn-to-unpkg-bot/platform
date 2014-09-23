// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerShipment.v.cs" company="Allors bvba">
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
    public partial class CustomerShipment
    {
        public void Cancel()
        {
            this.AppsCancel();
        }

        public void Hold()
        {
            this.AppsHold();
        }

        public void Continue()
        {
            this.AppsContinue();
        }

        public void Ship()
        {
            this.AppsShip();
        }

        public void SetPicked()
        {
            this.AppsSetPicked();
        }

        public void SetPacked()
        {
            this.AppsSetPacked();
        }

        public void DeriveInvoices(IDerivation derivation)
        {
            this.AppsDeriveInvoices(derivation);
        }

        public void DeriveQuantityDecreased(IDerivation derivation, ShipmentItem shipmentItem, SalesOrderItem orderItem, decimal quantity)
        {
            this.AppsDeriveQuantityDecreased(derivation, shipmentItem, orderItem, quantity);
        }

        public decimal DeriveShippingAndHandlingCharges(IDerivation derivation)
        {
            return this.AppsDeriveShippingAndHandlingCharges(derivation);
        }

        protected void PutOnHold()
        {
            this.AppsPutOnHold();
        }

        protected void ProcessOnContinue()
        {
            this.AppsProcessOnContinue();
        }

        private void DeriveCurrentObjectState(IDerivation derivation)
        {
            this.AppsDeriveCurrentObjectState(derivation);
        }
    }
}