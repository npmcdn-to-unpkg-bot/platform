// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalOrganisation.cs" company="Allors bvba">
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
    using System.Linq;

    using Allors.Domain;

    public partial class InternalOrganisation
    {
        // TODO: Cascading delete

        //public override void RemovePaymentMethod(PaymentMethod value)
        //{
        //    if (value.Equals(this.DefaultPaymentMethod))
        //    {
        //        this.RemoveDefaultPaymentMethod();
        //    }

        //    base.RemovePaymentMethod(value);
        //}

        public int DeriveNextSubAccountNumber()
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryOrganisation = (InternalOrganisation)repositorySession.Instantiate(this) ?? this;

            repositoryOrganisation.NextSubAccountNumber = repositoryOrganisation.ExistNextSubAccountNumber ? repositoryOrganisation.NextSubAccountNumber : 19;
            var subAccountNumber = repositoryOrganisation.NextSubAccountNumber;
            repositoryOrganisation.NextSubAccountNumber = this.NextValidElevenTestNumer(subAccountNumber + 1);

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return subAccountNumber;
        }

        public string DeriveNextPurchaseInvoiceNumber()
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryOrganisation = (InternalOrganisation)repositorySession.Instantiate(this) ?? this;

            repositoryOrganisation.NextPurchaseInvoiceNumber = repositoryOrganisation.ExistNextPurchaseInvoiceNumber ? repositoryOrganisation.NextPurchaseInvoiceNumber : 1;
            var purchaseInvoiceNumber = repositoryOrganisation.NextPurchaseInvoiceNumber;
            repositoryOrganisation.NextPurchaseInvoiceNumber++;

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryOrganisation.PurchaseInvoiceNumberPrefix, purchaseInvoiceNumber);
        }

        public string DeriveNextShipmentNumber()
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryOrganisation = (InternalOrganisation)repositorySession.Instantiate(this) ?? this;

            repositoryOrganisation.NextIncomingShipmentNumber = repositoryOrganisation.ExistNextIncomingShipmentNumber ? repositoryOrganisation.NextIncomingShipmentNumber : 1;
            var shipmentNumber = repositoryOrganisation.NextIncomingShipmentNumber;
            repositoryOrganisation.NextIncomingShipmentNumber++;

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryOrganisation.IncomingShipmentNumberPrefix, shipmentNumber);
        }

        public string DeriveNextPurchaseOrderNumber()
        {
            var repositorySession = this.DatabaseSession.Database.CreateSession();
            var repositoryOrganisation = (InternalOrganisation)repositorySession.Instantiate(this) ?? this;

            repositoryOrganisation.NextPurchaseOrderNumber = repositoryOrganisation.ExistNextPurchaseOrderNumber ? repositoryOrganisation.NextPurchaseOrderNumber : 1;
            var purchaseOrderNumber = repositoryOrganisation.NextPurchaseOrderNumber;
            repositoryOrganisation.NextPurchaseOrderNumber++;

            if (repositorySession.Database.ToString().IndexOf("Memory") < 0)
            {
                repositorySession.Commit();
            }

            return string.Format(repositoryOrganisation.PurchaseOrderNumberPrefix, purchaseOrderNumber);
        }

        protected string AppsDeriveDisplayName()
        {
            return this.Name;
        }

        protected string AppsDeriveSearchDataCharacterBoundaryText()
        {
            return this.Name;
        }

        protected string AppsDeriveSearchDataWordBoundaryText()
        {
            return null;
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistDoAccounting)
            {
                this.DoAccounting = false;
            }

            if (!this.ExistInvoiceSequence)
            {
                this.InvoiceSequence = new InvoiceSequences(this.Session).RestartOnFiscalYear;
            }

            if (!this.ExistFiscalYearStartMonth)
            {
                this.FiscalYearStartMonth = 1;
            }

            if (!this.ExistFiscalYearStartDay)
            {
                this.FiscalYearStartDay = 1;
            }
            
            if (this.DatabaseSession.Extent<TemplatePurpose>().Count > 0 &&
                this.DatabaseSession.Extent<StringTemplate>().Count > 0)
            {
                if (!this.ExistPurchaseOrderTemplates)
                {
                    var templates = this.ExistLocale ? this.Locale.StringTemplatesWhereLocale : Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    var template = templates.FirstOrDefault(t => t.ExistTemplatePurpose && t.TemplatePurpose.Equals(new TemplatePurposes(this.Session).PurchaseOrder));
                    this.AddPurchaseOrderTemplate(template);
                }

                if (!this.ExistQuoteTemplates)
                {
                    var templates = this.ExistLocale ? this.Locale.StringTemplatesWhereLocale : Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    var template = templates.FirstOrDefault(t => t.ExistTemplatePurpose && t.TemplatePurpose.Equals(new TemplatePurposes(this.Session).Quote));
                    this.AddQuoteTemplate(template);
                }

                if (!this.ExistPickListTemplates)
                {
                    var templates = this.ExistLocale ? this.Locale.StringTemplatesWhereLocale : Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    var template = templates.FirstOrDefault(t => t.ExistTemplatePurpose && t.TemplatePurpose.Equals(new TemplatePurposes(this.Session).PickList));
                    this.AddPickListTemplate(template);
                }

                if (!this.ExistPackagingSlipTemplates)
                {
                    var templates = this.ExistLocale ? this.Locale.StringTemplatesWhereLocale : Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    var template = templates.FirstOrDefault(t => t.ExistTemplatePurpose && t.TemplatePurpose.Equals(new TemplatePurposes(this.Session).PackagingSlip));
                    this.AddPackagingSlipTemplate(template);
                }

                if (!this.ExistPurchaseOrderTemplates)
                {
                    var templates = this.ExistLocale ? this.Locale.StringTemplatesWhereLocale : Singleton.Instance(this.Session).DefaultLocale.StringTemplatesWhereLocale;
                    var template = templates.FirstOrDefault(t => t.ExistTemplatePurpose && t.TemplatePurpose.Equals(new TemplatePurposes(this.Session).PurchaseShipment));
                    this.AddPurchaseOrderTemplate(template);
                }
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;

            this.AppsPartyDerive(derivation);

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                if (this.ExistClientRelationshipsWhereClient)
                {
                    foreach (ClientRelationship relationship in this.ClientRelationshipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (CustomerRelationship relationship in this.CustomerRelationshipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (DistributionChannelRelationship relationship in this.DistributionChannelRelationshipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (Employment relationship in this.EmploymentsWhereEmployer)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (Partnership relationship in this.PartnershipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (SalesRepRelationship relationship in this.SalesRepRelationshipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }

                    foreach (SupplierRelationship relationship in this.SupplierRelationshipsWhereInternalOrganisation)
                    {
                        derivation.AddDependency(relationship, this);
                    }
                }

                foreach (PaymentMethod paymentMethod in this.PaymentMethods)
                {
                    derivation.AddDependency((Derivable)paymentMethod, this);
                }
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            if (this.ExistPreviousCurrency)
            {
                derivation.Log.AssertAreEqual(this, InternalOrganisations.Meta.PreferredCurrency, InternalOrganisations.Meta.PreviousCurrency);
            }
            else
            {
                this.PreviousCurrency = this.PreferredCurrency;
            }

            this.BillingAddress = null;
            this.BillingInquiriesFax = null;
            this.BillingInquiriesPhone = null;
            this.CellPhoneNumber = null;
            this.GeneralFaxNumber = null;
            this.GeneralPhoneNumber = null;
            this.HeadQuarter = null;
            this.HomeAddress = null;
            this.InternetAddress = null;
            this.OrderAddress = null;
            this.OrderInquiriesFax = null;
            this.OrderInquiriesPhone = null;
            this.PersonalEmailAddress = null;
            this.SalesOffice = null;
            this.ShippingAddress = null;
            this.ShippingInquiriesFax = null;
            this.ShippingAddress = null;

            foreach (PartyContactMechanism partyContactMechanism in this.PartyContactMechanisms)
            {
                if (partyContactMechanism.UseAsDefault)
                {
                    if (partyContactMechanism.ContactPurpose.IsBillingAddress)
                    {
                        this.BillingAddress = partyContactMechanism.ContactMechanism;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsBillingInquiriesFax)
                    {
                        this.BillingInquiriesFax = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsBillingInquiriesPhone)
                    {
                        this.BillingInquiriesPhone = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsCellPhoneNumber)
                    {
                        this.CellPhoneNumber = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsGeneralFaxNumber)
                    {
                        this.GeneralFaxNumber = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsGeneralPhoneNumber)
                    {
                        this.GeneralPhoneNumber = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsHeadQuarters)
                    {
                        this.HeadQuarter = partyContactMechanism.ContactMechanism;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsHomeAddress)
                    {
                        this.HomeAddress = partyContactMechanism.ContactMechanism;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsInternetAddress)
                    {
                        this.InternetAddress = partyContactMechanism.ContactMechanism as ElectronicAddress;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsOrderAddress)
                    {
                        this.OrderAddress = partyContactMechanism.ContactMechanism;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsOrderInquiriesFax)
                    {
                        this.OrderInquiriesFax = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsOrderInquiriesPhone)
                    {
                        this.OrderInquiriesPhone = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsPersonalEmailAddress)
                    {
                        this.PersonalEmailAddress = partyContactMechanism.ContactMechanism as ElectronicAddress;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsSalesOffice)
                    {
                        this.SalesOffice = partyContactMechanism.ContactMechanism;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsShippingAddress)
                    {
                        this.ShippingAddress = partyContactMechanism.ContactMechanism as PostalAddress;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsShippingInquiriesFax)
                    {
                        this.ShippingInquiriesFax = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                        continue;
                    }

                    if (partyContactMechanism.ContactPurpose.IsShippingInquiriesPhone)
                    {
                        this.ShippingInquiriesPhone = partyContactMechanism.ContactMechanism as TelecommunicationsNumber;
                    }
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

            this.DisplayName = this.DeriveDisplayName();
            this.SearchData.CharacterBoundaryText = this.DeriveSearchDataCharacterBoundaryText();
            this.SearchData.WordBoundaryText = this.DeriveSearchDataWordBoundaryText();

            if (!this.ExistOwnerSecurityToken)
            {
                var securityToken = new SecurityTokenBuilder(this.Session).Build();
                this.OwnerSecurityToken = securityToken;

                this.AddSecurityToken(this.OwnerSecurityToken);
                this.AddSecurityToken(Singleton.Instance(this.Session).AdministratorSecurityToken);
            }

            this.DeriveEmployeeUserGroups(derivation);
        }

        private void AppsDeriveEmployeeUserGroups(IDerivation derivation)
        {
            if (this.IsInDatabase)
            {
                var employeeUserGroupsByName = new Dictionary<string, UserGroup>();
                foreach (UserGroup userGroup in this.UserGroupsWhereParty)
                {
                    employeeUserGroupsByName.Add(userGroup.Name, userGroup);
                }

                foreach (Role role in this.EmployeeRoles)
                {
                    var userGroupName = string.Format("{0} for {1})", role.ComposeDisplayName(), this.DisplayName);

                    if (!employeeUserGroupsByName.ContainsKey(userGroupName))
                    {
                        var userGroup = new UserGroupBuilder(this.DatabaseSession)
                            .WithName(userGroupName)
                            .WithParty(this)
                            .WithParent(role.UserGroupWhereRole)
                            .Build();

                        new AccessControlBuilder(this.DatabaseSession).WithRole(role).WithSubjectGroup(userGroup).WithObject(this.OwnerSecurityToken).Build();
                    }
                }
            }
        }

        private void AppsStartNewFiscalYear()
        {
            if (this.ExistActualAccountingPeriod && this.ActualAccountingPeriod.Active)
            {
                return;
            }

            int year = DateTime.Now.Year;
            if (this.ExistActualAccountingPeriod)
            {
                year = this.ActualAccountingPeriod.FromDate.Date.Year + 1;
            }

            var fromDate = new DateTime(year, this.FiscalYearStartMonth, this.FiscalYearStartDay).Date;

            var yearPeriod = new AccountingPeriodBuilder(this.Session)
                .WithPeriodNumber(1)
                .WithTimeFrequency(new TimeFrequencies(this.Session).Year)
                .WithFromDate(fromDate)
                .WithThroughDate(fromDate.AddYears(1).AddSeconds(-1).Date)
                .Build();

            var semesterPeriod = new AccountingPeriodBuilder(this.Session)
                .WithPeriodNumber(1)
                .WithTimeFrequency(new TimeFrequencies(this.Session).Semester)
                .WithFromDate(fromDate)
                .WithThroughDate(fromDate.AddMonths(6).AddSeconds(-1).Date)
                .WithParent(yearPeriod)
                .Build();

            var trimesterPeriod = new AccountingPeriodBuilder(this.Session)
                .WithPeriodNumber(1)
                .WithTimeFrequency(new TimeFrequencies(this.Session).Trimester)
                .WithFromDate(fromDate)
                .WithThroughDate(fromDate.AddMonths(3).AddSeconds(-1).Date)
                .WithParent(semesterPeriod)
                .Build();

            var monthPeriod = new AccountingPeriodBuilder(this.Session)
                .WithPeriodNumber(1)
                .WithTimeFrequency(new TimeFrequencies(this.Session).Month)
                .WithFromDate(fromDate)
                .WithThroughDate(fromDate.AddMonths(1).AddSeconds(-1).Date)
                .WithParent(trimesterPeriod)
                .Build();

            this.AddAccountingPeriod(yearPeriod);
            this.AddAccountingPeriod(semesterPeriod);
            this.AddAccountingPeriod(trimesterPeriod);
            this.AddAccountingPeriod(monthPeriod);

            this.ActualAccountingPeriod = monthPeriod;
        }

        private int NextValidElevenTestNumer(int previous)
        {
            var candidate = previous.ToString();
            var valid = false;

            while (!valid)
            {
                candidate = previous.ToString();
                var sum = 0;
                for (var i = candidate.Length; i > 0; i--)
                {
                    sum += int.Parse(candidate.Substring(candidate.Length - i, 1)) * i;
                }
                
                valid = (sum % 11 == 0);
                
                if (!valid)
                {
                    previous++;
                }
            }

            return int.Parse(candidate);
        }

        public bool IsPerson {
            get
            {
                return false;
            }
        }

        public bool IsOrganisation {
            get
            {
                return false;
            }
        }
    }
}