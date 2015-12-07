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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Allors;
    using Allors.Meta;

    /// <summary>
    /// List of permissions for an object/user combination.
    /// </summary>
    public class AccessControlList : IAccessControlList
    {
        private static readonly Dictionary<Guid, Operation[]> EmptyOperationsByOperandTypeId = new Dictionary<Guid, Operation[]>();
        private static readonly Operation[] EmptyOperations = new Operation[0]; 

        private readonly AccessControlledObject @object;
        private readonly User user;
        private readonly Class @class;
        private readonly ISession session;

        private Dictionary<Guid, Operation[]> operationsByOperandId;
       
        public AccessControlList(IObject obj, User user)
        {
            this.user = user;
            this.@class = (Class)obj.Strategy.Class;
            this.session = this.user.Strategy.Session;
            this.@object = (AccessControlledObject)obj;
        }

        public User User => this.user;

        private Dictionary<Guid, Operation[]> OperationsByOperandTypeId
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
            foreach (var objectId in this.OperationsByOperandTypeId.Keys)
            {
                var operandType = (OperandType)this.user.Strategy.Session.Database.MetaPopulation.Find(objectId);
                toString.Append(operandType.DisplayName + ":");
                foreach (var operation in this.OperationsByOperandTypeId[objectId])
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

        public Operation[] GetOperations(OperandType operandType)
        {
            Operation[] operations;
            if (!this.OperationsByOperandTypeId.TryGetValue(operandType.Id, out operations))
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
            if (this.OperationsByOperandTypeId.ContainsKey(operandTypeId))
            {
                var operationsList = this.OperationsByOperandTypeId[operandTypeId];
                return operationsList.Contains(operation);
            }

            return false;
        }

        private void LazyLoad()
        {
            if (this.operationsByOperandId == null)
            {
                this.operationsByOperandId = EmptyOperationsByOperandTypeId;

                SecurityToken[] securityTokens = this.@object.SecurityTokens;
                if (securityTokens.Length == 0)
                {
                    securityTokens = new[] { Singleton.Instance(this.session).DefaultSecurityToken };
                }

                var accesControls = securityTokens.SelectMany(v => v.AccessControls).Where(v=>v.EffectiveUsers.Contains(this.user)).ToArray();

                if (accesControls.Length > 0)
                {
                    var permissions = new HashSet<Permission>();
                    foreach (var accessControl in accesControls)
                    {
                        permissions.UnionWith(accessControl.EffectivePermissions);
                    }

                    permissions.ExceptWith(this.@object.DeniedPermissions);

                    this.operationsByOperandId = permissions.GroupBy(v => v.OperandTypePointer, v => v.Operation).ToDictionary(g => g.Key, g => g.ToArray());
                }
            }
        }
    }
}