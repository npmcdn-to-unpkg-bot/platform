// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Store.cs" company="Allors bvba">
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

    using Resources;

    public partial class Store
    {
        public string DeriveNextInvoiceNumber(int year)
        {
            int salesInvoiceNumber;
            if (this.Owner.InvoiceSequence.Equals(new InvoiceSequences(this.Strategy.Session).EnforcedSequence))
            {
                salesInvoiceNumber = Counters.NextValue(this.strategy.Session, Counters.SalesInvoiceCounterId);
            }
            else
            {
                FiscalYearInvoiceNumber fiscalYearInvoiceNumber = null;
                foreach (FiscalYearInvoiceNumber x in this.FiscalYearInvoiceNumbers)
                {
                    if (x.FiscalYear.Equals(year))
                    {
                        fiscalYearInvoiceNumber = x;
                        break;
                    }
                }

                if (fiscalYearInvoiceNumber == null)
                {
                    fiscalYearInvoiceNumber = new FiscalYearInvoiceNumberBuilder(this.Strategy.Session).WithFiscalYear(year).Build();
                    this.AddFiscalYearInvoiceNumber(fiscalYearInvoiceNumber);
                }

                salesInvoiceNumber = fiscalYearInvoiceNumber.DeriveNextSalesInvoiceNumber();
            }

            return string.Format(this.SalesInvoiceNumberPrefix, salesInvoiceNumber);
        }

        // TODO: Cascading delete
        //public override void RemovePaymentMethod(PaymentMethod value)
        //{
        //    if (value.Equals(this.DefaultPaymentMethod))
        //    {
        //        this.RemoveDefaultPaymentMethod();
        //    }

        //    base.RemovePaymentMethod(value);
        //}

        public string DeriveNextShipmentNumber()
        {
            var shipmentNumber = Counters.NextValue(this.strategy.Session, Counters.OutgoingShipmentCounterId);
            return string.Format(this.OutgoingShipmentNumberPrefix, shipmentNumber);
        }

        public string DeriveNextSalesOrderNumber()
        {
            var salesOrderNumber = Counters.NextValue(this.strategy.Session, Counters.SalesOrderCounterId);
            return string.Format(this.SalesOrderNumberPrefix, salesOrderNumber);
        }

        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            if (!this.ExistSalesOrderCounter)
            {
                this.SalesOrderCounter = new CounterBuilder(this.strategy.Session).WithUniqueId(Guid.NewGuid()).WithValue(0).Build();
            }

            if (!this.ExistCreditLimit)
            {
                this.CreditLimit = 0;
            }
            
            if (!this.ExistShipmentThreshold)
            {
                this.ShipmentThreshold = 0;
            }

            if (!this.ExistOrderThreshold)
            {
                this.OrderThreshold = 0;
            }

            if (!this.ExistPaymentGracePeriod)
            {
                this.PaymentGracePeriod = 0;
            }

            if (!this.ExistSalesInvoiceNumberPrefix)
            {
                this.SalesInvoiceNumberPrefix = "{0}";
            }

            if (new TemplatePurposes(this.Strategy.Session).SalesInvoice != null &&
                new TemplatePurposes(this.Strategy.Session).SalesInvoice.StringTemplatesWhereTemplatePurpose.Count > 0)
            {
                if (!this.ExistSalesInvoiceTemplates)
                {
                    Extent<StringTemplate> templates;
                    if (this.ExistOwner && this.Owner.ExistLocale)
                    {
                        templates = this.Owner.Locale.StringTemplatesWhereLocale;
                    }
                    else
                    {
                        templates = Singleton.Instance(this.Strategy.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new TemplatePurposes(this.Strategy.Session).SalesInvoice);
                    this.AddSalesInvoiceTemplate(templates.First);
                }
            }

            if (new TemplatePurposes(this.Strategy.Session).SalesOrder != null &&
                new TemplatePurposes(this.Strategy.Session).SalesOrder.StringTemplatesWhereTemplatePurpose.Count > 0)
            {
                if (!this.ExistSalesOrderTemplates)
                {
                    Extent<Domain.StringTemplate> templates;
                    if (this.ExistOwner && this.Owner.ExistLocale)
                    {
                        templates = this.Owner.Locale.StringTemplatesWhereLocale;
                    }
                    else
                    {
                        templates = Domain.Singleton.Instance(this.Strategy.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new Domain.TemplatePurposes(this.Strategy.Session).SalesOrder);
                    this.AddSalesOrderTemplate(templates.First);
                }
            }

            if (new TemplatePurposes(this.Strategy.Session).CustomerShipment != null &&
                new TemplatePurposes(this.Strategy.Session).CustomerShipment.StringTemplatesWhereTemplatePurpose.Count > 0)
            {
                if (!this.ExistCustomerShipmentTemplates)
                {
                    Extent<Domain.StringTemplate> templates;
                    if (this.ExistOwner && this.Owner.ExistLocale)
                    {
                        templates = this.Owner.Locale.StringTemplatesWhereLocale;
                    }
                    else
                    {
                        templates = Domain.Singleton.Instance(this.Strategy.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new Domain.TemplatePurposes(this.Strategy.Session).CustomerShipment);
                    this.AddCustomerShipmentTemplate(templates.First);
                }
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            if (!this.ExistOwner)
            {
                this.Owner = Domain.Singleton.Instance(this.Strategy.Session).DefaultInternalOrganisation;

                if (this.ExistOwner && this.Owner.ExistDefaultFacility)
                {
                    this.DefaultFacility = this.Owner.DefaultFacility;
                }
            }

            if (this.ExistDefaultPaymentMethod && !this.PaymentMethods.Contains(this.DefaultPaymentMethod))
            {
                this.AddPaymentMethod(this.DefaultPaymentMethod);
            }

            if (!this.ExistDefaultPaymentMethod && this.PaymentMethods.Count == 1)
            {
                this.DefaultPaymentMethod = this.PaymentMethods.First;
            }

            if (this.ExistOwner)
            {
                if (!this.ExistDefaultPaymentMethod && this.Owner.ExistDefaultPaymentMethod)
                {
                    this.DefaultPaymentMethod = this.Owner.DefaultPaymentMethod;

                    if (!this.ExistPaymentMethods || !this.PaymentMethods.Contains(this.DefaultPaymentMethod))
                    {
                        this.AddPaymentMethod(this.DefaultPaymentMethod);
                    }
                }
            }

            foreach (PaymentMethod paymentMethod in this.PaymentMethods)
            {
                if (this.ExistOwner && !this.Owner.PaymentMethods.Contains(paymentMethod))
                {
                    derivation.Log.AddError(this, Stores.Meta.PaymentMethods, ErrorMessages.PaymentApplicationNotLargerThanInvoiceItemAmount);
                }
            }

            derivation.Log.AssertExistsAtMostOne(this, Stores.Meta.FiscalYearInvoiceNumbers, Stores.Meta.SalesInvoiceCounter);
        }
    }
}