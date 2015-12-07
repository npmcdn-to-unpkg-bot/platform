//-------------------------------------------------------------------------------------------------
// <copyright file="PrefetchPolicyBuilderExtensions.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the ISessionExtension type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors
{
    using System.Collections.Generic;

    using Allors.Meta;

    public static partial class PrefetchPolicyBuilderExtensions
    {
        public static void WithGroupRules(this PrefetchPolicyBuilder @this, Class @class, string @group)
        {
            IList<RoleType> roleTypes;
            if (@class.RoleTypesByGroup.TryGetValue(group, out roleTypes))
            {
                foreach (var roleType in roleTypes)
                {
                    @this.WithRule(roleType);
                }
            }
        }

        public static void WithSecurityRules(this PrefetchPolicyBuilder @this)
        {
            @this.WithRule(AccessControlledObjectInterface.Instance.SecurityToken.RoleType);
        }
    }
}