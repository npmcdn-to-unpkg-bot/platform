// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManufacturerSuggestedRetailPrice.cs" company="Allors bvba">
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
    
    

    public partial class ManufacturerSuggestedRetailPrice
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSpecifiedFor)
            {
                this.SpecifiedFor = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PriceComponents.Meta.SpecifiedFor);
            derivation.Log.AssertExists(this, PriceComponents.Meta.FromDate);
            derivation.Log.AssertExists(this, PriceComponents.Meta.Price);

            derivation.Log.AssertExists(this, PriceComponents.Meta.Description);

            this.DisplayName = this.Description;

            this.SearchData.CharacterBoundaryText = this.DisplayName;
            this.SearchData.RemoveWordBoundaryText();
        }
    }
}