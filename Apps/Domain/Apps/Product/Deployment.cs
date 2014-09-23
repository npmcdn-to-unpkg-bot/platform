// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Deployment.cs" company="Allors bvba">
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
    using Allors.Domain;

    

    public partial class Deployment
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            this.DisplayName = string.Format(
                "{0} deployed with product offering {1}",
                this.ExistSerializedInventoryItem ? this.SerializedInventoryItem.ComposeDisplayName() : null,
                this.ExistProductOffering ? this.ProductOffering.ComposeDisplayName() : null);

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistSerializedInventoryItem ? this.SerializedInventoryItem.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistProductOffering ? this.ProductOffering.ComposeSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistSerializedInventoryItem ? this.SerializedInventoryItem.ComposeSearchDataWordBoundaryText() : null,
                this.ExistProductOffering ? this.ProductOffering.ComposeSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}