// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomatedAgent.cs" company="Allors bvba">
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

using System.Collections.Generic;

namespace Allors.Domain
{
    public partial class AutomatedAgent
    {
        public bool IsPerson 
        {
            get
            {
                return false;
            }
        }

        public bool IsOrganisation {
            get
            {
                return false;
            }
        }

        public List<string> Roles { get; private set; }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            this.AppsPartyDerive(derivation);

            this.DisplayName = this.DeriveDisplayName();
            this.SearchData.CharacterBoundaryText = this.DeriveSearchDataCharacterBoundaryText();
            this.SearchData.WordBoundaryText = this.DeriveSearchDataWordBoundaryText();
        }

        private string AppsDeriveDisplayName()
        {
            return this.Description;
        }

        private string AppsDeriveSearchDataCharacterBoundaryText()
        {
            return this.Description;
        }

        private string AppsDeriveSearchDataWordBoundaryText()
        {
            return null;
        }
    }
}