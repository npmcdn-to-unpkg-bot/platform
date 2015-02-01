// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.v.cs" company="Allors bvba">
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

using System;

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.Globalization;

    public partial class Person
    {
        public bool IsActiveClient(DateTime date)
        {
            return this.AppsIsActiveClient(date);
        }

        public bool IsActiveCustomer(DateTime date)
        {
            return this.AppsIsActiveCustomer(date);
        }

        public bool IsActiveEmployee(DateTime date)
        {
            return this.AppsIsActiveEmployee(date);
        }

        public bool IsActiveOrganisationContact(DateTime date)
        {
            return this.AppsIsActiveOrganisationContact(date);
        }

        public bool IsActiveSalesRep(DateTime date)
        {
            return this.AppsIsActiveSalesRep(date);
        }

        public bool IsActiveProspect(DateTime date)
        {
            return this.AppsIsActiveProspect(date);
        }

        public bool IsActiveSubContractor(DateTime date)
        {
            return this.AppsIsActiveSubContractor(date);
        }

        public void DeriveCommission()
        {
            this.AppsDeriveCommission();
        }

        public void DeriveCurrentSalesReps(IDerivation derivation)
        {
            this.AppsPartyDeriveCurrentSalesReps(derivation);
        }

        public void DeriveOpenOrderAmount()
        {
            this.AppsPartyDeriveOpenOrderAmount();
        }

        public void DeriveRevenue()
        {
            this.AppsPartyDeriveRevenue();
        }

        public string DeriveSearchDataCharacterBoundaryText()
        {
            return this.AppsDeriveSearchDataCharacterBoundaryText();
        }

        public string DeriveSearchDataWordBoundaryText()
        {
            return this.AppsDeriveSearchDataWordBoundaryText();
        }

        public NumberFormatInfo CurrencyFormat {
            get
            {
                return this.AppsPartyGetCurrencyFormat();
            }
        }

        public List<SalesOrder> PreOrders {
            get
            {
                return this.AppsPartyGetPreOrders();
            }
        }

        public IEnumerable<CustomerShipment> PendingCustomerShipments {
            get
            {
                return this.AppsPartyGetPendingCustomerShipments();
            }
        }

        public string DeriveDisplayName()
        {
            return this.AppsDeriveDisplayName();
        }

        public void DeriveFullName()
        {
            this.AppsDeriveFullName();
        }

        public CustomerShipment GetPendingCustomerShipmentForStore(PostalAddress address, Store store, ShipmentMethod shipmentMethod)
        {
            return this.AppsPartyGetPendingCustomerShipmentForStore(address, store, shipmentMethod);
        }

        private void DeriveCurrentEmployment(IDerivation derivation)
        {
            this.AppsDeriveCurrentEmployment(derivation);
        }

        private void BuildOwnerSecurityToken()
        {
            this.AppsBuildOwnerSecurityToken();
        }
    }
}