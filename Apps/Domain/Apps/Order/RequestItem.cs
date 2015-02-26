// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestItem.cs" company="Allors bvba">
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
    public partial class RequestItem
    {
        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, RequestItems.Meta.Product, RequestItems.Meta.ProductFeature, RequestItems.Meta.Description, RequestItems.Meta.NeededSkill, RequestItems.Meta.Deliverable);
            derivation.Log.AssertExistsAtMostOne(this, RequestItems.Meta.Product, RequestItems.Meta.ProductFeature, RequestItems.Meta.Description, RequestItems.Meta.NeededSkill, RequestItems.Meta.Deliverable);

            this.DisplayName = string.Format(
                "{0}{1}{2}{3} at level {4}{5}",
                this.ExistDescription ? this.Description : null,
                this.ExistProduct ? this.Product.ComposeDisplayName() : null,
                this.ExistProductFeature ? this.ProductFeature.ComposeDisplayName() : null,
                this.ExistNeededSkill ? this.NeededSkill.Skill.Name : null,
                this.ExistNeededSkill ? this.NeededSkill.SkillLevel.Name : null,
                this.ExistDeliverable ? this.Deliverable.Name : null);
        }
    }
}