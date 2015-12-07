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
    using System.Linq;

    public partial class AccessControl
    {
        [Flags]
        public enum OperationFlags
        {
            None = 0,
            Read = 1,
            Write = 2,
            Execute = 4
        }

        private Dictionary<Guid, Permissions> PermissionsByOperation

        private Dictionary[]

        public void BaseOnDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, Meta.Subjects, Meta.SubjectGroups);

            this.EffectiveUsers = this.SubjectGroups.SelectMany(v => v.Members).Union(this.Subjects).ToArray();
            this.EffectivePermissions = this.Role?.Permissions;
        }

        public bool IsPermitted(Guid operandTypeId, Operation operation)
        {
            foreach (Permission permission in this.EffectivePermissions)
            {
                if (permission.OperandTypePointer.Equals(operandTypeId) && permission.Operation.Equals(operation))
                {
                    return true;
                }
            }

            return false;
        }
    }
}