// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessControlList.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Allors.R1;
    using Allors.R1.Meta;

    /// <summary>
    /// List of permissions for an object/user combination.
    /// </summary>
    public class AccessControlList : IAccessControlList
    {
        private static readonly IList<Operation> EmptyOperations = new List<Operation>();
        private static readonly Dictionary<Guid, IList<Operation>> EmptyOperationsByOperandTypeId = new Dictionary<Guid, IList<Operation>>();

        private readonly AccessControlledObject accessControlledObject;
        private readonly User user;
        private readonly ObjectType objectType;
        private readonly IDatabaseSession databaseSession;

        private Dictionary<Guid, IList<Operation>> operationsByOperandId;

        private bool hasWriteOperation;
        private bool hasReadOperation;

        public AccessControlList(IObject obj, User user)
        {
            this.user = user;
            this.objectType = obj.Strategy.ObjectType;
            this.databaseSession = this.user.Strategy.DatabaseSession;

            if (!obj.Strategy.IsNewInWorkspace)
            {
                this.accessControlledObject = (AccessControlledObject)this.databaseSession.Instantiate(obj.Id);
            }
            else
            {
                this.accessControlledObject = (AccessControlledObject)obj;
            }
        }

        public User User
        {
            get
            {
                return this.user;
            }
        }

        public bool HasReadOperation
        {
            get
            {
                this.LazyLoad();
                return this.hasReadOperation;
            }
        }

        public bool HasWriteOperation
        {
            get
            {
                this.LazyLoad();
                return this.hasWriteOperation;
            }
        }

        public int Count
        {
            get
            {
                return this.OperationsByOperandObjectId.Count;
            }
        }

        private Dictionary<Guid, IList<Operation>> OperationsByOperandObjectId
        {
            get
            {
                this.LazyLoad();
                return this.operationsByOperandId;
            }
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            foreach (var objectId in this.OperationsByOperandObjectId.Keys)
            {
                var operandType = (OperandType)this.user.Strategy.Session.Population.Domain.Find(objectId);
                toString.Append(operandType.DisplayName + ":");
                foreach (var operation in this.OperationsByOperandObjectId[objectId])
                {
                    toString.Append(" ");
                    toString.Append(Enum.GetName(typeof(Operation), operation));
                }

                toString.Append("\n");
            }

            return toString.ToString();
        }

        public bool CanRead(PropertyType propertyType)
        {
            return this.IsPermitted(propertyType, Operation.Read);
        }

        public bool CanWrite(RoleType roleType)
        {
            return this.IsPermitted(roleType, Operation.Write);
        }

        public bool CanExecute(MethodType methodType)
        {
            return this.IsPermitted(methodType, Operation.Execute);
        }

        public bool CanExecute(Guid methodTypeId)
        {
            return this.CanExecute((MethodType)this.objectType.Domain.Find(methodTypeId));
        }

        public IList<Operation> GetOperations(OperandType operandType)
        {
            IList<Operation> operations;
            if (!this.OperationsByOperandObjectId.TryGetValue(operandType.Id, out operations))
            {
                return EmptyOperations;
            }

            return operations;
        }

        public bool IsPermitted(OperandType operandType, Operation operation)
        {
            return this.IsPermitted(operandType.Id, operation);
        }

        private bool IsPermitted(Guid operandTypeId, Operation operation)
        {
            if (this.OperationsByOperandObjectId.ContainsKey(operandTypeId))
            {
                var operationsList = this.OperationsByOperandObjectId[operandTypeId];
                return operationsList.Contains(operation);
            }

            return false;
        }

        private void LazyLoad()
        {
            if (this.operationsByOperandId == null)
            {
                this.operationsByOperandId = EmptyOperationsByOperandTypeId;

                if (this.accessControlledObject != null)
                {
                    Extent<AccessControl> accessControls = null;

                    if (this.accessControlledObject.ExistSecurityTokens)
                    {
                        var securityTokens = this.accessControlledObject.SecurityTokens;

                        if (this.accessControlledObject.Strategy.Session.Population is IWorkspace)
                        {
                            ICompositePredicate or = null;
                            foreach (SecurityToken securityToken in securityTokens)
                            {
                                foreach (AccessControl accessControl in securityToken.AccessControlsWhereObject)
                                {
                                    var connectedAccessControl = (AccessControl)this.databaseSession.Instantiate(accessControl.Id);
                                    if (connectedAccessControl == null)
                                    {
                                        throw new Exception("AccessControl must exist in the connected population.");
                                    }

                                    if (or == null)
                                    {
                                        accessControls = this.databaseSession.Extent<AccessControl>();
                                        or = accessControls.Filter.AddOr();
                                    }

                                    or.AddEquals(connectedAccessControl);
                                }
                            }
                        }
                        else
                        {
                            accessControls = this.databaseSession.Extent<AccessControl>();
                            accessControls.Filter.AddContainedIn(AccessControls.Meta.Object, (Extent)securityTokens);
                        }
                    }
                    else
                    {
                        var singleton = Singleton.Instance(this.databaseSession);
                        if (singleton != null &&
                            singleton.ExistDefaultSecurityToken &&
                            singleton.DefaultSecurityToken.ExistAccessControlsWhereObject)
                        {
                            accessControls = singleton.DefaultSecurityToken.AccessControlsWhereObject;
                        }
                        else
                        {
                            return;
                        }
                    }

                    var subjectOr = accessControls.Filter.AddOr();
                    subjectOr.AddContains(AccessControls.Meta.Subject, this.user);
                    foreach (UserGroup userGroup in this.user.UserGroupsWhereMember)
                    {
                        subjectOr.AddContains(AccessControls.Meta.SubjectGroup, userGroup);
                    }

                    if (accessControls.Count > 0)
                    {
                        Permission[] deniedPermissions;
                        if (this.accessControlledObject.Strategy.Session.Population is IWorkspace)
                        {
                            deniedPermissions = (Extent<Permission>)this.databaseSession.Instantiate(this.accessControlledObject.DeniedPermissions);
                        }
                        else
                        {
                            deniedPermissions = this.accessControlledObject.DeniedPermissions;
                        }

                        var securityCache = SecurityCache.GetSingleton(this.databaseSession);
                        this.operationsByOperandId = securityCache.GetOperationsByOperandObjectId(accessControls, this.objectType, out this.hasWriteOperation, out this.hasReadOperation);

                        foreach (var deniedPermission in deniedPermissions)
                        {
                            if (deniedPermission.ConcreteClassPointer.Equals(this.objectType.Id))
                            {
                                if (deniedPermission.OperandTypePointer.HasValue && deniedPermission.Operation.HasValue)
                                {
                                    IList<Operation> operations;
                                    if (this.operationsByOperandId.TryGetValue(
                                        deniedPermission.OperandTypePointer.Value,
                                        out operations))
                                    {
                                        operations.Remove(deniedPermission.Operation.Value);
                                    }
                                }
                                else
                                {
                                    throw new Exception("Denied permission " + deniedPermission + " has a missing operand type and/or operation");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}