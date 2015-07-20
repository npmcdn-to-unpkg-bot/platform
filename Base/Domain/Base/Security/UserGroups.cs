//-------------------------------------------------------------------------------------------------
// <copyright file="UserGroups.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the role type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    using Allors;

    public partial class UserGroups
    {
        public const string GuestsName = "Guests";
        public const string AdministratorsName = "Administrators";

        public static readonly Guid GuestsId = new Guid("{1B022AA5-1B73-486A-9386-81D6EBFF2A4B}");
        public static readonly Guid AdministratorsId = new Guid("CDC04209-683B-429C-BED2-440851F430DF");

        private UniquelyIdentifiableCache<UserGroup> cache;

        public UserGroup Administrators
        {
            get { return this.Cache.Get(AdministratorsId); }
        }

        public UserGroup Guests
        {
            get { return this.Cache.Get(GuestsId); }
        }

        private UniquelyIdentifiableCache<UserGroup> Cache
        {
            get { return this.cache ?? (this.cache = new UniquelyIdentifiableCache<UserGroup>(this.Session)); }
        }

        protected override void BasePrepare(Setup config)
        {
            base.BasePrepare(config);

            config.AddDependency(this.ObjectType, Roles.Meta.ObjectType);
        }

        protected override void BaseSetup(Setup config)
        {
            base.BaseSetup(config);

            // Default Groups
            new UserGroupBuilder(Session).WithName(GuestsName).WithUniqueId(GuestsId).WithRole(new Roles(Session).Guest).Build();
            new UserGroupBuilder(Session).WithName(AdministratorsName).WithUniqueId(AdministratorsId).WithRole(new Roles(Session).Administrator).Build();

            new SecurityCache(this.Session).Invalidate();
        }

        protected override void BaseSecure(Security config)
        {
            base.BaseSecure(config);

            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}