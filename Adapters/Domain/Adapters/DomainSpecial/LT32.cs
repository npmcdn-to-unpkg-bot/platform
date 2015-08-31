// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LT32.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.Domain
{
    using Allors;

    public partial class LT32
    {
        public static LT32 Create(IDatabaseSession session)
        {
            return (LT32)session.Create(Meta.ObjectType);
        }

        public static LT32[] Create(IDatabaseSession session, int count)
        {
            return (LT32[])session.Create(Meta.ObjectType, count);
        }

        public static LT32[] Instantiate(IDatabaseSession session, string[] ids)
        {
            return (LT32[])session.Instantiate(ids);
        }

        public static LT32[] Extent(IDatabaseSession session)
        {
            return (LT32[])session.Extent(Meta.ObjectType).ToArray();
        }
    }
}