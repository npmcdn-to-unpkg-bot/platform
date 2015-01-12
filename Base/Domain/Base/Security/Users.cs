// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Users.cs" company="Allors bvba">
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
    using System.Threading;

    using Allors;

    /// <summary>
    /// A user of this application.
    /// </summary>
    public partial class Users
    {
        public const string GuestUserName = "Guest";
        public const string AdministratorUserName = "Administrator";

        private static readonly string SessionKey = "Allors.Cache." + typeof(User);

        public User GetCurrentUser()
        {
            if (this.GetCurrentAuthenticatedUser() == null)
            {
                var singleton = Singleton.Instance(this.Session);
                if (singleton != null)
                {
                    return singleton.Guest;
                }
            }

            return this.GetCurrentAuthenticatedUser();
        }

        public User GetCurrentAuthenticatedUser()
        {
            var userId = Thread.CurrentPrincipal.Identity.Name;

            if (String.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var cached = (CachedUser)this.Session[SessionKey];
            if (cached == null || !userId.ToLower().Equals(cached.UserId))
            {
                var user = this.FindBy(Meta.UserName, userId.ToLowerInvariant());

                if (user == null)
                {
                    return null;
                }

                cached = new CachedUser(user);
                this.Session[SessionKey] = cached;
            }

            return cached.GetUser(this.Session);
        }

        public User GetUser(string userId)
        {
            var cached = (CachedUser)this.Session[SessionKey];
            if (cached == null || !userId.ToLower().Equals(cached.UserId))
            {
                var user = this.FindBy(Meta.UserName, userId.ToLowerInvariant());

                if (user == null)
                {
                    return null;
                }

                cached = new CachedUser(user);
                this.Session[SessionKey] = cached;
            }

            return cached.GetUser(this.Session);
        }

        private class CachedUser
        {
            public readonly string UserId;
            private readonly string objectId;

            public CachedUser(User user)
            {
                this.UserId = user.UserName.ToLower();
                this.objectId = user.Id.ToString();
            }

            public User GetUser(ISession session)
            {
                return (User)session.Instantiate(this.objectId);
            }
        }
    }
}