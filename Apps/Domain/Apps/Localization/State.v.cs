// --------------------------------------------------------------------------------------------------------------------
// <copyright file="State.v.cs" company="Allors bvba">
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
    public partial class State
    {
        public override void DeriveDisplayName()
        {
            this.AppsDeriveDisplayName();
        }

        public override void DeriveSearchDataCharacterBoundaryText()
        {
            this.AppsDeriveSearchDataCharacterBoundaryText();
        }

        public override void DeriveSearchDataWordBoundaryText()
        {
            this.AppsDeriveSearchDataWordBoundaryText();
        }

        public override string ComposeDisplayName()
        {
            return this.AppsComposeDisplayName();
        }

        public override string ComposeSearchDataCharacterBoundaryText()
        {
            return this.AppsComposeSearchDataCharacterBoundaryText();
        }

        public override string ComposeSearchDataWordBoundaryText()
        {
            return this.AppsComposeSearchDataWordBoundaryText();
        }
    }
}