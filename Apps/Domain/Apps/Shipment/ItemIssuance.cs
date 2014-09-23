// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemIssuance.cs" company="Allors bvba">
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
    using Allors.Domain;

    public partial class ItemIssuance
    {

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, ItemIssuances.Meta.Quantity);
            derivation.Log.AssertExists(this, ItemIssuances.Meta.InventoryItem);
            derivation.Log.AssertExists(this, ItemIssuances.Meta.ShipmentItem);

            this.DisplayName = string.Format(
                "inventory item {0}, quantity {1}, shipment item {2} {3}{4}",
                this.ExistInventoryItem ? this.InventoryItem.ComposeDisplayName() : null,
                this.ExistQuantity ? this.Quantity : 0,
                this.ExistShipmentItem ? this.ShipmentItem.ExistQuantity ? this.ShipmentItem.Quantity : 0 : 0,
                this.ExistShipmentItem ? this.ShipmentItem.ExistGood ? this.ShipmentItem.Good.ComposeDisplayName() : null : null,
                this.ExistShipmentItem ? this.ShipmentItem.ExistPart ? this.ShipmentItem.Part.ComposeDisplayName() : null : null);
        }
    }
}