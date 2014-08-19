// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sandbox.cs" company="Allors bvba">
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

    public partial class Sandbox
    {
        public static Sandbox Create(ISession session)
        {
            return (Sandbox)session.Create(SandboxMeta.ObjectType);
        }

        public static Sandbox[] Create(ISession session, int count)
        {
            return (Sandbox[])session.Create(SandboxMeta.ObjectType, count);
        }

        public static Sandbox[] Instantiate(ISession session, string[] ids)
        {
            return (Sandbox[])session.Instantiate(ids);
        }

        public static Sandbox[] Extent(ISession session)
        {
            return (Sandbox[])session.Extent(SandboxMeta.ObjectType).ToArray();
        }
    }
}