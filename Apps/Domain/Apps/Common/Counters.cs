// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Counters.cs" company="Allors bvba">
//   Copyright 2002-2011 Allors bvba.
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

    public partial class Counters
    {
        public static Guid SalesInvoiceCounterId = new Guid("2DF6BFD8-E278-4186-BA2F-551E6122D761");
        public static Guid SalesOrderCounterId = new Guid("12E56A65-531A-41C6-9C13-2E747FB654D0");
        public static Guid OutgoingShipmentCounterId = new Guid("3E1710DF-446A-44FB-8A15-CA623A520167");

        public static Guid SubAccountCounterId = new Guid("FE8BD481-5AA9-499E-AEB5-AF15195D2D3E");
        public static Guid PurchaseInvoiceCounterId = new Guid("509CD3CF-A7E5-4F80-8041-289923EA0296");
        public static Guid IncomingShipmentCounterId = new Guid("2781F1C0-B56A-4781-86DB-881BA20EAD8D");

        protected override void AppsSetup(Setup setup)
        {
            new CounterBuilder(this.Session).WithUniqueId(SalesInvoiceCounterId).Build();
            new CounterBuilder(this.Session).WithUniqueId(SalesOrderCounterId).Build();
            new CounterBuilder(this.Session).WithUniqueId(OutgoingShipmentCounterId).Build();
        
            new CounterBuilder(this.Session).WithUniqueId(SubAccountCounterId).Build();
            new CounterBuilder(this.Session).WithUniqueId(PurchaseInvoiceCounterId).Build();
            new CounterBuilder(this.Session).WithUniqueId(IncomingShipmentCounterId).Build();
        }
    }
}
