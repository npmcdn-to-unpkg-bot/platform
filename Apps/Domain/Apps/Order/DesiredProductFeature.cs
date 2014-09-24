// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesiredProductFeature.cs" company="Allors bvba">
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
    

    public partial class DesiredProductFeature
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
            

            derivation.Log.AssertExists(this, DesiredProductFeatures.Meta.Required);
            derivation.Log.AssertExists(this, DesiredProductFeatures.Meta.ProductFeature);

            this.DisplayName = this.ExistProductFeature ? this.ProductFeature.ComposeDisplayName() : null;

            var characterBoundaryText = this.ExistProductFeature ? this.ProductFeature.ComposeSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = this.ExistProductFeature ? this.ProductFeature.ComposeSearchDataWordBoundaryText() : null;

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}