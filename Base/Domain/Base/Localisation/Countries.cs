// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Countries.cs" company="Allors bvba">
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
    using Allors;

    public partial class Countries
    {
        private Cache<string, Country> countryByIsoCode;

        public Cache<string, Country> CountryByIsoCode
        {
            get
            {
                return this.countryByIsoCode
                       ?? (this.countryByIsoCode = new Cache<string, Country>(this.Session, Countries.Meta.IsoCode));
            }
        }

        public static Extent<Country> ExtentByCode(ISession session)
        {
            return session.Extent(Countries.Meta.ObjectType).AddSort(Countries.Meta.IsoCode);
        }

        protected override void BaseSecure(Security config)
        {
            base.BaseSecure(config);

            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}