// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalOrganisationRevenue.cs" company="Allors bvba">
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

    public partial class InternalOrganisationRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;
            
            this.PartyName = this.InternalOrganisation.Name;

            this.AppsDeriveDisplayName(derivation);

            this.AppsDeriveRevenue();
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistInternalOrganisation)
            {
                uiText.Append(this.InternalOrganisation.Name);
            }

            if (this.ExistRevenue)
            {
                uiText.Append(": ");
                uiText.Append(this.Year);
                uiText.Append("/");
                uiText.Append(this.Month);
                uiText.Append(" ");
                uiText.Append(DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat));
            }

            this.DisplayName = uiText.ToString();
        }

        private void AppsDeriveRevenue()
        {
            this.Revenue = 0;

            var storeRevenues = this.InternalOrganisation.StoreRevenuesWhereInternalOrganisation;
            storeRevenues.Filter.AddEquals(StoreRevenues.Meta.Year, this.Year);
            storeRevenues.Filter.AddEquals(StoreRevenues.Meta.Month, this.Month);

            foreach (StoreRevenue storeRevenue in storeRevenues)
            {
                this.Revenue += storeRevenue.Revenue;
            }

            var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.InternalOrganisation.InternalOrganisationRevenueHistoriesWhereInternalOrganisation;
                var history = histories.First ?? new InternalOrganisationRevenueHistoryBuilder(this.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .Build();

                history.AppsDeriveMovingAverage();
            }
        }
    }
}
