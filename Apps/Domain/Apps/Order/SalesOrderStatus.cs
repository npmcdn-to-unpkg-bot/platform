// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesOrderStatus.cs" company="Allors bvba">
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

    public partial class SalesOrderStatus
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            this.AppsEnsure();
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, SalesOrderStatuses.Meta.StartDateTime);
            derivation.Log.AssertExists(this, SalesOrderStatuses.Meta.SalesOrderObjectState);

            this.DeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var displayName = new StringBuilder();
            if (this.ExistSalesOrderObjectState)
            {
                displayName.Append(this.SalesOrderObjectState.Name);
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