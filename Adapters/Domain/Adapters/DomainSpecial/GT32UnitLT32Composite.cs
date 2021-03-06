// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GT32UnitLT32Composite.cs" company="Allors bvba">
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

    public partial class GT32UnitLT32Composite 
    {
        public static GT32UnitLT32Composite Create(ISession session)
        {
            return (GT32UnitLT32Composite)session.Create(Meta.ObjectType);
        }

        public static GT32UnitLT32Composite[] Create(ISession session, int count)
        {
            return (GT32UnitLT32Composite[])session.Create(Meta.ObjectType, count);
        }

        public static GT32UnitLT32Composite[] Instantiate(ISession session, string[] ids)
        {
            return (GT32UnitLT32Composite[])session.Instantiate(ids);
        }

        public static GT32UnitLT32Composite[] Extent(ISession session)
        {
            return (GT32UnitLT32Composite[])session.Extent(Meta.ObjectType).ToArray();
        }
    }
}