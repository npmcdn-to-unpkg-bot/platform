// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleCache.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases
{
    using System.Collections.Generic;
    using Allors.Meta;

    public class ClassCache : IClassCache
    {
        private Dictionary<ObjectId, IClass> classByObject;

        public ClassCache()
        {
            this.classByObject = new Dictionary<ObjectId, IClass>();
        }

        public bool TryGet(ObjectId @object, out IClass @class)
        {
            return this.classByObject.TryGetValue(@object, out @class);
        }

        public void Set(ObjectId @object, IClass @class)
        {
            this.classByObject[@object] = @class;
        }

        public void Invalidate()
        {
            this.classByObject = new Dictionary<ObjectId, IClass>();
        }
    }
}