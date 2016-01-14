// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumeration.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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
    using System.Linq;

    public static partial class EnumerationExtensions
    {
        public static string GetLocalisedName(this Enumeration enumeration, Locale locale)
        {
            var localisedName = enumeration.LocalisedNames.FirstOrDefault(localizedText => localizedText.Locale.Equals(locale));
            return localisedName != null ? localisedName.Text : enumeration.Name;
        }

        public static void SetLocalisedName(this Enumeration enumeration, Locale locale, string name)
        {
            var localisedName = enumeration.LocalisedNames.FirstOrDefault(localizedText => localizedText.Locale.Equals(locale));
            if (localisedName == null)
            {
                localisedName = new LocalisedTextBuilder(enumeration.Strategy.Session).WithText(name).WithLocale(locale).Build();
                enumeration.AddLocalisedName(localisedName);
            }
            else
            {
                localisedName.Text = name;
            }
        }
    }
}