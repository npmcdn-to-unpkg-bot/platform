// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelAttribute.cs" company="Allors bvba">
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

namespace Allors.Web.Mvc.Models
{
    using System.Web.Mvc;

    using Allors.Meta;

    public static class ModelMetadataExtensions
    {
        private const string ClassKey = "Class";
        private const string ThemeKey = "Theme";

        public static bool ExistComposite(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey(ClassKey);
        }

        public static Composite GetComposite(this ModelMetadata modelMetadata)
        {
            object objectType;
            modelMetadata.AdditionalValues.TryGetValue(ClassKey, out objectType);
            return (Composite)objectType;
        }

        public static void SetComposite(this ModelMetadata modelMetadata, Composite value)
        {
            modelMetadata.AdditionalValues[ClassKey] = value;
        }

        public static bool ExistTheme(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey(ThemeKey);
        }

        public static string GetTheme(this ModelMetadata modelMetadata)
        {
            object theme;
            modelMetadata.AdditionalValues.TryGetValue(ThemeKey, out theme);
            return (string)theme;
        }

        public static void SetTheme(this ModelMetadata modelMetadata, string value)
        {
            modelMetadata.AdditionalValues[ThemeKey] = value;
        }

        public static string GetEditorTemplate(this ModelMetadata modelMetadata)
        {
            var theme = GetTheme(modelMetadata);
            var template = theme + modelMetadata.ModelType.Name;
            return template;
        }
    }
}