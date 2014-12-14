//------------------------------------------------------------------------------------------------- 
// <copyreight file="Repository.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    public static partial class Repository
    {
        public static void BasePostInit(MetaPopulation meta)
        {
            // Counter
            AccessControlRole.RoleType.IsRequired = true;
            AccessControlObject.RoleType.IsRequired = true;

            // Counter
            CounterValue.RoleType.IsRequired = true;

            // Country
            CountryIsoCode.RoleType.IsRequired = true;
            CountryName.RoleType.IsRequired = true;

            // Currency
            CurrencyIsoCode.RoleType.IsRequired = true;
            CurrencyName.RoleType.IsRequired = true;
            CurrencySymbol.RoleType.IsRequired = true;

            // Enumeration
            EnumerationName.RoleType.IsRequired = true;
            EnumerationIsActive.RoleType.IsRequired = true;
            
            // Language
            LanguageIsoCode.RoleType.IsRequired = true;
            LanguageName.RoleType.IsRequired = true;

            // Locale
            LocaleLanguage.RoleType.IsRequired = true;
            LocaleCountry.RoleType.IsRequired = true;
            
            // LocalisedText
            LocalisedTextText.RoleType.IsRequired = true;

            // Media
            MediaMediaType.RoleType.IsRequired = true;
            MediaMediaType.RoleType.IsRequired = true;

            // MediaContent
            MediaContentValue.RoleType.IsRequired = true;

            // MediaType
            MediaTypeName.RoleType.IsRequired = true;

            // Permission
            PermissionOperandTypePointer.RoleType.IsRequired = true;
            PermissionConcreteClassPointer.RoleType.IsRequired = true;
            PermissionOperationEnum.RoleType.IsRequired = true;

            // Role
            RoleName.RoleType.IsRequired = true;

            // UniquelyIdentifiable
            UniquelyIdentifiableUniqueId.RoleType.IsRequired = true;

            // UserGroup
            UserGroupName.RoleType.IsRequired = true;

            // StringTemplate
            StringTemplateName.RoleType.IsRequired = true;
        }
    }
}