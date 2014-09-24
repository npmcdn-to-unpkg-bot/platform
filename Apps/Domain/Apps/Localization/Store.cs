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
    using Allors.Domain;
    

    using Resources;

    public partial class Store
    {
        public string DeriveNextInvoiceNumber(int year)
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryStore = (Store)repositorySession.Instantiate(this) ?? this;

            int salesInvoiceNumber;
            if (repositoryStore.Owner.InvoiceSequence.Equals(new Allors.Domain.InvoiceSequences(this.Session).EnforcedSequence))
            {
                repositoryStore.NextSalesInvoiceNumber = repositoryStore.ExistNextSalesInvoiceNumber ? repositoryStore.NextSalesInvoiceNumber : 1;
                salesInvoiceNumber = repositoryStore.NextSalesInvoiceNumber.Value;
                repositoryStore.NextSalesInvoiceNumber++;
            }
            else
            {
                Allors.Domain.FiscalYearInvoiceNumber fiscalYearInvoiceNumber = null;
                foreach (Allors.Domain.FiscalYearInvoiceNumber x in repositoryStore.FiscalYearInvoiceNumbers)
                {
                    if (x.FiscalYear.Equals(year))
                    {
                        fiscalYearInvoiceNumber = x;
                        break;
                    }
                }

                if (fiscalYearInvoiceNumber == null)
                {
                    fiscalYearInvoiceNumber = new FiscalYearInvoiceNumberBuilder(repositoryStore.Session).WithFiscalYear(year).Build();
                    repositoryStore.AddFiscalYearInvoiceNumber(fiscalYearInvoiceNumber);
                }

                salesInvoiceNumber = fiscalYearInvoiceNumber.DeriveNextSalesInvoiceNumber();
            }

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryStore.SalesInvoiceNumberPrefix, salesInvoiceNumber);
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
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryStore = (Store)repositorySession.Instantiate(this) ?? this;

            repositoryStore.NextOutgoingShipmentNumber = repositoryStore.ExistNextOutgoingShipmentNumber ? repositoryStore.NextOutgoingShipmentNumber : 1;
            var shipmentNumber = repositoryStore.NextOutgoingShipmentNumber;
            repositoryStore.NextOutgoingShipmentNumber++;

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryStore.OutgoingShipmentNumberPrefix, shipmentNumber);
        }

        public string DeriveNextSalesOrderNumber()
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryStore = (Store)repositorySession.Instantiate(this) ?? this;

            repositoryStore.NextSalesOrderNumber = repositoryStore.ExistNextSalesOrderNumber ? repositoryStore.NextSalesOrderNumber : 1;
            var salesOrderNumber = repositoryStore.NextSalesOrderNumber;
            repositoryStore.NextSalesOrderNumber++;

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryStore.SalesOrderNumberPrefix, salesOrderNumber);
        }

        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

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

            if (new Domain.TemplatePurposes(this.Session).SalesInvoice != null &&
                new Domain.TemplatePurposes(this.Session).SalesInvoice.StringTemplatesWhereTemplatePurpose.Count > 0)
            {
                if (!this.ExistSalesInvoiceTemplates)
                {
                    Extent<Domain.StringTemplate> templates;
                    if (this.ExistOwner && this.Owner.ExistLocale)
                    {
                        templates = this.Owner.Locale.StringTemplatesWhereLocale;
                    }
                    else
                    {
                        templates = Domain.Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new Domain.TemplatePurposes(this.Session).SalesInvoice);
                    this.AddSalesInvoiceTemplate(templates.First);
                }
            }

            if (new Domain.TemplatePurposes(this.Session).SalesOrder != null &&
                new Domain.TemplatePurposes(this.Session).SalesOrder.StringTemplatesWhereTemplatePurpose.Count > 0)
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
                        templates = Domain.Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new Domain.TemplatePurposes(this.Session).SalesOrder);
                    this.AddSalesOrderTemplate(templates.First);
                }
            }

            if (new Domain.TemplatePurposes(this.Session).CustomerShipment != null &&
                new Domain.TemplatePurposes(this.Session).CustomerShipment.StringTemplatesWhereTemplatePurpose.Count > 0)
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
                        templates = Domain.Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    }

                    templates.Filter.AddEquals(StringTemplates.Meta.TemplatePurpose, new Domain.TemplatePurposes(this.Session).CustomerShipment);
                    this.AddCustomerShipmentTemplate(templates.First);
                }
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            if (!this.ExistOwner)
            {
                this.Owner = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;

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
                    derivation.Log.AddError(this, Stores.Meta.PaymentMethod, ErrorMessages.PaymentApplicationNotLargerThanInvoiceItemAmount);
                }
            }

            derivation.Log.AssertExists(this, Stores.Meta.PaymentGracePeriod);
            derivation.Log.AssertExists(this, Stores.Meta.DefaultPaymentMethod);
            derivation.Log.AssertExistsAtMostOne(this, Stores.Meta.FiscalYearInvoiceNumber, Stores.Meta.NextSalesInvoiceNumber);
            derivation.Log.AssertExists(this, Stores.Meta.Name);
            derivation.Log.AssertExists(this, Stores.Meta.CreditLimit);
            derivation.Log.AssertExists(this, Stores.Meta.DefaultShipmentMethod);
            derivation.Log.AssertExists(this, Stores.Meta.DefaultCarrier);

            this.DisplayName = this.Name;
        }
    }
}