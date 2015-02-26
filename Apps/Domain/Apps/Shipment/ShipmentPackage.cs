// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShipmentPackage.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Globalization;

    using Allors.Domain;

    public partial class ShipmentPackage
    {
        public decimal TotalQuantity
        {
            get
            {
                var total = 0M;
                foreach (PackagingContent packagingContent in this.PackagingContents)
                {
                    total += packagingContent.Quantity;
                }

                return total;
            }
        }

        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistCreationDate)
            {
                this.CreationDate = DateTime.Now;
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.AppsDeriveSequenceNumber(derivation);

            this.DisplayName = string.Format("Package {0}", this.ExistSequenceNumber ? this.SequenceNumber.ToString(CultureInfo.InvariantCulture) : string.Empty);

            foreach (Document document in this.Documents)
            {
                if (document is PackagingSlip)
                {
                    document.PrintContent = this.PrintContent;
                }
            }

            if (!this.ExistDocuments)
            {
                this.AddDocument(new PackagingSlipBuilder(this.Strategy.Session).WithName(this.DisplayName).Build());
            }

            this.AppsDeriveTemplate(derivation);
        }

        private void AppsDeriveSequenceNumber(IDerivation derivation)
        {
            var highestNumber = 0;
            if (this.ExistShipmentWhereShipmentPackage)
            {
                foreach (ShipmentPackage shipmentPackage in this.ShipmentWhereShipmentPackage.ShipmentPackages)
                {
                    if (shipmentPackage.ExistSequenceNumber && shipmentPackage.SequenceNumber > highestNumber)
                    {
                        highestNumber = shipmentPackage.SequenceNumber;
                    }
                }

                if (!this.ExistSequenceNumber)
                {
                    this.SequenceNumber = highestNumber + 1;
                }
            }
        }

        private void AppsDeriveTemplate(IDerivation derivation)
        {
            Domain.StringTemplate template = null;

            var shipment = this.ShipmentWhereShipmentPackage;

            if (shipment != null)
            {
                if (shipment.ExistBillFromInternalOrganisation && shipment.ExistBillToParty && shipment.BillToParty.ExistLocale)
                {
                    var templates = shipment.BillFromInternalOrganisation.PackagingSlipTemplates;
                    templates.Filter.AddEquals(StringTemplates.Meta.Locale, shipment.BillToParty.Locale);
                    template = templates.First;
                }

                if (shipment.ExistBillFromInternalOrganisation && template == null && shipment.BillFromInternalOrganisation.ExistLocale)
                {
                    var templates = shipment.BillFromInternalOrganisation.PackagingSlipTemplates;
                    templates.Filter.AddEquals(StringTemplates.Meta.Locale, shipment.BillFromInternalOrganisation.Locale);
                    template = templates.First;
                }

                if (shipment.ExistBillFromInternalOrganisation && template == null)
                {
                    var templates = shipment.BillFromInternalOrganisation.PackagingSlipTemplates;
                    // TODO:
                    template = templates.First;
                }
            }

            if (template != null)
            {
                this.PrintContent = template.Apply(new Dictionary<string, object> { { "this", this } });
            }
        }
    }
}