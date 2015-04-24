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
    using System.Collections.Generic;

    using Allors;
    using Allors.Meta;

    public class SecurityCache
    {
        private static SecurityCache singleton;

        private readonly Dictionary<Guid, Dictionary<Guid, Dictionary<Guid, List<Operation>>>> operationsByOperandTypeIdByObjectTypeIdByRoleId;

        private SecurityCache(ISession session)
        {
            this.operationsByOperandTypeIdByObjectTypeIdByRoleId = new Dictionary<Guid, Dictionary<Guid, Dictionary<Guid, List<Operation>>>>();
            foreach (Role role in new Roles(session).Extent())
            {
                if (!role.ExistUniqueId)
                {
                    throw new Exception("Role " + role + " has no unique id");
                }

                var operationsByOperandObjectIdByObjectTypeId = new Dictionary<Guid, Dictionary<Guid, List<Operation>>>();
                this.operationsByOperandTypeIdByObjectTypeIdByRoleId[role.UniqueId] = operationsByOperandObjectIdByObjectTypeId;

                foreach (Permission permission in role.Permissions)
                {
                    if (!permission.ExistConcreteClassPointer || !permission.ExistOperandTypePointer || !permission.ExistOperation)
                    {
                        throw new Exception("Permission " + permission + " has no concrete class, operand type and/or operation.");
                    }

                    var concreteClassId = permission.ConcreteClassPointer;
                    var operandTypeId = permission.OperandTypePointer;
                    var operation = permission.Operation;
                    
                    Dictionary<Guid, List<Operation>> operationsByOperandObjectId;
                    if (!operationsByOperandObjectIdByObjectTypeId.TryGetValue(concreteClassId, out operationsByOperandObjectId))
                    {
                        operationsByOperandObjectId = new Dictionary<Guid, List<Operation>>();
                        operationsByOperandObjectIdByObjectTypeId[concreteClassId] = operationsByOperandObjectId;
                    }

                    List<Operation> operations;
                    if (!operationsByOperandObjectId.TryGetValue(operandTypeId, out operations))
                    {
                        operations = new List<Operation>();
                        operationsByOperandObjectId[operandTypeId] = operations;
                    }

                    operations.Add(operation);
                }
            }
        }

        public static SecurityCache GetSingleton(ISession session)
        {
            return singleton ?? (singleton = new SecurityCache(session));
        }

        public static void Invalidate()
        {
            singleton = null;
        }

        public Dictionary<Guid, IList<Operation>> GetOperationsByOperandObjectId(IList<Guid> roleUniqueIds, ObjectType objectType, out bool hasWriteOperation, out bool hasReadOperation)
        {
            var operationsByOperandId = new Dictionary<Guid, IList<Operation>>();

            hasWriteOperation = false;
            hasReadOperation = false;

            foreach (var roleUniqueId in roleUniqueIds)
            {
                var roleOperationsByOperandIdByObjectType = this.operationsByOperandTypeIdByObjectTypeIdByRoleId[roleUniqueId];
                Dictionary<Guid, List<Operation>> roleOperationsByOperandObjectId;
                if (roleOperationsByOperandIdByObjectType.TryGetValue(objectType.Id, out roleOperationsByOperandObjectId))
                {
                    foreach (var dictionaryEntry in roleOperationsByOperandObjectId)
                    {
                        var roleOperandTypeId = dictionaryEntry.Key;
                        var roleOperations = dictionaryEntry.Value;

                        IList<Operation> operations;
                        if (!operationsByOperandId.TryGetValue(roleOperandTypeId, out operations))
                        {
                            operations = new List<Operation>();
                            operationsByOperandId[roleOperandTypeId] = operations;
                        }

                        foreach (var roleOperation in roleOperations)
                        {
                            if (!operations.Contains(roleOperation))
                            {
                                if (roleOperation == Operation.Write)
                                {
                                    hasWriteOperation = true;
                                }

                                if (roleOperation == Operation.Read)
                                {
                                    hasReadOperation = true;
                                }

                                operations.Add(roleOperation);
                            }
                        }
                    }
                }
            }

            return operationsByOperandId;
        }
    }
}