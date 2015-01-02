//------------------------------------------------------------------------------------------------- 
// <copyright file="Repository.cs" company="Allors bvba">
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

    public static partial class Repository
    {
        public static void TestPostInit()
        {
            new MethodType(BaseDomain.Instance, new Guid("A80E3732-DAF2-4AD4-9378-B4BC13E74DDE"))
            {
                ObjectType = C1Class.Instance,
                Name = "ClassMethod"
            };

            new MethodType(BaseDomain.Instance, new Guid("336DC840-BDD8-45CC-8B95-DD0EA55F130D"))
            {
                ObjectType = I1Interface.Instance,
                Name = "InterfaceMethod"
            };

            new MethodType(BaseDomain.Instance, new Guid("5C7F1AB4-0B61-416D-97F4-660663F0E933"))
            {
                ObjectType = S1Interface.Instance,
                Name = "SuperinterfaceMethod"
            };




            new MethodType(BaseDomain.Instance, new Guid("55AAC529-BEAE-4D29-B069-DECDA86710A9"))
            {
                ObjectType = OrganisationClass.Instance,
                Name = "JustDoIt"
            };
        }
    }
}