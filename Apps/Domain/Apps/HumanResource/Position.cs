// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Position.cs" company="Allors bvba">
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
    

    public partial class Position
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

            derivation.Log.AssertExists(this, Positions.Meta.PositionType);
            derivation.Log.AssertExists(this, Positions.Meta.Organisation);
            derivation.Log.AssertExists(this, Positions.Meta.ActualFromDate);

            this.DisplayName = string.Format(
                "{0} within {1}",
                this.ExistPositionType ? this.PositionType.Description : null,
                this.ExistOrganisation ? this.Organisation.DeriveDisplayName() : null);

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistPositionType ? this.PositionType.Description : null,
                this.ExistOrganisation ? this.Organisation.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0}",
                this.ExistOrganisation ? this.Organisation.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}