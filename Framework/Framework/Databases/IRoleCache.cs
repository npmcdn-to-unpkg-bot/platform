// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleCache.cs" company="Allors bvba">
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
    using Allors.Meta;

    public interface IRoleCache
    {
        bool TryGetUnit(ObjectId association, object cacheId, IRoleType roleType, out object role);
       
        void SetUnit(ObjectId association, object cacheId, IRoleType roleType, object role);

        bool TryGetComposite(ObjectId association, object cacheId, IRoleType roleType, out ObjectId role);

        void SetComposite(ObjectId association, object cacheId, IRoleType roleType, ObjectId role);

        bool TryGetComposites(ObjectId association, object cacheId, IRoleType roleType, out ObjectId[] role);

        void SetComposites(ObjectId association, object cacheId, IRoleType roleType, ObjectId[] role);

        void Invalidate();
    }
}