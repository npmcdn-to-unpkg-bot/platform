// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Locale.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
    using System.Globalization;

    public partial class Locale
    {
        public bool ExistCultureInfo
        {
            get
            {
                return this.ExistName;
            }
        }

        public CultureInfo CultureInfo
        {
            get
            {
                return this.ExistName ? new CultureInfo(this.Name) : null;
            }
        }

        protected override void CoreDerive(IDerivation derivation)
        {
            base.CoreDerive(derivation);

            derivation.Log.AssertExists(this, Locales.Meta.Language);
            derivation.Log.AssertExists(this, Locales.Meta.Country);

            if (!this.ExistName && this.ExistLanguage && this.ExistCountry)
            {
                this.Name = this.Language.IsoCode + "-" + this.Country.IsoCode;
            }

            this.DisplayName = this.Name;
        }
    }
}