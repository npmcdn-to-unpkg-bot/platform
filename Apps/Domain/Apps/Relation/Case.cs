// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Case.cs" company="Allors bvba">
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
    public partial class Case
    {
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

        public void AppsOnPostBuild(ObjectOnPostBuild method)
        {
            

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new CaseObjectStates(this.Strategy.DatabaseSession).Opened;
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            this.DeriveCurrentObjectState();

            this.PreviousObjectState = this.CurrentObjectState;
        }

        private void DeriveCurrentObjectState()
        {
            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new CaseStatusBuilder(this.Strategy.Session).WithCaseObjectState(this.CurrentObjectState).Build();
                this.AddCaseStatus(currentStatus);
                this.CurrentCaseStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }
        }

        private void AppsClose()
        {
            var closed = new CaseObjectStates(this.Strategy.Session).Closed;
            this.CurrentObjectState = closed;
        }

        private void AppsComplete()
        {
            var completed = new CaseObjectStates(this.Strategy.Session).Completed;
            this.CurrentObjectState = completed;
        }

        private void AppsReopen()
        {
            var opened = new CaseObjectStates(this.Strategy.Session).Opened;
            this.CurrentObjectState = opened;
        }
    }
}