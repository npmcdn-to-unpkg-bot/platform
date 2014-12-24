// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommunicationEvent.cs" company="Allors bvba">
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

    public static partial class CommunicationEventExtensions
    {
        public static DateTime? AppsCommunicationEventGetStart(this CommunicationEvent communicationEvent)
        {
            if (communicationEvent.ExistActualStart)
            {
                return communicationEvent.ActualStart;
            }

            if (communicationEvent.ExistScheduledStart)
            {
                return communicationEvent.ScheduledStart;
            }

            return null;
        }
        
        public static void AppsDerivableDerive(this CommunicationEvent @this, DerivableDerive method)
        {
            if (!@this.ExistOwner)
            {
                @this.Owner = (Person)new Users(@this.Strategy.Session).GetCurrentAuthenticatedUser();
            }

            if (@this.ExistCurrentObjectState && !@this.CurrentObjectState.Equals(@this.PreviousObjectState))
            {
                var currentStatus = new CommunicationEventStatusBuilder(@this.Strategy.Session).WithCommunicationEventObjectState(@this.CurrentObjectState).Build();
                @this.AddCommunicationEventStatus(currentStatus);
                @this.CurrentCommunicationEventStatus = currentStatus;
            }

            if (@this.ExistCurrentObjectState)
            {
                @this.CurrentObjectState.Process(@this);
            }

            if (!@this.ExistInitialScheduledStartDate && @this.ExistScheduledStart)
            {
                @this.InitialScheduledStartDate = @this.ScheduledStart;
            }
        }

        public static void AppsCommunicationEventClose(this CommunicationEvent @this, CommunicationEventClose method)
        {
            @this.CurrentObjectState = new CommunicationEventObjectStates(@this.Strategy.Session).Closed;
        }

        public static void AppsCommunicationEventReopen(this CommunicationEvent @this, CommunicationEventReopen method)
        {
            @this.CurrentObjectState = new CommunicationEventObjectStates(@this.Strategy.Session).Opened;
        }

        public static void AppsCommunicationEventCancel(this CommunicationEvent @this, CommunicationEventCancel method)
        {
            @this.CurrentObjectState = new CommunicationEventObjectStates(@this.Strategy.Session).Cancelled;
        }
    }
}
