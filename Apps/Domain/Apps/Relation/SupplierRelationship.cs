// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SupplierRelationship.cs" company="Allors bvba">
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
    

    

    public partial class SupplierRelationship
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
                this.InternalOrganisation = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;
            }

            if (!this.ExistSubAccountNumber)
            {
                this.SubAccountNumber = this.InternalOrganisation.DeriveNextSubAccountNumber();
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            if (this.ExistSupplier)
            {
                derivation.AddDerivable(this.Supplier);

                foreach (OrganisationContactRelationship contactRelationship in this.Supplier.OrganisationContactRelationshipsWhereOrganisation)
                {
                    derivation.AddDerivable(contactRelationship);
                }
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, SupplierRelationships.Meta.Supplier);
            derivation.Log.AssertExists(this, SupplierRelationships.Meta.InternalOrganisation);
            derivation.Log.AssertExists(this, SupplierRelationships.Meta.FromDate);
            derivation.Log.AssertExists(this, SupplierRelationships.Meta.SubAccountNumber);

            this.DisplayName = string.Format(
                "{0} supplier for {1}",
                this.ExistSupplier ? this.Supplier.DeriveDisplayName() : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveDisplayName() : null);

            this.DeriveMembership(derivation);
            this.DeriveInternalOrganisationSupplier(derivation);

            if (this.Session is IDatabaseSession)
            {
                var supplierRelationships = this.InternalOrganisation.SupplierRelationshipsWhereInternalOrganisation;
                supplierRelationships.Filter.AddEquals(SupplierRelationships.Meta.SubAccountNumber, this.SubAccountNumber);
                if (supplierRelationships.Count == 1)
                {
                    if (!supplierRelationships[0].Equals(this))
                    {
                        derivation.Log.AddError(new DerivationErrorUnique(derivation.Log, this, SupplierRelationships.Meta.SubAccountNumber));
                    }
                }
                else if (supplierRelationships.Count > 1)
                {
                    derivation.Log.AddError(new DerivationErrorUnique(derivation.Log, this, SupplierRelationships.Meta.SubAccountNumber));
                }
            }
        }

        private void AppsDeriveInternalOrganisationSupplier(IDerivation derivation)
        {
            if (this.ExistSupplier && this.ExistInternalOrganisation)
            {
                if (this.FromDate <= DateTime.Now && (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                {
                    if (!this.Supplier.ExistInternalOrganisationWhereSupplier)
                    {
                        this.InternalOrganisation.AddSupplier(this.Supplier);
                    }
                }

                if (this.FromDate > DateTime.Now || (this.ExistThroughDate && this.ThroughDate < DateTime.Now))
                {
                    if (this.Supplier.ExistInternalOrganisationWhereSupplier)
                    {
                        this.InternalOrganisation.RemoveSupplier(this.Supplier);
                    }
                }
            }
        }

        private void AppsDeriveMembership(IDerivation derivation)
        {
            if (this.ExistSupplier && this.ExistInternalOrganisation)
            {
                if (this.Supplier.SupplierContactUserGroup != null)
                {
                    foreach (OrganisationContactRelationship contactRelationship in this.Supplier.OrganisationContactRelationshipsWhereOrganisation)
                    {
                        if (this.FromDate <= DateTime.Now &&
                            (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                        {
                            if (!this.Supplier.SupplierContactUserGroup.ContainsMember(contactRelationship.Contact))
                            {
                                this.Supplier.SupplierContactUserGroup.AddMember(contactRelationship.Contact);
                            }
                        }
                        else
                        {
                            if (this.Supplier.SupplierContactUserGroup.ContainsMember(contactRelationship.Contact))
                            {
                                this.Supplier.SupplierContactUserGroup.RemoveMember(contactRelationship.Contact);
                            }
                        }
                    }
                }
            }
        }
    }
}