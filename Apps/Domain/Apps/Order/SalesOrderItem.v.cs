// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesOrderItem.v.cs" company="Allors bvba">
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
    public partial class SalesOrderItem
    {
        public void CalculatePurchasePrice(IDerivation derivation)
        {
            this.AppsCalculatePurchasePrice(derivation);
        }

        public void CalculateUnitPrice(IDerivation derivation)
        {
            this.AppsCalculateUnitPrice(derivation);
        }

        public void DerivePrices(IDerivation derivation, decimal quantityOrdered = 0, decimal totalBasePrice = 0)
        {
            this.AppsDerivePrices(derivation, quantityOrdered, totalBasePrice);
        }

        public void DeriveShipTo(IDerivation derivation)
        {
            this.AppsDeriveShipTo(derivation);
        }

        public void DeriveDeliveryDate(IDerivation derivation)
        {
            this.AppsDeriveDeliveryDate(derivation);
        }

        public void DeriveReservedFromInventoryItem(IDerivation derivation)
        {
            this.AppsDeriveReservedFromInventoryItem(derivation);
        }

        public void DeriveQuantities(IDerivation derivation)
        {
            this.AppsDeriveQuantities(derivation);
        }

        public void DeriveCurrentObjectState(IDerivation derivation)
        {
            this.AppsDeriveCurrentObjectState(derivation);
        }

        public void DeriveCurrentOrderStatus(IDerivation derivation)
        {
            this.AppsDeriveCurrentOrderStatus(derivation);
        }

        public void DeriveCurrentShipmentStatus(IDerivation derivation)
        {
            this.AppsDeriveCurrentShipmentStatus(derivation);
        }

        public void DeriveMarkupAndProfitMargin(IDerivation derivation)
        {
            this.AppsDeriveMarkupAndProfitMargin(derivation);
        }

        public void DeriveOnShip(IDerivation derivation)
        {
            this.AppsDeriveOnShip(derivation);
        }

        public void DeriveOnShipped(IDerivation derivation, decimal quantity)
        {
            this.AppsDeriveOnShipped(derivation, quantity);
        }

        public void DeriveOnPicked(IDerivation derivation, decimal quantity)
        {
            this.AppsDeriveOnPicked(derivation, quantity);
        }

        public void DeriveAddToShipping(IDerivation derivation, decimal quantity)
        {
            this.AppsDeriveAddToShipping(derivation, quantity);
        }

        public void DeriveSubtractFromShipping(IDerivation derivation, decimal quantity)
        {
            this.AppsDeriveSubtractFromShipping(derivation, quantity);
        }

        public void DeriveSalesRep(IDerivation derivation)
        {
            this.AppsDeriveSalesRep(derivation);
        }

        public void ShipManually(decimal quantity)
        {
            this.AppsShipManually(quantity);
        }

        public void DeriveIsValidOrderItem(IDerivation derivation)
        {
            this.AppsDeriveIsValidOrderItem(derivation);
        }

        public void DeriveVatRate(IDerivation derivation)
        {
            this.AppsDeriveVatRate(derivation);
        }

        public void DeriveVatRegime(IDerivation derivation)
        {
            this.AppsDeriveVatRegime(derivation);
        }

        public void DeriveCurrentPaymentStatus(IDerivation derivation)
        {
            this.AppsDeriveCurrentPaymentStatus(derivation);
        }
    }
}