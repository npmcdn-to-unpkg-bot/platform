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

        public void BaseOnPostBuild(ObjectOnPostBuild method)
        {
            var builder = method.Builder;

            if (!this.ExistUserEmailConfirmed)
            {
                this.UserEmailConfirmed = false;
            }

            this.BuildOwnerSecurityToken();
        }

        public void BaseDerive(ObjectDerive method)
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
                var mySecurityToken = new SecurityTokenBuilder(this.Strategy.Session).Build();
                this.OwnerSecurityToken = mySecurityToken;

                if (!this.ExistAccessControlsWhereSubject && this.Strategy.Session.Population is IDatabase)
                {
                    new AccessControlBuilder(this.Strategy.DatabaseSession)
                        .WithRole(new Roles(this.Strategy.DatabaseSession).Owner)
                        .WithSubject(this)
                        .WithObject(this.OwnerSecurityToken)
                        .Build();
                }
            }
        }
    }
}