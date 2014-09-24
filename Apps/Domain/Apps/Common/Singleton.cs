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
    using Allors.Domain;

    

    /// <summary>
    /// The Application object serves as the singleton for
    /// your population.
    /// It is the ideal place to hold application settings
    /// (e.g. the domain, the guest user, ...).
    /// </summary>
    public partial class Singleton
    {
        protected override void AppsDerive(IDerivation derivation)
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

            var revenues = this.Session.Extent<PartyPackageRevenue>();
            foreach (PartyPackageRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyProductCategoryRevenue>();
            foreach (PartyProductCategoryRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyProductRevenue>();
            foreach (PartyProductRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyRevenue>();
            foreach (PartyRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
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

            revenues = this.Session.Extent<InternalOrganisationRevenue>();
            foreach (InternalOrganisationRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PackageRevenue>();
            foreach (PackageRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<ProductCategoryRevenue>();
            foreach (ProductCategoryRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<ProductRevenue>();
            foreach (ProductRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<SalesRepPartyProductCategoryRevenue>();
            foreach (SalesRepPartyProductCategoryRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<SalesRepPartyRevenue>();
            foreach (SalesRepPartyRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<SalesRepProductCategoryRevenue>();
            foreach (SalesRepProductCategoryRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<SalesRepRevenue>();
            foreach (SalesRepRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<StoreRevenue>();
            foreach (StoreRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<SalesChannelRevenue>();
            foreach (SalesChannelRevenue revenue in revenues)
            {
                revenue.Derive(derivation);
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
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyProductCategoryRevenueHistory>();
            foreach (PartyProductCategoryRevenueHistory revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyProductRevenueHistory>();
            foreach (PartyProductRevenueHistory revenue in revenues)
            {
                revenue.Derive(derivation);
            }

            this.Session.Commit();

            revenues = this.Session.Extent<PartyRevenueHistory>();
            foreach (PartyRevenueHistory revenue in revenues)
            {
                revenue.Derive(derivation);
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

            var revenueHistories = this.Session.Extent<InternalOrganisationRevenueHistory>();
            foreach (InternalOrganisationRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<PackageRevenueHistory>();
            foreach (PackageRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<ProductCategoryRevenueHistory>();
            foreach (ProductCategoryRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<ProductRevenueHistory>();
            foreach (ProductRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<SalesChannelRevenueHistory>();
            foreach (SalesChannelRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<SalesRepRevenueHistory>();
            foreach (SalesRepRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();

            revenueHistories = this.Session.Extent<StoreRevenueHistory>();
            foreach (StoreRevenueHistory revenueHistory in revenueHistories)
            {
                revenueHistory.Derive(derivation);
            }

            this.Session.Commit();
        }
    }
}
