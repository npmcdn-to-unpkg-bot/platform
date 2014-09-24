// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Organisation.cs" company="Allors bvba">
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

    public partial class Organisation
    {
        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsApplySecurityOnDerive()
        {
            this.RemoveSecurityTokens();
            this.AddSecurityToken(this.OwnerSecurityToken);
            this.AddSecurityToken(Singleton.Instance(this.Session).AdministratorSecurityToken);

            foreach (CustomerRelationship customerRelationship in this.CustomerRelationshipsWhereCustomer)
            {
                this.AddSecurityToken(customerRelationship.InternalOrganisation.OwnerSecurityToken);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            if (!this.ExistOwnerSecurityToken)
            {
                var securityToken = new SecurityTokenBuilder(this.Session).Build();
                this.OwnerSecurityToken = securityToken;

                this.AddSecurityToken(this.OwnerSecurityToken);
            }

            derivation.Log.AssertExists(this, Organisations.Meta.Name);
            derivation.Log.AssertExists(this, Organisations.Meta.Locale);

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

            this.DisplayName = this.DeriveDisplayName();
            this.SearchData.CharacterBoundaryText = this.DeriveSearchDataCharacterBoundaryText();
            this.SearchData.WordBoundaryText = this.DeriveSearchDataWordBoundaryText();

            this.DeriveUserGroups(derivation);
            this.DeriveCurrentContacts(derivation);
        }

        private bool AppsIsActiveCustomer(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var customerRelationships = this.CustomerRelationshipsWhereCustomer;
            foreach (CustomerRelationship customerRelationship in customerRelationships)
            {
                if (customerRelationship.FromDate <= date &&
                    (!customerRelationship.ExistThroughDate || customerRelationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActiveSupplier(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var supplierRelationships = this.SupplierRelationshipsWhereSupplier;
            foreach (SupplierRelationship supplierRelationship in supplierRelationships)
            {
                if (supplierRelationship.FromDate <= date &&
                    (!supplierRelationship.ExistThroughDate || supplierRelationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActivePartner(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var partnerships = this.PartnershipsWherePartner;
            foreach (Partnership partnership in partnerships)
            {
                if (partnership.FromDate <= date &&
                    (!partnership.ExistThroughDate || partnership.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private void AppsDeriveUserGroups(IDerivation derivation)
        {
            if (this.IsInDatabase)
            {
                var customerContactGroupName = string.Format("Customer contacts at {0} ({1})", this.DisplayName, this.UniqueId);
                var supplierContactGroupName = string.Format("Supplier contacts at {0} ({1})", this.DisplayName, this.UniqueId);
                var partnerContactGroupName = string.Format("Partner contacts at {0} ({1})", this.DisplayName, this.UniqueId);

                var customerContactGroupFound = false;
                var supplierContactGroupFound = false;
                var partnerContactGroupFound = false;

                foreach (UserGroup userGroup in this.UserGroupsWhereParty)
                {
                    if (userGroup.Name == customerContactGroupName)
                    {
                        customerContactGroupFound = true;
                    }

                    if (userGroup.Name == supplierContactGroupName)
                    {
                        supplierContactGroupFound = true;
                    }

                    if (userGroup.Name == partnerContactGroupName)
                    {
                        partnerContactGroupFound = true;
                    }
                }

                if (!customerContactGroupFound)
                {
                    this.CustomerContactUserGroup = new UserGroupBuilder(this.Session)
                        .WithName(customerContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Session).Customers)
                        .Build();

                    new AccessControlBuilder(this.Session).WithRole(new Roles(DatabaseSession).Customer).WithSubjectGroup(this.CustomerContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }

                if (!supplierContactGroupFound)
                {
                    this.SupplierContactUserGroup = new UserGroupBuilder(this.Session)
                        .WithName(supplierContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Session).Suppliers)
                        .Build();

                    new AccessControlBuilder(this.Session).WithRole(new Roles(DatabaseSession).Supplier).WithSubjectGroup(this.SupplierContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }

                if (!partnerContactGroupFound)
                {
                    this.PartnerContactUserGroup = new UserGroupBuilder(this.Session)
                        .WithName(partnerContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Session).Partners)
                        .Build();

                    new AccessControlBuilder(this.Session).WithRole(new Roles(DatabaseSession).Partner).WithSubjectGroup(this.PartnerContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }
            }
        }

        private void AppsDeriveCurrentContacts(IDerivation derivation)
        {
            this.RemoveCurrentContacts();

            var contactRelationships = this.OrganisationContactRelationshipsWhereOrganisation;
            foreach (OrganisationContactRelationship contactRelationship in contactRelationships)
            {
                if (contactRelationship.FromDate <= DateTime.Now &&
                    (!contactRelationship.ExistThroughDate || contactRelationship.ThroughDate >= DateTime.Now))
                {
                    this.AddCurrentContact(contactRelationship.Contact);
                }
            }
        }

        private string AppsDeriveDisplayName()
        {
            return this.Name;
        }

        private string AppsDeriveSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0} {1}",
                this.DisplayName,
                this.ExistShippingAddress ? this.ShippingAddress.SearchData.CharacterBoundaryText : null);
        }

        private string AppsDeriveSearchDataWordBoundaryText()
        {
            return null;
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
                return true;
            }
        }
    }
}