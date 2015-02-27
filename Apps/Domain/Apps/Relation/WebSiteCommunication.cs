// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebSiteCommunication.cs" company="Allors bvba">
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
    public partial class WebSiteCommunication
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
                this.CurrentObjectState = new CommunicationEventObjectStates(this.Strategy.DatabaseSession).Scheduled;
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.PreviousObjectState = this.CurrentObjectState;

            this.AppsDeriveFromParties();
            this.AppsDeriveToParties();
            this.AppsDeriveInvolvedParties(derivation);
        }

        private void AppsDeriveFromParties()
        {
            this.RemoveFromParties();
            this.AddFromParty(Originator);
        }

        private void AppsDeriveToParties()
        {
            this.RemoveToParties();
            this.AddToParty(Receiver);
        }

        private void AppsDeriveInvolvedParties(IDerivation derivation)
        {
            this.RemoveInvolvedParties();
            this.AddInvolvedParty(this.Owner);
            this.AddInvolvedParty(this.Originator);
            this.AddInvolvedParty(this.Receiver);
        }
    }
}
