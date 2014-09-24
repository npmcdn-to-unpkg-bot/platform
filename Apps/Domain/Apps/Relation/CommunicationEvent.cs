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
        
        public static void AppsCommunicationEventDerive(this CommunicationEvent communicationEvent, IDerivation derivation)
        {
            if (!communicationEvent.ExistOwner)
            {
                communicationEvent.Owner = (Person)new Users(communicationEvent.Strategy.Session).GetCurrentAuthenticatedUser();
            }

            if (communicationEvent.ExistCurrentObjectState && !communicationEvent.CurrentObjectState.Equals(communicationEvent.PreviousObjectState))
            {
                var currentStatus = new CommunicationEventStatusBuilder(communicationEvent.Strategy.Session).WithCommunicationEventObjectState(communicationEvent.CurrentObjectState).Build();
                communicationEvent.AddCommunicationEventStatus(currentStatus);
                communicationEvent.CurrentCommunicationEventStatus = currentStatus;
            }

            if (communicationEvent.ExistCurrentObjectState)
            {
                communicationEvent.CurrentObjectState.Process(communicationEvent);
            }

            if (!communicationEvent.ExistInitialScheduledStartDate && communicationEvent.ExistScheduledStart)
            {
                communicationEvent.InitialScheduledStartDate = communicationEvent.ScheduledStart;
            }
        }

        public static void AppsCommunicationEventClose(this CommunicationEvent communicationEvent)
        {
            communicationEvent.CurrentObjectState = new CommunicationEventObjectStates(communicationEvent.Strategy.Session).Closed;
        }

        public static void AppsCommunicationEventReopen(this CommunicationEvent communicationEvent)
        {
            communicationEvent.CurrentObjectState = new CommunicationEventObjectStates(communicationEvent.Strategy.Session).Opened;
        }

        public static void AppsCommunicationEventCancel(this CommunicationEvent communicationEvent)
        {
            communicationEvent.CurrentObjectState = new CommunicationEventObjectStates(communicationEvent.Strategy.Session).Cancelled;
        }
    }
}
