// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceReview.cs" company="Allors bvba">
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

    public partial class PerformanceReview
    {
        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;   

            this.DisplayName = string.Format(
                "{0} - {1}",
                this.ExistFromDate ? this.FromDate : DateTime.MinValue,
                this.ExistEmployee ? this.Employee.DeriveDisplayName() : null);

            var characterBoundaryText = this.ExistEmployee ? this.Employee.DeriveSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistFromDate ? this.FromDate : DateTime.MinValue,
                this.ExistEmployee ? this.Employee.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}