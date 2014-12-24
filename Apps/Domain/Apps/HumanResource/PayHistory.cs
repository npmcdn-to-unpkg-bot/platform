// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayHistory.cs" company="Allors bvba">
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

    public partial class PayHistory
    {
        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;   

            derivation.Log.AssertAtLeastOne(this, PayHistories.Meta.Amount, PayHistories.Meta.SalaryStep);
            derivation.Log.AssertExistsAtMostOne(this, PayHistories.Meta.Amount, PayHistories.Meta.SalaryStep);

            this.DisplayName = string.Format(
                "{0} employee at {1} : {2} through {3}, amount/step {4} {5} {6} per {7}",
                this.ExistEmployment ? this.Employment.ExistEmployee ? this.Employment.Employee.DeriveDisplayName() : null : null,
                this.ExistEmployment ? this.Employment.ExistEmployer ? this.Employment.Employer.DeriveDisplayName() : null : null,
                this.ExistFromDate ? this.FromDate : DateTime.MinValue,
                this.ExistThroughDate ? this.ThroughDate : DateTime.MaxValue,
                this.ExistAmount ? this.Amount : 0,
                this.ExistSalaryStep ? this.SalaryStep.ExistModifiedDate ? this.SalaryStep.ModifiedDate : DateTime.MinValue : DateTime.MinValue,
                this.ExistSalaryStep ? this.SalaryStep.ExistAmount ? this.SalaryStep.Amount : 0 : 0,
                this.ExistTimeFrequency ? this.TimeFrequency.Name : null);
        }
    }
}
