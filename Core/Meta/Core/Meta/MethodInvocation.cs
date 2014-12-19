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

    public partial class MethodInvocation<T>
        where T : IObject
    {
        private readonly MethodType methodType;

        private readonly List<MethodInfo> methodInfos; 

        public MethodInvocation(MethodType methodType)
        {
            this.methodType = methodType;
            this.methodInfos = new List<MethodInfo>();

            var types = new List<Type> { typeof(T) };
            types.AddRange(typeof(T).GetInterfaces());

            var metaPopulation = this.methodType.MetaPopulation;
            var domains = new List<Domain>(metaPopulation.Domains);
            domains.Sort((a, b) => a.Superdomains.Contains(b) ? -1 : 1);

            foreach (var type in types)
            {   

            }
        }

        public MethodType MethodType
        {
            get
            {
                return this.methodType;
            }
        }

        public void Execute(Method<T> mehod)
        {
        }
    }
}