// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Singleton.cs" company="Allors bvba">
//   Copyright 2002-2011 Allors bvba.
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
    /// <summary>
    /// The Application object serves as the singleton for
    /// your population.
    /// It is the ideal place to hold application settings
    /// (e.g. the domain, the guest user, ...).
    /// </summary>
    public partial class Singleton
    {
        public void AppsDerive(DerivableDerive method)
        {
            this.DisplayName = "Applications";
        }

        private void AppsDeriveRevenues()
        {
            PartyPackageRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            PartyProductCategoryRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            PartyProductRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            PartyRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            var derivation = new Derivation(this.Session);

            foreach (PartyPackageRevenue revenue in this.Session.Extent<PartyPackageRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyProductCategoryRevenue revenue in this.Session.Extent<PartyProductCategoryRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyProductRevenue revenue in this.Session.Extent<PartyProductRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyRevenue revenue in this.Session.Extent<PartyRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            CustomerRelationships.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            Parties.DeriveRevenues(this.Session);
            this.Session.Commit();

            InternalOrganisationRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            PackageRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            ProductCategoryRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            ProductRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            SalesRepPartyProductCategoryRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            SalesRepPartyRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            SalesRepProductCategoryRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            SalesRepRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            StoreRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();
            SalesChannelRevenues.AppsDeriveRevenues(this.Session);
            this.Session.Commit();

            foreach (InternalOrganisationRevenue revenue in this.Session.Extent<InternalOrganisationRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PackageRevenue revenue in this.Session.Extent<PackageRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (ProductCategoryRevenue revenue in this.Session.Extent<ProductCategoryRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (ProductRevenue revenue in this.Session.Extent<ProductRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesRepPartyProductCategoryRevenue revenue in this.Session.Extent<SalesRepPartyProductCategoryRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesRepPartyRevenue revenue in this.Session.Extent<SalesRepPartyRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesRepProductCategoryRevenue revenue in this.Session.Extent<SalesRepProductCategoryRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesRepRevenue revenue in this.Session.Extent<SalesRepRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (StoreRevenue revenue in this.Session.Extent<StoreRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesChannelRevenue revenue in this.Session.Extent<SalesChannelRevenue>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            SalesRepRelationships.DeriveCommissions(this.Session);
            this.Session.Commit();
            Persons.AppsDeriveCommissions(this.Session);
            this.Session.Commit();

            this.AppsDeriveHistories();
        }

        private void AppsDeriveHistories()
        {
            PartyPackageRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();

            PartyProductCategoryRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();

            PartyProductRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();

            PartyRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();

            var derivation = new Derivation(this.Session);

            var revenues = this.Session.Extent<PartyPackageRevenueHistory>();
            foreach (PartyPackageRevenueHistory revenue in revenues)
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyProductCategoryRevenueHistory revenue in this.Session.Extent<PartyProductCategoryRevenueHistory>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyProductRevenueHistory revenue in this.Session.Extent<PartyProductRevenueHistory>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PartyRevenueHistory revenue in this.Session.Extent<PartyRevenueHistory>())
            {
                revenue.Derive().Execute();
            }

            this.Session.Commit();

            InternalOrganisationRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            PackageRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            ProductCategoryRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            ProductRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            SalesChannelRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            SalesRepRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();
            StoreRevenueHistories.AppsDeriveHistory(this.Session);
            this.Session.Commit();

            foreach (InternalOrganisationRevenueHistory revenueHistory in this.Session.Extent<InternalOrganisationRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (PackageRevenueHistory revenueHistory in this.Session.Extent<PackageRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (ProductCategoryRevenueHistory revenueHistory in this.Session.Extent<ProductCategoryRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (ProductRevenueHistory revenueHistory in this.Session.Extent<ProductRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesChannelRevenueHistory revenueHistory in this.Session.Extent<SalesChannelRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (SalesRepRevenueHistory revenueHistory in this.Session.Extent<SalesRepRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();

            foreach (StoreRevenueHistory revenueHistory in this.Session.Extent<StoreRevenueHistory>())
            {
                revenueHistory.Derive().Execute();
            }

            this.Session.Commit();
        }
    }
}
