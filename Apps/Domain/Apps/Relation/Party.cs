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

    public static class PartyExtensions
    {
        public static NumberFormatInfo AppsPartyGetCurrencyFormat(this Party party)
        {
            var cultureInfo = new CultureInfo(party.Locale.Name, false);
            var currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
            currencyFormat.CurrencySymbol = party.PreferredCurrency.Symbol;
            return currencyFormat;
        }

        public static List<SalesOrder> AppsPartyGetPreOrders(this Party party)
        {
            var preOrders = new List<SalesOrder>();
            foreach (SalesOrder salesOrder in party.SalesOrdersWhereBillToCustomer)
            {
                if (salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(party.Strategy.DatabaseSession).Provisional))
                {
                    preOrders.Add(salesOrder);
                }
            }

            return preOrders;
        }

        public static IEnumerable<CustomerShipment> AppsPartyGetPendingCustomerShipments(this Party party)
        {
            var shipments = party.ShipmentsWhereShipToParty;

            var pending = new List<CustomerShipment>();
            foreach (CustomerShipment shipment in shipments)
            {
                if (shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Created) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Picked) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).OnHold) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Packed))
                {
                    pending.Add(shipment);
                }
            }

            return pending;
        }

        public static CustomerShipment AppsPartyGetPendingCustomerShipmentForStore(this Party party, PostalAddress address, Store store, ShipmentMethod shipmentMethod)
        {
            var shipments = party.ShipmentsWhereShipToParty;
            shipments.Filter.AddEquals(Shipments.Meta.ShipToAddress, address);
            shipments.Filter.AddEquals(Shipments.Meta.Store, store);
            shipments.Filter.AddEquals(Shipments.Meta.ShipmentMethod, shipmentMethod);

            foreach (CustomerShipment shipment in shipments)
            {
                if (shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Created) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Picked) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).OnHold) ||
                    shipment.CurrentObjectState.Equals(new CustomerShipmentObjectStates(party.Strategy.DatabaseSession).Packed))
                {
                    return shipment;
                }                
            }

            return null;
        }

        public static void AppsPartyOnPostBuild(this Party party, IObjectBuilder objectBuilder)
        {
            if (!party.ExistLocale && Singleton.Instance(party.Strategy.Session).ExistDefaultInternalOrganisation)
            {
                party.Locale = Singleton.Instance(party.Strategy.Session).DefaultInternalOrganisation.Locale;
            }

            if (!party.ExistPreferredCurrency && Singleton.Instance(party.Strategy.Session).ExistDefaultInternalOrganisation)
            {
                party.PreferredCurrency = Singleton.Instance(party.Strategy.Session).DefaultInternalOrganisation.PreferredCurrency;
            }
        }

        public static void AppsPartyDerive(this Party party, IDerivation derivation)
        {
            party.DeriveCurrentSalesReps(derivation);
            party.DeriveOpenOrderAmount();
            party.DeriveRevenue();
        }

        public static void AppsPartyDeriveCurrentSalesReps(this Party party, IDerivation derivation)
        {
            party.RemoveCurrentSalesReps();

            foreach (SalesRepRelationship salesRepRelationship in party.SalesRepRelationshipsWhereCustomer)
            {
                if (salesRepRelationship.FromDate <= DateTime.UtcNow &&
                    (!salesRepRelationship.ExistThroughDate || salesRepRelationship.ThroughDate >= DateTime.UtcNow))
                {
                    party.AddCurrentSalesRep(salesRepRelationship.SalesRepresentative);
                }
            }
        }

        public static void AppsPartyDeriveOpenOrderAmount(this Party party)
        {
            party.OpenOrderAmount = 0;
            foreach (SalesOrder salesOrder in party.SalesOrdersWhereBillToCustomer)
            {
                if (!salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(party.Strategy.Session).Finished) &&
                    !salesOrder.CurrentObjectState.Equals(new SalesOrderObjectStates(party.Strategy.Session).Cancelled))
                {
                    party.OpenOrderAmount += salesOrder.TotalIncVat;
                }
            }
        }

        public static void AppsPartyDeriveRevenue(this Party party)
        {
            party.YTDRevenue = 0;
            party.LastYearsRevenue = 0;

            foreach (PartyRevenue partyRevenue in party.PartyRevenuesWhereParty)
            {
                if (partyRevenue.Year == DateTime.UtcNow.Year)
                {
                    party.YTDRevenue += partyRevenue.Revenue;
                }

                if (partyRevenue.Year == DateTime.UtcNow.AddYears(-1).Year)
                {
                    party.LastYearsRevenue += partyRevenue.Revenue;
                }
            }
        }
    }
}