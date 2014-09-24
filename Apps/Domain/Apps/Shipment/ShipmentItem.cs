 // --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShipmentItem.cs" company="Allors bvba">
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

    public partial class ShipmentItem
    {
        public override void Delete()
        {
            if (this.ExistItemIssuancesWhereShipmentItem)
            {
                foreach (ItemIssuance itemIssuance in this.ItemIssuancesWhereShipmentItem)
                {
                    itemIssuance.Delete();
                }
            }

            base.Delete();
        }
        
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);
        
            if (!this.ExistQuantity)
            {
                this.Quantity = 0;
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                derivation.AddDependency(this.ShipmentWhereShipmentItem as Derivable, this);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            this.DeriveCustomerShipmentItem(derivation);

            this.DerivePurchaseShipmentItem(derivation);

            this.DisplayName = this.ExistContentsDescription ? this.ContentsDescription : string.Empty;

            if (string.IsNullOrEmpty(this.DisplayName))
            {
                this.DisplayName = string.Format(
                    "{0} {1} {2}", 
                    this.Quantity, 
                    this.ExistGood ? this.Good.ComposeDisplayName() : null, 
                    this.ExistPart ? this.Part.ComposeDisplayName() : null);
            }
        }

        private void AppsDerivePurchaseShipmentItem(IDerivation derivation)
        {
            if (this.ShipmentWhereShipmentItem is PurchaseShipment)
            {
                this.Quantity = 0;
                var shipmentReceipt = this.ShipmentReceiptWhereShipmentItem;
                this.Quantity += shipmentReceipt.QuantityAccepted + shipmentReceipt.QuantityRejected;
            }
        }

        private void AppsDeriveCustomerShipmentItem(IDerivation derivation)
        {
            if (this.ShipmentWhereShipmentItem is CustomerShipment)
            {
                this.QuantityShipped = 0;
                foreach (PackagingContent packagingContent in PackagingContentsWhereShipmentItem)
                {
                    this.QuantityShipped += packagingContent.Quantity;
                }
            }
        }
    }
}