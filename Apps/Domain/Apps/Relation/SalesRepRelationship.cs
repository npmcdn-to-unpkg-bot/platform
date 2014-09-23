// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesRepRelationship.cs" company="Allors bvba">
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
    using System.Text;

    
    using Allors.Domain;

    

    public partial class SalesRepRelationship
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistFromDate)
            {
                this.FromDate = DateTime.Now;
            }

            if (!this.ExistInternalOrganisation)
            {
                this.InternalOrganisation = Singleton.Instance(this.Session).DefaultInternalOrganisation;
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, SalesRepRelationships.Meta.Customer);
            derivation.Log.AssertExists(this, SalesRepRelationships.Meta.InternalOrganisation);
            derivation.Log.AssertExists(this, SalesRepRelationships.Meta.SalesRepresentative);
            derivation.Log.AssertExists(this, SalesRepRelationships.Meta.FromDate);

            this.DeriveDisplayName();

            this.DeriveMembership();
            this.DeriveCustomerContacts();

            if (this.ExistCustomer && this.ExistSalesRepresentative)
            {
                this.Customer.DeriveCurrentSalesReps(derivation);
                this.SalesRepresentative.Derive(derivation);
            }
        }

        private void AppsDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            if (this.ExistFromDate)
            {
                uiText.Append("from ");
                uiText.Append(this.FromDate.ToShortDateString());
            }

            if (this.ExistThroughDate)
            {
                uiText.Append(" thru ");
                uiText.Append(this.ThroughDate.ToShortDateString());
            }

            if (this.ExistSalesRepresentative)
            {
                uiText.Append(" ");
                uiText.Append(this.SalesRepresentative.DeriveDisplayName());
            }

            if (this.ExistCustomer)
            {
                uiText.Append(" sales rep. for ");
                uiText.Append(this.Customer.DeriveDisplayName());
            }

            this.DisplayName = uiText.ToString();
        }

        private void AppsCustomerContacts()
        {
            if (this.ExistSalesRepresentative && this.SalesRepresentative.ExistCurrentEmployment && this.ExistCustomer)
            {
                var customer = this.Customer as Organisation;
                if (customer != null)
                {
                    foreach (OrganisationContactRelationship contactRelationship in customer.OrganisationContactRelationshipsWhereOrganisation)
                    {
                        contactRelationship.Contact.ApplySecurityOnDerive();

                        foreach (CustomerRelationship customerRelationship in contactRelationship.Organisation.CustomerRelationshipsWhereCustomer)
                        {
                            this.AddSecurityToken(customerRelationship.InternalOrganisation.OwnerSecurityToken);
                        }
                    }
                }
            }
        }

        private void AppsDeriveMembership()
        {
            var usergroups = this.InternalOrganisation.UserGroupsWhereParty;
            usergroups.Filter.AddEquals(UserGroups.Meta.Parent, new Roles(this.Session).Sales.UserGroupWhereRole);
            var salesRepUserGroup = usergroups.First;

            if (this.ExistSalesRepresentative && this.SalesRepresentative.ExistCurrentEmployment
                && this.ExistInternalOrganisation)
            {
                if (salesRepUserGroup != null)
                {
                    if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                    {
                        if (!salesRepUserGroup.ContainsMember(this.SalesRepresentative))
                        {
                            salesRepUserGroup.AddMember(this.SalesRepresentative);
                        }
                    }
                    else
                    {
                        if (salesRepUserGroup.ContainsMember(this.SalesRepresentative))
                        {
                            salesRepUserGroup.RemoveMember(this.SalesRepresentative);
                        }
                    }
                }
            }
            else if (this.ExistSalesRepresentative && this.ExistInternalOrganisation)
            {
                salesRepUserGroup.RemoveMember(this.SalesRepresentative);
            }
        }

        private void AppsDeriveCommission()
        {
            this.YTDCommission = 0;
            this.LastYearsCommission = 0;

            foreach (SalesRepCommission salesRepCommission in this.SalesRepresentative.SalesRepCommissionsWhereSalesRep)
            {
                if (salesRepCommission.InternalOrganisation.Equals(this.InternalOrganisation))
                {
                    if (salesRepCommission.Year == DateTime.Now.Year)
                    {
                        this.YTDCommission += salesRepCommission.Year;
                    }

                    if (salesRepCommission.Year == DateTime.Now.AddYears(-1).Year)
                    {
                        this.LastYearsCommission += salesRepCommission.Year;
                    }
                }
            }
        }
    }
}