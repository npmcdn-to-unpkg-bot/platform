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

using System.Collections.Generic;

namespace Allors.Domain
{
    using System;

    public partial class Organisation
    {
        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistLocale)
            {
                this.Locale = Singleton.Instance(this.Strategy.Session).DefaultLocale;
            }
        }

        public void AppsApplySecurityOnDerive(ObjectApplySecurityOnDerive method)
        {
            this.RemoveSecurityTokens();
            this.AddSecurityToken(this.OwnerSecurityToken);
            this.AddSecurityToken(Singleton.Instance(this.Strategy.Session).AdministratorSecurityToken);

            foreach (CustomerRelationship customerRelationship in this.CustomerRelationshipsWhereCustomer)
            {
                this.AddSecurityToken(customerRelationship.InternalOrganisation.OwnerSecurityToken);
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.PartyName = this.Name;

            this.AppsPartyDerive(derivation);

            if (!this.ExistOwnerSecurityToken)
            {
                var securityToken = new SecurityTokenBuilder(this.Strategy.Session).Build();
                this.OwnerSecurityToken = securityToken;

                this.AddSecurityToken(this.OwnerSecurityToken);
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
                if (partyContactMechanism.UseAsDefault && partyContactMechanism.ExistContactPurpose)
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

            this.DeriveUserGroups(derivation);
            this.DeriveCurrentContacts(derivation);
        }
        
        private void AppsDeriveFullName()
        {
            this.FullName = this.Name;
        }

        private Person AppsFindCurrentContactByName(string name)
        {
            var personsFound = new List<Person>();
            name = name.ToLower();
            foreach (Person person in this.CurrentContacts)
            {
                if ((person.ExistFullName && person.FullName.ToLower() == name) || 
                    (person.ExistLastName && person.LastName.ToLower() == name) || 
                    (person.ExistFirstName &&person.FirstName.ToLower() == name))
                {
                    personsFound.Add(person);
                }
            }

            if (personsFound.Count == 1)
            {
                return personsFound[0];
            }

            return null;
        }

        private bool AppsIsActiveClient(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var clientRelationships = this.ClientRelationshipsWhereClient;
            foreach (ClientRelationship relationship in clientRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActiveCustomer(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var customerRelationships = this.CustomerRelationshipsWhereCustomer;
            foreach (CustomerRelationship relationship in customerRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActiveDistributor(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var distributorRelationships = this.DistributionChannelRelationshipsWhereDistributor;
            foreach (DistributionChannelRelationship relationship in distributorRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
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

        private bool AppsIsActiveProfessionalServicesProvider(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var professionalServicesRelationships = this.ProfessionalServicesRelationshipsWhereProfessionalServicesProvider;
            foreach (ProfessionalServicesRelationship relationship in professionalServicesRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActiveProspect(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var prospectRelationships = this.ProspectRelationshipsWhereProspect;
            foreach (ProspectRelationship relationship in prospectRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AppsIsActiveSubContractor(DateTime? date)
        {
            if (date == DateTime.MinValue)
            {
                return false;
            }

            var subContractorRelationships = this.SubContractorRelationshipsWhereSubContractor;
            foreach (SubContractorRelationship relationship in subContractorRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
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
            foreach (SupplierRelationship relationship in supplierRelationships)
            {
                if (relationship.FromDate <= date &&
                    (!relationship.ExistThroughDate || relationship.ThroughDate >= date))
                {
                    return true;
                }
            }

            return false;
        }

        private void AppsDeriveUserGroups(IDerivation derivation)
        {
            if (this.Strategy.Session.Population is IDatabase)
            {
                var customerContactGroupName = string.Format("Customer contacts at {0} ({1})", this.Name, this.UniqueId);
                var supplierContactGroupName = string.Format("Supplier contacts at {0} ({1})", this.Name, this.UniqueId);
                var partnerContactGroupName = string.Format("Partner contacts at {0} ({1})", this.Name, this.UniqueId);

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
                    this.CustomerContactUserGroup = new UserGroupBuilder(this.Strategy.Session)
                        .WithName(customerContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Strategy.Session).Customers)
                        .Build();

                    new AccessControlBuilder(this.Strategy.Session).WithRole(new Roles(this.Strategy.DatabaseSession).Customer).WithSubjectGroup(this.CustomerContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }

                if (!supplierContactGroupFound)
                {
                    this.SupplierContactUserGroup = new UserGroupBuilder(this.Strategy.Session)
                        .WithName(supplierContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Strategy.Session).Suppliers)
                        .Build();

                    new AccessControlBuilder(this.Strategy.Session).WithRole(new Roles(this.Strategy.DatabaseSession).Supplier).WithSubjectGroup(this.SupplierContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }

                if (!partnerContactGroupFound)
                {
                    this.PartnerContactUserGroup = new UserGroupBuilder(this.Strategy.Session)
                        .WithName(partnerContactGroupName)
                        .WithParty(this)
                        .WithParent(new UserGroups(this.Strategy.Session).Partners)
                        .Build();

                    new AccessControlBuilder(this.Strategy.Session).WithRole(new Roles(this.Strategy.DatabaseSession).Partner).WithSubjectGroup(this.PartnerContactUserGroup).WithObject(this.OwnerSecurityToken).Build();
                }
            }
        }

        private void AppsDeriveCurrentContacts(IDerivation derivation)
        {
            this.RemoveCurrentContacts();

            var contactRelationships = this.OrganisationContactRelationshipsWhereOrganisation;
            foreach (OrganisationContactRelationship contactRelationship in contactRelationships)
            {
                if (contactRelationship.FromDate <= DateTime.UtcNow &&
                    (!contactRelationship.ExistThroughDate || contactRelationship.ThroughDate >= DateTime.UtcNow))
                {
                    this.AddCurrentContact(contactRelationship.Contact);
                }
            }
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

        public List<string> Roles
        {
            get
            {
                var roles = new List<string>();

                if (IsActiveClient(DateTime.UtcNow.Date))
                {
                    roles.Add("Client");
                }

                if (IsActiveCustomer(DateTime.UtcNow.Date))
                {
                    roles.Add("Customer");
                }

                if (IsActiveDistributor(DateTime.UtcNow.Date))
                {
                    roles.Add("Distributor");
                }

                if (IsActivePartner(DateTime.UtcNow.Date))
                {
                    roles.Add("Partner");
                }

                if (IsActiveProfessionalServicesProvider(DateTime.UtcNow.Date))
                {
                    roles.Add("Professional Service Provider");
                }

                if (IsActiveProspect(DateTime.UtcNow.Date))
                {
                    roles.Add("Prospect");
                }

                if (IsActiveSubContractor(DateTime.UtcNow.Date))
                {
                    roles.Add("Subcontractor");
                }

                if (IsActiveSupplier(DateTime.UtcNow.Date))
                {
                    roles.Add("Supplier");
                }

                return roles;
            }            
        }
    }
}