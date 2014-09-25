// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkEffort.cs" company="Allors bvba">
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
    public static partial class WorkEffortExtensions
    {
        public static void AppsWorkEffortDerive(this WorkEffort workEffort, IDerivation derivation)
        {
            if (workEffort.ExistCurrentObjectState && !workEffort.CurrentObjectState.Equals(workEffort.PreviousObjectState))
            {
                var currentStatus = new WorkEffortStatusBuilder(workEffort.Strategy.Session).WithWorkEffortObjectState(workEffort.CurrentObjectState).Build();
                workEffort.AddWorkEffortStatus(currentStatus);
                workEffort.CurrentWorkEffortStatus = currentStatus;
            }

            if (workEffort.ExistCurrentObjectState)
            {
                workEffort.CurrentObjectState.Process(workEffort);
            }
        }

        public static void AppsWorkEffortConfirm(this WorkEffort workEffort)
        {
            workEffort.CurrentObjectState = new WorkEffortObjectStates(workEffort.Strategy.Session).Confirmed;
        }

        public static void AppsWorkEffortWorkDone(this WorkEffort workEffort)
        {
            workEffort.CurrentObjectState = new WorkEffortObjectStates(workEffort.Strategy.Session).Fulffilled;
        }

        public static void AppsWorkEffortFinish(this WorkEffort workEffort)
        {
            workEffort.CurrentObjectState = new WorkEffortObjectStates(workEffort.Strategy.Session).Finished;
        }

        public static void AppsWorkEffortCancel(this WorkEffort workEffort)
        {
            workEffort.CurrentObjectState = new WorkEffortObjectStates(workEffort.Strategy.Session).Cancelled;
        }

        public static void AppsWorkEffortReopen(this WorkEffort workEffort)
        {
            workEffort.CurrentObjectState = new WorkEffortObjectStates(workEffort.Strategy.Session).Created;
        }
    }
}
