// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmploymentApplication.cs" company="Allors bvba">
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

    public partial class EmploymentApplication
    {
        public void AppsDerive(DerivableDerive method)
        {
            this.DisplayName = string.Format(
                "{0} {1} {2} within {3}",
                this.ExistApplicationDate ? this.ApplicationDate : DateTime.MinValue,
                this.ExistPerson ? this.Person.DeriveDisplayName() : null,
                this.ExistPosition ? this.Position.ExistPositionType ? this.Position.PositionType.Description : null : null,
                this.ExistPosition ? this.Position.ExistOrganisation ? this.Position.Organisation.DeriveDisplayName() : null : null);

            var characterBoundaryText = this.ExistPerson ? this.Person.DeriveSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistApplicationDate ? this.ApplicationDate : DateTime.MinValue,
                this.ExistPerson ? this.Person.DeriveSearchDataWordBoundaryText() : null,
                this.ExistPosition ? this.Position.ExistPositionType ? this.Position.PositionType.Description : null : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}
