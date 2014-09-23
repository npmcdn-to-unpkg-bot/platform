// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Party.cs" company="Allors bvba">
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

    public abstract partial class Party
    {
        public virtual bool IsPerson
        {
            get
            {
                return false;
            }
        }

        public IFormatProvider CurrencyFormat
        {
            get
            {
                var cultureInfo = new CultureInfo(this.Locale.Name, false);
                var currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = this.PreferredCurrency.Symbol;
                return currencyFormat;
            }
        }

        public List<SalesOrder> PreOrders
        {
            get
            {
                var preOrders = new List<SalesOrder>();
                foreach (SalesOrder salesOrder in this.SalesOrdersWhereBillToCustomer)
                {
                    if (salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(this.DatabaseSession).Provisional))
                    {
                        preOrders.Add(salesOrder);
                    }
                }

                return preOrders;
            }
        }

        public IEnumerable<CustomerShipment> PendingCustomerShipments
        {
            get
            {
                var shipments = this.ShipmentsWhereShipToParty;

                var pending = new List<CustomerShipment>();
                foreach (CustomerShipment shipment in shipments)
                {
                    if (shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Created) ||
                        shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Picked) ||
                        shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).OnHold) ||
                        shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Packed))
                    {
                        pending.Add(shipment);
                    }
                }

                return pending;
            }
        }

        public abstract string DeriveDisplayName();

        public CustomerShipment GetPendingCustomerShipmentForStore(PostalAddress address, Store store, ShipmentMethod shipmentMethod)
        {
            var shipments = this.ShipmentsWhereShipToParty;
            shipments.Filter.AddEquals(Shipments.Meta.ShipToAddress, address);
            shipments.Filter.AddEquals(Shipments.Meta.Store, store);
            shipments.Filter.AddEquals(Shipments.Meta.ShipmentMethod, shipmentMethod);

            foreach (CustomerShipment shipment in shipments)
            {
                if (shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Created) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Picked) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).OnHold) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(this.DatabaseSession).Packed))
                {
                    return shipment;
                }                
            }

            return null;
        }

        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

            if (!this.ExistLocale && Singleton.Instance(this.Session).ExistDefaultInternalOrganisation)
            {
                this.Locale = Singleton.Instance(this.Session).DefaultInternalOrganisation.Locale;
            }

            if (!this.ExistPreferredCurrency && Singleton.Instance(this.Session).ExistDefaultInternalOrganisation)
            {
                this.PreferredCurrency = Singleton.Instance(this.Session).DefaultInternalOrganisation.PreferredCurrency;
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            this.DeriveCurrentSalesReps(derivation);
            this.DeriveOpenOrderAmount();
            this.DeriveRevenue();

            this.PartyName = this.DeriveDisplayName();
        }

        private void AppsDeriveCurrentSalesReps(IDerivation derivation)
        {
            this.RemoveCurrentSalesReps();

            foreach (SalesRepRelationship salesRepRelationship in this.SalesRepRelationshipsWhereCustomer)
            {
                if (salesRepRelationship.FromDate <= DateTime.Now &&
                    (!salesRepRelationship.ExistThroughDate || salesRepRelationship.ThroughDate >= DateTime.Now))
                {
                    this.AddCurrentSalesRep(salesRepRelationship.SalesRepresentative);
                }
            }
        }

        private void AppsDeriveOpenOrderAmount()
        {
            this.OpenOrderAmount = 0;
            foreach (SalesOrder salesOrder in this.SalesOrdersWhereBillToCustomer)
            {
                if (!salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).Finished) &&
                    !salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(this.Session).Cancelled))
                {
                    this.OpenOrderAmount += salesOrder.TotalIncVat;
                }
            }
        }

        private void AppsDeriveRevenue()
        {
            this.YTDRevenue = 0;
            this.LastYearsRevenue = 0;

            foreach (PartyRevenue partyRevenue in this.PartyRevenuesWhereParty)
            {
                if (partyRevenue.Year == DateTime.Now.Year)
                {
                    this.YTDRevenue += partyRevenue.Revenue;
                }

                if (partyRevenue.Year == DateTime.Now.AddYears(-1).Year)
                {
                    this.LastYearsRevenue += partyRevenue.Revenue;
                }
            }
        }
    }
}