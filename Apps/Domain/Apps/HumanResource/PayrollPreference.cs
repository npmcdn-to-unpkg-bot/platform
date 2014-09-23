// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayrollPreference.cs" company="Allors bvba">
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

    public partial class PayrollPreference
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, PayrollPreferences.Meta.PaymentMethod);
            derivation.Log.AssertExists(this, PayrollPreferences.Meta.TimeFrequency);
            derivation.Log.AssertAtLeastOne(this, PayrollPreferences.Meta.Amount, PayrollPreferences.Meta.Percentage);
            derivation.Log.AssertExistsAtMostOne(this, PayrollPreferences.Meta.Amount, PayrollPreferences.Meta.Percentage);

            this.DisplayName = string.Format(
                "{0} : amount/percentage {1}{2} per {3}",
                this.ExistPaymentMethod ? this.PaymentMethod.DisplayName : null,
                this.ExistAmount ? this.Amount : 0,
                this.ExistPercentage ? this.Percentage : 0,
                this.ExistTimeFrequency ? this.TimeFrequency.Name : null);
        }
    }
}