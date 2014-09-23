// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.v.cs" company="Allors bvba">
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
    public partial class Person
    {
        public override void Delete()
        {
            this.AppsDelete();
            this.AppsDelete();

            base.Delete();
        }

        public void DeriveCommission()
        {
            this.AppsDeriveCommission();
        }

        public override string DeriveSearchDataCharacterBoundaryText()
        {
            return this.AppsDeriveSearchDataCharacterBoundaryText();
        }

        public override string DeriveSearchDataWordBoundaryText()
        {
            return this.AppsDeriveSearchDataWordBoundaryText();
        }

        public override string DeriveDisplayName()
        {
            return this.AppsDeriveDisplayName();
        }

        private string DeriveUserId()
        {
            return this.CoreDeriveUserId();
        }

        private void DeriveCurrentEmployment(IDerivation derivation)
        {
            this.AppsDeriveCurrentEmployment(derivation);
        }

        private void BuildOwnerSecurityToken()
        {
            this.AppsBuildOwnerSecurityToken();
        }
    }
}