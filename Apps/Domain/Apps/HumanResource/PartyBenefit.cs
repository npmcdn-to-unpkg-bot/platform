// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartyBenefit.cs" company="Allors bvba">
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

    public partial class PartyBenefit
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PartyBenefits.Meta.Employment);
            derivation.Log.AssertExists(this, PartyBenefits.Meta.Benefit);

            this.DisplayName = string.Format(
                "{0} employee at {1} benefit {2}",
                this.ExistEmployment ? this.Employment.ExistEmployee ? this.Employment.Employee.DeriveDisplayName() : null : null,
                this.ExistEmployment ? this.Employment.ExistEmployer ? this.Employment.Employer.DeriveDisplayName() : null : null,
                this.ExistBenefit ? this.Benefit.Name : null);
        }
    }
}