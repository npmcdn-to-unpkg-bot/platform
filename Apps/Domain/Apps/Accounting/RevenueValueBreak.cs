// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RevenueValueBreak.cs" company="Allors bvba">
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
    public partial class RevenueValueBreak
    {
        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, RevenueValueBreaks.Meta.FromAmount, RevenueValueBreaks.Meta.ThroughAmount);

            this.DisplayName = string.Format(
                "from {0} through {1}",
                this.ExistFromAmount ? this.FromAmount : 0,
                this.ExistThroughAmount ? this.ThroughAmount : 0);
        }
    }
}