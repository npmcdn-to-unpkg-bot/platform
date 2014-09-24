// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bank.cs" company="Allors bvba">
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
    using System.Text.RegularExpressions;

    using Allors.Domain;

    using Resources;

    public partial class Bank
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, Banks.Meta.Name);
            derivation.Log.AssertExists(this, Banks.Meta.Country);
            derivation.Log.AssertExists(this, Banks.Meta.Bic);

            if (this.ExistBic)
            {
                if (!Regex.IsMatch(this.Bic, "^([a-zA-Z]){4}([a-zA-Z]){2}([0-9a-zA-Z]){2}([0-9a-zA-Z]{3})?$"))
                {
                    derivation.Log.AddError(this, Banks.Meta.Bic, ErrorMessages.NotAValidBic);
                }

                var country = new Countries(this.Session).FindBy(Countries.Meta.IsoCode, this.Bic.Substring(4, 2));
                if (country == null)
                {
                    derivation.Log.AddError(this, Banks.Meta.Bic, ErrorMessages.NotAValidBic);
                }
            }

            this.DisplayName = this.Name;

            this.SearchData.CharacterBoundaryText = this.DisplayName;
            this.SearchData.RemoveWordBoundaryText();
        }
    }
}