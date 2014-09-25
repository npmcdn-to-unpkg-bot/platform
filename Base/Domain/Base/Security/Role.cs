//-------------------------------------------------------------------------------------------------
// <copyright file="role.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the role type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class Role
    {
        protected override void BaseDerive(IDerivation derivation)
        {
            base.BaseDerive(derivation);

            derivation.Log.AssertExists(this, Meta.Name);
            derivation.Log.AssertIsUnique(this, Meta.Name);

            derivation.Log.AssertExists(this, Meta.UniqueId);
            derivation.Log.AssertIsUnique(this, Meta.UniqueId);

            this.DeriveDisplayName();

            SecurityCache.Invalidate();
        }

        private void BaseDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private string BaseComposeDisplayName()
        {
            var name = string.Empty;

            if (ExistName)
            {
                if (this.ExistPermissions)
                {
                    name = this.Name + string.Format(" with {0} members", this.Permissions.Count);
                }
                else
                {
                    name = this.Name;
                }
            }
            else
            {
                name = "Unnamed";
            }

            return name;
        }
    }
}