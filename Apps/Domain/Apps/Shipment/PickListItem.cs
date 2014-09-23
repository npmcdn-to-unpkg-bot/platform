// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PickListItem.cs" company="Allors bvba">
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
    using System;

    using Allors.Domain;

    using Resources;

    public partial class PickListItem
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, PickListItems.Meta.InventoryItem);
            derivation.Log.AssertExists(this, PickListItems.Meta.RequestedQuantity);

            if (this.ExistActualQuantity && this.ActualQuantity > this.RequestedQuantity)
            {
                derivation.Log.AddError(this, PickListItems.Meta.ActualQuantity, ErrorMessages.PickListItemQuantityMoreThanAllowed);
            }

            this.DisplayName = string.Format(
                "{0} - {1}",
                this.ExistRequestedQuantity ? this.RequestedQuantity : 0,
                this.ExistInventoryItem ? this.InventoryItem.DisplayName : null);

            this.AppsDeriveOrderItemAdjustment(derivation);
        }

        private void AppsDeriveOrderItemAdjustment(IDerivation derivation)
        {
            if (this.ExistPickListWherePickListItem && this.PickListWherePickListItem.CurrentObjectState.UniqueId.Equals(PickListObjectStates.PickedId))
            {
                var diff = this.RequestedQuantity - this.ActualQuantity;

                foreach (ItemIssuance itemIssuance in this.ItemIssuancesWherePickListItem)
                {
                    itemIssuance.IssuanceDateTime = DateTime.Now;
                    foreach (OrderShipment orderShipment in itemIssuance.ShipmentItem.OrderShipmentsWhereShipmentItem)
                    {
                        if (!orderShipment.Picked)
                        {
                            if (diff > 0)
                            {
                                if (orderShipment.Quantity >= diff)
                                {
                                    orderShipment.Quantity -= diff;
                                    orderShipment.ShipmentItem.Quantity -= diff;
                                    itemIssuance.Quantity -= diff;
                                    diff = 0;
                                }
                                else
                                {
                                    orderShipment.ShipmentItem.Quantity -= orderShipment.Quantity;
                                    itemIssuance.Quantity -= orderShipment.Quantity;
                                    diff -= orderShipment.Quantity;
                                    orderShipment.Quantity = 0;
                                }
                            }

                            orderShipment.SalesOrderItem.DeriveOnPicked(derivation, orderShipment.Quantity);
                            orderShipment.Picked = true;
                        }
                    }
                }
            }
        }
    }
}