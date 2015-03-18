// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGroup.cs" company="Allors bvba">
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
    public partial class UserGroup
    {
        public bool ContainsMember(User user)
        {
            foreach (User member in this.Members)
            {
                if (member.Equals(user))
                {
                    return true;
                }
            }

            return false;
        }

        protected void BaseOnPreDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;
            
            foreach (Object member in this.Members)
            {
                derivation.AddDependency(member, this);
            }
        }

        public void BaseOnDerive(ObjectOnDerive method)
        {
            if (this.ExistParent)
            {
                // TODO: members should be added to ancestor groups
            }
        }
    }
}