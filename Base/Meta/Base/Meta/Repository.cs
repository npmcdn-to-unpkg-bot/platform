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
        public static void BasePostInit()
        {
            new MethodType(BaseDomain.Instance, new Guid("62D48A76-A500-4D16-9D20-6FEF43AC6DCB"))
                {
                    ObjectType = ObjectInterface.Instance,
                    Name = "OnPostBuild" 
                };

            new MethodType(BaseDomain.Instance, new Guid("042375D8-BBDD-46E8-80B6-CC89D8782F1C"))
            {
                ObjectType = ObjectInterface.Instance,
                Name = "ApplySecurityOnPostBuild"
            };

            new MethodType(BaseDomain.Instance, new Guid("F4CC201F-D6CB-4D82-8AEB-E9C4C79C33F7"))
            {
                ObjectType = DeletableInterface.Instance,
                Name = "Delete"
            };
            
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
            UserGroupNameType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseUniquelyIdentifiable()
        {
            UniquelyIdentifiableUniqueIdType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseStringTemplate()
        {
            StringTemplateNameType.Instance.RoleType.IsRequired = true;
            // TODO:
            //StringTemplateLocaleType.Instance.RoleType.IsRequiredOverride = true;
        }

        private static void BaseRole()
        {
            RoleNameType.Instance.RoleType.IsRequired = true;
        }

        private static void BasePermission()
        {
            PermissionOperandTypePointerType.Instance.RoleType.IsRequired = true;
            PermissionConcreteClassPointerType.Instance.RoleType.IsRequired = true;
            PermissionOperationEnumType.Instance.RoleType.IsRequired = true;
        }

        private static void BasePeriod()
        {
            PeriodFromDateType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseMediaType()
        {
            MediaTypeNameType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseMediaContent()
        {
            MediaContentValueType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseMedia()
        {
            MediaMediaTypeType.Instance.RoleType.IsRequired = true;
            MediaMediaTypeType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseLocalisedText()
        {
            LocalisedTextTextType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseLocale()
        {
            LocaleLanguageType.Instance.RoleType.IsRequired = true;
            LocaleCountryType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseLanguage()
        {
            LanguageIsoCodeType.Instance.RoleType.IsRequired = true;
            LanguageNameType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseEnumeration()
        {
            EnumerationNameType.Instance.RoleType.IsRequired = true;
            EnumerationIsActiveType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseDerivable()
        {
            new MethodType(BaseDomain.Instance, new Guid("122D3D78-AB97-4A69-A725-F465C71757DA"))
            {
                ObjectType = DeletableInterface.Instance,
                Name = "PrepareDerivation"
            };

            new MethodType(BaseDomain.Instance, new Guid("527DA7F8-68B4-46AB-B0D8-6B9E82D2A5AC"))
            {
                ObjectType = DeletableInterface.Instance,
                Name = "Derive"
            };

            new MethodType(BaseDomain.Instance, new Guid("349CBCDE-B4E9-4965-B3FF-7C41B021825D"))
            {
                ObjectType = DeletableInterface.Instance,
                Name = "ApplySecurityOnDerive"
            };
        }

        private static void BaseCurrency()
        {
            CurrencyIsoCodeType.Instance.RoleType.IsRequired = true;
            CurrencyNameType.Instance.RoleType.IsRequired = true;
            CurrencySymbolType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseCountry()
        {
            CountryIsoCodeType.Instance.RoleType.IsRequired = true;
            CountryNameType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseAccessControl()
        {
            AccessControlRoleType.Instance.RoleType.IsRequired = true;
            AccessControlObjectType.Instance.RoleType.IsRequired = true;
        }

        private static void BaseCounter()
        {
            CounterValueType.Instance.RoleType.IsRequired = true;
        }
    }
}