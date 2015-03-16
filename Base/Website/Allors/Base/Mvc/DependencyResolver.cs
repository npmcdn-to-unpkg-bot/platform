// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllorsDependecyResolver.cs" company="Allors bvba">
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

namespace Allors.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class AllorsDependecyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver original;

        public AllorsDependecyResolver(IDependencyResolver original)
        {
            this.original = original;
        }

        public object GetService(Type serviceType)
        {
            var service = this.Get(serviceType);
            return service ?? this.original.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var service = this.Get(serviceType);
            return service != null ? new[] { service } : this.original.GetServices(serviceType);
        }

        private object Get(Type type)
        {
            return null;
        }
    }
}