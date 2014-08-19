// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassWithoutRoles.cs" company="Allors bvba">
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

namespace Domain
{
    using Allors.R1;

    public partial class ClassWithoutRoles
    {
        public static ClassWithoutRoles Create(ISession session)
        {
            return (ClassWithoutRoles)session.Create(ClassWithoutRolesMeta.ObjectType);
        }

        public static ClassWithoutRoles[] Create(ISession session, int count)
        {
            return (ClassWithoutRoles[])session.Create(ClassWithoutRolesMeta.ObjectType, count);
        }

        public static ClassWithoutRoles[] Instantiate(ISession session, string[] ids)
        {
            return (ClassWithoutRoles[])session.Instantiate(ids);
        }

        public static ClassWithoutRoles[] Extent(ISession session)
        {
            return (ClassWithoutRoles[])session.Extent(ClassWithoutRolesMeta.ObjectType).ToArray();
        }
    }
}