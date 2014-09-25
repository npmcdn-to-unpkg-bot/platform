// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartSpecificationStatus.cs" company="Allors bvba">
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

    using Allors.Domain;

    public partial class PartSpecificationStatus
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistStartDateTime)
            {
                this.StartDateTime = DateTime.Now;
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PartSpecificationStatuses.Meta.StartDateTime);
            derivation.Log.AssertExists(this, PartSpecificationStatuses.Meta.PartSpecificationObjectState);

            this.DisplayName = string.Format(
                "{0} starting {1}",
                this.ExistPartSpecificationObjectState ? this.PartSpecificationObjectState.Name : null,
                this.ExistStartDateTime ? this.StartDateTime : DateTime.MinValue);
        }
    }
}