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
            // All composites should implement Object
            foreach (var composite in meta.Composites)
            {
                if (!composite.ExistDirectSupertypes && !composite.Equals(Object))
                {
                    new InheritanceBuilder(Base, Guid.NewGuid()).WithSubtype(composite).WithSupertype(Object).Build();
                }
            }

            new MethodTypeBuilder(Base, new Guid("62D48A76-A500-4D16-9D20-6FEF43AC6DCB")).WithObjectType(Object).WithName("OnPostBuild").Build();
            new MethodTypeBuilder(Base, new Guid("042375D8-BBDD-46E8-80B6-CC89D8782F1C")).WithObjectType(Object).WithName("ApplySecurityOnPostBuild").Build();

            new MethodTypeBuilder(Base, new Guid("F4CC201F-D6CB-4D82-8AEB-E9C4C79C33F7")).WithObjectType(Deletable).WithName("Delete").Build();
            
            BaseCounter();
            BaseAccessControl();
            BaseCountry();
            BaseCurrency();
            BaseDerivable();
            BaseEnumeration();
            BaseLanguage();
            BaseLocale();
            BaseLocalisedText();
            BaseMedia();
            BaseMediaContent();
            BaseMediaType();
            BasePeriod();
            BasePermission();
            BaseRole();
            BaseStringTemplate();
            BaseUniquelyIdentifiable();
            BaseUserGroup();
        }

        private static void BaseUserGroup()
        {
            UserGroupName.IsRequired = true;
        }

        private static void BaseUniquelyIdentifiable()
        {
            UniquelyIdentifiableUniqueId.IsRequired = true;
        }

        private static void BaseStringTemplate()
        {
            StringTemplateName.IsRequired = true;
            StringTemplateLocale.IsRequiredOverride = true;
        }

        private static void BaseRole()
        {
            RoleName.IsRequired = true;
        }

        private static void BasePermission()
        {
            PermissionOperandTypePointer.IsRequired = true;
            PermissionConcreteClassPointer.IsRequired = true;
            PermissionOperationEnum.IsRequired = true;
        }

        private static void BasePeriod()
        {
            PeriodFromDate.IsRequired = true;
        }

        private static void BaseMediaType()
        {
            MediaTypeName.IsRequired = true;
        }

        private static void BaseMediaContent()
        {
            MediaContentValue.IsRequired = true;
        }

        private static void BaseMedia()
        {
            MediaMediaType.IsRequired = true;
            MediaMediaType.IsRequired = true;
        }

        private static void BaseLocalisedText()
        {
            LocalisedTextText.IsRequired = true;
        }

        private static void BaseLocale()
        {
            LocaleLanguage.IsRequired = true;
            LocaleCountry.IsRequired = true;
        }

        private static void BaseLanguage()
        {
            LanguageIsoCode.IsRequired = true;
            LanguageName.IsRequired = true;
        }

        private static void BaseEnumeration()
        {
            EnumerationName.IsRequired = true;
            EnumerationIsActive.IsRequired = true;
        }

        private static void BaseDerivable()
        {
            new MethodTypeBuilder(Base, new Guid("122D3D78-AB97-4A69-A725-F465C71757DA")).WithObjectType(Derivable).WithName("PrepareDerivation").Build();
            new MethodTypeBuilder(Base, new Guid("527DA7F8-68B4-46AB-B0D8-6B9E82D2A5AC")).WithObjectType(Derivable).WithName("Derive").Build();
            new MethodTypeBuilder(Base, new Guid("349CBCDE-B4E9-4965-B3FF-7C41B021825D")).WithObjectType(Derivable).WithName("ApplySecurityOnDerive").Build();
        }

        private static void BaseCurrency()
        {
            CurrencyIsoCode.IsRequired = true;
            CurrencyName.IsRequired = true;
            CurrencySymbol.IsRequired = true;
        }

        private static void BaseCountry()
        {
            CountryIsoCode.IsRequired = true;
            CountryName.IsRequired = true;
        }

        private static void BaseAccessControl()
        {
            AccessControlRole.IsRequired = true;
            AccessControlObject.IsRequired = true;
        }

        private static void BaseCounter()
        {
            CounterValue.IsRequired = true;
        }
    }
}