// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllorsModelMetadataProvider.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//   // Dual Licensed under
//   //   a) the General Public Licence v3 (GPL)
//   //   b) the Allors License
//   // The GPL License is included in the file gpl.txt.
//   // The Allors License is an addendum to your contract.
//   // Allors Applications is distributed in the hope that it will be useful,
//   // but WITHOUT ANY WARRANTY; without even the implied warranty of
//   // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   // GNU General Public License for more details.
//   // For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   The metadata provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Allors.Web.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Allors.Meta;

    public class AllorsModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider
    {
        private static readonly ITypeMetadataAware[] EmptyTypeAnnotations = { };
        private static readonly IPropertyMetadataAware[] EmptyPropertyAnnotations = { };

        private readonly Dictionary<Type, Composite> compositeByModelType;
        private readonly ITypeMetadataAware[] typeAnnotations;
        private readonly IPropertyMetadataAware[] propertyAnnotations;

        public AllorsModelMetadataProvider(Dictionary<Type, Composite> compositeByModelType, ITypeMetadataAware[] typeAnnotations, IPropertyMetadataAware[] propertyAnnotations)
        {
            this.compositeByModelType = compositeByModelType;
            this.typeAnnotations = typeAnnotations ?? EmptyTypeAnnotations;
            this.propertyAnnotations = propertyAnnotations ?? EmptyPropertyAnnotations;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            var modelMetaData = base.CreateMetadataFromPrototype(prototype, modelAccessor);

            Composite composite;
            if (this.compositeByModelType.TryGetValue(modelMetaData.ModelType, out composite))
            {
                modelMetaData.SetComposite(composite);
                foreach (var filter in this.typeAnnotations)
                {
                    filter.OnTypeMetadataCreated(modelMetaData);
                }
            }
            else if (modelMetaData.ContainerType != null && this.compositeByModelType.TryGetValue(modelMetaData.ContainerType, out composite))
            {
                foreach (var filter in this.propertyAnnotations)
                {
                    filter.OnPropertyMetadataCreated(modelMetaData);
                }
            }

            return modelMetaData;
        }
    }
}