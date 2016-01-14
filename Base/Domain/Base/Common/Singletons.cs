// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Singletons.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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
    public partial class Singletons
    {
        public Singleton Instance => Singleton.Instance(this.Session);

        protected override void BaseSecure(Security config)
        {
            var defaultSecurityToken = this.Instance.DefaultSecurityToken;

            if (!this.Instance.ExistDefaultAdministratorsAccessControl)
            {
                this.Instance.DefaultAdministratorsAccessControl = new AccessControlBuilder(this.Session)
                    .WithRole(new Roles(this.Session).Administrator)
                    .WithSubjectGroup(new UserGroups(this.Session).Administrators)
                    .Build();

                defaultSecurityToken.AddAccessControl(this.Instance.DefaultAdministratorsAccessControl);
            }

            if (!this.Instance.ExistDefaultGuestAccessControl)
            {
                this.Instance.DefaultGuestAccessControl = new AccessControlBuilder(this.Session)
                    .WithRole(new Roles(this.Session).Guest)
                    .WithSubjectGroup(new UserGroups(this.Session).Guests)
                    .Build();

                defaultSecurityToken.AddAccessControl(this.Instance.DefaultGuestAccessControl);
            }
        }
    }
}