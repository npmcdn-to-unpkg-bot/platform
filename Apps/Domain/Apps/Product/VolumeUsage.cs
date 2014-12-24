// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VolumeUsage.cs" company="Allors bvba">
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
    public partial class VolumeUsage
    {
        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, VolumeUsages.Meta.Quantity);
            derivation.Log.AssertExists(this, VolumeUsages.Meta.UnitOfMeasure);
            derivation.Log.AssertExists(this, VolumeUsages.Meta.TimeFrequency);

            this.DisplayName = string.Format(
                "{0} {1} per {2}",
                this.ExistQuantity ? this.Quantity : 0,
                this.ExistUnitOfMeasure ? this.UnitOfMeasure.Name : null,
                this.ExistTimeFrequency ? this.TimeFrequency.Name : null);
        }
    }
}