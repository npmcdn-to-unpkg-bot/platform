// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PickList.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Text;

    public partial class PickList
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

        public bool IsNegativePickList
        {
            get
            {
                //// Negative PickList only has 1 item.
                return this.ExistPickListItems && this.PickListItems.First.RequestedQuantity < 0;
            }
        }

        public bool IsComplete
        {
            get
            {
                foreach (PickListItem pickListItem in this.PickListItems)
                {
                    if (!pickListItem.ExistActualQuantity || pickListItem.RequestedQuantity != pickListItem.ActualQuantity)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        protected override void AppsOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.AppsOnPostBuild(objectBuilder);

            if (!this.ExistCreationDate)
            {
                this.CreationDate = DateTime.Now;
            }

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new PickListObjectStates(this.Session).Created;
            }

            if (!this.ExistStore)
            {
                var internalOrganisation = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;
                if (internalOrganisation.StoresWhereOwner.Count == 1)
                {
                    this.Store = internalOrganisation.StoresWhereOwner.First;
                }
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                foreach (PickListItem pickListItem in this.PickListItems)
                {
                    derivation.AddDependency(this, pickListItem);

                    var inventoryItem = pickListItem.InventoryItem as Allors.Domain.NonSerializedInventoryItem;
                    if (inventoryItem != null)
                    {
                        derivation.AddDependency(this, inventoryItem);
                    }
                }

                foreach (var customerShipment in this.ShipToParty.PendingCustomerShipments)
                {
                    derivation.AddDependency(customerShipment, this);
                }
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            this.DeriveCurrentObjectState();

            derivation.Log.AssertExists(this, PickLists.Meta.CreationDate);

            this.AppsDeriveDisplayName();

            var characterBoundaryText = this.ExistPicker ? this.Picker.DeriveSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = string.Format(
                "{0} {1}",
                this.ExistCreationDate ? this.CreationDate : DateTime.MinValue,
                this.ExistPicker ? this.Picker.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
            

            if (this.ExistPickListItems)
            {
                this.AppsDeriveTemplate(derivation);
            }

            this.PreviousObjectState = this.CurrentObjectState;

            // TODO: waarom?
            //this.AppsDeriveTemplate(derivation);
        }

        protected override void AppsApplySecurityOnDerive()
        {
            this.RemoveSecurityTokens();
            this.AddSecurityToken(Domain.Singleton.Instance(this.Session).AdministratorSecurityToken);

            if (this.ExistShipToParty)
            {
                this.AddSecurityToken(this.ShipToParty.OwnerSecurityToken);
            }

            if (this.ExistStore && this.Store.ExistOwner)
            {
                if (this.Store.Owner.ExistOwnerSecurityToken && !this.SecurityTokens.Contains(this.Store.Owner.OwnerSecurityToken))
                {
                    this.AddSecurityToken(Store.Owner.OwnerSecurityToken);
                }
            }
        }

        private void DeriveCurrentObjectState()
        {
            if (this.ExistCurrentObjectState && !this.CurrentObjectState.Equals(this.PreviousObjectState))
            {
                var currentStatus = new PickListStatusBuilder(this.Session).WithPickListObjectState(this.CurrentObjectState).Build();
                this.AddPickListStatus(currentStatus);
                this.CurrentPickListStatus = currentStatus;
            }

            if (this.ExistCurrentObjectState)
            {
                this.CurrentObjectState.Process(this);
            }

            this.PreviousObjectState = this.CurrentObjectState;
        }

        private void AppsDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            uiText.Append(this.CreationDate);

            if (this.ExistShipToParty)
            {
                uiText.Append(" for : ");
                uiText.Append(this.ShipToParty.DeriveDisplayName());
            }

            this.DisplayName = uiText.ToString();
        }

        private void AppsCancel()
        {
            this.CurrentObjectState = new PickListObjectStates(Session).Cancelled;
        }

        private void AppsHold()
        {
            this.CurrentObjectState = new PickListObjectStates(Session).OnHold;
        }

        private void AppsContinue()
        {
            this.CurrentObjectState = new PickListObjectStates(Session).Created;
        }

        private void AppsSetPicked()
        {
            this.CurrentObjectState = new PickListObjectStates(Session).Picked;

            foreach (PickListItem pickListItem in this.PickListItems)
            {
                if (!pickListItem.ExistActualQuantity)
                {
                    pickListItem.ActualQuantity = pickListItem.RequestedQuantity;
                }
            }
        }

        private void AppsDeriveTemplate(IDerivation derivation)
        {
            var internalOrganisation = this.PickListItems[0].InventoryItem.Facility.Owner;
            Domain.StringTemplate template = null;

            if (internalOrganisation.ExistLocale)
            {
                var templates = internalOrganisation.PickListTemplates;
                templates.Filter.AddEquals(StringTemplates.Meta.Locale, internalOrganisation.Locale);
                template = templates.First;
            }

            if (template == null)
            {
                var templates = internalOrganisation.PickListTemplates;
                // TODO:
                template = templates.First;
            }

            if (template != null)
            {
                this.PrintContent = template.Apply(new Dictionary<string, object> { { "this", this } });
            }
        }
    }
}