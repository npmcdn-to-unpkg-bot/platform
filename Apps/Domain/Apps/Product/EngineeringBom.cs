// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineeringBom.cs" company="Allors bvba">
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
    public partial class EngineeringBom
    {
        public void AppsDerive(ObjectDerive method)
        {
            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        private string AppsComposeDisplayName()
        {
            return string.Format(
                "{0} made up of {1} {2}",
                this.ExistComponentPart ? this.ComponentPart.ComposeDisplayName() : null,
                this.ExistQuantityUsed ? this.QuantityUsed : 0,
                this.ExistPart ? this.Part.ComposeDisplayName() : null);
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return string.Format(
                "{0}{1}",
                this.ExistComponentPart ? this.ComponentPart.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistPart ? this.Part.ComposeSearchDataCharacterBoundaryText() : null);
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0}{1}",
                this.ExistComponentPart ? this.ComponentPart.ComposeSearchDataWordBoundaryText() : null,
                this.ExistPart ? this.Part.ComposeSearchDataWordBoundaryText() : null);
        }
    }
}