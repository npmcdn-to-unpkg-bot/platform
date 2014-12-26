// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerRelationship.cs" company="Allors bvba">
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

    public partial class CustomerRelationship
    {
        public int? PaymentNetDays
        {
            get
            {
                int? customerPaymentNetDays = null;
                foreach (Agreement agreement in this.RelationshipAgreements)
                {
                    foreach (AgreementTerm term in agreement.AgreementTerms)
                    {
                        if (term.TermType.Equals(new TermTypes(this.Session).PaymentNetDays))
                        {
                            int netDays;
                            if (int.TryParse(term.TermValue, out netDays))
                            {
                                customerPaymentNetDays = netDays;
                            }

                            return customerPaymentNetDays;
                        }
                    }
                }

                return null;
            }
        }

        public void AppsDeriveRevenue(IDerivation derivation)
        {
            if (this.ExistCustomer)
            {
                this.YTDRevenue = 0;
                this.LastYearsRevenue = 0;

                foreach (Domain.PartyRevenue partyRevenue in this.Customer.PartyRevenuesWhereParty)
                {
                    if (partyRevenue.InternalOrganisation.Equals(this.InternalOrganisation))
                    {
                        if (partyRevenue.Year == DateTime.Now.Year)
                        {
                            this.YTDRevenue += partyRevenue.Year;
                        }

                        if (partyRevenue.Year == DateTime.Now.AddYears(-1).Year)
                        {
                            this.LastYearsRevenue += partyRevenue.Year;
                        }
                    }
                }
            }
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistFromDate)
            {
                this.FromDate = DateTime.Now;
            }

            if (!this.ExistInternalOrganisation)
            {
                this.InternalOrganisation = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;
            }

            if (!this.ExistAmountDue)
            {
                this.AmountDue = 0;
            }

            if (!this.ExistAmountOverDue)
            {
                this.AmountOverDue = 0;
            }

            if (!this.ExistSubAccountNumber)
            {
                this.SubAccountNumber = this.InternalOrganisation.DeriveNextSubAccountNumber();
            }
        }

        public void AppsPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;

            if (this.ExistCustomer)
            {
                derivation.AddDependency(this, this.Customer);
            
                var customer = this.Customer as Organisation;
                if (customer != null)
                {
                    foreach (OrganisationContactRelationship contactRelationship in customer.OrganisationContactRelationshipsWhereOrganisation)
                    {
                        derivation.AddDerivable(contactRelationship);
                    }
                }
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            if (this.Session is IDatabaseSession)
            {
                var customerRelationships = this.InternalOrganisation.CustomerRelationshipsWhereInternalOrganisation;
                customerRelationships.Filter.AddEquals(CustomerRelationships.Meta.SubAccountNumber, this.SubAccountNumber);
                if (customerRelationships.Count == 1)
                {
                    if (!customerRelationships[0].Equals(this))
                    {
                        derivation.Log.AddError(new DerivationErrorUnique(derivation.Log, this, CustomerRelationships.Meta.SubAccountNumber));
                    }
                }
                else if (customerRelationships.Count > 1)
                {
                    derivation.Log.AddError(new DerivationErrorUnique(derivation.Log, this, CustomerRelationships.Meta.SubAccountNumber));
                }
            }

            this.DisplayName = string.Format(
                "{0} customer at {1}",
                this.ExistCustomer ? this.Customer.DeriveDisplayName() : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveDisplayName() : null);

            this.DeriveInternalOrganisationCustomer(derivation);
            this.DeriveMembership(derivation);

            this.DeriveAmountDue(derivation);
            this.DeriveAmountOverDue(derivation);
            this.DeriveRevenue(derivation);
        }

        private void AppsDeriveInternalOrganisationCustomer(IDerivation derivation)
        {
            if (this.ExistCustomer && this.ExistInternalOrganisation)
            {
                if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                {
                    if (!this.Customer.ExistInternalOrganisationWhereCustomer)
                    {
                        this.InternalOrganisation.AddCustomer(this.Customer);
                    }
                }

                if (this.FromDate > DateTime.Now || (this.ExistThroughDate && this.ThroughDate < DateTime.Now))
                {
                    if (this.Customer.ExistInternalOrganisationWhereCustomer)
                    {
                        this.InternalOrganisation.RemoveCustomer(this.Customer);
                    }
                }
            }
        }

        private void AppsDeriveMembership(IDerivation derivation)
        {
            if (this.ExistCustomer && this.ExistInternalOrganisation)
            {
                var customerOrganisation = this.Customer as Organisation;
                if (customerOrganisation != null && customerOrganisation.ExistCustomerContactUserGroup)
                {
                    foreach (Person contact in customerOrganisation.CustomerContactUserGroup.Members)
                    {
                        customerOrganisation.CustomerContactUserGroup.RemoveMember(contact);
                    }

                    if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                    {
                        foreach (Person currentContact in customerOrganisation.CurrentContacts)
                        {
                            customerOrganisation.CustomerContactUserGroup.AddMember(currentContact);
                        }
                    }
                }
            }
        }

        private void AppsDeriveAmountDue(IDerivation derivation)
        {
            this.AmountDue = 0;

            if (this.ExistCustomer)
            {
                foreach (Allors.Domain.SalesInvoice salesInvoice in this.Customer.SalesInvoicesWhereBillToCustomer)
                {
                    if (salesInvoice.BilledFromInternalOrganisation.Equals(this.InternalOrganisation)
                        && !salesInvoice.CurrentObjectState.Equals(new Allors.Domain.SalesInvoiceObjectStates(this.Session).Paid))
                    {
                        if (salesInvoice.AmountPaid > 0)
                        {
                            this.AmountDue += salesInvoice.TotalIncVat - salesInvoice.AmountPaid;
                        }
                        else
                        {
                            foreach (Allors.Domain.SalesInvoiceItem invoiceItem in salesInvoice.InvoiceItems)
                            {
                                if (!invoiceItem.CurrentObjectState.Equals(new Allors.Domain.SalesInvoiceItemObjectStates(this.Session).Paid))
                                {
                                    if (invoiceItem.ExistTotalIncVat)
                                    {
                                        this.AmountDue += invoiceItem.TotalIncVat - invoiceItem.AmountPaid;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AppsDeriveAmountOverDue(IDerivation derivation)
        {
            this.AmountOverDue = 0;

            if (this.ExistCustomer)
            {
                foreach (SalesInvoice salesInvoice in this.Customer.SalesInvoicesWhereBillToCustomer)
                {
                    if (salesInvoice.BilledFromInternalOrganisation.Equals(this.InternalOrganisation)
                        && !salesInvoice.CurrentObjectState.Equals(new Allors.Domain.SalesInvoiceObjectStates(this.Session).Paid))
                    {
                        var gracePeriod = salesInvoice.Store.PaymentGracePeriod;

                        if (salesInvoice.DueDate.HasValue)
                        {
                            var dueDate = salesInvoice.DueDate.Value.AddDays(gracePeriod);

                            if (DateTime.Now > dueDate)
                            {
                                this.AmountOverDue += salesInvoice.TotalIncVat - salesInvoice.AmountPaid;
                            }
                        }
                    }
                }
            }
        }
    }
}