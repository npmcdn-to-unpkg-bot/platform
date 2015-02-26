// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayCheck.cs" company="Allors bvba">
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

    public partial class PayCheck
    {
        protected string AppsComposeDisplayName()
        {
            return string.Format(
                "{0}: {1} - {2}",
                this.ExistEmployment ? this.Employment.DisplayName : null,
                this.ExistEffectiveDate ? this.EffectiveDate : DateTime.MinValue,
                this.ExistAmount ? this.Amount : 0);
        }

        protected string AppsComposeSearchDataCharacterBoundaryText()
        {
            return null;
        }

        protected string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0} {1}",
                this.ExistEffectiveDate ? this.EffectiveDate : DateTime.MinValue,
                this.ExistAmount ? this.Amount : 0);
        }

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

        private void AppsSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }
    }
}