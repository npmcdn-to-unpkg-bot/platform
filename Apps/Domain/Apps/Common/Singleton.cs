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
        private void AppsDeriveRevenues()
        {
            PartyPackageRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyProductCategoryRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyProductRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            var derivation = new Derivation(this.Strategy.Session);

            foreach (PartyPackageRevenue revenue in this.Strategy.Session.Extent<PartyPackageRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyProductCategoryRevenue revenue in this.Strategy.Session.Extent<PartyProductCategoryRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyProductRevenue revenue in this.Strategy.Session.Extent<PartyProductRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyRevenue revenue in this.Strategy.Session.Extent<PartyRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            CustomerRelationships.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            Parties.DeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            InternalOrganisationRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            PackageRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            ProductCategoryRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            ProductRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesRepPartyProductCategoryRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesRepPartyRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesRepProductCategoryRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesRepRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            StoreRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesChannelRevenues.AppsDeriveRevenues(this.Strategy.Session);
            this.Strategy.Session.Commit();

            foreach (InternalOrganisationRevenue revenue in this.Strategy.Session.Extent<InternalOrganisationRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PackageRevenue revenue in this.Strategy.Session.Extent<PackageRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (ProductCategoryRevenue revenue in this.Strategy.Session.Extent<ProductCategoryRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (ProductRevenue revenue in this.Strategy.Session.Extent<ProductRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesRepPartyProductCategoryRevenue revenue in this.Strategy.Session.Extent<SalesRepPartyProductCategoryRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesRepPartyRevenue revenue in this.Strategy.Session.Extent<SalesRepPartyRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesRepProductCategoryRevenue revenue in this.Strategy.Session.Extent<SalesRepProductCategoryRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesRepRevenue revenue in this.Strategy.Session.Extent<SalesRepRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (StoreRevenue revenue in this.Strategy.Session.Extent<StoreRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesChannelRevenue revenue in this.Strategy.Session.Extent<SalesChannelRevenue>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            SalesRepRelationships.DeriveCommissions(this.Strategy.Session);
            this.Strategy.Session.Commit();
            Persons.AppsDeriveCommissions(this.Strategy.Session);
            this.Strategy.Session.Commit();

            this.AppsDeriveHistories();
        }

        private void AppsDeriveHistories()
        {
            PartyPackageRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyProductCategoryRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyProductRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();

            PartyRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();

            var derivation = new Derivation(this.Strategy.Session);

            var revenues = this.Strategy.Session.Extent<PartyPackageRevenueHistory>();
            foreach (PartyPackageRevenueHistory revenue in revenues)
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyProductCategoryRevenueHistory revenue in this.Strategy.Session.Extent<PartyProductCategoryRevenueHistory>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyProductRevenueHistory revenue in this.Strategy.Session.Extent<PartyProductRevenueHistory>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PartyRevenueHistory revenue in this.Strategy.Session.Extent<PartyRevenueHistory>())
            {
                revenue.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            InternalOrganisationRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            PackageRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            ProductCategoryRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            ProductRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesChannelRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            SalesRepRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();
            StoreRevenueHistories.AppsDeriveHistory(this.Strategy.Session);
            this.Strategy.Session.Commit();

            foreach (InternalOrganisationRevenueHistory revenueHistory in this.Strategy.Session.Extent<InternalOrganisationRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (PackageRevenueHistory revenueHistory in this.Strategy.Session.Extent<PackageRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (ProductCategoryRevenueHistory revenueHistory in this.Strategy.Session.Extent<ProductCategoryRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (ProductRevenueHistory revenueHistory in this.Strategy.Session.Extent<ProductRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesChannelRevenueHistory revenueHistory in this.Strategy.Session.Extent<SalesChannelRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (SalesRepRevenueHistory revenueHistory in this.Strategy.Session.Extent<SalesRepRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();

            foreach (StoreRevenueHistory revenueHistory in this.Strategy.Session.Extent<StoreRevenueHistory>())
            {
                revenueHistory.Derive().WithDerivation(derivation).Execute();
            }

            this.Strategy.Session.Commit();
        }
    }
}
