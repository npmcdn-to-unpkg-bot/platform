// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeededSkill.cs" company="Allors bvba">
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

    public partial class NeededSkill
    {
        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, NeededSkills.Meta.Skill);
            derivation.Log.AssertExists(this, NeededSkills.Meta.SkillLevel);

            this.DisplayName = string.Format(
                "{0} at level {1}",
                this.ExistSkill ? this.Skill.Name : null,
                this.ExistSkillLevel ? this.SkillLevel.Name : null);
        }
    }
}