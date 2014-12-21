// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Allors bvba">
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
    using System;
    using System.Text;

    using Allors.Domain;

    public partial class Person
    {
        public bool IsGuest
        {
            get
            {
                return this.ExistSingletonWhereGuest;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                var roleId = UserGroups.AdministratorsId;
                return this.InRole(roleId);
            }
        }

        public bool InRole(Guid roleId)
        {
            foreach (UserGroup group in this.UserGroupsWhereMember)
            {
                if (@group.UniqueId.Equals(roleId))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void BaseOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.BaseOnPostBuild(objectBuilder);

            if (!this.ExistUserEmailConfirmed)
            {
                this.UserEmailConfirmed = false;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }

            this.BuildOwnerSecurityToken();
        }

        public void BaseDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;
            
            this.DisplayName = this.DeriveDisplayName();
            this.SearchData.CharacterBoundaryText = this.DeriveSearchDataCharacterBoundaryText();
            this.SearchData.WordBoundaryText = this.DeriveSearchDataWordBoundaryText();
        }

        private string CoreDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            if (this.ExistFirstName)
            {
                uiText.Append(this.FirstName);
            }

            if (this.ExistMiddleName)
            {
                if (uiText.Length > 0)
                {
                    uiText.Append(" ");
                }

                uiText.Append(this.MiddleName);
            }

            if (this.ExistLastName)
            {
                if (uiText.Length > 0)
                {
                    uiText.Append(" ");
                }

                uiText.Append(this.LastName);
            }

            return uiText.ToString();
        }

        private string CoreDeriveSearchDataCharacterBoundaryText()
        {
            return this.DeriveDisplayName();
        }

        private string CoreDeriveSearchDataWordBoundaryText()
        {
            return null;
        }

        private void CoreCreateOwnerSecurityToken()
        {
            if (!this.ExistOwnerSecurityToken)
            {
                var mySecurityToken = new SecurityTokenBuilder(this.Session).Build();
                this.OwnerSecurityToken = mySecurityToken;

                if (!this.ExistAccessControlsWhereSubject && this.IsInDatabase)
                {
                    new AccessControlBuilder(this.DatabaseSession)
                        .WithRole(new Roles(this.DatabaseSession).Owner)
                        .WithSubject(this)
                        .WithObject(this.OwnerSecurityToken)
                        .Build();
                }
            }
        }

        private void CoreDelete()
        {
            if (this.ExistSearchData)
            {
                this.SearchData.Delete();
            }

            if (this.ExistOwnerSecurityToken)
            {
                foreach (AccessControl acl in this.OwnerSecurityToken.AccessControlsWhereObject)
                {
                    acl.Delete();
                }

                this.OwnerSecurityToken.Delete();
            }
        }
    }
}