// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalaryStep.cs" company="Allors bvba">
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

    public partial class SalaryStep
    {
        public void AppsDerive(ObjectDerive method)
        {
            this.DisplayName = string.Format(
                "{0} {1}",
                this.ExistModifiedDate ? this.ModifiedDate : DateTime.MinValue,
                this.ExistAmount ? this.Amount : 0);
        }
    }
}
