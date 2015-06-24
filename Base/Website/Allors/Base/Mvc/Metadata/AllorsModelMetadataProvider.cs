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
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using Allors.Meta;

    public class AllorsModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider
    {
        private static readonly ITypeMetadataAware[] EmptyTypeAnnotations = { };
        private static readonly IPropertyMetadataAware[] EmptyPropertyAnnotations = { };

        private readonly Dictionary<Type, Composite> compositeByModelType;
        private readonly Dictionary<Type, Dictionary<string, Path>> pathByPropertyNameByModelType;
 
        private readonly ITypeMetadataAware[] typeAnnotations;
        private readonly IPropertyMetadataAware[] propertyAnnotations;

        public AllorsModelMetadataProvider(Dictionary<Type, Composite> compositeByModelType, ITypeMetadataAware[] typeAnnotations, IPropertyMetadataAware[] propertyAnnotations)
        {
            this.compositeByModelType = compositeByModelType;
            this.pathByPropertyNameByModelType = new Dictionary<Type, Dictionary<string, Path>>();

            this.typeAnnotations = typeAnnotations ?? EmptyTypeAnnotations;
            this.propertyAnnotations = propertyAnnotations ?? EmptyPropertyAnnotations;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            var modelMetaData = base.CreateMetadataFromPrototype(prototype, modelAccessor);

            Composite composite;
            if (this.compositeByModelType.TryGetValue(modelMetaData.ModelType, out composite))
            {
                // CompositeType
                modelMetaData.SetComposite(composite);

                foreach (var filter in this.typeAnnotations)
                {
                    filter.OnTypeMetadataCreated(modelMetaData);
                }
            }
            else if (modelMetaData.ContainerType != null && this.compositeByModelType.TryGetValue(modelMetaData.ContainerType, out composite))
            {
                modelMetaData.SetComposite(composite);

                // Path
                if (composite != null)
                {
                    Dictionary<string, Path> pathByPropertyName;
                    if (this.pathByPropertyNameByModelType.TryGetValue(modelMetaData.ContainerType, out pathByPropertyName))
                    {
                        Path path;
                        if (pathByPropertyName.TryGetValue(modelMetaData.PropertyName, out path))
                        {
                            modelMetaData.SetPath(path);
                        }
                    }
                }

                foreach (var filter in this.propertyAnnotations)
                {
                    filter.OnPropertyMetadataCreated(modelMetaData);
                }
            }

            return modelMetaData;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributeEnumerations, Type containerType, Type modelType, string propertyName)
        {
            var attributes = new List<Attribute>(attributeEnumerations);

            Composite composite;
            if (containerType != null && this.compositeByModelType.TryGetValue(containerType, out composite))
            {
                Path path;
                if (composite != null)
                {
                    Dictionary<string, Path> pathByPropertyName;
                    if (!this.pathByPropertyNameByModelType.TryGetValue(containerType, out pathByPropertyName))
                    {
                        pathByPropertyName = new Dictionary<string, Path>();
                        this.pathByPropertyNameByModelType[containerType] = pathByPropertyName;
                    }

                    var pathString = propertyName;
                    if (!pathByPropertyName.TryGetValue(pathString, out path))
                    {
                        var pathAttribute = (PathAttribute)attributes.FirstOrDefault(x => x is PathAttribute);
                        if (pathAttribute != null && pathAttribute.PropertyIds.Length > 0)
                        {
                            path = new Path(composite.MetaPopulation, pathAttribute.PropertyIds);
                        }
                        else
                        {
                            Path.TryParse(composite, pathString, out path);
                        }

                        pathByPropertyName[pathString] = path;

                        if (path != null)
                        {
                            var roleType = path.End.PropertyType as RoleType;
                            if (roleType != null)
                            {
                                if (!attributes.Any(x => x is DisplayAttribute))
                                {
                                    var displayAttribute = roleType.DisplayAttribute;
                                    if (displayAttribute != null)
                                    {
                                        attributes.Add(displayAttribute);
                                    }
                                }

                                if (!attributes.Any(x => x is DataTypeAttribute))
                                {
                                    var dataTypeAttribute = roleType.DataTypeAttribute;
                                    if (dataTypeAttribute != null)
                                    {
                                        attributes.Add(dataTypeAttribute);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);
        }
    }
}