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
    using System.Linq;
    using System.Web.Mvc;

    using Allors.Meta;

    public class AllorsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly Dictionary<Type, Composite> compositeByModelType;
        private readonly List<IModelMetadataFilter> metadataFilters;

        public AllorsModelMetadataProvider(Dictionary<Type, Composite> compositeByModelType)
        {
            this.metadataFilters = new List<IModelMetadataFilter>();
            this.compositeByModelType = compositeByModelType;
        }

        public List<IModelMetadataFilter> MetadataFilters 
        {
            get
            {
                return this.metadataFilters;
            }
        }
        
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributeEnumerable, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var attributes = attributeEnumerable.ToArray();
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            Composite composite;
            if (containerType != null && this.compositeByModelType.TryGetValue(containerType, out composite))
            {
                this.MetadataFilters.ForEach(m => m.Transform(composite, metadata, attributes));
            }

            return metadata;
        }
    }
}