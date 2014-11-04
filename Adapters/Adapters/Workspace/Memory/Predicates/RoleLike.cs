// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleLike.cs" company="Allors bvba">
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

namespace Allors.Adapters.Workspace.Memory
{
    using System.Text.RegularExpressions;

    using Allors.Adapters;

    using Allors.Meta;
    using Allors.Populations;

    internal sealed class RoleLike : Predicate
    {
        private readonly IRoleType roleType;
        private readonly bool isEmpty;
        private readonly Regex regex;

        internal RoleLike(Extent extent, IRoleType roleType, string like)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleLikeFilter(roleType, like);

            this.roleType = roleType;
            this.isEmpty = like.Length == 0;
            this.regex = new Regex("^" + like.Replace("%", ".*") + "$");
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            var value = (string)strategy.GetInternalizedUnitRole(this.roleType);

            if (value == null)
            {
                return ThreeValuedLogic.Unknown;
            }

            if (this.isEmpty)
            {
                return ThreeValuedLogic.False;
            }

            return this.regex.Match(value).Success
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}