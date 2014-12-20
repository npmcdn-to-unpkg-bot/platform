//------------------------------------------------------------------------------------------------- 
// <copyright file="MethodInvocation.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public partial class MethodInvocation<T>
        where T : IObject
    {
        private readonly MethodType methodType;

        private readonly List<MethodInfo> methodInfos; 

        public MethodInvocation(MethodType methodType)
        {
            this.methodType = methodType;
            this.methodInfos = new List<MethodInfo>();

            var metaPopulation = this.methodType.MetaPopulation;
            var domains = new List<Domain>(metaPopulation.Domains);
            domains.Sort((a, b) => a.Superdomains.Contains(b) ? -1 : 1);

            var @class = typeof(T);
            var interfaces = @class.GetInterfaces();

            foreach (var domain in domains)
            {
                var methodName = domain.Name + this.methodType.Name;

                var methodInfo = @class.GetMethod(methodName);
                if (methodInfo != null)
                {
                    this.methodInfos.Add(methodInfo);
                }

                foreach (var @interface in interfaces)
                {
                    var extensionMethodInfos = GetExtensionMethods(@interface.Assembly, @interface, methodName);
                    if (extensionMethodInfos.Length > 1)
                    {
                        throw new Exception("Interface " + @interface + " has 2 extension methods for " + methodName);
                    }

                    if (extensionMethodInfos.Length == 1)
                    {
                        this.methodInfos.Add(extensionMethodInfos[0]);
                    }
                }
            }
        }

        public MethodType MethodType
        {
            get
            {
                return this.methodType;
            }
        }

        public void Execute(Method method)
        {
            foreach (var methodInfo in this.methodInfos)
            {
                if (methodInfo.IsStatic)
                {
                    methodInfo.Invoke(null, new object[] { method.Object, method });
                }
                else
                {
                    methodInfo.Invoke(method.Object, new object[] { method });
                }
            }
        }

        private static MethodInfo[] GetExtensionMethods(Assembly assembly, Type @interface, string methodName)
        {
            var query = from type in assembly.GetTypes()
                        where type.IsSealed && !type.IsGenericType && !type.IsNested
                        from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.Name.Equals(methodName)
                        where method.GetParameters()[0].ParameterType == @interface
                        select method;
            return query.ToArray();
        }
    }
}