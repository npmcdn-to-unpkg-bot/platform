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

    public abstract partial class CommunicationEvent
    {
        public DateTime? Start
        {
            get
            {
                if (this.ExistActualStart)
                {
                    return ActualStart;
                }

                if (ExistScheduledStart)
                {
                    return ScheduledStart;
                }

                return null;
            }
        }

        ObjectState Transitional.PreviousObjectState
        {
            get
            {
                return this.PreviousObjectState;
            }
        }

        ObjectState Transitional.CurrentObjectState
        {
            get
            {
                return this.CurrentObjectState;
            }
        }

        protected abstract void AppsDeriveDisplayName();

        protected abstract void AppsDeriveSearchDataCharacterBoundaryText();

        protected abstract void AppsDeriveSearchDataWordBoundaryText();

        protected abstract string AppsComposeDisplayName();

        protected abstract string AppsComposeSearchDataCharacterBoundaryText();

        protected abstract string AppsComposeSearchDataWordBoundaryText();

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            if (!this.ExistOwner)
            {
                this.Owner = (Person)new Users(this.Session).GetCurrentAuthenticatedUser();
            }

            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new CommunicationEventStatusBuilder(this.Session).WithCommunicationEventObjectState(this.CurrentObjectState).Build();
                this.AddCommunicationEventStatus(currentStatus);
                this.CurrentCommunicationEventStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }

            if (!this.ExistInitialScheduledStartDate && this.ExistScheduledStart)
            {
                this.InitialScheduledStartDate = this.ScheduledStart;
            }
        }

        private void AppsClose()
        {
            this.CurrentObjectState = new CommunicationEventObjectStates(Session).Closed;
        }

        private void AppsReopen()
        {
            this.CurrentObjectState = new CommunicationEventObjectStates(Session).Opened;
        }

        private void AppsCancel()
        {
            this.CurrentObjectState = new CommunicationEventObjectStates(Session).Cancelled;
        }
    }
}
