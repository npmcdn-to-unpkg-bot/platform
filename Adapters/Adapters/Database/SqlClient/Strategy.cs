// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Strategy.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;

    using Allors.Meta;

    public class Strategy : IStrategy
    {
        private readonly DatabaseSession session;
        private readonly ObjectId objectId;
        private WeakReference<IObject> weakReferenceToObject;

        private IClass objectType;
        private bool isNew;
        private bool isDeleted;
        
        public Strategy(DatabaseSession session, IClass objectType, ObjectId objectId, bool isNew)
        {
            this.session = session;
            this.objectType = objectType;
            this.objectId = objectId;
            this.isNew = isNew;
        }

        public ISession Session 
        {
            get
            {
                return this.session;
            }
        }

        public IDatabaseSession DatabaseSession 
        {
            get
            {
                return this.session;
            }
        }

        public IClass ObjectType 
        {
            get
            {
                return this.objectType;
            }
        }

        public ObjectId ObjectId 
        {
            get
            {
                return this.objectId;
            }
        }

        public bool IsDeleted 
        {
            get
            {
                return this.isDeleted;
            }
        }

        public bool IsNewInSession 
        {
            get
            {
                return this.isNew;
            }
        }

        public bool IsNewInWorkspace 
        {
            get
            {
                return false;
            }
        }

        public IObject GetObject()
        {
            IObject obj = null;
            if (this.weakReferenceToObject != null)
            {
                this.weakReferenceToObject.TryGetTarget(out obj);
            }

            if (obj == null)
            {
                obj = this.session.Database.ObjectFactory.Create(this);
                this.weakReferenceToObject = new WeakReference<IObject>(obj);
            }

            return obj;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool ExistRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public object GetRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public void SetRole(IRoleType roleType, object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public bool ExistUnitRole(IRoleType roleType)
        {
            return this.session.GetUnitRole(this.objectId, roleType) != null;
        }

        public object GetUnitRole(IRoleType roleType)
        {
            return this.session.GetUnitRole(this.ObjectId, roleType);
        }

        public void SetUnitRole(IRoleType roleType, object unit)
        {
            this.session.SetUnitRole(this.objectId, roleType, unit);
        }

        public void RemoveUnitRole(IRoleType roleType)
        {
            this.session.SetUnitRole(this.objectId, roleType, null);
        }

        public bool ExistCompositeRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public IObject GetCompositeRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public void SetCompositeRole(IRoleType roleType, IObject composite)
        {
            throw new NotImplementedException();
        }

        public void RemoveCompositeRole(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public bool ExistCompositeRoles(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public Extent GetCompositeRoles(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public void AddCompositeRole(IRoleType roleType, IObject objectToAdd)
        {
            throw new NotImplementedException();
        }

        public void RemoveCompositeRole(IRoleType roleType, IObject objectToRemove)
        {
            throw new NotImplementedException();
        }

        public void SetCompositeRoles(IRoleType roleType, Extent roles)
        {
            throw new NotImplementedException();
        }

        public void RemoveCompositeRoles(IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public bool ExistAssociation(IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        public object GetAssociation(IAssociationType roleType)
        {
            throw new NotImplementedException();
        }

        public bool ExistCompositeAssociation(IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        public IObject GetCompositeAssociation(IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        public bool ExistCompositeAssociations(IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        public Extent GetCompositeAssociations(IAssociationType associationType)
        {
            throw new NotImplementedException();
        }
    }
}