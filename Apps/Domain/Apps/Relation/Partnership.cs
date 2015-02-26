// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Partnership.cs" company="Allors bvba">
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

    public partial class Partnership
    {
        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistFromDate)
            {
                this.FromDate = DateTime.Now;
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.DisplayName = string.Format(
                "{0} partner for {1}",
                this.ExistPartner ? this.Partner.DeriveDisplayName() : null,
                this.ExistInternalOrganisation ? this.InternalOrganisation.DeriveDisplayName() : null);

            this.AppsDeriveMembership();
            this.AppsDerivePartnerContacts(derivation);
        }

        private void AppsDerivePartnerContacts(IDerivation derivation)
        {
            if (this.ExistPartner)
            {
                var partner = this.Partner;
                foreach (OrganisationContactRelationship contactRelationship in partner.OrganisationContactRelationshipsWhereOrganisation)
                {
                    contactRelationship.Contact.Derive().WithDerivation(derivation).Execute();                    
                }
            }
        }

        private void AppsDeriveMembership()
        {
            if (this.ExistPartner && this.ExistInternalOrganisation)
            {
                if (this.Partner.PartnerContactUserGroup != null)
                {
                    foreach (OrganisationContactRelationship contactRelationship in this.Partner.OrganisationContactRelationshipsWhereOrganisation)
                    {
                        if (this.FromDate <= DateTime.Now &&
                            (!this.ExistThroughDate || this.ThroughDate >= DateTime.Now))
                        {
                            if (!this.Partner.PartnerContactUserGroup.ContainsMember(contactRelationship.Contact))
                            {
                                this.Partner.PartnerContactUserGroup.AddMember(contactRelationship.Contact);
                            }
                        }
                        else
                        {
                            if (this.Partner.PartnerContactUserGroup.ContainsMember(contactRelationship.Contact))
                            {
                                this.Partner.PartnerContactUserGroup.RemoveMember(contactRelationship.Contact);
                            }
                        }
                    }
                }
            }
        }
    }
}