// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemVariance.cs" company="Allors bvba">
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
    public partial class InventoryItemVariance
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            this.DisplayName = string.Format(
                "{0} quantity {1}",
                this.ExistReason ? this.Reason.DisplayName : null,
                this.ExistQuantity ? this.Quantity : 0);

            var wordBoundaryText = this.ExistReason ? this.Reason.Name : null;

            this.SearchData.CharacterBoundaryText = null;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}