// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkEffortObjectStates.cs" company="Allors bvba">
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

    public partial class WorkEffortObjectStates
    {
        public static readonly Guid NeedsActionId = new Guid("0D2618A7-A3B7-40f5-88C4-30DADE4D5164");
        public static readonly Guid ConfirmedId = new Guid("61E5FCB6-7814-4FED-8CE7-28C26EB88EE6");
        public static readonly Guid InProgressId = new Guid("0DFFC51A-4982-4DDA-808B-8F70D46F2749");
        public static readonly Guid FulffilledId = new Guid("F30F51F6-7301-456E-9AF3-E922739C73A1");
        public static readonly Guid FinishedId = new Guid("E8E941CD-7175-4931-AB1E-50E52DC6D720");
        public static readonly Guid CancelledId = new Guid("D3EBD54F-35B0-4bc4-AC8E-4EC583028B0A");

        private UniquelyIdentifiableCache<WorkEffortObjectState> stateCache;

        public WorkEffortObjectState NeedsAction
        {
            get { return this.StateCache.Get(NeedsActionId); }
        }

        public WorkEffortObjectState Confirmed
        {
            get { return this.StateCache.Get(ConfirmedId); }
        }

        public WorkEffortObjectState Fulffilled
        {
            get { return this.StateCache.Get(FulffilledId); }
        }

        public WorkEffortObjectState InProgress
        {
            get { return this.StateCache.Get(InProgressId); }
        }

        public WorkEffortObjectState Finished
        {
            get { return this.StateCache.Get(FinishedId); }
        }

        public WorkEffortObjectState Cancelled
        {
            get { return this.StateCache.Get(CancelledId); }
        }

        private UniquelyIdentifiableCache<WorkEffortObjectState> StateCache
        {
            get
            {
                return this.stateCache ?? (this.stateCache = new UniquelyIdentifiableCache<WorkEffortObjectState>(this.Session));
            }
        }

        protected override void AppsSetup(Setup setup)
        {
            base.AppsSetup(setup);

            var englishLocale = new Locales(Session).EnglishGreatBritain;
            var dutchLocale = new Locales(Session).DutchNetherlands;

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(NeedsActionId)
                .WithName("NeedsAction")
                .Build();

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(ConfirmedId)
                .WithName("Confirmed")
                .Build();

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(FulffilledId)
                .WithName("Fullfilled")
                .Build();

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(InProgressId)
                .WithName("In Progress")
                .Build();

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(FinishedId)
                .WithName("Finished")
                .Build();

            new WorkEffortObjectStateBuilder(Session)
                .WithUniqueId(CancelledId)
                .WithName("Cancelled")
                .Build();
        }

        protected override void AppsSecure(Domain.Security config)
        {
            base.AppsSecure(config);

            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}