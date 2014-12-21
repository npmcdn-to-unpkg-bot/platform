// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBase.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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

namespace Allors
{
    using System;

    using Allors.Domain;
    using Allors.Meta;

    public abstract partial class ObjectBase
    {
        public ISession Session
        {
            get { return this.Strategy.Session; }
        }

        public IDatabaseSession DatabaseSession
        {
            get { return this.Strategy.DatabaseSession; }
        }

        public bool IsInDatabase
        {
            get { return this.Session.Population is IDatabase; }
        }

        public bool IsInWorkspace
        {
            get { return this.Session.Population is IWorkspace; }
        }

        public bool IsNewInWorkspace
        {
            get { return this.Strategy.IsNewInWorkspace; }
        }

        internal virtual void OnBuild(IObjectBuilder builder)
        {
        }

        protected virtual void AddCreatorSecurityToken()
        {
            var accessControlledObject = this as AccessControlledObject;
            if (accessControlledObject != null)
            {
                var creator = new Users(this.Session).GetCurrentAuthenticatedUser();

                if (creator != null)
                {
                    accessControlledObject.AddSecurityToken(creator.OwnerSecurityToken);
                }
            }
        }

        protected virtual void BaseOnPostBuild(IObjectBuilder builder)
        {
            // TODO: Optimize
            foreach (var concreteRoleType in ((Class)this.strategy.ObjectType).ConcreteRoleTypes)
            {
                if (concreteRoleType.IsRequired)
                {
                    var roleType = concreteRoleType.RoleType;
                    var unit = roleType.ObjectType as IUnit;
                    if (unit != null && !this.strategy.ExistRole(roleType))
                    {
                        switch (unit.UnitTag)
                        {
                            case UnitTags.AllorsBoolean:
                                this.strategy.SetUnitRole(roleType, false);
                                break;
                            case UnitTags.AllorsDecimal:
                                this.strategy.SetUnitRole(roleType, 0m);
                                break;
                            case UnitTags.AllorsFloat:
                                this.strategy.SetUnitRole(roleType, 0d);
                                break;
                            case UnitTags.AllorsInteger:
                                this.strategy.SetUnitRole(roleType, 0);
                                break;
                            case UnitTags.AllorsUnique:
                                this.strategy.SetUnitRole(roleType, Guid.NewGuid());
                                break;
                        }
                    }
                }
            }
        }

        protected virtual void BaseApplySecurityOnPostBuild()
        {
        }

        protected virtual void BaseOnDelete()
        {
        }
        
        private string CoreToString()
        {
            var userInterfaceable = this as UserInterfaceable;
            return (userInterfaceable != null && userInterfaceable.ExistDisplayName) ? userInterfaceable.DisplayName : base.ToString();
        }
    }
}