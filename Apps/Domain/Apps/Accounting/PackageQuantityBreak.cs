// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageQuantityBreak.cs" company="Allors bvba">
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

    public partial class PackageQuantityBreak
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertAtLeastOne(this, PackageQuantityBreaks.Meta.From, PackageQuantityBreaks.Meta.Through);

            this.DisplayName = string.Format(
                "Package quantity: from {0} through {1}",
                this.ExistFrom ? this.From : 0,
                this.ExistThrough ? this.Through : 0);
        }
    }
}