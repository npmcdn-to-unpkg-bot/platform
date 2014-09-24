// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonSerializedInventoryItem.cs" company="Allors bvba">
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

    public partial class NonSerializedInventoryItem
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

        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new NonSerializedInventoryItemObjectStates(Session).Good;
            }

            if (!this.ExistAvailableToPromise)
            {
                this.AvailableToPromise = 0;
            }

            if (!this.ExistQuantityCommittedOut)
            {
                this.QuantityCommittedOut = 0;
            }

            if (!this.ExistQuantityExpectedIn)
            {
                this.QuantityExpectedIn = 0;
            }

            if (!this.ExistQuantityOnHand)
            {
                this.QuantityOnHand = 0;
            }

            if (!this.ExistFacility)
            {
                if (Singleton.Instance(this.Session).DefaultInternalOrganisation != null)
                {
                    this.Facility = Singleton.Instance(this.Session).DefaultInternalOrganisation.DefaultFacility;
                }
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, NonSerializedInventoryItems.Meta.Facility);
            derivation.Log.AssertExists(this, NonSerializedInventoryItems.Meta.CurrentObjectState);
            derivation.Log.AssertAtLeastOne(this, NonSerializedInventoryItems.Meta.Good, NonSerializedInventoryItems.Meta.Part);
            derivation.Log.AssertExistsAtMostOne(this, NonSerializedInventoryItems.Meta.Good, NonSerializedInventoryItems.Meta.Part);

            this.AppsDeriveQuantityOnHand(derivation);
            this.AppsDeriveQuantityCommittedOut(derivation);
            this.AppsDeriveQuantityExpectedIn(derivation);
            this.AppsDeriveQuantityAvailableToPromise(derivation);

            if (this.ExistPreviousQuantityOnHand && this.QuantityOnHand > this.PreviousQuantityOnHand)
            {
                this.AppsReplenishSalesOrders(derivation);
            }

            if (this.ExistPreviousQuantityOnHand && this.QuantityOnHand < this.PreviousQuantityOnHand)
            {
                this.AppsDepleteSalesOrders(derivation);
            }

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
            
            this.DeriveCurrentObjectState(derivation);

            this.AppsDeriveSku(derivation);
            this.AppsDeriveName(derivation);
            this.AppsDeriveUnitOfMeasure(derivation);

            if (this.ExistGood)
            {
                this.Good.DeriveAvailableToPromise();
            }

            this.PreviousObjectState = this.CurrentObjectState;
            this.PreviousQuantityOnHand = this.QuantityOnHand;
        }

        private void AppsDeriveQuantityOnHand(IDerivation derivation)
        {
            this.QuantityOnHand = 0M;

            foreach (InventoryItemVariance inventoryItemVariance in this.InventoryItemVariances)
            {
                this.QuantityOnHand += inventoryItemVariance.Quantity;
            }

            foreach (PickListItem pickListItem in this.PickListItemsWhereInventoryItem)
            {
                if (pickListItem.PickListWherePickListItem.CurrentObjectState.Equals(new PickListObjectStates(this.Session).Picked))
                {
                    this.QuantityOnHand -= pickListItem.ActualQuantity;
                }
            }

            foreach (ShipmentReceipt shipmentReceipt in this.ShipmentReceiptsWhereInventoryItem)
            {
                 if (shipmentReceipt.ShipmentItem.ShipmentWhereShipmentItem.CurrentObjectState.Equals(new PurchaseShipmentObjectStates(this.Session).Completed))
                {
                    this.QuantityOnHand += shipmentReceipt.QuantityAccepted;
                }
            }
        }

        private void AppsDeriveQuantityCommittedOut(IDerivation derivation)
        {
            this.QuantityCommittedOut = 0M;

            foreach (PickListItem pickListItem in this.PickListItemsWhereInventoryItem)
            {
                if (pickListItem.PickListWherePickListItem.CurrentObjectState.Equals(new PickListObjectStates(this.Session).Picked))
                {
                    this.QuantityCommittedOut -= pickListItem.ActualQuantity;
                }
            }

            foreach (SalesOrderItem salesOrderItem in this.SalesOrderItemsWhereReservedFromInventoryItem)
            {
                if (salesOrderItem.CurrentObjectState.Equals(new SalesOrderItemObjectStates(this.Session).InProcess) && !salesOrderItem.SalesOrderWhereSalesOrderItem.ScheduledManually)
                {
                    this.QuantityCommittedOut += salesOrderItem.QuantityOrdered;
                }
            }
        }

        private void AppsDeriveQuantityExpectedIn(IDerivation derivation)
        {
            this.QuantityExpectedIn = 0M;

            if (this.ExistGood)
            {
                foreach (PurchaseOrderItem purchaseOrderItem in this.Good.PurchaseOrderItemsWhereProduct)
                {
                    if (purchaseOrderItem.CurrentObjectState.Equals(new PurchaseOrderItemObjectStates(this.Session).InProcess))
                    {
                        this.QuantityExpectedIn += purchaseOrderItem.QuantityOrdered;
                        this.QuantityExpectedIn -= purchaseOrderItem.QuantityReceived;
                    }
                }
            }

            if (this.ExistPart)
            {
                foreach (PurchaseOrderItem purchaseOrderItem in this.Part.PurchaseOrderItemsWherePart)
                {
                    if (purchaseOrderItem.CurrentObjectState.Equals(new PurchaseOrderItemObjectStates(this.Session).InProcess))
                    {
                        this.QuantityExpectedIn += purchaseOrderItem.QuantityOrdered;
                        this.QuantityExpectedIn -= purchaseOrderItem.QuantityReceived;
                    }
                }
            }
        }

        private void AppsDeriveQuantityAvailableToPromise(IDerivation derivation)
        {
            this.AvailableToPromise = this.QuantityOnHand - this.QuantityCommittedOut;

            if (this.AvailableToPromise < 0)
            {
                this.AvailableToPromise = 0;
            }
        }

        private void AppsDeriveCurrentObjectState(IDerivation derivation)
        {
            

            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new NonSerializedInventoryItemStatusBuilder(this.Session).WithNonSerializedInventoryItemObjectState(this.CurrentObjectState).Build();
                this.AddNonSerializedInventoryItemStatus(currentStatus);
                this.CurrentInventoryItemStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }

            this.AppsDeriveProductCategories(derivation);
        }

        private void AppsReplenishSalesOrders(IDerivation derivation)
        {
            Extent<SalesOrderItem> salesOrderItems = this.DatabaseSession.Extent<SalesOrderItem>();
            salesOrderItems.Filter.AddEquals(SalesOrderItems.Meta.CurrentObjectState, new SalesOrderItemObjectStates(this.Session).InProcess);
            salesOrderItems.AddSort(SalesOrderItems.Meta.DeliveryDate, SortDirection.Ascending);

            if (this.ExistGood)
            {
                salesOrderItems.Filter.AddEquals(SalesOrderItems.Meta.PreviousProduct, this.Good);                
            }

            salesOrderItems = this.Session.Instantiate(salesOrderItems);

            var extra = this.QuantityOnHand - this.PreviousQuantityOnHand;

            foreach (SalesOrderItem salesOrderItem in salesOrderItems)
            {
                if (extra > 0 && salesOrderItem.QuantityShortFalled > 0)
                {
                    decimal diff;
                    if (extra >= salesOrderItem.QuantityShortFalled)
                    {
                        diff = salesOrderItem.QuantityShortFalled;
                    }
                    else
                    {
                        diff = extra;
                    }

                    extra -= diff;

                    salesOrderItem.DeriveAddToShipping(derivation, diff);
                    salesOrderItem.SalesOrderWhereSalesOrderItem.Derive(derivation);
                }
            }
        }

        private void AppsDepleteSalesOrders(IDerivation derivation)
        {
            Extent<SalesOrderItem> salesOrderItems = this.DatabaseSession.Extent<SalesOrderItem>();
            salesOrderItems.Filter.AddEquals(SalesOrderItems.Meta.CurrentObjectState, new SalesOrderItemObjectStates(this.Session).InProcess);
            salesOrderItems.AddSort(SalesOrderItems.Meta.DeliveryDate, SortDirection.Descending);

            salesOrderItems = this.Session.Instantiate(salesOrderItems);

            var subtract = this.PreviousQuantityOnHand - this.QuantityOnHand;

            foreach (SalesOrderItem salesOrderItem in salesOrderItems)
            {
                if (subtract > 0 && salesOrderItem.QuantityRequestsShipping > 0)
                {
                    decimal diff;
                    if (subtract >= salesOrderItem.QuantityRequestsShipping)
                    {
                        diff = salesOrderItem.QuantityRequestsShipping;
                    }
                    else
                    {
                        diff = subtract;
                    }

                    subtract -= diff;

                    salesOrderItem.DeriveSubtractFromShipping(derivation, diff);
                    salesOrderItem.SalesOrderWhereSalesOrderItem.Derive(derivation);
                }
            }
        }

        private void AppsDeriveProductCategories(IDerivation derivation)
        {
            this.RemoveDerivedProductCategories();

            if (this.ExistGood)
            {
                foreach (ProductCategory productCategory in this.Good.ProductCategories)
                {
                    this.AddDerivedProductCategory(productCategory);
                    this.AddParentCategories(productCategory);
                }
            }
        }

        private void AddParentCategories(ProductCategory productCategory)
        {
            if (productCategory.ExistParents)
            {
                foreach (ProductCategory parent in productCategory.Parents)
                {
                    this.AddDerivedProductCategory(parent);
                    this.AddParentCategories(parent);
                }
            }
        }

        private void AppsDeriveSku(IDerivation derivation)
        {
            this.Sku = this.ExistGood ? this.Good.Sku : string.Empty;
        }

        private void AppsDeriveName(IDerivation derivation)
        {
            if (this.ExistGood)
            {
                this.Name = this.Good.Name;
            }

            if (this.ExistPart)
            {
                this.Name = this.Part.Name;
            }
        }

        private void AppsDeriveUnitOfMeasure(IDerivation derivation)
        {
            if (this.ExistGood)
            {
                this.UnitOfMeasure = this.Good.UnitOfMeasure;
            }

            if (this.ExistPart)
            {
                this.UnitOfMeasure = this.Part.UnitOfMeasure;
            }
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        private string AppsComposeDisplayName()
        {
            return string.Format(
                "{0}{1}, {2} items located at {3}",
                this.ExistGood ? this.Good.ComposeDisplayName() : null,
                this.ExistPart ? this.Part.ComposeDisplayName() : null,
                this.QuantityOnHand,
                this.ExistFacility ? this.Facility.ComposeDisplayName() : null);
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0} {1} {2}",
                this.ExistGood ? this.Good.ComposeSearchDataCharacterBoundaryText(): null,
                this.ExistPart ? this.Part.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistFacility ? this.Facility.ComposeSearchDataCharacterBoundaryText() : null);
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0} {1} {2}",
                this.ExistGood ? this.Good.ComposeSearchDataWordBoundaryText() : null,
                this.ExistPart ? this.Part.ComposeSearchDataWordBoundaryText() : null,
                this.ExistFacility ? this.Facility.ComposeSearchDataWordBoundaryText() : null);
        }
    }
}