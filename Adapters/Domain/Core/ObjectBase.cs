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
    using Allors.Domain;

    public abstract partial class ObjectBase : Derivable
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

        protected virtual void CoreOnPostBuild(IObjectBuilder builder)
        {
        }

        protected virtual void CoreApplySecurityOnPostBuild()
        {
        }

        protected virtual void CorePrepareDerivation(IDerivation derivation)
        {
            var changeSet = derivation.ChangeSet;
            if (derivation.IsForced(this.Id) || changeSet.Associations.Contains(this.Id) || changeSet.Created.Contains(this.Id))
            {
                if (!derivation.DerivedObjects.Contains(this))
                {
                    derivation.AddDerivable(this);
                }
            }
        }

        protected virtual void CoreDerive(IDerivation derivation)
        {
        }

        protected virtual void CoreApplySecurityOnDerive()
        {
        }

        protected virtual void CoreOnDelete()
        {
        }
        
        private string CoreToString()
        {
            var userInterfaceable = this as UserInterfaceable;
            return (userInterfaceable != null && userInterfaceable.ExistDisplayName) ? userInterfaceable.DisplayName : base.ToString();
        }
    }
}