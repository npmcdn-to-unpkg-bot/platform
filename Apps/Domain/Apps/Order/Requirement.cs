// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Requirement.cs" company="Allors bvba">
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
    using Allors.Domain;

    public partial class Requirement
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

        protected abstract void AppsDeriveDisplayName();

        protected abstract void AppsDeriveSearchDataCharacterBoundaryText();

        protected abstract void AppsDeriveSearchDataWordBoundaryText();

        protected abstract string AppsComposeDisplayName();

        protected abstract string AppsComposeSearchDataCharacterBoundaryText();

        protected abstract string AppsComposeSearchDataWordBoundaryText();

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new RequirementStatusBuilder(this.Session).WithRequirementObjectState(this.CurrentObjectState).Build();
                this.AddRequirementStatus(currentStatus);
                this.CurrentRequirementStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }

            this.DisplayName = this.Description;

            this.SearchData.CharacterBoundaryText = this.DisplayName;
            this.SearchData.RemoveWordBoundaryText();
        }

        private void AppsCancel()
        {
            this.CurrentObjectState = new RequirementObjectStates(Session).Cancelled;
        }

        private void AppsClose()
        {
            this.CurrentObjectState = new RequirementObjectStates(Session).Closed;
        }

        private void AppsHold()
        {
            this.CurrentObjectState = new RequirementObjectStates(Session).OnHold;
        }
    }
}
