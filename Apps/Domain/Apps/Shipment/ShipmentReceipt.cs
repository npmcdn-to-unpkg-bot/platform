// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShipmentReceipt.cs" company="Allors bvba">
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

    public partial class ShipmentReceipt
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistReceivedDateTime)
            {
                this.ReceivedDateTime = DateTime.Now;
            }

            if (!this.ExistQuantityAccepted)
            {
                this.QuantityAccepted = 0;
            }

            if (!this.ExistQuantityRejected)
            {
                this.QuantityRejected = 0;
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            this.ReceivedDateTime = this.ReceivedDateTime.Date;

            if (this.ExistOrderItem && this.ExistShipmentItem)
            {
                var orderShipmentsWhereShipmentItem = this.ShipmentItem.OrderShipmentsWhereShipmentItem;
                orderShipmentsWhereShipmentItem.Filter.AddEquals(OrderShipments.Meta.SalesOrderItem, this.OrderItem);

                if (orderShipmentsWhereShipmentItem.First == null)
                {
                    new OrderShipmentBuilder(this.Strategy.Session)
                        .WithPurchaseOrderItem((Allors.Domain.PurchaseOrderItem)this.OrderItem)
                        .WithShipmentItem(this.ShipmentItem)
                        .WithQuantity(this.QuantityAccepted)
                        .Build();
                }
                else
                {
                    orderShipmentsWhereShipmentItem.First.Quantity += this.QuantityAccepted;
                }
            }

            this.AppsDeriveInventoryItem(derivation);

            var orderNumber = string.Empty;
            var salesOrderItem = this.ExistOrderItem ? this.OrderItem as Allors.Domain.SalesOrderItem : null;
            if (salesOrderItem != null)
            {
                orderNumber = salesOrderItem.SalesOrderWhereSalesOrderItem.OrderNumber;
            }

            var purchaseOrderItem = this.ExistOrderItem ? this.OrderItem as Allors.Domain.PurchaseOrderItem : null;
            if (purchaseOrderItem != null)
            {
                orderNumber = purchaseOrderItem.PurchaseOrderWherePurchaseOrderItem.OrderNumber;
            }

            this.DisplayName =
                string.Format(
                    "Received on {0}: from shipment {1} for order {2}, allocated to {3}, {4} items accepted, {5} items rejected",
                    this.ExistReceivedDateTime ? this.ReceivedDateTime : DateTime.MinValue,
                    this.ExistShipmentItem ? this.ShipmentItem.ShipmentWhereShipmentItem.ShipmentNumber : null,
                    orderNumber,
                    this.ExistInventoryItem ? this.InventoryItem.ComposeDisplayName() : null,
                    this.QuantityAccepted,
                    this.QuantityRejected);
        }

        private void AppsDeriveInventoryItem(IDerivation derivation)
        {
            if (this.ExistShipmentItem && this.ShipmentItem.ExistOrderShipmentsWhereShipmentItem)
            {
                var purchaseOrderItem = this.ShipmentItem.OrderShipmentsWhereShipmentItem[0].PurchaseOrderItem;
                var order = purchaseOrderItem.PurchaseOrderWherePurchaseOrderItem;

                if (purchaseOrderItem.ExistProduct)
                {
                    var good = purchaseOrderItem.Product as Allors.Domain.Good;
                    if (good != null)
                    {
                        if (good.ExistFinishedGood)
                        {
                            if (!this.ExistInventoryItem || !this.InventoryItem.Part.Equals(good.FinishedGood))
                            {
                                var inventoryItems = good.FinishedGood.InventoryItemsWherePart;
                                inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, order.ShipToBuyer.DefaultFacility);
                                this.InventoryItem = inventoryItems.First as Allors.Domain.NonSerializedInventoryItem;
                            }
                        }
                        else
                        {
                            if (!this.ExistInventoryItem || !this.InventoryItem.Good.Equals(good))
                            {
                                var inventoryItems = good.InventoryItemsWhereGood;
                                inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, order.ShipToBuyer.DefaultFacility);
                                this.InventoryItem = inventoryItems.First as Allors.Domain.NonSerializedInventoryItem ??
                                                     new NonSerializedInventoryItemBuilder(this.Strategy.Session).WithGood(good).Build();
                            }
                        }
                    }
                }

                if (purchaseOrderItem.ExistPart)
                {
                    if (!this.ExistInventoryItem || !this.InventoryItem.Part.Equals(purchaseOrderItem.Part))
                    {
                        var inventoryItems = purchaseOrderItem.Part.InventoryItemsWherePart;
                        inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, order.ShipToBuyer.DefaultFacility);
                        this.InventoryItem = inventoryItems.First as Allors.Domain.NonSerializedInventoryItem;
                    }
                }
            }
        }
    }
}