// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PurchaseOrderStatus.cs" company="Allors bvba">
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
    using System.Text;

    using Allors.Domain;

    public partial class PurchaseOrderStatus
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            this.AppsEnsure();
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PurchaseOrderStatuses.Meta.StartDateTime);
            derivation.Log.AssertExists(this, PurchaseOrderStatuses.Meta.PurchaseOrderObjectState);

            this.DeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var displayName = new StringBuilder();
            if (this.ExistPurchaseOrderObjectState)
            {
                displayName.Append(this.PurchaseOrderObjectState.Name);
            }

            if (this.ExistStartDateTime)
            {
                displayName.Append(" ");
                displayName.Append(this.StartDateTime);
            }

            this.DisplayName = displayName.ToString();
        }

        private void AppsEnsure()
        {
            if (!this.ExistStartDateTime)
            {
                this.StartDateTime = DateTime.Now;
            }
        }
    }
}