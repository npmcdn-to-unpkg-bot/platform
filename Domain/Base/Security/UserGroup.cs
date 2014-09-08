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
    using System.Text;

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

        public override void AddMember(User value)
        {
            base.AddMember(value);

            if (this.ExistParent)
            {
                this.Parent.AddMember(value);
            }
        }

        public override void RemoveMember(User value)
        {
            base.RemoveMember(value);

            if (this.ExistParent)
            {
                this.Parent.RemoveMember(value);
            }
        }

        protected override void CorePrepareDerivation(IDerivation derivation)
        {
            base.CorePrepareDerivation(derivation);

            foreach (Derivable member in this.Members)
            {
                derivation.AddDependency(member, this);
            }
        }

        protected override void CoreDerive(IDerivation derivation)
        {
            base.CoreDerive(derivation);

            derivation.Log.AssertExists(this, Meta.Name);
            derivation.Log.AssertIsUnique(this, Meta.Name);

            if (this.ExistName)
            {
                if (this.ExistMembers)
                {
                    this.DisplayName = this.Name + string.Format(" with {0} members", this.Members.Count);
                }
                else
                {
                    this.DisplayName = this.Name;
                }
            }
            else
            {
                this.DisplayName = "Unnamed";
            }
        }

        private void CoreDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            if (this.ExistName)
            {
                uiText.Append(this.Name);
            }

            if (this.ExistMembers)
            {
                uiText.Append(" with ");
                uiText.Append(this.Members.Count);
                uiText.Append(" members");
            }

            this.DisplayName = uiText.ToString();
        }
    }
}