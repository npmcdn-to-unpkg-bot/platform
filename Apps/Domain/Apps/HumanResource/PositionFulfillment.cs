// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionFulfillment.cs" company="Allors bvba">
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

    public partial class PositionFulfillment
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PositionFulfillments.Meta.Person);
            derivation.Log.AssertExists(this, PositionFulfillments.Meta.Position);

            this.DisplayName = string.Format(
                "{0} within {1} fulfilled by {2}",
                this.ExistPosition ? this.Position.ExistPositionType ? this.Position.PositionType.Description : null : null,
                this.ExistPosition ? this.Position.ExistOrganisation ? this.Position.Organisation.DeriveDisplayName() : null : null,
                this.ExistPerson ? this.Person.DeriveDisplayName() : null);
        }
    }
}