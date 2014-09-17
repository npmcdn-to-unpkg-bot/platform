// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Languages.cs" company="Allors bvba">
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

    public partial class Languages
    {
        private Cache<string, Language> languageByCode;

        public Cache<string, Language> LanguageByCode
        {
            get
            {
                return this.languageByCode ?? (this.languageByCode = new Cache<string, Language>(this.Session, Meta.IsoCode));
            }
        }

        protected override void CoreSecure(Security config)
        {
            base.CoreSecure(config);

            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}