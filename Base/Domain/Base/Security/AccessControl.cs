// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessControl.cs" company="Allors bvba">
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
    using System.Linq;

    public partial class AccessControl
    {
        private const string CacheKey = nameof(AccessControl) + ".";

        public Dictionary<Guid, Dictionary<Guid, Operations>> OperationsByOperandTypeIdByClassId
        {
            get
            {
                var database = this.strategy.Session.Database;
                var key = CacheKey + this.strategy.ObjectId;
                var kvp = (KeyValuePair<object, Dictionary<Guid, Dictionary<Guid, Operations>>>?)database[key];
                if (!kvp.HasValue || !this.strategy.ObjectVersion.Equals(kvp.Value.Key))
                {
                    var operationsByOperandTypeByClass = new Dictionary<Guid, Dictionary<Guid, Operations>>();

                    foreach (Permission permission in this.EffectivePermissions)
                    {
                        var classId = permission.ConcreteClassPointer;
                        Dictionary<Guid, Operations> operationsByOperandType;
                        if (!operationsByOperandTypeByClass.TryGetValue(classId, out operationsByOperandType))
                        {
                            operationsByOperandType = new Dictionary<Guid, Operations>();
                            operationsByOperandTypeByClass.Add(classId, operationsByOperandType);
                        }

                        var operandTypeId = permission.OperandTypePointer;
                        var operation = permission.Operation;

                        Operations operations;
                        operationsByOperandType.TryGetValue(operandTypeId, out operations);
                        operations = operations | operation;

                        operationsByOperandType[operandTypeId] = operations;
                    }

                    kvp = new KeyValuePair<object, Dictionary<Guid, Dictionary<Guid, Operations>>>(this.strategy.ObjectVersion, operationsByOperandTypeByClass);
                    database[key] = kvp;
                }

                return kvp.Value.Value;
            }
        }

        public void BaseOnDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, Meta.Subjects, Meta.SubjectGroups);

            this.EffectiveUsers = this.SubjectGroups.SelectMany(v => v.Members).Union(this.Subjects).ToArray();
            this.EffectivePermissions = this.Role?.Permissions;
        }

        public void WarmUp()
        {
            var x = this.OperationsByOperandTypeIdByClassId;
        }
    }
}