// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityCache.cs" company="Allors bvba">
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
    using Allors;
    using Allors.Meta;

    public class SecurityCache
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        private const string CacheKey = "Allors.Cache.Security";

        private readonly IDatabase database;
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, List<Operation>>>> operationsByOperandTypeIdByObjectTypeIdByRoleId;

        public SecurityCache(ISession session)
        {
            this.database = session.Database;
            
            this.operationsByOperandTypeIdByObjectTypeIdByRoleId = (ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, List<Operation>>>>)this.database[CacheKey];

            if (this.operationsByOperandTypeIdByObjectTypeIdByRoleId == null)
            {
                this.operationsByOperandTypeIdByObjectTypeIdByRoleId = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, List<Operation>>>>();

                foreach (Role role in new Roles(session).Extent())
                {
                    if (!role.ExistUniqueId)
                    {
                        throw new Exception("Role " + role + " has no unique id");
                    }

                    var operationsByOperandTypeIdByObjectTypeId = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, List<Operation>>>();
                    this.operationsByOperandTypeIdByObjectTypeIdByRoleId[role.UniqueId] = operationsByOperandTypeIdByObjectTypeId;

                    foreach (Permission permission in role.Permissions)
                    {
                        if (!permission.ExistConcreteClassPointer || !permission.ExistOperandTypePointer || !permission.ExistOperation)
                        {
                            throw new Exception("Permission " + permission + " has no concrete class, operand type and/or operation.");
                        }

                        var concreteClassId = permission.ConcreteClassPointer;
                        var operandTypeId = permission.OperandTypePointer;
                        var operation = permission.Operation;

                        ConcurrentDictionary<Guid, List<Operation>> operationsByOperandTypeId;
                        if (!operationsByOperandTypeIdByObjectTypeId.TryGetValue(concreteClassId, out operationsByOperandTypeId))
                        {
                            operationsByOperandTypeId = new ConcurrentDictionary<Guid, List<Operation>>();
                            operationsByOperandTypeIdByObjectTypeId[concreteClassId] = operationsByOperandTypeId;
                        }

                        List<Operation> operations;
                        if (!operationsByOperandTypeId.TryGetValue(operandTypeId, out operations))
                        {
                            operations = new List<Operation>();
                            operationsByOperandTypeId[operandTypeId] = operations;
                        }

                        operations.Add(operation);
                    }
                }

                database[CacheKey] = this.operationsByOperandTypeIdByObjectTypeIdByRoleId;
            }
        }

        public void Invalidate()
        {
            this.database[CacheKey] = null;
        }

        public ConcurrentDictionary<Guid, IList<Operation>> GetOperationsByOperandTypeId(HashSet<Guid> roleUniqueIds, ObjectType objectType, out bool hasWriteOperation, out bool hasReadOperation)
        {
            var resultOperationsByOperandId = new ConcurrentDictionary<Guid, IList<Operation>>();

            hasWriteOperation = false;
            hasReadOperation = false;

            foreach (var roleUniqueId in roleUniqueIds)
            {
                var roleOperationsByOperandIdByObjectType = this.operationsByOperandTypeIdByObjectTypeIdByRoleId[roleUniqueId];
                ConcurrentDictionary<Guid, List<Operation>> roleOperationsByOperandObjectId;
                if (roleOperationsByOperandIdByObjectType.TryGetValue(objectType.Id, out roleOperationsByOperandObjectId))
                {
                    foreach (var dictionaryEntry in roleOperationsByOperandObjectId)
                    {
                        var roleOperandTypeId = dictionaryEntry.Key;
                        var roleOperations = dictionaryEntry.Value;

                        IList<Operation> resultOperations;
                        if (!resultOperationsByOperandId.TryGetValue(roleOperandTypeId, out resultOperations))
                        {
                            resultOperations = new List<Operation>();
                            resultOperationsByOperandId[roleOperandTypeId] = resultOperations;
                        }

                        foreach (var roleOperation in roleOperations)
                        {
                            if (!resultOperations.Contains(roleOperation))
                            {
                                if (roleOperation == Operation.Write)
                                {
                                    hasWriteOperation = true;
                                }

                                if (roleOperation == Operation.Read)
                                {
                                    hasReadOperation = true;
                                }

                                resultOperations.Add(roleOperation);
                            }
                        }
                    }
                }
            }

            return resultOperationsByOperandId;
        }
    }
}