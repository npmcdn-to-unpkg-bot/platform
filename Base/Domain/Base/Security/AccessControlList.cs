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
    using System.Linq;
    using System.Text;

    using Allors;
    using Allors.Meta;

    /// <summary>
    /// List of permissions for an object/user combination.
    /// </summary>
    public class AccessControlList : IAccessControlList
    {
        private readonly AccessControlledObject @object;
        private readonly User user;
        private readonly ISession session;

        private readonly Guid classId;

        private AccessControl[] accesControls;
        private Permission[] deniedPermissions;

        public AccessControlList(IObject obj, User user)
        {
            this.user = user;
            this.session = this.user.Strategy.Session;
            this.@object = (AccessControlledObject)obj;
            this.classId = obj.Strategy.Class.Id;
        }

        public User User => this.user;

        public override string ToString()
        {
            this.LazyLoad();

            var toString = new StringBuilder();
            toString.Append("ACL: ");
            foreach (var accessControl in this.accesControls)
            {
                toString.Append(" +");
                toString.Append(accessControl);
            }

            return toString.ToString();
        }

        public bool CanRead(PropertyType propertyType)
        {
            return this.IsPermitted(propertyType, Operations.Read);
        }

        public bool CanWrite(RoleType roleType)
        {
            return this.IsPermitted(roleType, Operations.Write);
        }

        public bool CanExecute(MethodType methodType)
        {
            return this.IsPermitted(methodType, Operations.Execute);
        }
        
        public bool IsPermitted(OperandType operandType, Operations operation)
        {
            return this.IsPermitted(operandType.Id, operation);
        }

        private bool IsPermitted(Guid operandTypeId, Operations operation)
        {
            this.LazyLoad();

            if (this.deniedPermissions.Length > 0)
            {
                if (this.deniedPermissions.Any(deniedPermission => deniedPermission.OperandTypePointer.Equals(operandTypeId) && deniedPermission.Operation.Equals(operation)))
                {
                    return false;
                }
            }

            return this.accesControls.Any(accessControl => accessControl.IsPermitted(this.classId, operandTypeId, operation));
        }

        private void LazyLoad()
        {
            if (this.accesControls == null)
            {
                SecurityToken[] securityTokens = this.@object.SecurityTokens;
                if (securityTokens.Length == 0)
                {
                    securityTokens = new[] { Singleton.Instance(this.session).DefaultSecurityToken };
                }

                this.accesControls = securityTokens.SelectMany(v => v.AccessControls).Where(v=>v.EffectiveUsers.Contains(this.user)).ToArray();
                this.deniedPermissions = this.@object.DeniedPermissions.ToArray();
            }
        }
    }
}