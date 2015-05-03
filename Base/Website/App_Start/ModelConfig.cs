namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Allors.Meta;
    using Allors.Web.Mvc.Models;
    using Allors.Web.Mvc.Models.Annotations;

    public class ModelConfig
    {
        private static readonly ITypeMetadataAware[] DefaultTypeAnnotations =
            {
            };

        private static readonly IPropertyMetadataAware[] DefaultPropertyAnnotations =
            {
                new IdPostfixAnnotation(), 
                new DisplayNameAnnotation(), 
                new WatermarkAnnotation()
            };

        public static Dictionary<Type, Composite> CompositeByModelType
        {
            get
            {
                var modelTypes =
                    Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(type => 
                            type != typeof(IMetadataModel) && 
                            type != typeof(IMetadataModel<>) && 
                            typeof(IMetadataModel).IsAssignableFrom(type))
                        .ToArray();

                var compositeByModelType = new Dictionary<Type, Composite>();
                foreach (var modelType in modelTypes)
                {
                    var genericModelInterface = modelType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IMetadataModel<>));
                    if (genericModelInterface != null)
                    {
                        var compositeType = genericModelInterface.GenericTypeArguments[0];
                        var property = compositeType.GetProperty("Instance");
                        var composite = (Composite)property.GetMethod.Invoke(null, null);
                        compositeByModelType[modelType] = composite;
                    }
                    else
                    {
                        compositeByModelType[modelType] = null;
                    }
                }

                return compositeByModelType;
            }
        }

        public static void Register()
        {
            ModelMetadataProviders.Current = new AllorsModelMetadataProvider(CompositeByModelType, DefaultTypeAnnotations, DefaultPropertyAnnotations);
        }
    }
}
