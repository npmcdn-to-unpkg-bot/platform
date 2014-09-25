// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesOrderItem.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    
    using Allors.Domain;

    using Resources;

    public partial class SalesOrderItem
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

        public decimal PriceAdjustment
        {
            get
            {
                return this.TotalSurcharge - this.TotalDiscount;
            }
        }

        public decimal PriceAdjustmentAsPercentage
        {
            get
            {
                return decimal.Round(((this.TotalSurcharge - this.TotalDiscount) / this.TotalBasePrice) * 100, 2);
            }
        }

        public string GetActualUnitBasePriceAsCurrencyString
        {
            get
            {
                return this.ExistActualUnitPrice ? DecimalExtensions.AsCurrencyString(this.ActualUnitPrice, this.SalesOrderWhereSalesOrderItem.CurrencyFormat) : string.Empty;
            }
        }

        public string GetExtraDiscountAmountAsCurrencyString
        {
            get
            {
                if (this.ExistDiscountAdjustment)
                {
                    if (this.DiscountAdjustment.ExistAmount)
                    {
                        return DecimalExtensions.AsCurrencyString(this.DiscountAdjustment.Amount, this.SalesOrderWhereSalesOrderItem.CurrencyFormat);
                    }
                }

                return string.Empty;
            }
        }

        public string GetExtraDiscountPercentage
        {
            get
            {
                if (this.ExistDiscountAdjustment)
                {
                    if (this.DiscountAdjustment.ExistPercentage)
                    {
                        return this.DiscountAdjustment.Percentage.ToString(new CultureInfo(this.SalesOrderWhereSalesOrderItem.Locale.Name));
                    }
                }

                return string.Empty;
            }
        }

        public string GetDiscountAsPercentageString
        {
            get
            {
                if (this.ExistTotalDiscountAsPercentage)
                {
                    return this.TotalDiscountAsPercentage.ToString(new CultureInfo(this.SalesOrderWhereSalesOrderItem.Locale.Name));
                }

                return string.Empty;
            }
        }

        public string GetSurchargeAsPercentageString
        {
            get
            {
                if (this.ExistTotalSurchargeAsPercentage)
                {
                    return this.TotalSurchargeAsPercentage.ToString(new CultureInfo(this.SalesOrderWhereSalesOrderItem.Locale.Name));
                }

                return string.Empty;
            }
        }

        public string GetUnitBasePriceAsCurrencyString
        {
            get
            {
                return DecimalExtensions.AsCurrencyString(this.UnitBasePrice, this.SalesOrderWhereSalesOrderItem.CurrencyFormat);
            }
        }

        public string GetPriceAdjustmentAsCurrencyString
        {
            get
            {
                return this.PriceAdjustment.AsCurrencyString(this.SalesOrderWhereSalesOrderItem.CurrencyFormat);
            }
        }

        public string GetPriceAdjustmentAsPercentageString
        {
            get
            {
                return this.PriceAdjustmentAsPercentage.ToString("##.##");
            }
        }

        public string GetNothingAsCurrencyString
        {
            get
            {
                const decimal Nothing = 0;
                return Nothing.AsCurrencyString(this.SalesOrderWhereSalesOrderItem.CurrencyFormat);
            }
        }

        public string GetTotalExVatAsCurrencyString
        {
            get
            {
                return DecimalExtensions.AsCurrencyString(this.TotalExVat, this.SalesOrderWhereSalesOrderItem.CurrencyFormat);
            }
        }

        public Party ItemDifferentShippingParty
        {
            get
            {
                if (this.ExistAssignedShipToParty && !this.SalesOrderWhereSalesOrderItem.ShipToCustomer.Equals(this.AssignedShipToParty))
                {
                    return this.AssignedShipToParty;
                }

                return null;
            }
        }

        public PostalAddress ItemDifferentShippingAddress
        {
            get
            {
                if (this.ExistAssignedShipToAddress && !this.SalesOrderWhereSalesOrderItem.ShipToAddress.Equals(this.AssignedShipToAddress))
                {
                    return this.AssignedShipToAddress;
                }

                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Cancelled) ||
                    this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Rejected) ||
                    this.QuantityOrdered == 0 ||
                    (this.ExistCalculatedUnitPrice && this.CalculatedUnitPrice == 0))
                {
                    return false;
                }

                return true;
            }
        }

        public void SetActualDiscountAmount(decimal amount)
        {
            if (!this.ExistDiscountAdjustment)
            {
                this.DiscountAdjustment = new DiscountAdjustmentBuilder(this.Session).Build();
            }

            this.DiscountAdjustment.Amount = amount;
            this.DiscountAdjustment.RemovePercentage();
        }

        public void SetActualDiscountPercentage(decimal percentage)
        {
            if (!this.ExistDiscountAdjustment)
            {
                this.DiscountAdjustment = new DiscountAdjustmentBuilder(this.Session).Build();
            }

            this.DiscountAdjustment.Percentage = percentage;
            this.DiscountAdjustment.RemoveAmount();
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new SalesOrderItemObjectStates(this.Session).Created;
            }

            if (!this.ExistQuantityOrdered)
            {
                this.QuantityOrdered = 0;
            }

            if (!this.ExistQuantityPicked)
            {
                this.QuantityPicked = 0;
            }

            if (!this.ExistQuantityRequestsShipping)
            {
                this.QuantityRequestsShipping = 0;
            }

            if (!this.ExistQuantityReserved)
            {
                this.QuantityReserved = 0;
            }

            if (!this.ExistQuantityReturned)
            {
                this.QuantityReturned = 0;
            }

            if (!this.ExistQuantityShipped)
            {
                this.QuantityShipped = 0;
            }

            if (!this.ExistQuantityPendingShipment)
            {
                this.QuantityPendingShipment = 0;
            }

            if (!this.ExistQuantityShortFalled)
            {
                this.QuantityShortFalled = 0;
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            if (this.ExistSalesOrderWhereSalesOrderItem)
            {
                derivation.AddDependency(this.SalesOrderWhereSalesOrderItem, this);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            foreach (SalesOrderItem featureItem in this.OrderedWithFeatures)
            {
                featureItem.Derive(derivation);
            }

            if (this.ExistPreviousProduct && !this.PreviousProduct.Equals(this.Product))
            {
                derivation.Log.AddError(this, SalesOrderItems.Meta.Product, ErrorMessages.SalesOrderItemProductChangeNotAllowed);
            }
            else
            {
                this.PreviousProduct = this.Product;
            }

            if (this.ExistSalesOrderItemWhereOrderedWithFeature && !this.ExistProductFeature)
            {
                derivation.Log.AssertExists(this, SalesOrderItems.Meta.ProductFeature);
            }

            if (this.ExistProduct && this.ExistQuantityOrdered && this.QuantityOrdered < this.QuantityShipped)
            {
                derivation.Log.AddError(this, SalesOrderItems.Meta.QuantityOrdered, ErrorMessages.SalesOrderItemLessThanAlreadeyShipped);
            }

            if (!this.ExistAssignedShipToAddress && this.ExistAssignedShipToParty)
            {
                this.AssignedShipToAddress = this.AssignedShipToParty.ShippingAddress;
            }

            derivation.Log.AssertExists(this, SalesOrderItems.Meta.QuantityOrdered);
            derivation.Log.AssertAtLeastOne(this, SalesOrderItems.Meta.Product, SalesOrderItems.Meta.ProductFeature);
            derivation.Log.AssertExistsAtMostOne(this, SalesOrderItems.Meta.Product, SalesOrderItems.Meta.ProductFeature);
            derivation.Log.AssertExistsAtMostOne(this, SalesOrderItems.Meta.ActualUnitPrice, SalesOrderItems.Meta.DiscountAdjustment, SalesOrderItems.Meta.SurchargeAdjustment);
            derivation.Log.AssertExistsAtMostOne(this, SalesOrderItems.Meta.RequiredMarkupPercentage, SalesOrderItems.Meta.RequiredProfitMargin, SalesOrderItems.Meta.DiscountAdjustment, SalesOrderItems.Meta.SurchargeAdjustment);

            this.AppsDeriveIsValidOrderItem(derivation);

            this.DeriveCurrentObjectState(derivation);

            this.DeriveDisplayName();
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private string AppsComposeDisplayName()
        {
            var uiText = new StringBuilder();

            if (this.ExistQuantityOrdered)
            {
                uiText.Append(this.QuantityOrdered);
                uiText.Append(" ");
            }

            if (this.ExistProduct)
            {
                uiText.Append(this.Product.ComposeDisplayName());
            }

            if (this.ExistProductFeature)
            {
                uiText.Append(this.ProductFeature.ComposeDisplayName());
            }

            if (!this.ExistProduct && !this.ExistProductFeature)
            {
                uiText.Append(this.Description);
            }

            if (this.ExistTotalExVat)
            {
                uiText.Append(", Total: ");
                uiText.Append(string.Format("{0:N2}", this.TotalExVat));
            }

            return uiText.ToString();
        }

        private void AppsDeriveIsValidOrderItem(IDerivation derivation)
        {
            if (this.ExistSalesOrderWhereSalesOrderItem)
            {
                this.SalesOrderWhereSalesOrderItem.RemoveValidOrderItem(this);

                if (this.IsValid)
                {
                    this.SalesOrderWhereSalesOrderItem.AddValidOrderItem(this);
                }
            }
        }

        private void AppsDeriveCurrentObjectState(IDerivation derivation)
        {
            if (this.ExistOrderWhereValidOrderItem)
            {
                var order = this.SalesOrderWhereSalesOrderItem;

                if (order.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).InProcess))
                {
                    if (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(Session).Created))
                    {
                        this.Confirm();
                    }
                }

                if (order.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).Finished))
                {
                    this.Finish();
                }

                if (order.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).Cancelled))
                {
                    this.Cancel();
                }

                if (order.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).Rejected))
                {
                    this.Reject();
                }
            }

            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new SalesOrderItemStatusBuilder(this.Session).WithSalesOrderItemObjectState(this.CurrentObjectState).Build();
                this.AddOrderItemStatus(currentStatus);
                this.CurrentOrderItemStatus = currentStatus;
                this.PreviousObjectState = this.CurrentObjectState;
            }

            if (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).InProcess))
            {
                this.DeriveReservedFromInventoryItem(derivation);
                this.DeriveQuantities(derivation);

                this.PreviousQuantity = this.QuantityOrdered;
                this.PreviousReservedFromInventoryItem = this.ReservedFromInventoryItem;
                this.PreviousProduct = this.Product;
            }

            if (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Cancelled) ||
                this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Rejected))
            {
                this.DeriveQuantities(derivation);
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }
        }

        private void AppsCancel()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).Cancelled;
        }

        private void AppsConfirm()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).InProcess;
        }

        private void AppsReject()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).Rejected;
        }

        private void AppsApprove()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).InProcess;
        }

        private void AppsContinue()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).InProcess;
        }

        private void AppsFinish()
        {
            this.CurrentObjectState = new SalesOrderItemObjectStates(Session).Finished;
        }

        private void AppsDeriveDeliveryDate(IDerivation derivation)
        {
            if (this.ExistAssignedDeliveryDate)
            {
                this.DeliveryDate = this.AssignedDeliveryDate;
            }
            else if (this.ExistSalesOrderWhereSalesOrderItem && this.SalesOrderWhereSalesOrderItem.ExistDeliveryDate)
            {
                this.DeliveryDate = this.SalesOrderWhereSalesOrderItem.DeliveryDate;
            }
        }
        
        private void AppsDeriveShipTo(IDerivation derivation)
        {
            if (this.ExistSalesOrderWhereSalesOrderItem)
            {
                this.ShipToAddress = this.ExistAssignedShipToAddress ? this.AssignedShipToAddress : this.SalesOrderWhereSalesOrderItem.ShipToAddress;
                this.ShipToParty = this.ExistAssignedShipToParty ? this.AssignedShipToParty : this.SalesOrderWhereSalesOrderItem.ShipToCustomer;
            }
        }

        private void AppsDeriveReservedFromInventoryItem(IDerivation derivation)
        {
            if (this.ExistProduct)
            {
                var good = this.Product as Good;
                if (good != null)
                {
                    if (good.ExistFinishedGood)
                    {
                        if (!this.ExistReservedFromInventoryItem || !this.ReservedFromInventoryItem.Part.Equals(good.FinishedGood))
                        {
                            var inventoryItems = good.FinishedGood.InventoryItemsWherePart;
                            inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, SalesOrderWhereSalesOrderItem.TakenByInternalOrganisation.DefaultFacility);
                            this.ReservedFromInventoryItem = inventoryItems.First as NonSerializedInventoryItem;
                        }
                    }
                    else
                    {
                        if (!this.ExistReservedFromInventoryItem || !this.ReservedFromInventoryItem.Good.Equals(good))
                        {
                            var inventoryItems = good.InventoryItemsWhereGood;
                            inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, SalesOrderWhereSalesOrderItem.TakenByInternalOrganisation.DefaultFacility);
                            this.ReservedFromInventoryItem = inventoryItems.First as NonSerializedInventoryItem;
                        }
                    }
                }
            }
        }

        private void AppsDeriveQuantities(IDerivation derivation)
        {
            if (this.SalesOrderWhereSalesOrderItem.ScheduledManually)
            {
                this.AppsDeriveQuantitiesmanualShipment(derivation);
            }
            else
            {
                this.AppsDeriveQuantitiesAutomaticShipment(derivation);
            }
        }

        private void AppsDeriveQuantitiesmanualShipment(IDerivation derivation)
        {
            foreach (SalesOrderItem item in this.OrderedWithFeatures)
            {
                this.QuantityOrdered = item.QuantityOrdered;
            }

            if (this.ExistReservedFromInventoryItem &&
                this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).InProcess) &&
                (this.QuantityReserved > 0 || this.QuantityPendingShipment > 0))
            {
                if (this.ExistPreviousQuantity && !this.QuantityOrdered.Equals(this.PreviousQuantity))
                {
                    var diff = this.QuantityOrdered - this.PreviousQuantity;

                    if (diff < 0)
                    {
                        var leftToShip = this.PreviousQuantity - this.QuantityShipped - this.QuantityPendingShipment;
                        var shipmentCorrection = leftToShip + diff;

                        this.QuantityReserved += shipmentCorrection;

                        if (this.QuantityShortFalled > 0)
                        {
                            this.QuantityShortFalled += diff;
                            if (this.QuantityShortFalled < 0)
                            {
                                this.QuantityShortFalled = 0;
                            }
                        }

                        if (this.QuantityRequestsShipping > this.QuantityReserved)
                        {
                            this.QuantityRequestsShipping = this.QuantityReserved;
                        }

                        if (this.ExistQuantityPendingShipment && shipmentCorrection < 0)
                        {
                            this.DecreasePendingShipmentQuantity(derivation, 0 - shipmentCorrection);
                        }

                        this.ReservedFromInventoryItem.Derive(derivation);
                    }
                }

                this.PreviousQuantity = this.QuantityOrdered;
            }

            if (this.ExistReservedFromInventoryItem &&
                (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Cancelled) ||
                this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Rejected)))
            {
                if (this.ExistQuantityPendingShipment)
                {
                    this.DecreasePendingShipmentQuantity(derivation, this.QuantityPendingShipment);
                }

                this.ReservedFromInventoryItem.Derive(derivation);
            }
        }

        private void AppsDeriveQuantitiesAutomaticShipment(IDerivation derivation)
        {
            foreach (SalesOrderItem item in this.OrderedWithFeatures)
            {
                this.QuantityOrdered = item.QuantityOrdered;
            }

            if (this.ExistReservedFromInventoryItem && this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).InProcess))
            {
                if (this.ExistPreviousReservedFromInventoryItem && !this.ReservedFromInventoryItem.Equals(this.PreviousReservedFromInventoryItem))
                {
                    this.PreviousReservedFromInventoryItem.Derive(derivation);

                    this.SetQuantitiesWithInventoryFirstTime(derivation);
                }
                else
                {
                    if (this.ExistPreviousQuantity && !this.QuantityOrdered.Equals(this.PreviousQuantity))
                    {
                        var diff = this.QuantityOrdered - this.PreviousQuantity;

                        if (diff > 0)
                        {
                            this.QuantityReserved += diff;

                            if (diff > this.ReservedFromInventoryItem.AvailableToPromise)
                            {
                                this.QuantityRequestsShipping += this.ReservedFromInventoryItem.AvailableToPromise;
                                this.QuantityShortFalled += diff - this.ReservedFromInventoryItem.AvailableToPromise;
                            }
                            else
                            {
                                this.QuantityRequestsShipping += diff;
                            }
                        }
                        else
                        {
                            var leftToShip = this.PreviousQuantity - this.QuantityShipped - this.QuantityPendingShipment;

                            this.QuantityReserved += diff;

                            if (this.QuantityShortFalled > 0)
                            {
                                this.QuantityShortFalled += diff;
                                if (this.QuantityShortFalled < 0)
                                {
                                    this.QuantityShortFalled = 0;
                                }
                            }

                            if (this.QuantityRequestsShipping > this.QuantityReserved)
                            {
                                this.QuantityRequestsShipping = this.QuantityReserved;
                            }

                            var shipmentCorrection = leftToShip + diff;
                            if (this.ExistQuantityPendingShipment && shipmentCorrection < 0)
                            {
                                this.DecreasePendingShipmentQuantity(derivation, 0 - shipmentCorrection);
                            }
                        }

                        this.ReservedFromInventoryItem.Derive(derivation);
                    }

                    //// When first Confirmed.
                    if (!this.ExistPreviousQuantity)
                    {
                        this.SetQuantitiesWithInventoryFirstTime(derivation);
                    }
                }

                this.PreviousQuantity = this.QuantityOrdered;
            }

            if (this.ExistReservedFromInventoryItem &&
                (this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Cancelled) ||
                this.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Rejected)))
            {
                if (this.ExistQuantityPendingShipment)
                {
                    this.DecreasePendingShipmentQuantity(derivation, this.QuantityPendingShipment);
                }

                this.ReservedFromInventoryItem.Derive(derivation);
            }
        }

        private void SetQuantitiesWithInventoryFirstTime(IDerivation derivation)
        {
            this.QuantityReserved = 0;
            this.QuantityRequestsShipping = 0;
            this.QuantityShortFalled = 0;

            this.QuantityRequestsShipping = this.QuantityOrdered > this.ReservedFromInventoryItem.AvailableToPromise ?
                this.ReservedFromInventoryItem.AvailableToPromise : this.QuantityOrdered;

            if (this.QuantityRequestsShipping < 0)
            {
                this.QuantityRequestsShipping = 0;
            }

            this.QuantityReserved = this.QuantityOrdered;
            this.QuantityShortFalled = this.QuantityOrdered - this.QuantityRequestsShipping;

            this.ReservedFromInventoryItem.Derive(derivation);
        }

        private void DecreasePendingShipmentQuantity(IDerivation derivation, decimal diff)
        {
            var pendingShipment = this.ShipToParty.GetPendingCustomerShipmentForStore(this.ShipToAddress, this.SalesOrderWhereSalesOrderItem.Store, this.SalesOrderWhereSalesOrderItem.ShipmentMethod);

            if (pendingShipment != null)
            {
                foreach (ShipmentItem shipmentItem in pendingShipment.ShipmentItems)
                {
                    foreach (OrderShipment orderShipment in shipmentItem.OrderShipmentsWhereShipmentItem)
                    {
                        if (orderShipment.SalesOrderItem.Equals(this))
                        {
                            this.QuantityPendingShipment -= diff;
                            pendingShipment.DeriveQuantityDecreased(derivation, shipmentItem, this, diff);
                            break;
                        }
                    }
                }
            }
        }

        private void AppsDeriveCurrentOrderStatus(IDerivation derivation)
        {
            if (this.ExistCurrentShipmentStatus && this.CurrentShipmentStatus.SalesOrderItemObjectState.Equals(new SalesOrderItemObjectStates(this.Session).PartiallyShipped))
            {
                this.CurrentObjectState = new SalesOrderItemObjectStates(Session).PartiallyShipped;
                this.DeriveCurrentObjectState(derivation);
            }

            if (this.ExistCurrentShipmentStatus && this.CurrentShipmentStatus.SalesOrderItemObjectState.Equals(new SalesOrderItemObjectStates(this.Session).Shipped))
            {
                this.CurrentObjectState = new SalesOrderItemObjectStates(Session).Completed;
                this.DeriveCurrentObjectState(derivation);
            }
        }

        private void AppsDeriveCurrentShipmentStatus(IDerivation derivation)
        {
            if (this.QuantityShipped > 0)
            {
                if (this.QuantityShipped < this.QuantityOrdered)
                {
                    if (!this.ExistCurrentShipmentStatus || !this.CurrentShipmentStatus.SalesOrderItemObjectState.Equals(new SalesOrderItemObjectStates(Session).PartiallyShipped))
                    {
                        this.CurrentShipmentStatus = new SalesOrderItemStatusBuilder(this.Session).WithSalesOrderItemObjectState(new SalesOrderItemObjectStates(Session).PartiallyShipped).Build();
                    }
                }
                else
                {
                    if (!this.ExistCurrentShipmentStatus || !this.CurrentShipmentStatus.SalesOrderItemObjectState.Equals(new SalesOrderItemObjectStates(Session).Shipped))
                    {
                        this.CurrentShipmentStatus = new SalesOrderItemStatusBuilder(this.Session).WithSalesOrderItemObjectState(new SalesOrderItemObjectStates(Session).Shipped).Build();
                    }
                }

                this.AddShipmentStatus(CurrentShipmentStatus);
            }

            this.DeriveCurrentOrderStatus(derivation);
        }
        
        private void AppsCalculatePurchasePrice(IDerivation derivation)
        {
            this.UnitPurchasePrice = 0;

            if (this.ExistProduct &&
                this.Product.ExistSupplierOfferingsWhereProduct &&
                this.Product.SupplierOfferingsWhereProduct.Count == 1 &&
                this.Product.SupplierOfferingsWhereProduct.First.ExistProductPurchasePrices)
            {
                ProductPurchasePrice productPurchasePrice = null;

                var prices = this.Product.SupplierOfferingsWhereProduct.First.ProductPurchasePrices;
                foreach (ProductPurchasePrice purchasePrice in prices)
                {
                    if (purchasePrice.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                        (!purchasePrice.ExistThroughDate || purchasePrice.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                    {
                        productPurchasePrice = purchasePrice;
                    }
                }

                if (productPurchasePrice == null)
                {
                    var index = this.Product.SupplierOfferingsWhereProduct.First.ProductPurchasePrices.Count;
                    var lastKownPrice = this.Product.SupplierOfferingsWhereProduct.First.ProductPurchasePrices[index - 1];
                    productPurchasePrice = lastKownPrice;
                }

                if (productPurchasePrice != null)
                {
                    this.UnitPurchasePrice = productPurchasePrice.Price;
                    if (!productPurchasePrice.UnitOfMeasure.Equals(this.Product.UnitOfMeasure))
                    {
                        foreach (UnitOfMeasureConversion unitOfMeasureConversion in productPurchasePrice.UnitOfMeasure.UnitOfMeasureConversions)
                        {
                            if (unitOfMeasureConversion.ToUnitOfMeasure.Equals(this.Product.UnitOfMeasure))
                            {
                                this.UnitPurchasePrice = decimal.Round(this.UnitPurchasePrice * (1 / unitOfMeasureConversion.ConversionFactor), 2);
                            }
                        }
                    }
                }
            }
        }

        private void AppsCalculateUnitPrice(IDerivation derivation)
        {
            if (this.ExistRequiredMarkupPercentage && this.UnitPurchasePrice > 0)
            {
                this.ActualUnitPrice = decimal.Round((1 + (this.RequiredMarkupPercentage / 100)) * this.UnitPurchasePrice, 2);
            }

            if (this.ExistRequiredProfitMargin && this.UnitPurchasePrice > 0)
            {
                this.ActualUnitPrice = decimal.Round(this.UnitPurchasePrice / (1 - (this.RequiredProfitMargin / 100)), 2);
            }
        }

        private void AppsDerivePrices(IDerivation derivation, decimal quantityOrdered, decimal totalBasePrice)
        {
            this.RemoveCurrentPriceComponents();

            this.UnitBasePrice = 0;
            this.UnitDiscount = 0;
            this.UnitSurcharge = 0;
            this.CalculatedUnitPrice = 0;
            decimal discountAdjustmentAmount = 0;
            decimal surchargeAdjustmentAmount = 0;

            var internalOrganisation = this.SalesOrderWhereSalesOrderItem.TakenByInternalOrganisation;
            var customer = this.SalesOrderWhereSalesOrderItem.BillToCustomer;
            var salesOrder = this.SalesOrderWhereSalesOrderItem;

            var baseprices = new PriceComponent[0];
            if (this.ExistProduct && this.Product.ExistBasePrices)
            {
                baseprices = Product.BasePrices;
            }

            if (this.ExistProductFeature && this.ProductFeature.ExistBasePrices)
            {
                baseprices = ProductFeature.BasePrices;
            }

            foreach (BasePrice priceComponent in baseprices)
            {
                if (priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                    (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                {
                    if (PriceComponents.IsEligible(new PriceComponents.IsEligibleParams
                    {
                        PriceComponent = priceComponent,
                        Customer = customer,
                        Product = this.Product,
                        SalesOrder = salesOrder,
                        QuantityOrdered = quantityOrdered,
                        ValueOrdered = totalBasePrice
                    }))
                    {
                        if (priceComponent.ExistPrice)
                        {
                            if (this.UnitBasePrice == 0 || priceComponent.Price < this.UnitBasePrice)
                            {
                                this.UnitBasePrice = priceComponent.Price;

                                this.RemoveCurrentPriceComponents();
                                this.AddCurrentPriceComponent(priceComponent);
                            }
                        }
                    }
                }
            }

            ////SafeGuard
            if (this.ExistProduct && !this.ExistActualUnitPrice)
            {
                var invalid = true;
                foreach (BasePrice basePrice in this.CurrentPriceComponents)
                {
                    if (basePrice.Price > 0)
                    {
                        invalid = false;
                    }
                }

                if (invalid)
                {
                    this.QuantityOrdered = 0;
                }
            }

            if (!this.ExistActualUnitPrice)
            {
                PartyRevenueHistory partyRevenueHistory = null;
                Dictionary<ProductCategory, PartyProductCategoryRevenueHistory> partyProductCategoryRevenueHistoryByProductCategory = null;
                Extent<PartyPackageRevenueHistory> partyPackageRevenuesHistories = null;

                if (customer != null)
                {
                    var partyRevenueHistories = customer.PartyRevenueHistoriesWhereParty;
                    partyRevenueHistories.Filter.AddEquals(PartyRevenueHistories.Meta.InternalOrganisation, internalOrganisation);
                    partyRevenueHistory = partyRevenueHistories.First;

                    partyProductCategoryRevenueHistoryByProductCategory = PartyProductCategoryRevenueHistories.PartyProductCategoryRevenueHistoryByProductCategory(internalOrganisation, customer);

                    partyPackageRevenuesHistories = customer.PartyPackageRevenueHistoriesWhereParty;
                    partyPackageRevenuesHistories.Filter.AddEquals(PartyPackageRevenueHistories.Meta.InternalOrganisation, internalOrganisation);
                }

                var priceComponents = this.GetPriceComponents(internalOrganisation);

                var revenueBreakDiscount = 0M;
                var revenueBreakSurcharge = 0M;

                foreach (var priceComponent in priceComponents)
                {
                    if (priceComponent.Strategy.ObjectType.Equals(DiscountComponents.Meta.ObjectType) || priceComponent.Strategy.ObjectType.Equals(SurchargeComponents.Meta.ObjectType))
                    {
                        if (PriceComponents.IsEligible(new PriceComponents.IsEligibleParams
                        {
                            PriceComponent = priceComponent,
                            Customer = customer,
                            Product = this.Product,
                            SalesOrder = salesOrder,
                            QuantityOrdered = quantityOrdered,
                            ValueOrdered = totalBasePrice,
                            PartyPackageRevenueHistoryList = partyPackageRevenuesHistories,
                            PartyProductCategoryRevenueHistoryByProductCategory = partyProductCategoryRevenueHistoryByProductCategory,
                            PartyRevenueHistory = partyRevenueHistory
                        }))
                        {
                            this.AddCurrentPriceComponent(priceComponent);

                            if (priceComponent.Strategy.ObjectType.Equals(DiscountComponents.Meta.ObjectType))
                            {
                                var discountComponent = (DiscountComponent)priceComponent;
                                decimal discount;

                                if (discountComponent.ExistPrice)
                                {
                                    discount = discountComponent.Price;
                                    this.UnitDiscount += discount;
                                }
                                else
                                {
                                    discount = decimal.Round((this.UnitBasePrice * discountComponent.Percentage) / 100, 2);
                                    this.UnitDiscount += discount;
                                }

                                ////Revenuebreaks on quantity and value are mutually exclusive.
                                if (priceComponent.ExistRevenueQuantityBreak || priceComponent.ExistRevenueValueBreak)
                                {
                                    if (revenueBreakDiscount == 0)
                                    {
                                        revenueBreakDiscount = discount;
                                    }
                                    else
                                    {
                                        ////Apply highest of the two. Revert the other one. 
                                        if (discount > revenueBreakDiscount)
                                        {
                                            this.UnitDiscount -= revenueBreakDiscount;
                                        }
                                        else
                                        {
                                            this.UnitDiscount -= discount;
                                        }
                                    }
                                }
                            }

                            if (priceComponent.Strategy.ObjectType.Equals(SurchargeComponents.Meta.ObjectType))
                            {
                                var surchargeComponent = (SurchargeComponent)priceComponent;
                                decimal surcharge;

                                if (surchargeComponent.ExistPrice)
                                {
                                    surcharge = surchargeComponent.Price;
                                    this.UnitSurcharge += surcharge;
                                }
                                else
                                {
                                    surcharge = decimal.Round((this.UnitBasePrice * surchargeComponent.Percentage) / 100, 2);
                                    this.UnitSurcharge += surcharge;
                                }

                                ////Revenuebreaks on quantity and value are mutually exclusive.
                                if (priceComponent.ExistRevenueQuantityBreak || priceComponent.ExistRevenueValueBreak)
                                {
                                    if (revenueBreakSurcharge == 0)
                                    {
                                        revenueBreakSurcharge = surcharge;
                                    }
                                    else
                                    {
                                        ////Apply highest of the two. Revert the other one. 
                                        if (surcharge > revenueBreakSurcharge)
                                        {
                                            this.UnitDiscount -= revenueBreakSurcharge;
                                        }
                                        else
                                        {
                                            this.UnitDiscount -= surcharge;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var adjustmentBase = this.UnitBasePrice - this.UnitDiscount + this.UnitSurcharge;

                if (this.ExistDiscountAdjustment)
                {
                    if (this.DiscountAdjustment.ExistPercentage)
                    {
                        discountAdjustmentAmount = decimal.Round((adjustmentBase * this.DiscountAdjustment.Percentage) / 100, 2);
                    }
                    else
                    {
                        discountAdjustmentAmount = this.DiscountAdjustment.Amount;
                    }

                    this.UnitDiscount += discountAdjustmentAmount;
                }

                if (this.ExistSurchargeAdjustment)
                {
                    if (this.SurchargeAdjustment.ExistPercentage)
                    {
                        surchargeAdjustmentAmount = decimal.Round((adjustmentBase * this.SurchargeAdjustment.Percentage) / 100, 2);
                    }
                    else
                    {
                        surchargeAdjustmentAmount = this.SurchargeAdjustment.Amount;
                    }

                    this.UnitSurcharge += surchargeAdjustmentAmount;
                }
            }

            var price = this.ExistActualUnitPrice ? this.ActualUnitPrice : this.UnitBasePrice;

            decimal vat = 0;
            if (this.ExistDerivedVatRate)
            {
                var vatRate = this.DerivedVatRate.Rate;
                var vatBase = price - this.UnitDiscount + this.UnitSurcharge;
                vat = decimal.Round((vatBase * vatRate) / 100, 2);
            }

            this.UnitVat = vat;
            this.TotalBasePrice = price * this.QuantityOrdered;
            this.TotalDiscount = this.UnitDiscount * this.QuantityOrdered;
            this.TotalSurcharge = this.UnitSurcharge * this.QuantityOrdered;
            this.TotalOrderAdjustment = (0 - discountAdjustmentAmount + surchargeAdjustmentAmount) * this.QuantityOrdered;

            if (this.TotalBasePrice > 0)
            {
                this.TotalDiscountAsPercentage = decimal.Round((this.TotalDiscount / this.TotalBasePrice) * 100, 2);
                this.TotalSurchargeAsPercentage = decimal.Round((this.TotalSurcharge / this.TotalBasePrice) * 100, 2);
            }

            if (this.ExistActualUnitPrice)
            {
                this.CalculatedUnitPrice = this.ActualUnitPrice;
            }
            else
            {
                this.CalculatedUnitPrice = this.UnitBasePrice - this.UnitDiscount + this.UnitSurcharge;
            }

            this.TotalExVat = this.CalculatedUnitPrice * this.QuantityOrdered;
            this.TotalVat = this.UnitVat * this.QuantityOrdered;
            this.TotalIncVat = this.TotalExVat + this.TotalVat;

            foreach (SalesOrderItem featureItem in this.OrderedWithFeatures)
            {
                this.CalculatedUnitPrice += featureItem.CalculatedUnitPrice;
                this.TotalBasePrice += featureItem.TotalBasePrice;
                this.TotalDiscount += featureItem.TotalDiscount;
                this.TotalSurcharge += featureItem.TotalSurcharge;
                this.TotalExVat += featureItem.TotalExVat;
                this.TotalVat += featureItem.TotalVat;
                this.TotalIncVat += featureItem.TotalIncVat;
            }

            var toCurrency = this.SalesOrderWhereSalesOrderItem.CustomerCurrency;
            var fromCurrency = internalOrganisation.PreferredCurrency;

            if (fromCurrency.Equals(toCurrency))
            {
                this.TotalBasePriceCustomerCurrency = this.TotalBasePrice;
                this.TotalDiscountCustomerCurrency = this.TotalDiscount;
                this.TotalSurchargeCustomerCurrency = this.TotalSurcharge;
                this.TotalExVatCustomerCurrency = this.TotalExVat;
                this.TotalVatCustomerCurrency = this.TotalVat;
                this.TotalIncVatCustomerCurrency = this.TotalIncVat;
            }
            else
            {
                this.TotalBasePriceCustomerCurrency = Currencies.ConvertCurrency(this.TotalBasePrice, fromCurrency, toCurrency);
                this.TotalDiscountCustomerCurrency = Currencies.ConvertCurrency(this.TotalDiscount, fromCurrency, toCurrency);
                this.TotalSurchargeCustomerCurrency = Currencies.ConvertCurrency(this.TotalSurcharge, fromCurrency, toCurrency);
                this.TotalExVatCustomerCurrency = Currencies.ConvertCurrency(this.TotalExVat, fromCurrency, toCurrency);
                this.TotalVatCustomerCurrency = Currencies.ConvertCurrency(this.TotalVat, fromCurrency, toCurrency);
                this.TotalIncVatCustomerCurrency = Currencies.ConvertCurrency(this.TotalIncVat, fromCurrency, toCurrency);
            }

            this.DeriveMarkupAndProfitMargin(derivation);
        }

        private List<PriceComponent> GetPriceComponents(InternalOrganisation internalOrganisation)
        {
            var priceComponents = new List<PriceComponent>();

            var extent = internalOrganisation.PriceComponentsWhereSpecifiedFor;
            if (this.ExistProduct)
            {
                foreach (PriceComponent priceComponent in extent)
                {
                    if (priceComponent.ExistProduct && priceComponent.Product.Equals(this.Product) && !priceComponent.ExistProductFeature &&
                        priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                        (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                    {
                        priceComponents.Add(priceComponent);
                    }
                }

                if (priceComponents.Count == 0 && this.Product.ExistProductWhereVariant)
                {
                    extent = internalOrganisation.PriceComponentsWhereSpecifiedFor;
                    foreach (PriceComponent priceComponent in extent)
                    {
                        if (priceComponent.ExistProduct && priceComponent.Product.Equals(this.Product.ProductWhereVariant) && !priceComponent.ExistProductFeature &&
                            priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                            (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                        {
                            priceComponents.Add(priceComponent);
                        }
                    }
                }
            }

            if (this.ExistProductFeature && !this.ExistSalesOrderItemWhereOrderedWithFeature)
            {
                foreach (PriceComponent priceComponent in extent)
                {
                    if (priceComponent.ExistProductFeature && priceComponent.ProductFeature.Equals(this.ProductFeature) && !priceComponent.ExistProduct &&
                        priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                        (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                    {
                        priceComponents.Add(priceComponent);
                    }
                }
            }

            if (this.ExistProductFeature && this.ExistSalesOrderItemWhereOrderedWithFeature)
            {
                extent = internalOrganisation.PriceComponentsWhereSpecifiedFor;
                var found = false;
                foreach (PriceComponent priceComponent in extent)
                {
                    if (priceComponent.ExistProduct && priceComponent.Product.Equals(this.SalesOrderItemWhereOrderedWithFeature.Product) &&
                        priceComponent.ExistProductFeature && priceComponent.ProductFeature.Equals(this.ProductFeature) &&
                        priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                        (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                    {
                        found = true;
                        priceComponents.Add(priceComponent);
                    }
                }

                if (!found)
                {
                    foreach (PriceComponent priceComponent in extent)
                    {
                        if (priceComponent.ExistProductFeature && priceComponent.ProductFeature.Equals(this.ProductFeature) &&
                            priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                            (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                        {
                            priceComponents.Add(priceComponent);
                        }
                    }
                }
            }

            // Discounts and surcharges can be specified without product or product feature, these need te be added to collection of pricecomponents
            extent = internalOrganisation.PriceComponentsWhereSpecifiedFor;
            foreach (PriceComponent priceComponent in extent)
            {
                if (!priceComponent.ExistProduct && !priceComponent.ExistProductFeature &&
                    priceComponent.FromDate <= this.SalesOrderWhereSalesOrderItem.OrderDate &&
                    (!priceComponent.ExistThroughDate || priceComponent.ThroughDate >= this.SalesOrderWhereSalesOrderItem.OrderDate))
                {
                    priceComponents.Add(priceComponent);
                }
            }

            return priceComponents;
        }

        private void AppsDeriveMarkupAndProfitMargin(IDerivation derivation)
        {
            this.InitialMarkupPercentage = 0;
            this.MaintainedMarkupPercentage = 0;
            this.InitialProfitMargin = 0;
            this.MaintainedProfitMargin = 0;

            ////internet wiki page on markup business
            if (this.ExistUnitPurchasePrice && this.UnitPurchasePrice != 0 && this.CalculatedUnitPrice != 0 && this.UnitBasePrice != 0)
            {
                this.InitialMarkupPercentage = decimal.Round(((this.UnitBasePrice / this.UnitPurchasePrice) - 1) * 100, 2);
                this.MaintainedMarkupPercentage = decimal.Round(((this.CalculatedUnitPrice / this.UnitPurchasePrice) - 1) * 100, 2);

                this.InitialProfitMargin = decimal.Round(((this.UnitBasePrice - this.UnitPurchasePrice) / this.UnitBasePrice) * 100, 2);
                this.MaintainedProfitMargin = decimal.Round(((this.CalculatedUnitPrice - this.UnitPurchasePrice) / this.CalculatedUnitPrice) * 100, 2);
            }
        }

        private void AppsDeriveOnShip(IDerivation derivation)
        {
            this.QuantityPendingShipment += this.QuantityRequestsShipping;
            this.QuantityRequestsShipping = 0;
        }

        private void AppsDeriveOnShipped(IDerivation derivation, decimal quantity)
        {
            this.QuantityPicked -= quantity;
            this.QuantityPendingShipment -= quantity;
            this.QuantityShipped += quantity;

            this.DeriveCurrentShipmentStatus(derivation);
        }

        private void AppsDeriveOnPicked(IDerivation derivation, decimal quantity)
        {
            this.QuantityPicked += quantity;
            this.QuantityReserved -= quantity;
        }

        private void AppsDeriveAddToShipping(IDerivation derivation, decimal quantity)
        {
            this.QuantityRequestsShipping += quantity;
            this.QuantityShortFalled -= quantity;
        }

        private void AppsShipManually(decimal quantity)
        {
            // TODO: @(martien) afstemmen met Koen
            ////if (this.SalesOrderWhereSalesOrderItem.ScheduledManually)
            ////{
            ////    if (quantity > this.ReservedFromInventoryItem.AvailableToPromise)
            ////    {
            ////        derivation.DerivationLog.AddError(new DerivationErrorGeneric(new DerivationRole(this, SalesOrderItems.Meta.QuantityRequestsShipping), "Quantity not available."));
            ////    }
            ////    else if (quantity > this.QuantityOrdered)
            ////    {
            ////        derivation.DerivationLog.AddError(new DerivationErrorGeneric(new DerivationRole(this, SalesOrderItems.Meta.QuantityRequestsShipping), "Quantity is more than is ordered."));
            ////    }
            ////    else if (quantity > this.QuantityOrdered - this.QuantityShipped - this.QuantityPendingShipment + this.QuantityReturned)
            ////    {
            ////        derivation.DerivationLog.AddError(new DerivationErrorGeneric(new DerivationRole(this, SalesOrderItems.Meta.QuantityRequestsShipping), "Quantity is more than remaining."));
            ////    }
            ////    else
            ////    {
            ////        if (quantity > 0)
            ////        {
            ////            this.QuantityReserved += quantity;
            ////            this.QuantityRequestsShipping += quantity;
            ////        }
            ////        else
            ////        {
            ////            this.DecreasePendingShipmentQuantity(derivation, 0 - quantity);
            ////        }

            ////        this.ReservedFromInventoryItem.Derive(derivation);
            ////        this.SalesOrderWhereSalesOrderItem.Derive(derivation);
            ////    }
            ////}
        }

        private void AppsDeriveSubtractFromShipping(IDerivation derivation, decimal quantity)
        {
            this.QuantityRequestsShipping -= quantity;
            if (this.QuantityRequestsShipping < 0)
            {
                this.QuantityRequestsShipping = 0;
            }

            this.QuantityShortFalled = this.QuantityOrdered - this.QuantityRequestsShipping;
        }

        private void AppsDeriveSalesRep(IDerivation derivation)
        {
            this.SalesRep = null;
            var customer = this.ShipToParty as Organisation;
            if (customer != null)
            {
                if (this.ExistProduct)
                {
                    this.SalesRep = SalesRepRelationships.SalesRep(customer, Product.PrimaryProductCategory, this.SalesOrderWhereSalesOrderItem.OrderDate);
                }
                else
                {
                    this.SalesRep = SalesRepRelationships.SalesRep(customer, null, this.SalesOrderWhereSalesOrderItem.OrderDate);
                }
            }
        }

        private void AppsDeriveVatRegime(IDerivation derivation)
        {
            if (this.ExistSalesOrderWhereSalesOrderItem)
            {
                this.VatRegime = this.ExistAssignedVatRegime ? this.AssignedVatRegime : this.SalesOrderWhereSalesOrderItem.VatRegime;

                this.DeriveVatRate(derivation);
            }
        }

        private void AppsDeriveVatRate(IDerivation derivation)
        {
            if (this.ExistProduct || this.ExistProductFeature)
            {
                this.DerivedVatRate = this.ExistProduct ? this.Product.VatRate : this.ProductFeature.VatRate;
            }

            if (this.ExistVatRegime && this.VatRegime.ExistVatRate)
            {
                this.DerivedVatRate = this.VatRegime.VatRate;
            }
        }

        private void AppsDeriveCurrentPaymentStatus(IDerivation derivation)
        {
            SalesOrderItemObjectState state = null;
            foreach (OrderShipment orderShipment in this.OrderShipmentsWhereSalesOrderItem)
            {
                foreach (SalesInvoiceItem invoiceItem in orderShipment.ShipmentItem.InvoiceItems)
                {
                    if (state == null && invoiceItem.SalesInvoiceWhereSalesInvoiceItem.CurrentObjectState.Equals(new SalesInvoiceObjectStates(this.Session).Paid))
                    {
                        state = new SalesOrderItemObjectStates(this.Session).Paid;
                    }
                    else
                    {
                        if (state != null && state.Equals(new SalesOrderItemObjectStates(this.Session).Paid))
                        {
                            state = new SalesOrderItemObjectStates(this.Session).PartiallyPaid;
                        }
                    }
                }
            }

            if (state != null)
            {
                if (!this.ExistCurrentPaymentStatus || !this.CurrentPaymentStatus.SalesOrderItemObjectState.Equals(state))
                {
                    this.CurrentPaymentStatus = new SalesOrderItemStatusBuilder(this.Session).WithSalesOrderItemObjectState(state).Build();
                    this.AddPaymentStatus(CurrentPaymentStatus);
                }
            }
        }
    }
}