// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerRelationship.v.cs" company="Allors bvba">
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
    public partial class CustomerRelationship
    {
        public void DeriveMembership(IDerivation derivation)
        {
            this.AppsDeriveMembership(derivation);
        }

        public void DeriveInternalOrganisationCustomer(IDerivation derivation)
        {
            this.AppsDeriveInternalOrganisationCustomer(derivation);
        }

        public void DeriveAmountDue(IDerivation derivation)
        {
            this.AppsDeriveAmountDue(derivation);
        }

        public void DeriveAmountOverDue(IDerivation derivation)
        {
            this.AppsDeriveAmountOverDue(derivation);
        }

        public void DeriveRevenue(IDerivation derivation)
        {
            this.AppsDeriveRevenue(derivation);
        }

    }
}