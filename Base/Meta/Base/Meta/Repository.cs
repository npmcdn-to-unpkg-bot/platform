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
    using System;

    public static partial class Repository
    {
        public static void BasePostInit(MetaPopulation meta)
        {
            // Counter
            AccessControlRole.IsRequired = true;
            AccessControlObject.IsRequired = true;

            // Counter
            CounterValue.IsRequired = true;

            // Country
            CountryIsoCode.IsRequired = true;
            CountryName.IsRequired = true;

            // Currency
            CurrencyIsoCode.IsRequired = true;
            CurrencyName.IsRequired = true;
            CurrencySymbol.IsRequired = true;

            // Derivation
            new MethodTypeBuilder(Base, new Guid("122D3D78-AB97-4A69-A725-F465C71757DA")).WithObjectType(Derivable).WithName("PrepareDerivation").Build();
            new MethodTypeBuilder(Base, new Guid("527DA7F8-68B4-46AB-B0D8-6B9E82D2A5AC")).WithObjectType(Derivable).WithName("Derive").Build();
            new MethodTypeBuilder(Base, new Guid("349CBCDE-B4E9-4965-B3FF-7C41B021825D")).WithObjectType(Derivable).WithName("ApplySecurityOnDerive").Build();
            
            // Enumeration
            EnumerationName.IsRequired = true;
            EnumerationIsActive.IsRequired = true;
            
            // Language
            LanguageIsoCode.IsRequired = true;
            LanguageName.IsRequired = true;

            // Locale
            LocaleLanguage.IsRequired = true;
            LocaleCountry.IsRequired = true;
            
            // LocalisedText
            LocalisedTextText.IsRequired = true;

            // Media
            MediaMediaType.IsRequired = true;
            MediaMediaType.IsRequired = true;

            // MediaContent
            MediaContentValue.IsRequired = true;

            // MediaType
            MediaTypeName.IsRequired = true;

            // Period
            PeriodFromDate.IsRequired = true;

            // Permission
            PermissionOperandTypePointer.IsRequired = true;
            PermissionConcreteClassPointer.IsRequired = true;
            PermissionOperationEnum.IsRequired = true;

            // Role
            RoleName.IsRequired = true;

            // StringTemplate
            StringTemplateLocale.IsRequiredOverride = true;
            StringTemplateTemplatePurpose.IsRequired = true;

            // UniquelyIdentifiable
            UniquelyIdentifiableUniqueId.IsRequired = true;

            // UserGroup
            UserGroupName.IsRequired = true;

            // StringTemplate
            StringTemplateName.IsRequired = true;
        }
    }
}