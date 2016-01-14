//-------------------------------------------------------------------------------------------------
// <copyright file="roles.cs" company="Allors bvba">
// Copyright 2002-2016 Allors bvba.
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

    public partial class Roles
    {
        public const string GuestName = "Guest";
        public const string AdministratorName = "Administrator";
        public const string OwnerName = "Owner";

        // Horizontal roles
        public static readonly Guid AdministratorId = new Guid("9D162C26-15B2-428e-AB80-DB4B3EBDBB7A");
        public static readonly Guid GuestId = new Guid("86445257-3F62-41e0-8B4A-2DF9FB18A8AA");

        // DAC
        public static readonly Guid OwnerId = new Guid("E22EA50F-E616-4429-92D5-B91684AD3C2A");

        private UniquelyIdentifiableCache<Role> cache;

        public Role Administrator
        {
            get
            {
                return this.RoleCache.Get(AdministratorId);
            }
        }

        public Role Guest
        {
            get { return this.RoleCache.Get(GuestId); }
        }

        public Role Owner
        {
            get { return this.RoleCache.Get(OwnerId); }
        }

        private UniquelyIdentifiableCache<Role> RoleCache
        {
            get
            {
                return this.cache ?? (this.cache = new UniquelyIdentifiableCache<Role>(this.Session));
            }
        }

        protected override void BaseSetup(Setup config)
        {
            base.BaseSetup(config);

            // Horizontal Roles
            new RoleBuilder(this.Session).WithName(AdministratorName).WithUniqueId(AdministratorId).Build();
            new RoleBuilder(this.Session).WithName(GuestName).WithUniqueId(GuestId).Build();

            // DAC emulation
            new RoleBuilder(this.Session).WithName(OwnerName).WithUniqueId(OwnerId).Build();
        }

        protected override void BaseSecure(Security config)
        {
            base.BaseSecure(config);

            var full = new[] { Domain.Operations.Read, Domain.Operations.Write, Domain.Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}