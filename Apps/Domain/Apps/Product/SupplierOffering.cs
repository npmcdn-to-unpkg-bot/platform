// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SupplierOffering.cs" company="Allors bvba">
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

    public partial class SupplierOffering
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, SupplierOfferings.Meta.Supplier);
            derivation.Log.AssertExists(this, SupplierOfferings.Meta.ProductPurchasePrice);
            derivation.Log.AssertAtLeastOne(this, SupplierOfferings.Meta.Part, SupplierOfferings.Meta.Product);
            derivation.Log.AssertExistsAtMostOne(this, SupplierOfferings.Meta.Part, SupplierOfferings.Meta.Product);

            this.DisplayName = string.Format(
                "{0}{1} supplied by {2}",
                this.ExistPart ? this.Part.ComposeDisplayName() : null,
                this.ExistProduct ? this.Product.ComposeDisplayName() : null,
                this.ExistSupplier ? this.Supplier.DeriveDisplayName() : null);

            this.DeriveInventoryItem(derivation);
        }

        private void AppsDeriveInventoryItem(IDerivation derivation)
        {
            Good good = null;
            if (this.ExistProduct)
            {
                good = this.Product as Good;
            }

            var supplier = this.Supplier as Organisation;
            if (supplier != null && good != null)
            {
                if (good.ExistInventoryItemKind && good.InventoryItemKind.Equals(new InventoryItemKinds(this.Session).NonSerialized))
                {
                    foreach (SupplierRelationship supplierRelationship in supplier.SupplierRelationshipsWhereSupplier)
                    {
                        foreach (Facility facility in supplierRelationship.InternalOrganisation.FacilitiesWhereOwner)
                        {
                            var inventoryItems = good.InventoryItemsWhereGood;
                            inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, facility);
                            var inventoryItem = inventoryItems.First;

                            if (inventoryItem == null)
                            {
                                new NonSerializedInventoryItemBuilder(this.Session).WithFacility(facility).WithGood(good).Build();
                            }
                        }
                    }
                }
                else
                {
                    if (good.ExistFinishedGood &&
                        good.FinishedGood.ExistInventoryItemKind && 
                        good.FinishedGood.InventoryItemKind.Equals(new InventoryItemKinds(this.Session).NonSerialized))
                    {
                        foreach (SupplierRelationship supplierRelationship in supplier.SupplierRelationshipsWhereSupplier)
                        {
                            foreach (Facility facility in supplierRelationship.InternalOrganisation.FacilitiesWhereOwner)
                            {
                                var inventoryItems = good.FinishedGood.InventoryItemsWherePart;
                                inventoryItems.Filter.AddEquals(InventoryItems.Meta.Facility, facility);
                                var inventoryItem = inventoryItems.First;

                                if (inventoryItem == null)
                                {
                                    new NonSerializedInventoryItemBuilder(this.Session).WithFacility(facility).WithPart(good.FinishedGood).Build();
                                }
                            }
                        }
                    }                   
                }
            }
        }
    }
}