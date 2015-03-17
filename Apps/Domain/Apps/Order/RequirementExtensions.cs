// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequirementExtensions.cs" company="Allors bvba">
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
    public static class RequirementExtensions
    {
        public static void AppsDerive(this Requirement requirement, IDerivation derivation)
        {
            if (requirement.ExistCurrentObjectState && !requirement.CurrentObjectState.Equals(requirement.PreviousObjectState))
            {
                var currentStatus = new RequirementStatusBuilder(requirement.Strategy.Session).Build();
                requirement.AddRequirementStatus(currentStatus);
                requirement.CurrentRequirementStatus = currentStatus;
            }

            if (requirement.ExistCurrentObjectState)
            {
                requirement.CurrentObjectState.Process(requirement);
            }
        }

        public static void AppsCancel(this Requirement requirement)
        {
            requirement.CurrentObjectState = new RequirementObjectStates(requirement.Strategy.Session).Cancelled;
        }

        public static void AppsClose(this Requirement requirement)
        {
            requirement.CurrentObjectState = new RequirementObjectStates(requirement.Strategy.Session).Closed;
        }

        public static void AppsHold(this Requirement requirement)
        {
            requirement.CurrentObjectState = new RequirementObjectStates(requirement.Strategy.Session).OnHold;
        }
    }
}
