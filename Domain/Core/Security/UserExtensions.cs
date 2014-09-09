// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System.Collections.Generic;

    public static partial class UserExtensions
    {
        public static IEnumerable<UserGroup> GetUserGroupHierarchy(this User user)
        {
            var userGroupHierarchy = new HashSet<UserGroup>();
            foreach (UserGroup userGroup in user.UserGroupsWhereMember)
            {
                userGroupHierarchy.Add(userGroup);
                AddParentUserGroups(userGroupHierarchy, userGroup);
            }

            return userGroupHierarchy;
        }

        private static void AddParentUserGroups(HashSet<UserGroup> userGroupHierarchy, UserGroup userGroup)
        {
            if (userGroup.ExistParent)
            {
                userGroupHierarchy.Add(userGroup.Parent);
                AddParentUserGroups(userGroupHierarchy, userGroup.Parent);
            }
        }
    }
}