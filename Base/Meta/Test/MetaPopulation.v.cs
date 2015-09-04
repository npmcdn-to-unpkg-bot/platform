using System;
using System.Linq;
using System.Reflection;

namespace Allors.Meta
{
    public partial class MetaPopulation
    {
        private void Extend()
        {
            // Create MethodInfo objects
            foreach (var composite in this.Composites)
            {
                var type = composite.GetType();
                var methodTypeFields = type
                    .GetFields()
                    .Where(field => field.FieldType == typeof (MethodType));

                foreach (var methodTypeField in methodTypeFields)
                {
                    var idAttribute = (IdAttribute)Attribute.GetCustomAttribute(methodTypeField, typeof(IdAttribute));
                    var id = new Guid(idAttribute.Value);
                    var methodType = (MethodType)Activator.CreateInstance(typeof(MethodType),new object[] {composite.DefiningDomain, id});
                    methodType.Name = methodTypeField.Name;
                    methodType.ObjectType = composite;
                    methodTypeField.SetValue(composite, methodType);
                }
            }


            foreach (var composite in this.Composites)
            {
                composite.BaseExtend();
            }

            foreach (var composite in this.Composites)
            {
                composite.TestsExtend();
            }
        }
    }
}