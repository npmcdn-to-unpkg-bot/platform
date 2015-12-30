//------------------------------------------------------------------------------------------------- 
// <copyright file="Interface.cs" company="Allors bvba">
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
// <summary>Defines the IObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Tools.Repository
{
    using System.Collections.Generic;

    public class Interface : Composite
    {
        public Interface(string name)
            : base(name)
        {
            this.PartialByAssemblyName = new Dictionary<string, PartialInterface>();
        }

        public Dictionary<string, PartialInterface> PartialByAssemblyName { get; }

        public override string ToString()
        {
            return this.Name;
        }


        public Property GetImplementedProperty(Property property)
        {
            Property implementedProperty;
            if (this.PropertyByName.TryGetValue(property.Name, out implementedProperty))
            {
                return implementedProperty;
            }

            foreach (var @interface in this.ImplementedInterfaces)
            {
                implementedProperty = @interface.GetImplementedProperty(property);
                if (implementedProperty != null)
                {
                    return implementedProperty;
                }
            }

            return null;
        }

        public Method GetImplementedMethod(Method method)
        {
            Method implementedMethod;
            if (this.MethodByName.TryGetValue(method.Name, out implementedMethod))
            {
                return implementedMethod;
            }

            foreach (var @interface in this.ImplementedInterfaces)
            {
                implementedMethod = @interface.GetImplementedMethod(method);
                if (implementedMethod != null)
                {
                    return implementedMethod;
                }
            }

            return null;
        }
    }
}