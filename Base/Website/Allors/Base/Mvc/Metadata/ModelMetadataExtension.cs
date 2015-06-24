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
        private const string CompositeKey = "Composite";
        private const string PathPropertyIdsKey = "PathPropertyIds";
        private const string PathKey = "Path";

        public static bool ExistComposite(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey(CompositeKey);
        }

        public static Composite GetComposite(this ModelMetadata modelMetadata)
        {
            object objectType;
            modelMetadata.AdditionalValues.TryGetValue(CompositeKey, out objectType);
            return (Composite)objectType;
        }

        public static void SetComposite(this ModelMetadata modelMetadata, Composite value)
        {
            modelMetadata.AdditionalValues[CompositeKey] = value;
        }

        public static bool ExistPathPropertyIds(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey(PathPropertyIdsKey);
        }

        public static string[] GetPathPropertyIds(this ModelMetadata modelMetadata)
        {
            object objectType;
            modelMetadata.AdditionalValues.TryGetValue(PathPropertyIdsKey, out objectType);
            return (string[])objectType;
        }

        public static void SetPathPropertyIds(this ModelMetadata modelMetadata, string[] value)
        {
            modelMetadata.AdditionalValues[PathPropertyIdsKey] = value;
        }

        public static bool ExistPath(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey(PathKey);
        }

        public static Path GetPath(this ModelMetadata modelMetadata)
        {
            object objectType;
            modelMetadata.AdditionalValues.TryGetValue(PathKey, out objectType);
            return (Path)objectType;
        }

        public static void SetPath(this ModelMetadata modelMetadata, Path value)
        {
            modelMetadata.AdditionalValues[PathKey] = value;
        }
    }
}