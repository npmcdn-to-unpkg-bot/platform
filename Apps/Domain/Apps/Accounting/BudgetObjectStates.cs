// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BudgetObjectStates.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

    using Allors.Domain;

    public partial class BudgetObjectStates
    {
        public static readonly Guid OpenedId = new Guid("FA6E7137-D028-498a-99D1-AB2B85C0EC30");
        public static readonly Guid ClosedId = new Guid("10B82C2D-9D43-4a1c-A0CA-A03C29C7E8CC");

        private UniquelyIdentifiableCache<BudgetObjectState> cache;

        public BudgetObjectState Opened
        {
            get { return this.Cache.Get(OpenedId); }
        }

        public BudgetObjectState Closed
        {
            get { return this.Cache.Get(ClosedId); }
        }

        private UniquelyIdentifiableCache<BudgetObjectState> Cache
        {
            get
            {
                return this.cache ?? (this.cache = new UniquelyIdentifiableCache<BudgetObjectState>(this.Session));
            }
        }

        protected override void AppsSetup(Setup setup)
        {
            base.AppsSetup(setup);

            var englishLocale = new Locales(Session).EnglishGreatBritain;
            var dutchLocale = new Locales(Session).DutchNetherlands;

            new BudgetObjectStateBuilder(Session)
                .WithUniqueId(OpenedId)
                .WithName("Open")
                .Build();

            new BudgetObjectStateBuilder(Session)
                .WithUniqueId(ClosedId)
                .WithName("Closed")
                .Build();

            new BudgetObjectStateBuilder(Session)
                .WithUniqueId(ClosedId)
                .WithName("Reopened")
                .Build();
        }

        protected override void AppsSecure(Security config)
        {
            base.AppsSecure(config);

            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}