// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionReportingStructure.cs" company="Allors bvba">
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

    public partial class PositionReportingStructure
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PositionReportingStructures.Meta.Position);
            derivation.Log.AssertExists(this, PositionReportingStructures.Meta.ManagedByPosition);

            this.DisplayName = string.Format(
                "{0} within {1} reports to {2} within {3}",
                this.ExistPosition ? this.Position.ExistPositionType ? this.Position.PositionType.Description : null : null,
                this.ExistPosition ? this.Position.ExistOrganisation ? this.Position.Organisation.DeriveDisplayName() : null : null,
                this.ExistManagedByPosition ? this.ManagedByPosition.ExistPositionType ? this.ManagedByPosition.PositionType.Description : null : null,
                this.ExistManagedByPosition ? this.ManagedByPosition.ExistOrganisation ? this.ManagedByPosition.Organisation.DeriveDisplayName() : null : null);
        }
    }
}