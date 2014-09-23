// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganisationContactRelationship.cs" company="Allors bvba">
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

    using Allors.Domain;

    public partial class OrganisationContactRelationship
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistFromDate)
            {
                this.FromDate = DateTime.Now;
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, OrganisationContactRelationships.Meta.Contact);
            derivation.Log.AssertExists(this, OrganisationContactRelationships.Meta.Organisation);
            derivation.Log.AssertExists(this, OrganisationContactRelationships.Meta.FromDate);

            this.DisplayName = string.Format(
                "{0} contact for {1}",
                this.ExistContact ? this.Contact.DeriveDisplayName() : null,
                this.ExistOrganisation ? this.Organisation.DeriveDisplayName() : null);

            this.DeriveCustomerContactMemberShip(derivation);
            this.DeriveSupplierContactMemberShip(derivation);
            this.DerivePartnerContactMemberShip(derivation);

            ////Before deriving this.Contact
            if (this.ExistOrganisation)
            {
                this.Organisation.DeriveCurrentContacts(derivation);
            }

            ////After deriving this.Organisation
            if (this.ExistContact)
            {
                this.Contact.Derive(derivation);
            }
        }

        private void AppsDeriveCustomerContactMemberShip(IDerivation derivation)
        {
            if (this.ExistContact && this.ExistOrganisation && this.Organisation.ExistCustomerContactUserGroup)
            {
                if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                {
                    if (this.Organisation.IsActiveCustomer(this.FromDate))
                    {
                        if (!this.Organisation.CustomerContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.CustomerContactUserGroup.AddMember(this.Contact);
                        }
                    }
                    else
                    {
                        if (this.Organisation.CustomerContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.CustomerContactUserGroup.RemoveMember(this.Contact);
                        }
                    }
                }
                else
                {
                    if (this.Organisation.CustomerContactUserGroup.ContainsMember(this.Contact))
                    {
                        this.Organisation.CustomerContactUserGroup.RemoveMember(this.Contact);
                    }
                }                
            }
        }

        private void AppsDeriveSupplierContactMemberShip(IDerivation derivation)
        {
            if (this.ExistContact && this.ExistOrganisation && this.Organisation.ExistSupplierContactUserGroup)
            {
                if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                {
                    if (this.Organisation.IsActiveSupplier(this.FromDate))
                    {
                        if (!this.Organisation.SupplierContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.SupplierContactUserGroup.AddMember(this.Contact);
                        }
                    }
                    else
                    {
                        if (this.Organisation.SupplierContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.SupplierContactUserGroup.RemoveMember(this.Contact);
                        }
                    }
                }
                else
                {
                    if (this.Organisation.SupplierContactUserGroup.ContainsMember(this.Contact))
                    {
                        this.Organisation.SupplierContactUserGroup.RemoveMember(this.Contact);
                    }
                }   
            }                
        }

        private void AppsDerivePartnerContactMemberShip(IDerivation derivation)
        {
            if (this.ExistContact && this.ExistOrganisation && this.Organisation.ExistPartnerContactUserGroup)
            {
                if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                {
                    if (this.Organisation.IsActivePartner(this.FromDate))
                    {
                        if (!this.Organisation.PartnerContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.PartnerContactUserGroup.AddMember(this.Contact);
                        }
                    }
                    else
                    {
                        if (this.Organisation.PartnerContactUserGroup.ContainsMember(this.Contact))
                        {
                            this.Organisation.PartnerContactUserGroup.RemoveMember(this.Contact);
                        }
                    }
                }
                else
                {
                    if (this.Organisation.PartnerContactUserGroup.ContainsMember(this.Contact))
                    {
                        this.Organisation.PartnerContactUserGroup.RemoveMember(this.Contact);
                    }
                }                
            }
        }
    }
}