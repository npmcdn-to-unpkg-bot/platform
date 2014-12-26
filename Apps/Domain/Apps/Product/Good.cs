// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Good.cs" company="Allors bvba">
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
    public partial class Good
    {
        public void AddToBasePrice(BasePrice basePrice)
        {
            this.AddBasePrice(basePrice);
        }

        public void RemoveFromBasePrices(BasePrice basePrice)
        {
            this.RemoveBasePrice(basePrice);
        }

        public void DeriveVirtualProductPriceComponent()
        {
            if (!this.ExistProductWhereVariant)
            {
                this.RemoveVirtualProductPriceComponents();
            }

            if (this.ExistVariants)
            {
                this.RemoveVirtualProductPriceComponents();

                var priceComponents = this.PriceComponentsWhereProduct;

                foreach (Good product in this.Variants)
                {
                    foreach (PriceComponent priceComponent in priceComponents)
                    {
                        product.AddVirtualProductPriceComponent(priceComponent);

                        var basePrice = priceComponent as BasePrice;
                        if (basePrice != null && !priceComponent.ExistProductFeature)
                        {
                            product.AddToBasePrice(basePrice);
                        }
                    }
                }
            }
        }

        public void DeriveProductCategoryExpanded()
        {
            this.RemoveProductCategoriesExpanded();

            if (this.ExistPrimaryProductCategory)
            {
                this.AddProductCategoryExpanded(this.PrimaryProductCategory);
                foreach (ProductCategory ancestor in this.PrimaryProductCategory.Ancestors)
                {
                    this.AddProductCategoryExpanded(ancestor);
                }
            }

            foreach (ProductCategory productCategory in this.ProductCategories)
            {
                this.AddProductCategoryExpanded(productCategory);
                foreach (ProductCategory ancestor in productCategory.Ancestors)
                {
                    this.AddProductCategoryExpanded(ancestor);
                }
            }
        }

        public void DeriveAvailableToPromise()
        {
            this.AvailableToPromise = 0;

            foreach (InventoryItem inventoryItem in this.InventoryItemsWhereGood)
            {
                if (inventoryItem is NonSerializedInventoryItem)
                {
                    var nonSerialized = (NonSerializedInventoryItem)inventoryItem;
                    this.AvailableToPromise += nonSerialized.AvailableToPromise;
                }
            }
        }

        public void DeriveThumbnail()
        {
            if (this.ExistPhoto && this.Photo.ExistMediaType && this.Photo.MediaType.IsImage && this.Photo.ExistMediaContent)
            {
                if (!this.ExistThumbnail)
                {
                    this.Thumbnail = new MediaBuilder(this.Strategy.Session).Build();
                }

                // TODO: Resize
                //var thumbNail = Media.CreateThumbnail(this.Photo.Bitmap, ThumbnailWidth);
                //this.Thumbnail.Load(thumbNail, ImageFormat.Jpeg);
            }
            else
            {
                this.RemoveThumbnail();
            }
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSoldBy)
            {
                this.SoldBy = Domain.Singleton.Instance(this.Strategy.Session).DefaultInternalOrganisation;
            }
        }

        public void AppsPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                if(this.ExistInventoryItemsWhereGood)
                {
                    foreach (InventoryItem inventoryItem in InventoryItemsWhereGood)
                    {
                        derivation.AddDependency((Derivable)inventoryItem, this);
                    }
                }
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, Goods.Meta.FinishedGood, Goods.Meta.InventoryItemKind);
            derivation.Log.AssertExistsAtMostOne(this, Goods.Meta.FinishedGood, Goods.Meta.InventoryItemKind);

            if (this.ProductCategories.Count == 1 && !this.ExistPrimaryProductCategory)
            {
                this.PrimaryProductCategory = this.ProductCategories.First;
            }

            if (this.ExistPrimaryProductCategory && !this.ExistProductCategories)
            {
                this.AddProductCategory(PrimaryProductCategory);
            }

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();

            this.DeriveVirtualProductPriceComponent();
            this.DeriveProductCategoryExpanded();
            this.DeriveAvailableToPromise();
            this.DeriveThumbnail();
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
            return string.Format("{0}, SKU: {1}", this.ExistName ? this.Name : null, this.ExistSku ? this.Sku : null);
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0} {1} {2} {3} {4}",
                this.ExistName ? this.Name : null,
                this.ExistSku ? this.Sku : null,
                this.ExistBarCode ? this.BarCode : null,
                this.ExistManufacturerId ? this.ManufacturerId : null,
                this.ExistArticleNumber ? this.ArticleNumber : null);
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return null;
        }
    }
}