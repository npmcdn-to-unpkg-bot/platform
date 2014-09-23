// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequirementCommunication.cs" company="Allors bvba">
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

    public partial class RequirementCommunication
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, RequirementCommunications.Meta.AssociatedProfessional);
            derivation.Log.AssertExists(this, RequirementCommunications.Meta.CommunicationEvent);
            derivation.Log.AssertExists(this, RequirementCommunications.Meta.Requirement);

            this.DisplayName = string.Format(
                "{0} {1} {2}",
                this.ExistAssociatedProfessional ? this.AssociatedProfessional.DeriveDisplayName(): null,
                this.ExistCommunicationEvent ? this.CommunicationEvent.ComposeDisplayName() : null,
                this.ExistRequirement ? this.Requirement.ComposeDisplayName() : null);
        }
    }
}